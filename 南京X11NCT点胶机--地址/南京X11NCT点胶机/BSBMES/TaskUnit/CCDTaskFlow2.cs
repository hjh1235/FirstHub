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
    public class CCDTaskFlow2 : TaskClass
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
        string strIndex = "";
        string ccdCount = "1";
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
                Thread.Sleep(10);
                try
                {
                    if (TaskClass.plcConnState == false || !FormStart.bInit)
                    {
                        continue;
                    }
                    switch (iCCDRunStep)
                    {
                        case (int)CCDControlFlag.等待开始拍照信号:
                            ccdCount = FormStart.pLCTool.ReadPLC("相机2检测位号").ToString();
                            icount = 0;
                            uint.TryParse(ccdCount, out icount);
                            if (FormStart.pLCTool.ReadPLC("相机2触发检测").ToString() == "True")
                            {
                                sw2.Restart();
                                Data.bUserFlag2 = true;
                                Log.log2($"接受到相机2触发检测，点位:[{ccdCount}]");
                                _Client.msg = "";
                                iCCDRunStep = (int)CCDControlFlag.触发相机拍照;
                            }
                            break;
                        case (int)CCDControlFlag.触发相机拍照:
                            string code = ClassCANS.OLD.模组码;
                            //if (code == "")
                            //{
                            //    code = FormStart.pLCTool.ReadPLC("模组条码").ToString().Replace("\r", "").Replace("\0", "");
                            //    ClassCANS.OLD.模组码 = code;
                            //}
                            string productType = ClassCANS.OLD.当前配方;
                            if (code == "" || code == "ERROR")
                            {
                                Log.alarmLog($"触发相机2检测失败，模组码为空，请检查是否扫码", 1);
                                iCCDRunStep = (int)CCDControlFlag.拍照失败;
                                continue;
                            }
                            string ccdOrder = $"Start,{code},{ccdCount},{productType}";
                            if (_Client.Send(ccdOrder) == false)
                            {
                                Log.alarmLog($"触发相机2检测失败，请检查相机2是否打开、通讯", 2);
                                iCCDRunStep = (int)CCDControlFlag.拍照失败;
                                continue;
                            }
                            Log.log2($"发送相机2检测命令{ccdOrder}，耗时:[{sw2.Elapsed.Milliseconds}]ms");
                            sw2.Restart();
                            iCCDRunStep = (int)CCDControlFlag.等待拍照结果;
                            // }         
                            break;
                        case (int)CCDControlFlag.等待拍照结果:
                            //_Client.msg = "OK#11#33";
                            if (_Client.msg.Contains("OK"))
                            {
                                try
                                {
                                    Log.log($"接受到相机2返回检测OK结果【{_Client.msg}】");
                                    string[] str = _Client.msg.Trim().Split(',');
                                    if (str.Length < 2)
                                    {
                                        Log.alarmLog($"相机2视觉返回结果错误，视觉返回:[{_Client.msg}]", 1);
                                        iCCDRunStep = (int)CCDControlFlag.拍照失败;
                                        return;
                                    }
                                    InitClass.CCDArea2.Add(double.Parse(str[1]));
                                    InitClass.Proportion2.Add(str[2]);
                                    //string[] str = _Client.msg.Split(',');
                                    //if (str.Length >= 2)
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
                                catch (Exception e)
                                {
                                    Log.alarmLog($"相机2视觉返回数据解析失败，视觉返回:[{_Client.msg}]", 2);
                                    iCCDRunStep = (int)CCDControlFlag.拍照失败;
                                    return;

                                }
                                Log.log2($"相机2检测OK，视觉返回:[{_Client.msg}]，耗时:[{sw2.Elapsed.Milliseconds}]ms");
                                sw2.Restart();
                                iCCDRunStep = (int)CCDControlFlag.拍照成功;
                            }
                            else if (_Client.msg.Contains("NG"))
                            {
                                //string[] str = _Client.msg.Split(',');
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
                                Log.alarmLog($"相机2检测NG，视觉返回:[{_Client.msg}]", 2);
                                iCCDRunStep = (int)CCDControlFlag.拍照失败;
                                continue;
                            }
                            else if (sw2.ElapsedMilliseconds > 20000)
                            {
                                Log.alarmLog($"相机2检测超时！", 2);
                                sw2.Restart();
                                iCCDRunStep = (int)CCDControlFlag.拍照失败;
                                continue;
                            }
                            break;
                        case (int)CCDControlFlag.拍照成功:
                            if (FormStart.pLCTool.WritePLC("相机2检测OK", "True"))
                            {
                                Log.log2($"相机2发送检测成功信号，耗时:[{sw2.Elapsed.Milliseconds}]ms");
                                iCCDRunStep = (int)CCDControlFlag.等待PLC复位拍照完成信号;
                            }
                            break;
                        case (int)CCDControlFlag.拍照失败:
                            if (FormStart.pLCTool.WritePLC("相机2检测NG", "True"))
                            {
                                Log.alarmLog($"相机2发送检测失败信号.", 2);
                                iCCDRunStep = (int)CCDControlFlag.等待PLC复位拍照完成信号;
                            }
                            break;
                        case (int)CCDControlFlag.等待PLC复位拍照完成信号:
                            if (FormStart.pLCTool.ReadPLC("相机2触发检测").ToString() == "False")
                            {
                                if (FormStart.pLCTool.WritePLC("相机2检测OK", "False") && FormStart.pLCTool.WritePLC("相机2检测NG", "False"))
                                {
                                    Log.log2($"相机2检测完成.");
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
                    Log.alarmLog("工位2检测流程异常,原因:" + ex.Message, 2);
                    Thread.Sleep(2000);
                }
            }
        }

        public override void iniPlc(string str)
        {
            _Client = FormStart.CCDConnect1;
            FormStart.pLCTool.WritePLC("相机2检测OK", "False");
            FormStart.pLCTool.WritePLC("相机2检测NG", "False");
            _Client.msg = "";
            iCCDRunStep = (int)CCDControlFlag.等待开始拍照信号;
            sw2.Reset();
            Log.log("初始化CCD2检测流程完成。");
        }
    }
}