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
using NPOI.SS.Formula.Functions;

namespace UpperComputer.TaskUnit
{
    public class FirstCheckCCDTaskFlow : TaskClass
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
        string ccdCount1 = "";
        uint icount1 = 0;
        string ccdCount2 = "";
        uint icount2 = 0;
        FirstCheckData check = new FirstCheckData();
        public static event Action<Dictionary<string, string>, string> CallBack_1;
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

        public static void Callback_1(Dictionary<string, string> dic, string str)
        {
            CallBack_1?.Invoke(dic, str);
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
                            string strCheck1 = FormStart.pLCTool.ReadPLC("防错点检_相机1触发").ToString();
                            string strCheck2 = FormStart.pLCTool.ReadPLC("防错点检_相机2触发").ToString();
                            ccdCount1 = FormStart.pLCTool.ReadPLC("防错点检_相机检测位号").ToString();
                            icount1 = 0;
                            uint.TryParse(ccdCount1, out icount1);
                            //ccdCount2 = FormStart.pLCTool.ReadPLC("防错点检_相机2检测位号").ToString();
                            ccdCount2 = ccdCount1;
                            icount2 = 0;
                            uint.TryParse(ccdCount2, out icount2);
                            if ( strCheck1== "True"  )
                            {
                                Thread.Sleep(1000);
                                string str = FormStart.pLCTool.ReadPLC("防错点检_相机1触发").ToString() ;
                                if (str != strCheck1)
                                {
                                    Log.log($"接受到PLC防错点检相机1信号[{strCheck1},{str}]");
                                    break;
                                }
                                strMarkindex = "1";
                                if (icount1 == 1)
                                {
                                    check = new FirstCheckData();
                                }
                                check.station = "1";
                                Log.log("接受到PLC防错点检相机1信号");
                                _Client1.msg = "";
                                iCCDRunStep = (int)CCDControlFlag.触发相机1拍照;
                            }
                            else if (strCheck2 == "True" && icount2 <= 4 && icount2 > 0)
                            {
                                Thread.Sleep(1000);
                                string str = FormStart.pLCTool.ReadPLC("防错点检_相机2触发").ToString();
                                if (str != strCheck2)
                                {
                                    Log.log2($"接受到PLC防错点检相机2信号[{strCheck2},{str}]");
                                    break;
                                }
                                if (icount2 == 1)
                                {
                                    check = new FirstCheckData();
                                }
                                check.station = "2";
                                strMarkindex = "2";
                                Log.log("接受到PLC防错点检相机2信号");
                                _Client2.msg = "";
                                iCCDRunStep = (int)CCDControlFlag.触发相机2拍照;
                            }
                            break;
                        case (int)CCDControlFlag.触发相机1拍照:
                            string ccdOrder = $"Start,{123},{icount2},{"X11"}";
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
                                if (icount1 == 1)
                                {
                                    check.status1 = "NG";
                                    if (_Client1.msg.Trim().Split(',').Length >= 2)
                                    {
                                        check.value1 = _Client1.msg.Trim().Split(',')[1];
                                    }
                                }
                                else if (icount1 == 2)
                                {
                                    check.status2 = "NG";
                                    if (_Client1.msg.Trim().Split(',').Length >= 2)
                                    {
                                        check.value2 = _Client1.msg.Trim().Split(',')[1];
                                    }
                                }
                                //else if (icount1 == 3)
                                //{
                                //    check.status3 = "NG";
                                //    if (_Client1.msg.Trim().Split(',').Length >= 2)
                                //    {
                                //        check.value3 = _Client1.msg.Trim().Split(',')[1];
                                //    }
                                //}
                                //else if (icount1 == 4)
                                //{
                                //    check.status4 = "NG";
                                //    if (_Client1.msg.Trim().Split(',').Length >= 2)
                                //    {
                                //        check.value4 = _Client1.msg.Trim().Split(',')[1];
                                //    }
                                //}
                                //else if (icount1 == 5)
                                //{
                                //    check.status5 = "NG";
                                //    if (_Client1.msg.Trim().Split(',').Length >= 2)
                                //    {
                                //        check.value5 = _Client1.msg.Trim().Split(',')[1];
                                //    }
                                //}

                                Log.log($"相机{strMarkindex}拍照NG，CCD返回:{_Client2.msg}，发送PLC拍照完成信号");
                                iCCDRunStep = (int)CCDControlFlag.拍照成功;
                                continue;
                            }
                            else if (_Client1.msg.Contains("OK"))
                            {
                                if (icount1 == 1)
                                {
                                    check.status1 = "OK";
                                    if (_Client1.msg.Trim().Split(',').Length >= 2)
                                    {
                                        check.value1 = _Client1.msg.Trim().Split(',')[1];
                                    }
                                }
                                else if (icount1 == 2)
                                {
                                    check.status2 = "OK";
                                    if (_Client1.msg.Trim().Split(',').Length >= 2)
                                    {
                                        check.value2 = _Client1.msg.Trim().Split(',')[1];
                                    }
                                }
                                //else if (icount1 == 3)
                                //{
                                //    check.status3 = "OK";
                                //    if (_Client1.msg.Trim().Split(',').Length >= 2)
                                //    {
                                //        check.value3 = _Client1.msg.Trim().Split(',')[1];
                                //    }
                                //}
                                //else if (icount1 == 4)
                                //{
                                //    check.status4 = "OK";
                                //    if (_Client1.msg.Trim().Split(',').Length >= 2)
                                //    {
                                //        check.value4 = _Client1.msg.Trim().Split(',')[1];
                                //    }
                                //}
                                //else if (icount1 == 5)
                                //{
                                //    check.status5 = "OK";
                                //    if (_Client1.msg.Trim().Split(',').Length >= 2)
                                //    {
                                //        check.value5 = _Client1.msg.Trim().Split(',')[1];
                                //    }
                                //}

                                Log.log($"相机{strMarkindex}拍照OK，CCD返回:{_Client1.msg}，发送PLC拍照完成信号");
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
                            string ccdOrder2 = $"Start,{123},{icount2},{"X11"}";
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
                                if (icount2 == 1)
                                {
                                    check.status1 = "NG";
                                    if (_Client2.msg.Trim().Split(',').Length >= 2)
                                    {
                                        check.value1 = _Client2.msg.Trim().Split(',')[1];
                                    }
                                }
                                else if (icount2 == 2)
                                {
                                    check.status2 = "NG";
                                    if (_Client2.msg.Trim().Split(',').Length >= 2)
                                    {
                                        check.value2 = _Client2.msg.Trim().Split(',')[1];
                                    }
                                }
                                //else if (icount2 == 3)
                                //{
                                //    check.status3 = "NG";
                                //    if (_Client2.msg.Trim().Split(',').Length >= 2)
                                //    {
                                //        check.value3 = _Client2.msg.Trim().Split(',')[1];
                                //    }
                                //}
                                //else if (icount2 == 4)
                                //{
                                //    check.status4 = "NG";
                                //    if (_Client2.msg.Trim().Split(',').Length >= 2)
                                //    {
                                //        check.value4 = _Client2.msg.Trim().Split(',')[1];
                                //    }
                                //}
                                //else if (icount2 == 5)
                                //{
                                //    check.status5 = "NG";
                                //    if (_Client2.msg.Trim().Split(',').Length >= 2)
                                //    {
                                //        check.value5 = _Client2.msg.Trim().Split(',')[1];
                                //    }
                                //}

                                Log.alarmLog($"相机{strMarkindex}拍照NG，CCD返回:{_Client2.msg}，发送PLC拍照失败信号",2);
                                iCCDRunStep = (int)CCDControlFlag.拍照成功;
                                continue;
                            }
                            else if (_Client2.msg.Contains("OK"))
                            {
                                if (icount2 == 1)
                                {
                                    check.status1 = "OK";
                                    if (_Client2.msg.Trim().Split(',').Length >= 2)
                                    {
                                        check.value1 = _Client2.msg.Trim().Split(',')[1];
                                    }
                                }
                                else if (icount2 == 2)
                                {
                                    check.status2 = "OK";
                                    if (_Client2.msg.Trim().Split(',').Length >= 2)
                                    {
                                        check.value2 = _Client2.msg.Trim().Split(',')[1];
                                    }
                                }
                                //else if (icount2 == 3)
                                //{
                                //    check.status3 = "OK";
                                //    if (_Client2.msg.Trim().Split(',').Length >= 2)
                                //    {
                                //        check.value3 = _Client2.msg.Trim().Split(',')[1];
                                //    }
                                //}
                                //else if (icount2 == 4)
                                //{
                                //    check.status4 = "OK";
                                //    if (_Client2.msg.Trim().Split(',').Length >= 2)
                                //    {
                                //        check.value4 = _Client2.msg.Trim().Split(',')[1];
                                //    }
                                //}
                                //else if (icount2 == 5)
                                //{
                                //    check.status5 = "OK";
                                //    if (_Client2.msg.Trim().Split(',').Length >= 2)
                                //    {
                                //        check.value5 = _Client2.msg.Trim().Split(',')[1];
                                //    }
                                //}

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
                                if (FormStart.pLCTool.WritePLC("防错点检_相机1OK", "True"))
                                {
                                    Log.log($"相机1发送PLC拍照成功信号");
                                    iCCDRunStep = (int)CCDControlFlag.等待PLC复位拍照完成信号;
                                }
                            }
                            else if (strMarkindex == "2")
                            {
                                if (FormStart.pLCTool.WritePLC("防错点检_相机2OK", "True"))
                                {
                                    Log.log2($"相机2发送PLC拍照成功信号");
                                    iCCDRunStep = (int)CCDControlFlag.等待PLC复位拍照完成信号;
                                }
                            }
                            break;
                        case (int)CCDControlFlag.拍照失败:
                            if (strMarkindex == "1")
                            {
                                if (FormStart.pLCTool.WritePLC("防错点检_相机1NG", "True"))
                                {
                                    Log.log($"相机1发送PLC拍照失败信号");
                                    iCCDRunStep = (int)CCDControlFlag.等待PLC复位拍照完成信号;
                                }
                            }
                            else if (strMarkindex == "2")
                            {
                                if (FormStart.pLCTool.WritePLC("防错点检_相机2NG", "True"))
                                {
                                    Log.log2($"相机2发送PLC拍照失败信号");
                                    iCCDRunStep = (int)CCDControlFlag.等待PLC复位拍照完成信号;
                                }
                            }
                            break;
                        case (int)CCDControlFlag.等待PLC复位拍照完成信号:
                            if (strMarkindex == "1")
                            {
                                if (FormStart.pLCTool.ReadPLC("防错点检_相机1触发").ToString() == "False")
                                {
                                    if (FormStart.pLCTool.WritePLC("防错点检_相机1OK", "False") && FormStart.pLCTool.WritePLC("防错点检_相机1NG", "False"))
                                    {
                                        Log.log($"相机1检测完成");
                                        if (icount1 == 2)
                                        {
                                            SaveCheckExcel(check);
                                            Log.log($"相机1防错点检完成");
                                        }
                                        iCCDRunStep = (int)CCDControlFlag.等待开始拍照信号;
                                    }
                                }
                            }
                            else if (strMarkindex == "2")
                            {
                                if (FormStart.pLCTool.ReadPLC("防错点检_相机2触发").ToString() == "False")
                                {
                                    if (FormStart.pLCTool.WritePLC("防错点检_相机2OK", "False") && FormStart.pLCTool.WritePLC("防错点检_相机2NG", "False"))
                                    {
                                        Log.log2($"相机2检测完成");
                                        if (icount2 == 2)
                                        {
                                            SaveCheckExcelSec(check);
                                            Log.log2($"相机2防错点检完成");
                                        }
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
                    Log.log("防错点检流程异常,原因:" + ex.Message);
                    Thread.Sleep(1000);
                }
            }
        }
        
