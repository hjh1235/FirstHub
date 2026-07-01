using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UpperComputer
{
    public class UserPerClass
    {
        public static bool bUser;
        public static string userID = "";
        public static int PLCLevel;

        public static string userpermissions = "未登录";
        public static int userLevel = 0;

        static bool bCCD = false;
        static bool bSendCCD = false;
        static bool bUpdateCCD = false;

        static Stopwatch time = new Stopwatch();
        public void TestPermission()
        {
            while (true)
            {
                Thread.Sleep(10);
                try
                {
                    string strPer = "";
                    string strid = "";
                    string strPer1 = FormStart.pLCTool.ReadPLC("权限").ToString();
                    //string strid1 = FormStart.pLCTool.ReadPLC("账号").ToString();
                    Thread.Sleep(200);
                    string strPer2 = FormStart.pLCTool.ReadPLC("权限").ToString();
                    //string strid2 = FormStart.pLCTool.ReadPLC("账号").ToString();
                    //if (!(strPer1 == strPer2 && strid1 == strid2))
                    if (!(strPer1 == strPer2 ))
                    {
                        continue;
                    }
                    //strid = strid1;
                    strPer = strPer1;
                    //if (strid == "" || strid.ToUpper().Contains("ERROR") || strPer.ToUpper().Contains("ERROR"))
                    if ( strPer.ToUpper().Contains("ERROR"))
                        continue;

                    int i = 0;
                    if (int.TryParse(strPer, out i))
                    {
                        //if (i != PLCLevel || strid != userID)
                        if (i != 0 )
                        {
                            PLCLevel = i;
                            userID = strid;
                            userLevel = 0;
                            //bUser = true;
                            userpermissions = "未登录";
                            if (i == 1)
                            {
                                userLevel = 1;
                                userpermissions = "操作员";
                            }
                            else if (i == 3)
                            {
                                userLevel = 2;
                                userpermissions = "工程师";
                            }
                            else if (i == 7)
                            {
                                userLevel = 3;
                                userpermissions = "管理员";
                            }
                            FormUserLogoin.userID = userID;
                            FormUserLogoin.userpermissions = userpermissions;
                            FormUserLogoin.userLevel = userLevel;
                            bUpdateCCD = true;
                            bSendCCD = false;
                            FormUserLogoin.bPermission = true;
                            FormStart.pLCTool.WritePLC("权限", 0);
                            Log.log($"当前刷卡权限:账号:[{userID}],权限:[{userpermissions}]");
                        }
                    }
                    if (FormStart.CCDPERConnectB.ConnStr != "已连接服务器")
                    {
                        continue;
                    }
                    if (!bUpdateCCD)
                        continue;
                    string msg = $"{userLevel},{userID}";
                    if (bSendCCD == false)
                    {
                        FormStart.CCDPERConnectB.ClearData();
                        if (FormStart.CCDPERConnectB.Send(msg))
                        {
                            bSendCCD = true;
                            time.Restart();
                        }
                    }
                    if (FormStart.CCDPERConnectB.msg.Contains("OK"))
                    {
                        FormStart.CCDPERConnectB.ClearData();
                        bUpdateCCD = false;
                        time.Stop();
                        Log.log($"当前刷卡权限:账号:[{userID}],权限:[{userpermissions}]发送:[{msg}]CCD成功");
                    }
                    else if (FormStart.CCDPERConnectB.msg.Contains("NG"))
                    {
                        FormStart.CCDPERConnectB.ClearData();
                        bUpdateCCD = false;
                        time.Stop();
                        Log.log($"当前刷卡权限:账号:[{userID}],权限:[{userpermissions}]发送:[{msg}]CCDNG");
                    }
                    else if (time.Elapsed.TotalSeconds > 3)
                    {
                        FormStart.CCDPERConnectB.ClearData();
                        bSendCCD = false;
                        bUpdateCCD = false;
                        time.Stop();
                        Log.log($"当前刷卡权限:账号:[{userID}],权限:[{userpermissions}]发送:[{msg}]CCD，接收CCD返回OK超时");
                    }
                }
                catch (Exception)
                {

                }

            }
        }
    }
    public static class Data
    {
        public static double 工位1点胶量;
        public static double 工位1点胶时间;
        public static double 工位2点胶量;
        public static double 工位2点胶时间;
        public static string 胶水保质期时间;
        public static bool bUserFlag1 = false;
        public static bool bUserFlag2 = false;
    }
    public class MonitorTask
    {
        public void TaskRun()
        {
            while (true)
            {
                Thread.Sleep(2000);
                try
                {
                    string strGlueValue1 = 0.75.ToString();
                    string strGlueTime1 = FormStart.pLCTool.ReadPLC("工位1点胶时间").ToString();
                    string strGlueValue2 = 0.75.ToString();
                    string strGlueTime2 = FormStart.pLCTool.ReadPLC("工位2点胶时间").ToString();
                    string strGlueTime3 = Program.GlueTime;
                    double dGlueValue1 = 0, dGlueTime1 = 0, dGlueValue2 = 0, dGlueTime2 = 0 , dGlueTime3=0;
                    if (strGlueValue1.ToUpper() != "ERROR" && strGlueValue1 != "")
                    {
                        if (double.TryParse(strGlueValue1, out dGlueValue1))
                        {
                            Data.工位1点胶量 = dGlueValue1;
                        }
                    }
                    if (strGlueTime1.ToUpper() != "ERROR" && strGlueTime1 != "")
                    {
                        if (double.TryParse(strGlueTime1, out dGlueTime1))
                        {
                            Data.工位1点胶时间 = dGlueTime1*10;
                        }
                    }
                    if (strGlueValue2.ToUpper() != "ERROR" && strGlueValue2 != "")
                    {
                        if (double.TryParse(strGlueValue2, out dGlueValue2))
                        {
                            Data.工位2点胶量 = dGlueValue2;
                        }
                    }
                    if (strGlueTime2.ToUpper() != "ERROR" && strGlueTime2 != "")
                    {
                        if (double.TryParse(strGlueTime2, out dGlueTime2))
                        {
                            Data.工位2点胶时间 = dGlueTime2*10;
                        }
                    }
                    if ( strGlueTime3 != "")
                    {
                        //if (double.TryParse(strGlueTime3, out dGlueTime3))
                        //{
                        Data.胶水保质期时间 = strGlueTime3;
                        //}
                    }
                }
                catch (Exception)
                {

                }
            }
        }
    }
    public enum TxType
    {
        客户端,
        服务端,
        串口,
        PLC,
        MES,
        初始化,
        PLC心跳,
        激光图档,
    }
    public class TxClass
    {
        public string Name { get; set; }
        public TxType txType { get; set; }

        public object obj { get; set; }

        public bool bStatus { get; set; }

        public int nImageIndex { get; set; }

        public string strTxt { get; set; }

        public bool bShow { get; set; }
    }
    public class CCDDataClass
    {
        public string strPoint { get; set; }
        public string status { get; set; }

        public string value { get; set; }
        public string value2 { get; set; }
    }

}
