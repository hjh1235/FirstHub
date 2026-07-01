//#define IsVisual
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BADB;
using EquipBase;
using Equipment;
using StationBLL;
using BLL;
using Newtonsoft.Json;
using MES;
using System.Threading;
using System.Windows;
using System.Windows.Forms;

namespace UpperComputer
{
    public class FingerPermission 
    {
  
        public static string s_Message = "", str_UserName = "", str_UserLevel = "";
        public static bool bSendCCD = false;
        public void Permission()
        {
            //等待进入主界面
            while (FormStart.openfinger == 1)
            {
                Thread.Sleep(50);
            }
            string Msg = "";
            ShowErrMsg1("等待指纹仪打开",true);
            while (Msg != "指纹仪打开成功!")
            {
                FingerReader.OpenDevice(0, out Msg);
                Thread.Sleep(50);
            }
            ShowErrMsg1("指纹仪打开成功", true);
            while (true)
            {
                Thread.Sleep(50);
                try
                {
                    if (!isStaionCheck)//主界面的循环
                    {
                        s_Message = "";
                        str_UserName = "";
                        str_UserLevel = "";
                        if (FingerReader.UserIndentify(out str_UserName, out str_UserLevel, out s_Message))
                        {
                            //MessageBox.Show("指纹仪打开失败！");

                            ShowErrMsg1($"指纹信息验证成功,当前用户为:{str_UserName},职位为:{str_UserLevel}", true);
                            Global.UserPower = str_UserLevel;
                            IsCheckFG = true;
                        }
                        mLogin = new Login();
                        if (s_Message != "没有识别到")
                        {
                            bSendCCD = false;
                            if (str_UserLevel == "管理员")//指纹-工艺
                            {
                                IsCheckFG = true;
                                Log.log(str_UserLevel + str_UserName + "进行指纹登录！");
                                FormUserLogoin.userLevel = 3;
                                FormStart.pLCTool.WritePLC("权限",3);
                                FormUserLogoin.userID = str_UserName;
                                FormUserLogoin.userpermissions = "管理员";
                                FormStart.CCDConnect.ClearData();
                                FormStart.CCDConnect.Send("userLevel,3");
                                //if (Parames.CcdCheck.gIsChecked)
                                //{
                                //    string Powerccd = Station.ccdPort.SendRecieve("3", "\r\n", "\r\n", 0).Trim(new char[] { '\r', '\n', ' ', '\0' });
                                //    Log.log($"下发CCD权限等级:3,返回结果{Powerccd}");
                                //}
                            }
                            else if (str_UserLevel == "工程师")//指纹-品质工程师
                            {
                                IsCheckFG = true;
                                FormUserLogoin.userLevel = 2;
                                FormStart.pLCTool.WritePLC("权限", 2);
                                FormUserLogoin.userID = str_UserName;
                                FormUserLogoin.userpermissions = "工程师";
                                FormStart.CCDConnect.ClearData();
                                FormStart.CCDConnect.Send("userLevel,2");
                                //if (Parames.CcdCheck.gIsChecked)
                                //{
                                //    string Powerccd = Station.ccdPort.SendRecieve("3", "\r\n", "\r\n", 0).Trim(new char[] { '\r', '\n', ' ', '\0' });
                                //    Log.log($"下发CCD权限等级:3,返回结果{Powerccd}");
                                //}
                            }
                            else if (str_UserLevel == "操作员")//指纹-品质工程师
                            {
                                IsCheckFG = true;
                                FormUserLogoin.userLevel = 1;
                                FormStart.pLCTool.WritePLC("权限", 1);
                                FormUserLogoin.userID = str_UserName;
                                FormUserLogoin.userpermissions = "操作员";
                                //if (Parames.CcdCheck.gIsChecked)
                                //{
                                //    string Powerccd = Station.ccdPort.SendRecieve("3", "\r\n", "\r\n", 0).Trim(new char[] { '\r', '\n', ' ', '\0' });
                                //    Log.log($"下发CCD权限等级:3,返回结果{Powerccd}");
                                //}
                            }
                            else//操作员
                            {
                                IsCheckFG = true;
                                ShowErrMsg1("指纹信息验证失败，可能当前指纹未录入，请重试！", false);
                                //if (Parames.CcdCheck.gIsChecked)
                                //{
                                //    string Powerccd = Station.ccdPort.SendRecieve("1", "\r\n", "\r\n", 0).Trim(new char[] { '\r', '\n', ' ', '\0' });
                                //    AddLog($"下发CCD权限等级:3,返回结果{Powerccd}");
                                //}
                            }
                            string msg = $"{FormUserLogoin.userLevel}";
                            if (bSendCCD == false)
                            {
                                FormStart.CCDConnect.ClearData();
                                if (FormStart.CCDConnect.Send(msg))
                                {
                                    bSendCCD = true;
                                }
                            }
                            int time = 180;
                            Task.Run(() =>
                            {
                                while (time > 0)
                                {
                                    time--;
                                    Thread.Sleep(1000);
                                }
                                IsCheckFG = false;
                                FormUserLogoin.userLevel = 0;
                                FormStart.pLCTool.WritePLC("权限", 0);
                                FormUserLogoin.userID = "未登录";
                                FormUserLogoin.userpermissions = "未登录";
                            });
                        }
                    }
                    else
                    {

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("指纹仪异常！");
                }

            }
        }


        private UserInformationParameter bb = new UserInformationParameter();
        public static List<Dictionary<string, string>> MQGrouplist = new List<Dictionary<string, string>>();
        public static List<Dictionary<string, string>> MPQGrouplist = new List<Dictionary<string, string>>();
        public static List<Dictionary<string, string>> ADMINGrouplist = new List<Dictionary<string, string>>();
        public static Dictionary<string, string> Users = new Dictionary<string, string>();
        public static bool IsCheckFG = false; // plc刷卡权限
        public static Login mLogin = null;
        public static bool isStaionCheck = false;
        //public override string ScanObj(string obj)
        //{
        //    try
        //    {
        //        Collect();
        //        Calculate();
        //        UpdateMes();
        //        ShowStation();
        //        return ReturnStr();
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.alarmLog($"{ex.ToString()}");
        //        throw;
        //    }
        //}
        bool plcrst = false;
        void Collect()
        {

        }
        void Calculate()
        {

        }
        void UpdateMes()
        {

        }
        string ReturnStr()
        {
            return plcrst ? "OK" : "NG";
        }
        void ShowErrMsg(bool brest, string errMsg)
        {
           /* SetTestResult(false, errMsg);*///None类中，该函数不会设置eqFields.TestResult.Value，只会报错和记录日志。
            plcrst = brest;
            //cStation.cView.SetItem(cStation.gStationNo, "GridErr", "HErrMsg", errMsg);
            // cStation.AddErrMsgs(errMsg);//在添加入错误列表下拉菜单
        }

        public void ShowErrMsg1(string errMsg,bool state)//false一般消息 true报警消息
        {
            Log.alarmLog(errMsg, 1);
        }
    }
}