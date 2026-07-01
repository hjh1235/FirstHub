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
    public class AGVTaskFlow
    {

        public TCP_IP_Connect _AGVServer;
        string msg = "";
        string Name = "";
        int station = 1;
        public AGVTaskFlow(string name,int Station, TCP_IP_Connect agvServer)
        {
            Name = name;
            _AGVServer = agvServer;
            _AGVServer.msg = "";
            station = Station;
        }

        public enum AGVFlag
        {
            请求进入 = 1,
            产品状态 = 2,
            允许进入 = 3,
            正在进入 = 4,
            小车到位 = 5,
            加工结果 = 6,
            允许离开 = 7,
            正在离开 = 8,
            离开完成 = 9,
            工位异常 = 10,
            夹料完成=11,
            放料完成=12,
        }
        /// <summary>
        /// AGV交互流程
        /// </summary>
        public void TaskRun()
        {
            while (true)
            {
                Thread.Sleep(5);
                try
                {
                    if (TaskClass.plcConnState == false)
                    {
                        continue;
                    }
                    if (_AGVServer.msg != "")
                    {
                        msg = _AGVServer.msg;
                        _AGVServer.msg = "";
                        log($"[{Name}]接收到AGV信号:[{msg}]，进入流程");
                        string[] str = msg.Split(',');
                        switch (str[0])
                        {
                            //读取
                            case "0":
                                ReadPLC(msg);
                                //_AGVServer.msg = "";
                                break;
                            //写入
                            case "1":
                                WritePLC(msg);
                                //_AGVServer.msg = "";
                                break;
                            default:
                                log($"[{Name}]接收到AGV未知指令:[{msg}]");
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    log("[{Name}]AGV流程异常,原因:" + ex.Message);
                    Thread.Sleep(1000);
                    _AGVServer.msg = "";
                }
            }
        }
        public bool ReadPLC(string msg)
        {
            string[] str = msg.Split(',');
            int type = int.Parse(str[1]);
            string name = Enum.GetName(typeof(AGVFlag), type);
            if (name == null)
                return false;
            string result1 = FormStart.pLCTool.ReadPLC(name).ToString().ToLower();
            Thread.Sleep(200);
            string result2 = FormStart.pLCTool.ReadPLC(name).ToString().ToLower();
            if (result1 != result2)
            {
                log($"#[{result1}][{result2}]");
                return false;
            }
            log($"[{Name}]读取PLC信号:[{name}]值:[{result1}]");
            if (result1 != "error" && result1 != "999")
            {
                string strRt = $"OK,{msg},{result1}";
                if (_AGVServer.ServerSendMsg(strRt))
                {
                    return true;
                }
                return false;
            }
            return false;

        }
        public bool WritePLC(string msg)
        {
            string[] str = msg.Split(',');
            int type = int.Parse(str[1]);
            string value = str[2];
            string name = Enum.GetName(typeof(AGVFlag), type);
            bool result = FormStart.pLCTool.WritePLC(name, value);
            if (name == null)
                return false;
            if (result)
            {
                log($"[{Name}]写入PLC信号成功，名称:[{name}]值:[{value}]");
                string strRt = $"OK,{msg}";
                if (_AGVServer.ServerSendMsg(strRt))
                {
                    return true;
                }
                return false;
            }
            else
            {
                log($"[{Name}]写入PLC信号失败，名称:[{name}]值:[{value}]");
                string strRt = $"NG,{msg}";
                if (_AGVServer.ServerSendMsg(strRt))
                {
                    return true;
                }
                return false;
            }

        }
        public void log(string str)
        {
            if (station == 1)
            {
                Log.logAGV(str);
            }
            else
            {
                Log.logAGVRes(str);
            }
        }
    }
}