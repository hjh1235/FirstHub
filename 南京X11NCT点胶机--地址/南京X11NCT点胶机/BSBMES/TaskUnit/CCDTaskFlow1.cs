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
    public class CCDTaskFlow1 : TaskClass
    {
        _Client _Client;
        /// <summary>
        /// 流程步骤
        /// </summary>
        int iCCDRunStep = 0;
        /// <summary>
        /// 拍照类型
        /// </summary>
        string type1 = "";
        string ccdCount = "";
        Stopwatch S1 = new Stopwatch();
        uint icount = 0;
        public enum CCDControlFlag
        {
            等待开始拍照信号,
            触发相机拍照,
            等待拍照结果,
            拍照成功,
            拍照失败,
            等待PLC复位拍照完成信号
        }

        /// <summary>
        /// CCD相机检测流程
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
                            ccdCount = FormStart.pLCTool.ReadPLC("相机1检测位号").ToString();
                            icount = 0;
                            uint.TryParse(ccdCount, out icount);
                            if (FormStart.pLCTool.ReadPLC("相机1触发检测").ToString() == "True")
                            {
                                sw2.Restart();
                                Data.bUserFlag1 = true;
                                Log.log($"接受到相机1触发检测，点位:[{ccdCount}]");
                                _Client.msg = "";
                                iCCDRunStep = (int)CCDControlFlag.触发相机拍照;
                                S1.Restart();
                            }
                            break;
                        case (int)CCDControlFlag.触发相机拍照:
                            Log.log($"计时：{S1.Elapsed.TotalMilliseconds}ms");
                            string code = ClassCANS.OLD.模组码;
                            //if (code == "")
                            //{
                            //    code = FormStart.pLCTool.ReadPLC("模组条码").ToString().Replace("\r", "").Replace("\0", "");
                            //    ClassCANS.OLD.模组码 = code;
                            //}
                            string productType = ClassCANS.OLD.当前配方;
                            if (code == "" || code == "ERROR")
                            {
                                Log.alarmLog($"触发相机1检测失败，模组码为空，请检查是否扫码", 1);
                                iCCDRunStep = (int)CCDControlFlag.拍照失败;
                                continue;
                            }
                            string ccdOrder = $"Start,{code},{ccdCount},{productType}";
                            if (_Client.Send(ccdOrder) == false)
                            {
                                Log.alarmLog($"触发相机1检测失败，请检查相机1是否打开、通讯", 1);
                                iCCDRunStep = (int)CCDControlFlag.拍照失败;
                                continue;
                            }
                            sw1.Restart();
                            Log.log($"发送相机1检测命令{ccdOrder}，耗时:[{sw2.Elapsed.Milliseconds}]ms");
                            sw2.Restart();
                            iCCDRunStep = (int)CCDControlFlag.等待拍照结果;
                            // }         
                            break;
                        case (int)CCDControlFlag.等待拍照结果:
                            if (_Client.msg.Contains("OK"))
                            {
                                try
                                {
                                    Log.log($"接受到相机1返回检测ok结果【{_Client.msg}】");
                                    //string Result = _Client.msg.Replace("/r/n", "");
                                    //string Area = Result.Split('#')[1];
                                    //string Diameter = Result.Split('#')[2];
                                    //string Proportion = Result.Split('#')[3];

                                    string[] str = _Client.msg.Trim().Split(',');
                                    if (str.Length < 3)
                                    {
                                        Log.alarmLog($"相机1视觉返回结果错误，视觉返回:[{_Client.msg}]", 1);
                                        iCCDRunStep = (int)CCDControlFlag.拍照失败;
                                        return;
                                    }
                                    InitClass.CCDArea.Add(double.Parse( str[1]));
                                    InitClass.Proportion.Add(str[2]);
                                    //    if (str.Length >= 2)
                                    //{
                                    //    var mModels1 = HisDataClass.Data.list.Find((c) => c.strPoint == ccdCount);
                                    //    if (mModels1 != null)
                                    //    {
                                    //        mModels1.value = str[1];
                                    //        mModels1.value2 = str[2];
                                    //    }
                                    //    else
                                    //    {
                                    //        CCDDataClass ccd = new CCDDataClass();
                                    //        ccd.strPoint = ccdCount;
                                    //        ccd.value = str[1];
                                    //        ccd.value2 = str[2];
                                    //    }
                                    //    ClassXMLGET.xmlset<HisDataClass>("HistroyData.xml", HisDataClass.Data);


                                    //    if (!FormStart.ccdData.ContainsKey(ccdCount))
                                    //    {
                                    //        FormStart.ccdData.Add(ccdCount, new CCDDataClass { strPoint = ccdCount, status = "OK", value = str[1], value2 = str[2] });
                                    //    }
                                    //    else
                                    //    {
                                    //        FormStart.ccdData[ccdCount] = new CCDDataClass { strPoint = ccdCount, status = "OK", value = str[1], value2 = str[2] };
                                    //    }
                                    //}
                                }
                                catch (Exception)
                                {
                                    Log.alarmLog($"相机1解析视觉数据失败，视觉返回:[{_Client.msg}]", 1);
                                    iCCDRunStep = (int)CCDControlFlag.拍照失败;
                                    return;
                                }
                                Log.log($"相机1检测OK，视觉返回:[{_Client.msg}]，耗时:[{sw2.Elapsed.Milliseconds}]ms");
                                sw2.Restart();
                                iCCDRunStep = (int)CCDControlFlag.拍照成功;
                            }

                            else if (_Client.msg.Contains("NG"))
                            {
                                //string[] str = _Client.msg.Trim().Split(',');
                                //if (str.Length >= 2)
                                //{
                                //    try
                                //    {
                                //        var mModels1 = HisDataClass.Data.list.Find((c) => c.strPoint == ccdCount);
                                //        if (mModels1 != null)
                                //        {
                                //            mModels1.value = str[1];
                                //            mModels1.value2 = str[2];
                                //        }
                                //        else
                                //        {
                                //            CCDDataClass ccd = new CCDDataClass();
                                //            ccd.strPoint = ccdCount;
                                //            ccd.value = str[1];
                                //            ccd.value2 = str[2];
                                //        }
                                //        ClassXMLGET.xmlset<HisDataClass>("HistroyData.xml", HisDataClass.Data);
                                //    }
                                //    catch (Exception)
                                //    {

                                //    }

                                //    if (!FormStart.ccdData.ContainsKey(ccdCount))
                                //    {
                                //        FormStart.ccdData.Add(ccdCount, new CCDDataClass { strPoint = ccdCount, status = "NG", value = str[1], value2 = str[2] });
                                //    }
                                //    else
                                //    {
                                //        FormStart.ccdData[ccdCount] = new CCDDataClass { strPoint = ccdCount, status = "NG", value = str[1], value2 = str[2] };
                                //    }
                                //}
                                Log.alarmLog($"相机1检测NG，视觉返回:[{_Client.msg}]，耗时:[{sw2.Elapsed.Milliseconds}]ms", 1);
                                Log.alarmLog($"测试点胶git3", 1);
                                sw2.Restart();
                                iCCDRunStep = (int)CCDControlFlag.拍照失败;
                                continue;
                            }

                            else if (sw1.ElapsedMilliseconds > 20000)
                            {
                                Log.alarmLog($"相机1检测超时！", 1);
                                sw1.Restart();
                                iCCDRunStep = (int)CCDControlFlag.拍照失败;
                                continue;
                            }
                            //}
                            break;
                        case (int)CCDControlFlag.拍照成功:
                            if (FormStart.pLCTool.WritePLC("相机1检测OK", "True"))
                            {
                                Log.log($"相机1发送检测成功信号，耗时:[{sw2.Elapsed.Milliseconds}]ms");
                                sw2.Restart();
                                iCCDRunStep = (int)CCDControlFlag.等待PLC复位拍照完成信号;
                            }
                            break;
                        case (int)CCDControlFlag.拍照失败:
                            if (FormStart.pLCTool.WritePLC("相机1检测NG", "True"))
                            {
                                Log.alarmLog($"相机1发送检测失败信号，耗时:[{sw2.Elapsed.Milliseconds}]ms", 1);
                                sw2.Restart();
                                iCCDRunStep = (int)CCDControlFlag.等待PLC复位拍照完成信号;
                            }
                            break;
                        case (int)CCDControlFlag.等待PLC复位拍照完成信号:
                            if (FormStart.pLCTool.ReadPLC("相机1触发检测").ToString() == "False")
                            {
                                if (FormStart.pLCTool.WritePLC("相机1检测OK", "False") && FormStart.pLCTool.WritePLC("相机1检测NG", "False"))
                                {
                                    Log.log($"相机1检测完成，耗时:[{sw2.Elapsed.Milliseconds}]ms");
                                    sw2.Restart();
                                    iCCDRunStep = (int)CCDControlFlag.等待开始拍照信号;
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Log.log("工位1检测流程异常,原因:" + ex.Message);
                }
            }
        }

        public override void iniPlc(string str)
        {
            _Client = FormStart.CCDConnect;
            FormStart.pLCTool.WritePLC("相机1检测OK", "False");
            FormStart.pLCTool.WritePLC("相机1检测NG", "False");
            _Client.msg = "";
            iCCDRunStep = (int)CCDControlFlag.等待开始拍照信号;
            sw1.Reset();
            Log.log("初始化CCD1检测流程完成。");
        }

        //public object ReadPLC(string name)
        //{
        //    try
        //    {
        //        Thread.Sleep(1);
        //        var address = FormStart.pLCTool.dicPLC[name].PointAddress;
        //        var type = FormStart.pLCTool.dicPLC[name].PointType;
        //        for (int i = 0; i < 8; i++)
        //        {
        //            if (type.ToUpper() == "INT")
        //            {
        //                var result = FormStart.pLCTool.PLC.ReadInt32(address);
        //                var result1 = FormStart.pLCTool.PLC.ReadInt32(address);
        //                bool valueEqual = result.Content.ToString() == result1.Content.ToString();
        //                if (result.IsSuccess == true && valueEqual)
        //                {
        //                    return result.Content;
        //                }
        //                else
        //                {
        //                    Log.alarmLog($"读取失败地址{address},结果{result.Content},{result1.Content}", 1);
        //                }
        //            }
        //            else if (type.ToUpper() == "BOOL")
        //            {
        //                var result = FormStart.pLCTool.PLC.ReadBool(address);
        //                var result1 = FormStart.pLCTool.PLC.ReadBool(address);
        //                bool valueEqual = result.Content.ToString() == result1.Content.ToString();
        //                if (result.IsSuccess == true && valueEqual)
        //                {
        //                    return result.Content;
        //                }

        //            }
        //            else if (type.ToUpper() == "SHORT")
        //            {
        //                var result = FormStart.pLCTool.PLC.ReadInt16(address);
        //                var result1 = FormStart.pLCTool.PLC.ReadInt16(address);
        //                bool valueEqual = result.Content.ToString() == result1.Content.ToString();
        //                if (result.IsSuccess == true && valueEqual)
        //                {
        //                    return result.Content;
        //                }

        //            }
        //            else if (type.ToUpper() == "DOUBLE")
        //            {
        //                var result = FormStart.pLCTool.PLC.ReadDouble(address);
        //                var result1 = FormStart.pLCTool.PLC.ReadDouble(address);
        //                bool valueEqual = result.Content.ToString() == result1.Content.ToString();
        //                if (result.IsSuccess == true && valueEqual)
        //                {
        //                    return result.Content;
        //                }
        //                else
        //                {
        //                    Log.alarmLog($"读取失败地址{address},结果{result.Content},{result1.Content}", 1);
        //                }
        //            }
        //            else if (type.ToUpper() == "STRING")
        //            {
        //                var result = FormStart.pLCTool.PLC.ReadString(address, 14);
        //                var result1 = FormStart.pLCTool.PLC.ReadString(address, 14);
        //                //bool valueEqual = result.Content.ToString() == result1.Content.ToString();
        //                if (result.IsSuccess == true && result1.IsSuccess == true)
        //                {
        //                    if (result.Content.ToString().Replace("\r", "").Replace("\0", "") == result1.Content.ToString().Replace("\r", "").Replace("\0", ""))
        //                        return result.Content.ToString();
        //                }
        //                else
        //                {
        //                    Log.alarmLog($"读取失败地址{address},结果{result.Content},{result1.Content}", 1);
        //                }
        //            }
        //            else
        //            {
        //                return "ERROR";
        //            }
        //        }
        //        return "ERROR";
        //    }
        //    catch (Exception)
        //    {
        //        return "ERROR";
        //    }
        //}

    }
}