        /// <summary>
        /// 按条数据导入Excel表格清洗数据
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool SaveCheckExcel(FirstCheckData checkdata)
        {
            try
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                Dictionary<string, string> dicTmp = new Dictionary<string, string>();
                List<string> list = new List<string>();
                DateTime time = DateTime.Now;
                string year = time.Year.ToString();
                string month = time.Month.ToString();
                string day = time.Day.ToString();
                string path = "D:\\生产记录\\防错样件点检记录\\" + year + "\\" + month;
                string txtname = year + month + day;
                string code = time.ToString("yyyyMMddHHmmss");
                string date = time.ToString("yyyy-MM-dd HH:mm:ss");
                //dic.Add("防错样件点检条码", "PACK-P01-H77-022");
                //dic.Add("编号", "23049");
                //dic.Add("时间", date);
                //dic.Add("工位", checkdata.station);
                //dic.Add("检测结果1", checkdata.status1); 
                //dic.Add("检测面积1", checkdata.value1);
                
                //dic.Add("备注", " ");

                string _code = "SHOU JIAN";
                //string _code = "";
                //_code += "PACK-P01-H77-022;";
                
                //List<string> errList = new List<string>();
                //if (!firstUp(dic, ref errList))
                //{
                //    return false;
                //}

                //dic.Clear();
                //date = time.ToString("yyyy-MM-dd HH:mm:ss");
                //dic.Add("防错样件点检条码", "PACK-P01-H77-023");
                //dic.Add("编号", "23048");
                //dic.Add("时间", date);
                //dic.Add("工位", checkdata.station);
                //dic.Add("检测结果1", checkdata.status2); 
                //dic.Add("检测面积1", checkdata.value2);
                //_code += "PACK-P01-H77-023;";
                
                //errList = new List<string>();
                //if (!firstUp(dic, ref errList))
                //{

                //    return false;
                //}

                //dic.Clear();
                //date = time.ToString("yyyy-MM-dd HH:mm:ss");
                //dic.Add("防错样件点检条码", "PACK-P01-H77-024");
                //dic.Add("编号", "23047");
                //dic.Add("时间", date);
                //dic.Add("工位", checkdata.station);
                //dic.Add("检测结果1", checkdata.status3); 
                //dic.Add("检测面积1", checkdata.value3);
                //_code += "PACK-P01-H77-024;";
                
                //errList = new List<string>();
                //if (!firstUp(dic, ref errList))
                //{

                //    return false;
                //}

                //dic.Clear();
                //date = time.ToString("yyyy-MM-dd HH:mm:ss");
                //dic.Add("防错样件点检条码", "PACK-P01-H77-025");
                //dic.Add("编号", "23050");
                //dic.Add("时间", date);
                //dic.Add("工位", checkdata.station);
                //dic.Add("检测结果1", checkdata.status4); 
                //dic.Add("检测面积1", checkdata.value4);
                //_code += "PACK-P01-H77-025;";
                
                //errList = new List<string>();
                //if (!firstUp(dic, ref errList))
                //{

                //    return false;
                //}

                //dic.Clear();
                //date = time.ToString("yyyy-MM-dd HH:mm:ss");
                //dic.Add("防错样件点检条码", "PACK-P01-H77-026");
                //dic.Add("编号", "23051");
                //dic.Add("时间", date);
                //dic.Add("工位", checkdata.station);
                //dic.Add("检测结果1", checkdata.status5);
                //dic.Add("检测面积1", checkdata.value5);
                //_code += "PACK-P01-H77-026;";

                //errList = new List<string>();
                //if (!firstUp(dic, ref errList))
                //{

                //    return false;
                //}

                date = time.ToString("yyyy-MM-dd HH:mm:ss");
                dicTmp.Add("防错样件点检条码", _code);
                dicTmp.Add("时间", date);
                //dicTmp.Add("工位", checkdata.station);
                dicTmp.Add("工位", "左工位");
                dicTmp.Add("点位1检测结果", checkdata.status1);
                dicTmp.Add("点位2检测结果", checkdata.status2);
                //dicTmp.Add("点位3检测结果", checkdata.status3);
                //dicTmp.Add("点位4检测结果", checkdata.status4);
                //dicTmp.Add("点位5检测结果", checkdata.status5);
                dicTmp.Add("备注", " ");

                Callback_1(dicTmp, "");
                
                List<string> listhead = new List<string>();
                list = new List<string>();
                foreach (var item in dicTmp)
                {
                    listhead.Add(item.Key);
                    list.Add(item.Value);
                }
                Task.Run(() => {
                    int i = Excelhelper.DataTableToExcel(_code, listhead, list, path, txtname + ".xls");
                });
            }
            catch (Exception ex)
            {
                Log.alarmLog("保存EXCEL防错点检数据出错：" + ex.ToString(), 1);
                return false;
            }
            return true;
        }

        public static bool SaveCheckExcelSec(FirstCheckData checkdata)
        {
            try
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                Dictionary<string, string> dicTmp = new Dictionary<string, string>();
                List<string> list = new List<string>();
                DateTime time = DateTime.Now;
                string year = time.Year.ToString();
                string month = time.Month.ToString();
                string day = time.Day.ToString();
                string path = "D:\\生产记录\\防错样件点检记录\\" + year + "\\" + month;
                string txtname = year + month + day;
                string code = time.ToString("yyyyMMddHHmmss");
                string date = time.ToString("yyyy-MM-dd HH:mm:ss");
                //dic.Add("防错样件点检条码", "PACK-P01-H77-022");
                //dic.Add("编号", "23049");
                //dic.Add("时间", date);
                //dic.Add("工位", checkdata.station);
                //dic.Add("检测结果1", checkdata.status1); 
                //dic.Add("检测面积1", checkdata.value1);
                //dic.Add("备注", " ");
                string _code = "SHOU JIAN";

                //string _code = "";
                //_code += "PACK-P01-H77-022;";
                //List<string> errList = new List<string>();
                //if (!firstUp(dic, ref errList))
                //{

                //    return false;
                //}

                //dic.Clear();
                //date = time.ToString("yyyy-MM-dd HH:mm:ss");
                //dic.Add("防错样件点检条码", "PACK-P01-H77-023");
                //dic.Add("编号", "23048");
                //dic.Add("时间", date);
                //dic.Add("工位", checkdata.station);
                //dic.Add("检测结果1", checkdata.status2); 
                //dic.Add("检测面积1", checkdata.value2);
                //_code += "PACK-P01-H77-023;";
                //errList = new List<string>();
                //if (!firstUp(dic, ref errList))
                //{

                //    return false;
                //}

                //dic.Clear();
                //date = time.ToString("yyyy-MM-dd HH:mm:ss");
                //dic.Add("防错样件点检条码", "PACK-P01-H77-024");
                //dic.Add("编号", "23047");
                //dic.Add("时间", date);
                //dic.Add("工位", checkdata.station);
                //dic.Add("检测结果1", checkdata.status3); 
                //dic.Add("检测面积1", checkdata.value3);
                //_code += "PACK-P01-H77-024;";
                //errList = new List<string>();
                //if (!firstUp(dic, ref errList))
                //{

                //    return false;
                //}


                //dic.Clear();
                //date = time.ToString("yyyy-MM-dd HH:mm:ss");
                //dic.Add("防错样件点检条码", "PACK-P01-H77-025");
                //dic.Add("编号", "23050");
                //dic.Add("时间", date);
                //dic.Add("工位", checkdata.station);
                //dic.Add("检测结果1", checkdata.status4); dic.Add("检测面积1", checkdata.value4);
                //_code += "PACK-P01-H77-025;";
                //errList = new List<string>();
                //if (!firstUp(dic, ref errList))
                //{

                //    return false;
                //}

                //dic.Clear();
                //date = time.ToString("yyyy-MM-dd HH:mm:ss");
                //dic.Add("防错样件点检条码", "PACK-P01-H77-026");
                //dic.Add("编号", "23051");
                //dic.Add("时间", date);
                //dic.Add("工位", checkdata.station);
                //dic.Add("检测结果1", checkdata.status5); 
                //dic.Add("检测面积1", checkdata.value5);
                //_code += "PACK-P01-H77-026;";
                //errList = new List<string>();
                //if (!firstUp(dic, ref errList))
                //{

                //    return false;
                //}

                date = time.ToString("yyyy-MM-dd HH:mm:ss");
                dicTmp.Add("防错样件点检条码", _code);
                dicTmp.Add("时间", date);
                //dicTmp.Add("工位", checkdata.station);
                dicTmp.Add("工位", "右工位");
                dicTmp.Add("点位1检测结果", checkdata.status1);
                dicTmp.Add("点位2检测结果", checkdata.status2);
                //dicTmp.Add("点位3检测结果", checkdata.status3);
                //dicTmp.Add("点位4检测结果", checkdata.status4);
                //dicTmp.Add("点位5检测结果", checkdata.status5);
                dicTmp.Add("备注", " ");

                Callback_1(dicTmp, "");

                List<string> listhead = new List<string>();
                list = new List<string>();
                foreach (var item in dicTmp)
                {
                    listhead.Add(item.Key);
                    list.Add(item.Value);
                }
                Task.Run(() => {
                    int i = Excelhelper.DataTableToExcel(_code, listhead, list, path, txtname + ".xls");
                });

            }
            catch (Exception ex)
            {
                Log.alarmLog("保存EXCEL防错点检数据出错：" + ex.ToString(), 1);
                return false;
            }
            return true;
        }
        public override void iniPlc(string str)
        {
            _Client1 = FormStart.CCDConnect;
            _Client2 = FormStart.CCDConnect1;
            FormStart.pLCTool.WritePLC("防错点检_相机1OK", "False");
            FormStart.pLCTool.WritePLC("防错点检_相机2OK", "False");
            FormStart.pLCTool.WritePLC("防错点检_相机1NG", "False");
            FormStart.pLCTool.WritePLC("防错点检_相机2NG", "False");
            _Client1.msg = "";
            _Client2.msg = "";
            iCCDRunStep = (int)CCDControlFlag.等待开始拍照信号;
            sw1.Reset();
            Log.log("初始化CCD防错样件点检流程完成。");
        }

        public static bool firstUp(Dictionary<string, string> dic, ref List<string> errList)
        {
            try
            {
                string testRes = "0";
                List<Dictionary<string, string>> listUp = new List<Dictionary<string, string>>();
                listUp.Add(new Dictionary<string, string> 
                {
                    ["paramName"] = "检测结果",
                    ["paramCode"] = dic["编号"],
                    ["paramValue"] = "",
                    ["paramResult"] = testRes,
                    ["paramUnit"] = ""
                });
                //foreach (var itemMain in dic)
                //{
                //    foreach (var item in Formlogin.dicPar.Values)
                //    {
                //        if (item.paramName == itemMain.Key)
                //        {
                //            double paramFirstLower = Convert.ToDouble(item.paramFirstLower); paramFirstLower = 100;
                //            double paramFirstUpper = Convert.ToDouble(item.paramFirstUpper); paramFirstUpper = 200;
                //            if (Convert.ToDouble(itemMain.Value) > paramFirstUpper ||
                //       Convert.ToDouble(itemMain.Value) < paramFirstLower)
                //            {
                //                string value = itemMain.Value;
                //                testRes = "1";
                //                Log.alarmLog($"参数名称:[{item.paramName}]当前值:[{value}]超出管控范围[{paramFirstLower}-{paramFirstUpper}]", 1);
                //                errList.Add($"参数名称:[{item.paramName}]当前值:[{value}]超出管控范围[{paramFirstLower}-{paramFirstUpper}]");

                //            }
                //            else
                //            {
                //                testRes = "0";
                //            }
                //            if (Convert.ToDouble(itemMain.Value) > paramFirstUpper ||
                //       Convert.ToDouble(itemMain.Value) < paramFirstLower)
                //            {
                //                string value = itemMain.Value;
                //                testRes = "1";
                //                Log.alarmLog($"参数名称:[{item.paramName}]当前值:[{value}]超出管控范围[{paramFirstLower}-{paramFirstUpper}]", 1);
                //                errList.Add($"参数名称:[{item.paramName}]当前值:[{value}]超出管控范围[{paramFirstLower}-{paramFirstUpper}]");

                //            }
                //            else
                //            {
                //                testRes = "0";
                //            }

                //            listUp.Add(new Dictionary<string, string>
                //            {
                //                ["paramName"] = item.paramName,
                //                ["paramCode"] = item.paramCode,
                //                ["paramValue"] = itemMain.Value,
                //                ["paramResult"] = testRes,
                //                ["paramUnit"] = item.paramUnit
                //            });
                //        }
                //    }
                //}
                string upRes = "";
                if (!uploadProductFir(dic, listUp))
                {
            
                    return false;
                }



            }
            catch (Exception ex)
            {
                Log.alarmLog($"处理MES数据异常,异常原因:{ex.Message}", 1);
                return false;
            }
            return true;
        }
        public static bool uploadProductFir(Dictionary<string, string> dic,List<Dictionary<string, string>> testList)
        {
            string str = ""; List<Dictionary<string, string>> listUpStep = new List<Dictionary<string, string>>();
            string checkResultUpload = mes.Instance().SampleDataUpload(
                                             ClassCANS.OLD.SessionId,  ClassCANS.OLD.groupCode, dic["防错样件点检条码"], dic["检测结果1"].ToLower() == "ok" ? "0" : "1", testList, listUpStep, ref str);
            if (checkResultUpload.ToUpper().Contains("TRUE"))
            {
                Log.log($"模组组条码[{dic["防错样件点检条码"]}]过站成功");
            }
            else
            {
                string msg = $"模组条码[{dic["防错样件点检条码"]}]过站失败,原因：{checkResultUpload}";
                //errList.Add(checkResultUpload);
                Log.alarmLog(msg, 1);
                return false;
            }
            return true;
        }

    }
}