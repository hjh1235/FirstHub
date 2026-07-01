using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UpperComputer.TaskUnit
{
    public class ScanTask : TaskClass
    {
        _Client _Client;
        /// <summary>
        /// 扫码流程步骤
        /// </summary>
        int Scan1RunStep = 0;
        /// <summary>
        /// 模组码列表
        /// </summary>
        List<SNInformtion> snList = new List<SNInformtion>();
        List<string> errlist = new List<string>();
        string msg = "";

        public enum ScanControlFlag
        {
            等待扫码信号,
            等待扫码结果,
            开始扫码,
            扫码结果,
            扫码成功,
            扫码失败,
            Mes进站校验,
            Mes过站,
            进站MES失败,
            存入PLC二维码,
            等待PLC复位扫码结果
        }

        //扫码流程
        public override void TaskRun()
 
        {
            while (true)
            {
                Thread.Sleep(30);
                if ( TaskClass.plcConnState == false || !FormStart.bInit)
                {
                    continue;
                }
                switch (Scan1RunStep)
                {
                    case (int)ScanControlFlag.等待扫码信号:
                        msg = FormStart.pLCTool.ReadPLC("扫码触发").ToString();
                        if (msg== "True")
                        {
                            Thread.Sleep(1000);
                            string str = FormStart.pLCTool.ReadPLC("扫码触发").ToString();
                            Log.log($"接收到扫码信号[{msg},{str}]");
                            if (msg != str)
                                break;
                            _Client.msg = "";
                            Data.bUserFlag1 = false;
                            Data.bUserFlag2 = false;
                            InitClass.CCDArea.Clear();
                            FormStart.ccdData.Clear();
                            errlist.Clear();
                            ClassCANS.OLD.小车码 = "";
                            ClassCANS.OLD.模组码 = "";
                            Log.log($"触发扫码,等待扫码结果......");
                            if (ClassCANS.OLD.屏蔽扫码)
                            {
                                Log.log($"屏蔽扫码");
                                Scan1RunStep = (int)ScanControlFlag.等待扫码结果;
                                break;
                            }
                            bool brt = false;
                            for (int i = 0; i < 3; i++)
                            {
                                Log.log($"第[{i + 1}]扫码触发");
                                _Client.Send(ClassCANS.OLD.扫码触发信号);
                                sw0.Restart();
                                while (true)
                                {
                                    Thread.Sleep(10);
                                    if (_Client.msg != "" && !_Client.msg.Contains("ERROR")/*&& _Client.msg.Length>=ClassCANS.OLD.nLen*/)
                                    {
                                        brt = true;
                                        _Client.msg = _Client.msg.Trim();
                                        //Log.log($"获取到条码[{_Client.msg}]");
                                        Scan1RunStep = (int)ScanControlFlag.等待扫码结果;
                                        break;
                                    }
                                    if (_Client.msg != "" && _Client.msg.Contains("ERROR"))
                                    {
                                        break;
                                    }
                                    else if (sw0.Elapsed.TotalSeconds > 3)
                                    {
                                        break;
                                    }
                                }
                                if (brt)
                                {
                                    Scan1RunStep = (int)ScanControlFlag.等待扫码结果;
                                    break;
                                }
                                else
                                {
                                    Scan1RunStep = (int)ScanControlFlag.扫码失败;
                                }
                            }
                        }
                        break;
                    case (int)ScanControlFlag.等待扫码结果:
                        if (!ClassCANS.OLD.屏蔽扫码&&ClassCANS.OLD.启用MES)
                        {                           
                            ClassCANS.OLD.小车码 = _Client.msg;
                            Log.log($"获取到条码[{_Client.msg}]");
                            Scan1RunStep = (int)ScanControlFlag.Mes进站校验;
                        }
                       else if (!ClassCANS.OLD.屏蔽扫码)
                        {
                            ClassCANS.OLD.小车码 = _Client.msg;
                            ClassCANS.OLD.模组码 = _Client.msg;
                            Log.log($"屏蔽MES，获取到条码[{_Client.msg}]");
                            Scan1RunStep = (int)ScanControlFlag.存入PLC二维码;
                        }
                        else
                        {
                            ClassCANS.OLD.小车码 = ClassCANS.OLD.工位固定条码;
                            ClassCANS.OLD.模组码 = ClassCANS.OLD.工位固定条码;
                            Log.log($"屏蔽扫码和MES,使用固定条码[{ ClassCANS.OLD.模组码}]");
                            Scan1RunStep = (int)ScanControlFlag.存入PLC二维码;
                        }
                        break;
                    case (int)ScanControlFlag.Mes进站校验:
                        if (ClassCANS.OLD.启用MES)
                        {
                            string getSnResult = mes.Instance().GetTaryBarcode(ClassCANS.OLD.小车码, ClassCANS.OLD.MachineNo, ClassCANS.OLD.groupCode, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ref snList);
                            if (getSnResult.ToUpper().Contains("TRUE") == false)
                            {
                                errlist.Add($"小车码[{ ClassCANS.OLD.小车码}]获取PACK码失败，原因：{getSnResult}");
                                Log.alarmLog($"小车码[{ ClassCANS.OLD.小车码}]获取PACK码失败，原因：{getSnResult}", 1);
                                Scan1RunStep = (int)ScanControlFlag.进站MES失败;
                                continue;
                            }
                            if (snList.Count < 1)
                            {
                                errlist.Add($"小车码{ ClassCANS.OLD.小车码}获取绑定PACK码数量为0");
                                Log.alarmLog($"小车码{ ClassCANS.OLD.小车码}获取绑定PACK码数量为0", 1);
                                Scan1RunStep = (int)ScanControlFlag.进站MES失败;
                                continue;
                            }
                            try
                            {
                                ClassCANS.OLD.模组码 = snList[0].sn;
                            }
                            catch (Exception)
                            {
                                Log.alarmLog($"小车码获取到的模组码为空",1);
                            }               
                            
                            string index = "";
                            string checkResult = mes.Instance().StationCheck(ClassCANS.OLD.groupCode, ClassCANS.OLD.MachineNo, ClassCANS.OLD.SessionId, ClassCANS.OLD.模组码, ClassCANS.OLD.MoNumber, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "1", ref index);
                            if (checkResult.ToUpper().Contains("TRUE"))
                            {
                                Log.log($"校验{ClassCANS.OLD.模组码}成功！");
                                Scan1RunStep = (int)ScanControlFlag.存入PLC二维码;
                            }
                            else
                            {
                                errlist.Add($"校验{ClassCANS.OLD.模组码}失败，原因:{checkResult}");
                                Log.alarmLog($"校验{ClassCANS.OLD.模组码}失败，原因:{checkResult}", 1);
                                Scan1RunStep = (int)ScanControlFlag.进站MES失败;
                            }
                        }
                        else
                        {
                           
                            Scan1RunStep = (int)ScanControlFlag.存入PLC二维码;
                        }
                        break;
                    case (int)ScanControlFlag.存入PLC二维码:
                        if (/*FormStart.pLCTool.WritePLC("模组条码", ClassCANS.OLD.模组码) &&*/ FormStart.pLCTool.WritePLC("扫码OK", "True"))
                        {
                            //这里不用写回给PLC
                            Log.log($"反馈PLC扫码OK成功！");
                            Scan1RunStep = (int)ScanControlFlag.等待PLC复位扫码结果;
                        }
                        break;
                    case (int)ScanControlFlag.等待PLC复位扫码结果:
                        if (FormStart.pLCTool.ReadPLC("扫码触发").ToString() == "False")
                        {
                            if (FormStart.pLCTool.WritePLC("扫码OK", "False") && FormStart.pLCTool.WritePLC("扫码NG", "False") && FormStart.pLCTool.WritePLC("入站失败", "False"))
                            {
                                Scan1RunStep = (int)ScanControlFlag.等待扫码信号;
                            }
                        }
                        break;
                    case (int)ScanControlFlag.扫码失败:
                        if (FormStart.pLCTool.WritePLC("扫码NG", "True"))
                        {
                            errlist.Add("扫码失败");
                            SaveExcel(ClassCANS.OLD.模组码, "NG", errlist);
                            Log.log("发送PLC扫码失败信号");
                            Scan1RunStep = (int)ScanControlFlag.等待PLC复位扫码结果;
                        }
                        break;
                    case (int)ScanControlFlag.进站MES失败:
                        if (FormStart.pLCTool.WritePLC("入站失败", "True"))
                        {
                            SaveExcel(ClassCANS.OLD.模组码, "NG", errlist);
                            Log.alarmLog($"发送PLC模组NG信号",1);
                            Scan1RunStep = (int)ScanControlFlag.等待PLC复位扫码结果;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        public void SaveExcel(string code, string status, List<string> errlist)
        {
            try
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();

                string strErr = "";

                strErr = String.Join(";", errlist);
                dic.Add("小车条码", ClassCANS.OLD.小车码);
                dic.Add("PACK条码", code);
                dic.Add("时间", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                dic.Add("状态", status);
                dic.Add("类型", "扫码");
                //dic.Add("工位", "");
                //dic.Add("点胶量", "");
                //dic.Add("点胶时间", "");
                //for (int count = 1; count <= 28; count++)
                //{
                //    dic.Add($"检测面积{count}", FormStart.ccdData.ContainsKey(count.ToString()) == true ? FormStart.ccdData[count.ToString()].value.Trim() : "0");
                //}
                dic.Add("备注", "");

                MesTaskFlow.Callback(dic, status);
                List<string> list = new List<string>();
                DateTime time = DateTime.Now;
                string year = time.Year.ToString();
                string month = time.Month.ToString();
                string day = time.Day.ToString();
                string path = "D:\\生产记录\\生产信息\\" + year + "\\" + month;
                string txtname = year + month + day;
                //添加标题名称
                List<string> listhead = new List<string>();
                foreach (var item in dic)
                {
                    listhead.Add(item.Key);
                    list.Add(item.Value);
                }
                Task.Run(() => {
                    int i = Excelhelper.DataTableToExcel(code, listhead, list, path, txtname + ".xls");
                });
            }
            catch (Exception)
            {


            }
        }
        public override void iniPlc(string str)
        {
            ClassCANS.OLD.小车码 = "";
            ClassCANS.OLD.模组码 = "";
            _Client = FormStart.ScanConnect;
            _Client.msg = "";
            FormStart.pLCTool.WritePLC("扫码OK", "False");
            FormStart.pLCTool.WritePLC("扫码NG", "False");
            FormStart.pLCTool.WritePLC("入站失败", "False");
            sw0.Reset();
            Scan1RunStep = (int)ScanControlFlag.等待扫码信号;
            Log.log("初始化扫码流程完成。");
        }
    }
}
