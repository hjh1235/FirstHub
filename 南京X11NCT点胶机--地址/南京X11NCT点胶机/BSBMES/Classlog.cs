using Sunny.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UpperComputer
{
    public class Log
    {
        static string strlog1 = @"D:\生产记录\Log\工位1\";
        static string strlog2 = @"D:\生产记录\Log\工位2\";
        static string strMESlog = @"D:\生产记录\MESLog\";
        static string strErrorlog = @"D:\生产记录\ERRORLog\";
        static string strAGVlog = @"D:\生产记录\AGVLog\信号交互日志\";
        static string strAGVlogRes = @"D:\生产记录\AGVLog\信号刷新日志\";
        public static string strProPath= @"D:\生产记录";
        static UIRichTextBox systemLogBoxMes = new UIRichTextBox();
        static UIRichTextBox systemLogBox = new UIRichTextBox();
        static UIRichTextBox alarmLogBox = new UIRichTextBox();
        static UIRichTextBox AGVLogBox = new UIRichTextBox();
        static UIRichTextBox AGVLogBoxRes = new UIRichTextBox();
        public static List<String> LOG = new List<String>();
        private static Object lockS = new object();
        public static void log(string str)
        {
            var list = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff") + "--->:" + str;
            ADD(list);
            Task.Factory.StartNew(() => {
                lock (lockS)
                {
                    CERR(strlog1, strlog1 + DateTime.Now.ToString("yyyy-MM-dd") + ".log", null, list);
                }
            });
        }
        public static void log2(string str)
        {
            var list = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff") + "->:" + str;
            addLog2(list);
            Task.Factory.StartNew(() => {
                lock (lockS)
                {
                    CERR(strlog2, strlog2 + DateTime.Now.ToString("yy-MM-dd") + ".log", null, list);
                }
            });
        }
        public static void logAGV(string str)
        {
            var list = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff") + "->:" + str;
            addAGV(list);
            Task.Factory.StartNew(() => {
                lock (lockS)
                {
                    CERR(strAGVlog, strAGVlog + DateTime.Now.ToString("yy-MM-dd") + ".log", null, list);
                }
            });
        }
        public static void logAGVRes(string str)
        {
            var list = DateTime.Now.ToString("yy-MM-dd HH:mm:ss fff") + "->:" + str;
            addAGVLogRes(list);
            Task.Factory.StartNew(() =>
            {
                lock (lockS)
                {
                    CERR(strAGVlogRes, strAGVlogRes + "\\" + DateTime.Now.ToString("yy-MM-dd") + ".log", null, list);
                }
            });
        }
        public static void logMES(string str, int index)
        {
            var list = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff") + "->:" + str;
            if (index == 1)
                ADD(list);
            else if (index == 2)
                addLog2(list);
            Task.Factory.StartNew(() => {
                lock (lockS)
                {
                    CERR(strMESlog, strMESlog + DateTime.Now.ToString("yy-MM-dd") + ".log", null, list);
                }
            });
        }
        public static void alarmLog(string str, int index,bool bWrite=true)
        {
            var list = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff") + "->:" + str;
            if (bWrite)
            {
                addAlarmLog(list);
                if (index == 1)
                {
                    log(list);
                }
                else if (index == 2)
                {
                    log2(list);
                }                
            }
            Task.Factory.StartNew(() => {
                lock (lockS)
                {
                    CERR(strErrorlog,strErrorlog + DateTime.Now.ToString("yy-MM-dd") + ".log", null, list);
                }
            });
        }

        public static void initLog(UIRichTextBox Box)
        {
            systemLogBox = Box;
            log("程序启动.........");
        }
        public static void initLogMes(UIRichTextBox Box)
        {
            systemLogBoxMes = Box;
            log2("程序启动.........");
        }
        public static void initLogAlaram(UIRichTextBox Box)
        {
            alarmLogBox = Box;
            alarmLog("程序启动.........",1);
        }
        public static void initLogAGV(UIRichTextBox Box)
        {
            AGVLogBox = Box;
            logAGV("程序启动.........");
        }
        public static void initLogAGVRes(UIRichTextBox Box)
        {
            AGVLogBoxRes = Box;
            logAGVRes("程序启动.........");
        }
        static void ADD(String STR)
        {
            try
            {
                systemLogBox.Invoke(new Action(() =>
                {
                    systemLogBox.Invoke(new Action(() =>
                    {
                        if (systemLogBox.Lines.Length > 1000)
                            systemLogBox.Clear();
                        systemLogBox.AppendText(STR + "\r\n");
                        systemLogBox.ScrollToCaret();
                    }));
                }));
            }
            catch (Exception)
            {}      
        }

        static void addAlarmLog(String STR)
        {
            try
            {
                alarmLogBox.Invoke(new Action(() =>
                {
                    alarmLogBox.Invoke(new Action(() =>
                    {
                        if (alarmLogBox.Lines.Length > 1000)
                            alarmLogBox.Lines[0].Replace(" ", " ");
                        alarmLogBox.AppendText(STR + "\r\n");
                        alarmLogBox.ScrollToCaret();
                    }));
                }));
            }
            catch (Exception)
            { }
        }
        static void addLog2(String STR)
        {
            try
            {
                systemLogBoxMes.Invoke(new Action(() =>
                {
                    systemLogBoxMes.Invoke(new Action(() =>
                    {
                        if (systemLogBoxMes.Lines.Length > 1000)
                            systemLogBoxMes.Clear();
                        //systemLogBoxMes.Lines[systemLogBoxMes.Lines.Length - 1].Replace("", "");
                        systemLogBoxMes.AppendText(STR + "\r\n");
                        systemLogBoxMes.ScrollToCaret();
                    }));
                }));
            }
            catch (Exception)
            { }
        }
        static void addAGV(String STR)
        {
            try
            {
                AGVLogBox.Invoke(new Action(() =>
                {
                    if (AGVLogBox.Lines.Length > 1000)
                        AGVLogBox.Clear();
                    //systemLogBoxMes.Lines[systemLogBoxMes.Lines.Length - 1].Replace("", "");
                    AGVLogBox.AppendText(STR + "\r\n");
                    AGVLogBox.ScrollToCaret();
                }));
            }
            catch (Exception)
            { }
        }
        static void addAGVLogRes(String STR)
        {
            try
            {
                AGVLogBoxRes.Invoke(new Action(() =>
                {
                    if (AGVLogBoxRes.Lines.Length > 1000)
                        AGVLogBoxRes.Clear();
                    //systemLogBox.Lines[systemLogBox.Lines.Length - 1].Replace("", "");
                    AGVLogBoxRes.AppendText(STR + "\r\n");
                    AGVLogBoxRes.ScrollToCaret();
                }));
            }
            catch (Exception)
            { }
        }
        public static void CERR(string PATH1, string PATH2, string NAME, string VALUE)
        {
            //导出文件路径
            if (!Directory.Exists(PATH1))
            {
                Directory.CreateDirectory(PATH1);
            }
            if (!File.Exists(PATH2))
            {
                VALUE = NAME + "\r\n" + VALUE;
            }
            try
            {
                using (StreamWriter JKs = new StreamWriter(PATH2, true, System.Text.Encoding.Default))
                {
                    JKs.WriteLine(VALUE);
                    JKs.Close();
                };
            }
            catch (Exception ex)
            {
                var list = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff") + "--->:" + ex.ToString();
                ADD(list);
            }
        }

    }
}
