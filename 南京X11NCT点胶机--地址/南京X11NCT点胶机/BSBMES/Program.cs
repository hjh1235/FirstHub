using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Reflection;
using System.Windows.Forms;
using UpperComputer;

namespace UpperComputer
{
    static class Program
    {
        /// <summary>
        ///通讯访问数据
        /// </summary>
        static public SerialPortDate_Save S_PortDate_Save;
        /// <summary>
        /// 存储过站的数据（关键字下发参数一样，通过码获取对应数据）
        /// </summary>
        public static Dictionary<string, Dictionary<string,string>> 模组数据 = new Dictionary<string, Dictionary<string, string>>();
        public static Dictionary<string, Dictionary<string,string>> 模组数据MES = new Dictionary<string, Dictionary<string, string>>();
        public static string GlueTime = "";
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool newMutexCreated = true;
            using (new Mutex(true, Assembly.GetExecutingAssembly().FullName, out newMutexCreated))
            {
                if (!newMutexCreated)
                {
                    MessageBox.Show("程序已启动！请不要启动多个程序！");
                    Environment.Exit(0);
                }
                else
                {
                    LoadParam();//加载参数，通讯等
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new MainForm1());
                }
            }
        }

        private static void LoadParam()
        {
            try
            {
                try
                {
                    Communication_DateLoadData.Load();
                    ///通讯访问数据初始化
                    CommunicationFun.Communication_Init();
                }
                catch (Exception ex)
                {
                    //MainForm.m_FormAlarm.InsertAlarmMessage(ex.Message);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
