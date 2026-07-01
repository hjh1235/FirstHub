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
    public class DealWithDataFlow1 : TaskClass
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
        /// <summary>
        /// X补偿
        /// </summary>
        public double i_X = 0;
        /// <summary>
        /// Y补偿
        /// </summary>
        public double i_Y = 0;
        string strIndex = "";
        string ccdCount = "";
        uint icount = 0;
        public enum CCDControlFlag
        {
            等待开始拍照信号,
            触发相机拍照,
            等待拍照结果,
            拍照成功,
            拍照失败,
            发送偏移量给PLC,
            等待PLC复位拍照完成信号
        }

        /// <summary>
        /// CCD相机拍照校正流程
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
                            ccdCount = FormStart.pLCTool.ReadPLC("相机1检测位号").ToString();
                            icount = 0;
                            uint.TryParse(ccdCount, out icount);
                            if (FormStart.pLCTool.ReadPLC("相机1触发拍照").ToString() == "True")
                            {
                                Log.log($"接受到相机1触发拍照，点位:[{ccdCount}]");
                                _Client.msg = "";
                                sw2.Restart();
                                iCCDRunStep = (int)CCDControlFlag.触发相机拍照;
                            }
                            break;
                        case (int)CCDControlFlag.触发相机拍照:
                            string code = ClassCANS.OLD.模组码;
                            //if (code == "")
                            //{
                            //    //code = FormStart.pLCTool.ReadPLC("模组条码").ToString().Replace("\r", "").Replace("\0", "");
                            //    ClassCANS.OLD.模组码 = code;
                            //}
                            string productType = ClassCANS.OLD.当前配方;
                            if (code == "" || code == "ERROR")
                            {
                                Log.alarmLog($"触发相机1拍照失败，模组码为空，请检查是否扫码", 1);
                                iCCDRunStep = (int)CCDControlFlag.拍照失败;
                                continue;
                            }
                            string ccdOrder = $"Trigger,{code},{ccdCount},{productType}";
                            if (_Client.Send(ccdOrder) == false)
                            {
                                Log.alarmLog($"触发相机1拍照失败，请检查相机1是否打开、通讯",1);
                                iCCDRunStep = (int)CCDControlFlag.拍照失败;
                                continue;
                            }
                            sw1.Restart();
                            Log.log($"发送相机1拍照命令{ccdOrder}，耗时:[{sw2.Elapsed.Milliseconds}]ms");
                            sw2.Restart();
                            iCCDRunStep = (int)CCDControlFlag.等待拍照结果;
                            // }         
                            break;
                        case (int)CCDControlFlag.等待拍照结果:
                            if (_Client.msg.Contains("OK"))
                            {
                                Log.log($"相机1拍照OK！{_Client.msg}，耗时:[{sw2.Elapsed.Milliseconds}]ms");
                                sw2.Restart();
                                i_X = Convert.ToDouble(_Client.msg.Split(',')[1]);//X补偿
                                i_X = Math.Round(i_X, 3);
                                i_Y = Convert.ToDouble(_Client.msg.Split(',')[2]);//Y补偿
                                i_Y = Math.Round(i_Y, 3);
                                iCCDRunStep = (int)CCDControlFlag.发送偏移量给PLC;
                            }
                            else if (_Client.msg.Contains("NG"))
                            {
                                Log.alarmLog($"相机1拍照NG，耗时:[{sw2.Elapsed.Milliseconds}]ms",1);
                                sw2.Restart();
                                iCCDRunStep = (int)CCDControlFlag.拍照失败;
                                continue;
                            }
                            else if (sw1.ElapsedMilliseconds > 20000)
                            {
                                Log.alarmLog($"相机1拍照超时！",1);
                                sw1.Restart();
                                iCCDRunStep = (int)CCDControlFlag.拍照失败;
                                continue;
                            }
                            break;
                        case (int)CCDControlFlag.发送偏移量给PLC:
                            
                            if (FormStart.pLCTool.WritePLC("相机1_X轴偏移数据", i_X) && FormStart.pLCTool.WritePLC("相机1_Y轴偏移数据", i_Y))
                            {
                                Log.log($"发送工位1偏移X{i_X}，偏移Y{i_Y}，耗时:[{sw2.Elapsed.Milliseconds}]ms");
                                sw2.Restart();
                                iCCDRunStep = (int)CCDControlFlag.拍照成功;
                            }
                            break;
                        case (int)CCDControlFlag.拍照成功:
                            if (FormStart.pLCTool.WritePLC("相机1拍照OK", "True"))
                            {
                                Log.log($"相机1发送拍照成功信号，耗时:[{sw2.Elapsed.Milliseconds}]ms");
                                sw2.Restart();
                                iCCDRunStep = (int)CCDControlFlag.等待PLC复位拍照完成信号;
                            }
                            break;
                        case (int)CCDControlFlag.拍照失败:
                            if (FormStart.pLCTool.WritePLC("相机1拍照NG", "True"))
                            {
                                Log.alarmLog($"相机1发送拍照失败信号，耗时:[{sw2.Elapsed.Milliseconds}]ms",1);
                                sw2.Restart();
                                iCCDRunStep = (int)CCDControlFlag.等待PLC复位拍照完成信号;
                            }
                            break;
                        case (int)CCDControlFlag.等待PLC复位拍照完成信号:
                            if (FormStart.pLCTool.ReadPLC("相机1触发拍照").ToString() == "False")
                            {
                                if (FormStart.pLCTool.WritePLC("相机1拍照OK", "False") && FormStart.pLCTool.WritePLC("相机1拍照NG", "False"))
                                {
                                    Log.log($"相机1拍照完成，耗时:[{sw2.Elapsed.Milliseconds}]ms");
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
                    Log.alarmLog("工位1拍照校正流程异常,原因:" + ex.Message,1);
                    _Client.ClearData();
                    iCCDRunStep = (int)CCDControlFlag.等待PLC复位拍照完成信号;
                }
            }
        }
       
        public override void iniPlc(string str)
        {
            i_X = 0;
            i_Y = 0;
            _Client = FormStart.CCDConnect;
            FormStart.pLCTool.WritePLC("相机1_X轴偏移数据", 0);
            FormStart.pLCTool.WritePLC("相机1_Y轴偏移数据", 0);
            FormStart.pLCTool.WritePLC("相机1拍照OK", "False");
            FormStart.pLCTool.WritePLC("相机1拍照NG", "False");
            _Client.msg = "";
            iCCDRunStep = (int)CCDControlFlag.等待开始拍照信号;
            sw1.Reset();
            Log.log("初始化CCD1校正流程完成。");
        }
    }
}