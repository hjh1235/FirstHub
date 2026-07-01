using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace UpperComputer.TaskUnit
{
    public class CCDCheckTaskFlow : TaskClass
    {

        public _Client _Client1;
        public _Client _Client2;

        /// <summary>
        /// 拍照步骤
        /// </summary>
        public int iCCDRunStep = 0;
        string strMarkindex = "0";
        string strCheck1 = "";
        string strCheck2 = "";
        CheckData check = new CheckData();
        public enum CCDControlFlag
        {
            等待开始拍照信号,
            触发相机1拍照,
            等待拍照1结果,
            触发相机2拍照,
            等待拍照2结果,
            拍照成功,
            拍照失败,
            等待PLC复位拍照完成信号,
        }

        /// <summary>
        /// CCD相机拍照流程
        /// </summary>
        public override void TaskRun()
        {
            while (true)
            {
                Thread.Sleep(5);
                try
                {
                    if (TaskClass.plcConnState == false || !FormStart.bInit)
                    {
                        continue;
                    }
                    switch (iCCDRunStep)
                    {
                        case (int)CCDControlFlag.等待开始拍照信号:
                            string strCheck1 = FormStart.pLCTool.ReadPLC("点检_相机1触发").ToString();
                            string strCheck2 = FormStart.pLCTool.ReadPLC("点检_相机2触发").ToString();
                            if ( strCheck1== "True")
                            {
                                Thread.Sleep(1000);
                                string str = FormStart.pLCTool.ReadPLC("点检_相机1触发").ToString() ;
                                if (str != strCheck1)
                                {
                                    Log.log($"接受到PLC点检相机1信号[{strCheck1},{str}]");
                                    break;
                                }
                                strMarkindex = "1";
                                check = new CheckData();
                                check.station = "1";
                                Log.log("接受到PLC点检相机1信号");
                                _Client1.msg = "";
                                iCCDRunStep = (int)CCDControlFlag.触发相机1拍照;
                            }
                            else if (strCheck2 == "True")
                            {
                                Thread.Sleep(1000);
                                string str = FormStart.pLCTool.ReadPLC("点检_相机2触发").ToString();
                                if (str != strCheck2)
                                {
                                    Log.log2($"接受到PLC点检相机2信号[{strCheck2},{str}]");
                                    break;
                                }
                                check = new CheckData();
                                check.station = "2";
                                strMarkindex = "2";
                                Log.log("接受到PLC点检相机2信号");
                                _Client2.msg = "";
                                iCCDRunStep = (int)CCDControlFlag.触发相机2拍照;
                            }
                            break;
                        case (int)CCDControlFlag.触发相机1拍照:
                            string ccdOrder = $"Check,{strMarkindex},dianjian,0,0";
                            if (_Client1.Send(ccdOrder) == false)
                            {
                                Log.alarmLog($"触发相机{strMarkindex}拍照失败，请检查相机{strMarkindex}是否打开、通讯",1);
                                iCCDRunStep = (int)CCDControlFlag.拍照失败;
                                continue;
                            }
                            sw1.Restart();
                            Log.log($"发送相机{strMarkindex}拍照命令:{ccdOrder}");
                            iCCDRunStep = (int)CCDControlFlag.等待拍照1结果;
                            break;
                        case (int)CCDControlFlag.等待拍照1结果:
                            if (_Client1.msg.Contains("NG"))
                            {
                                check.status = "NG";
                                check.value = _Client1.msg.Trim();
                                Log.alarmLog($"相机{strMarkindex}拍照NG，CCD返回:{_Client2.msg}，发送PLC拍照失败信号",1);
                                iCCDRunStep = (int)CCDControlFlag.拍照失败;
                                continue;
                            }
                            else if (_Client1.msg.Contains("OK"))
                            {
                                check.status = "OK";
                                check.value = _Client1.msg.Trim();
                                Log.log($"相机{strMarkindex}拍照OK，CCD返回:{_Client1.msg}，发送PLC拍照成功信号");
                                iCCDRunStep = (int)CCDControlFlag.拍照成功;
                            }
                            else if (sw1.ElapsedMilliseconds > 20000)
                            {
                                Log.alarmLog($"相机{strMarkindex}拍照超时，发送PLC拍照失败信号",1);
                                sw1.Restart();
                                iCCDRunStep = (int)CCDControlFlag.拍照失败;
                                continue;
                            }
                            break;
                        case (int)CCDControlFlag.触发相机2拍照:
                            string ccdOrder2 = $"Check,{strMarkindex},dianjian,0,0";
                            if (_Client2.Send(ccdOrder2) == false)
                            {
                                Log.alarmLog($"触发相机{strMarkindex}拍照失败，请检查相机{strMarkindex}是否打开、通讯", 2);
                                iCCDRunStep = (int)CCDControlFlag.拍照失败;
                                continue;
                            }
                            sw1.Restart();
                            Log.log2($"发送相机{strMarkindex}拍照命令:{ccdOrder2}");
                            iCCDRunStep = (int)CCDControlFlag.等待拍照2结果;
                            break;
                        case (int)CCDControlFlag.等待拍照2结果:
                            if (_Client2.msg.Contains("NG"))
                            {
                                check.status = "NG";
                                check.value = _Client2.msg.Trim();
                                Log.alarmLog($"相机{strMarkindex}拍照NG，CCD返回:{_Client2.msg}，发送PLC拍照失败信号",2);
                                iCCDRunStep = (int)CCDControlFlag.拍照失败;
                                continue;
                            }
                            else if (_Client2.msg.Contains("OK"))
                            {
                                check.status = "OK";
                                check.value = _Client2.msg.Trim();
                                Log.log2($"相机{strMarkindex}拍照OK，CCD返回:{_Client2.msg}，发送PLC拍照成功信号");
                                iCCDRunStep = (int)CCDControlFlag.拍照成功;
                            }
                            else if (sw1.ElapsedMilliseconds > 20000)
                            {
                                Log.alarmLog($"相机{strMarkindex}拍照超时，发送PLC拍照失败信号",2);
                                sw1.Restart();
                                iCCDRunStep = (int)CCDControlFlag.拍照失败;
                                continue;
                            }
                            break;
                        case (int)CCDControlFlag.拍照成功:
                            if (strMarkindex=="1")
                            {
                                if (FormStart.pLCTool.WritePLC("点检_相机1OK", "True"))
                                {
                                    Log.log($"相机1发送PLC拍照成功信号");
                                    iCCDRunStep = (int)CCDControlFlag.等待PLC复位拍照完成信号;
                                }
                            }
                            else if (strMarkindex == "2")
                            {
                                if (FormStart.pLCTool.WritePLC("点检_相机2OK", "True"))
                                {
                                    Log.log2($"相机2发送PLC拍照成功信号");
                                    iCCDRunStep = (int)CCDControlFlag.等待PLC复位拍照完成信号;
                                }
                            }
                            break;
                        case (int)CCDControlFlag.拍照失败:
                            if (strMarkindex == "1")
                            {
                                if (FormStart.pLCTool.WritePLC("点检_相机1NG", "True"))
                                {
                                    Log.log($"相机1发送PLC拍照失败信号");
                                    iCCDRunStep = (int)CCDControlFlag.等待PLC复位拍照完成信号;
                                }
                            }
                            else if (strMarkindex == "2")
                            {
                                if (FormStart.pLCTool.WritePLC("点检_相机2NG", "True"))
                                {
                                    Log.log2($"相机2发送PLC拍照失败信号");
                                    iCCDRunStep = (int)CCDControlFlag.等待PLC复位拍照完成信号;
                                }
                            }
                            break;
                        case (int)CCDControlFlag.等待PLC复位拍照完成信号:
                            if (strMarkindex == "1")
                            {
                                if (FormStart.pLCTool.ReadPLC("点检_相机1触发").ToString() == "False")
                                {
                                    if (FormStart.pLCTool.WritePLC("点检_相机1OK", "False") && FormStart.pLCTool.WritePLC("点检_相机1NG", "False"))
                                    {
                                        SaveCheckExcel(check);
                                        Log.log($"相机1点检完成");
                                        iCCDRunStep = (int)CCDControlFlag.等待开始拍照信号;
                                    }
                                }
                            }
                            else if (strMarkindex == "2")
                            {
                                if (FormStart.pLCTool.ReadPLC("点检_相机2触发").ToString() == "False")
                                {
                                    if (FormStart.pLCTool.WritePLC("点检_相机2OK", "False") && FormStart.pLCTool.WritePLC("点检_相机2NG", "False"))
                                    {
                                        SaveCheckExcel(check);
                                        Log.log2($"相机2点检完成");
                                        iCCDRunStep = (int)CCDControlFlag.等待开始拍照信号;
                                    }
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Log.log("点检流程异常,原因:" + ex.Message);
                    Thread.Sleep(1000);
                }
            }
        }
        /// <summary>
        /// 按条数据导入Excel表格清洗数据
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool SaveCheckExcel(CheckData checkdata)
        {
            try
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                List<string> list = new List<string>();
                DateTime time = DateTime.Now;
                string year = time.Year.ToString();
                string month = time.Month.ToString();
                string day = time.Day.ToString();
                string path = "D:\\生产记录\\点检记录\\" + year + "\\" + month;
                string txtname = year + month + day;
                string code = time.ToString("yyyyMMddHHmmss");
                dic.Add("条码",code);
                dic.Add("工位", checkdata.station);
                dic.Add("状态", checkdata.status);
                dic.Add("点检结果",checkdata.status);
                dic.Add("备注", String.Join(";",checkdata));
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
            catch (Exception ex)
            {
                Log.alarmLog("保存EXCEL点检数据出错：" + ex.ToString(), 1);
                return false;
            }
            return true;
        }
        public override void iniPlc(string str)
        {
            _Client1 = FormStart.CCDConnect;
            _Client2 = FormStart.CCDConnect1;
            FormStart.pLCTool.WritePLC("点检_相机1OK", "False");
            FormStart.pLCTool.WritePLC("点检_相机1NG", "False");
            FormStart.pLCTool.WritePLC("点检_相机2OK", "False");
            FormStart.pLCTool.WritePLC("点检_相机2NG", "False");
            _Client1.msg = "";
            _Client2.msg = "";
            iCCDRunStep = (int)CCDControlFlag.等待开始拍照信号;
            sw1.Reset();
            Log.log("初始化CCD点检流程完成。");
        }
    }
}