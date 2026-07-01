using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Threading;
using HslCommunication.Profinet.Siemens;
using System.Diagnostics;
using System.IO;
using UpperComputer.TaskUnit;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO.Ports;
using HslCommunication.Core;
using HslCommunication.Profinet.Omron;
using System.Reflection;
using Newtonsoft.Json;

namespace UpperComputer
{
    public partial class FormStart : Form
    {
        public FormStart()
        {
            InitializeComponent();
            formStart = this;
        }
        #region
        public static _Client ScanConnect = new _Client();
        public static _Client CCDConnect = new _Client();
        public static _Client CCDConnect1 = new _Client();
         public static _Client CCDPERConnectB = new _Client();
        public static _Client PLCConnect = new _Client();
        public static TCP_IP_Connect _AGVServer = new TCP_IP_Connect();
        public static TCP_IP_Connect _AGVServerRes= new TCP_IP_Connect();
        public AGVTaskFlow aGVTaskFlow = null;
        public AGVTaskFlow aGVTaskFlowRes = null;
        public MonitorTask monitorTask = new MonitorTask();
        UserPerClass user = new UserPerClass();
        public static Dictionary<string, PLCinfomartion> dicPLC = new Dictionary<string, PLCinfomartion>();
        public static Dictionary<string, CCDDataClass> ccdData = new Dictionary<string, CCDDataClass>();
        public static bool 在线模式 = false;
        public static FormStart formStart;
        public static PLCTool pLCTool;
        public static int openfinger;
        FingerPermission finger = new FingerPermission();

        /// <summary>
        /// 状态
        /// </summary>
        public static string strSta = "";
        public static HslCommunication.Profinet.Omron.OmronFinsUdp PLC;

        /// <summary>
        /// 工位0条码
        /// </summary>
        public static string Station0SN = "";
        /// <summary>
        /// 初始化标志
        /// </summary>
        public static bool bInit = false;
        #endregion

        private void Form2_Load(object sender, EventArgs e)
        {
            HisDataClass.Data = ClassXMLGET.xmlget<HisDataClass>("HistroyData.xml");
            //初始化PLC型号
            pLCTool = new PLCToolOmron_UDP();
            pLCTool.intance();
            pLCTool.dicPLC = ClassCANS.OLD.PLClist.ToDictionary(key => key.PointName, value => value); //加载数据
            foreach (var item in pLCTool.dicPLC)
            {
                item.Value.PointResult = "ERROR";
            }
            InitCon();
            //PLC

            //Task.Factory.StartNew(() => { aGVTaskFlow.TaskRun(); });//AGV流程
            Task.Factory.StartNew(() => { pLCTool.ResPLCData(); });//PLC实时监控
            Task.Factory.StartNew(() => { pLCTool.ResPLCData2(); });//PLC实时监控
            //Task.Factory.StartNew(() => { finger.Permission(); });
            //Task.Factory.StartNew(() => { monitorTask.TaskRun(); });//点胶参数流程
            //Task.Factory.StartNew(() => { UserPerClass.TestPermission(); });//权限流程

            ThreadPool.QueueUserWorkItem((x) => { heartBeatSignal(); });
            ThreadPool.QueueUserWorkItem((x) => { getplc(); });
            ThreadPool.QueueUserWorkItem((x) => { plcInit(); });
            ThreadPool.QueueUserWorkItem((x) => { aGVTaskFlow.TaskRun(); });
            ThreadPool.QueueUserWorkItem((x) => { aGVTaskFlowRes.TaskRun(); });
            //ThreadPool.QueueUserWorkItem((x) => { pLCTool.ResPLCData(); });
            ThreadPool.QueueUserWorkItem((x) => { monitorTask.TaskRun(); });
            ThreadPool.QueueUserWorkItem((x) => { user.TestPermission(); });

            //整体业务流程

            MesTaskFlow mesTaskFlow = new MesTaskFlow();//mes流程
            ScanTask scanTask = new ScanTask();//扫码流程
            DealWithDataFlow1 dealWithDataFlow1 = new DealWithDataFlow1();//相机1校正流程
            DealWithDataFlow1First dealWithDataFlow1First = new DealWithDataFlow1First();//相机1防错样件校正流程
            DealWithDataFlow2 dealWithDataFlow2 = new DealWithDataFlow2();//相机2校正流程
            DealWithDataFlow2First dealWithDataFlow2First = new DealWithDataFlow2First();//相机2防错样件校正流程
            CCDTaskFlow1 ccdTaskFlow1 = new CCDTaskFlow1();//相机1检测流程
            CCDTaskFlow2 ccdTaskFlow2 = new CCDTaskFlow2();//相机2检测流程
            CCDCheckTaskFlow ccdCheckTaskFlow = new CCDCheckTaskFlow();//相机点检流程
            FirstCheckCCDTaskFlow firstCheckCCDTaskFlow = new FirstCheckCCDTaskFlow();//防错样件点检流程

            MesTaskFlow.CallBack += MesTaskFlow_CallBack;

            FirstCheckCCDTaskFlow.CallBack_1 += FirstCheckCCDTaskFlow_CallBack_1;

            foreach (var item in TaskClass.taskThreadList)
            {
                item.action?.BeginInvoke(item.GetType().ToString(), callback =>
                {

                }, null);
                item.start();
            }
        }

        public void plcInit()
        {
            bool flag = false;
            while (true)
            {
                Thread.Sleep(100);
                if (pLCTool.ReadPLC("PLC初始化").ToString() == "True")
                {
                    Thread.Sleep(1000);
                    if (pLCTool.ReadPLC("PLC初始化").ToString() != "True")
                    {
                        continue;
                    }
                    pLCTool.WritePLC("上位机初始化完成", "False");
                    var mModels = MainForm1.txClasses.Find((c) => c.Name == "初始化");
                    FormStart.bInit = false;
                    mModels.bStatus = false;
                    Log.log("收到PLC初始化信号");
                    foreach (var item in TaskClass.taskThreadList)
                    {
                        item.action?.BeginInvoke(item.GetType().ToString(), callback =>
                        {

                        }, null);
                    }
                    pLCTool.WritePLC("上位机初始化完成", "True");
                    flag = true;
                    FormStart.bInit = true;
                    mModels.bStatus = true;
                    Log.log("写入初始化完成");
                    Log.log("等待PLC关闭初始化触发信号！！！！！！！！！！");
                    while (flag)
                    {
                        if (pLCTool.ReadPLC("PLC初始化").ToString() != "True")
                        {
                            pLCTool.WritePLC("上位机初始化完成", "false");
                            Log.log("复位初始化完成");
                            flag = false;
                        }
                    }
                    Log.log("上位机初始化完成");
                    Thread.Sleep(3000);
                }
            }
        }
        public void getplc()
        {
            while (true)
            {
                Thread.Sleep(500);
                var mModels = MainForm1.txClasses.Find((c) => c.Name == "PLC");
                if (!TaskClass.plcConnState)
                {
                    Thread.Sleep(2000);
                    Log.log("PLC掉线开始重连！");
                    var ISOK = FormStart.pLCTool.connectToPlc();
                    if (ISOK)
                    {
                        if (mModels != null)
                            mModels.bStatus = true;
                        TaskClass.plcConnState = true;
                        time.Restart();
                        Log.log("PLC重连成功！");
                    }
                    else
                    {
                        if (mModels != null)
                            mModels.bStatus = false;
                        Log.alarmLog("PLC重连不成功！", 1);
                    }
                }
            }
        }
        Stopwatch time = new Stopwatch();

        public void heartBeatSignal()
        {
            while (true)
            {
              
                var mModels1 = MainForm1.txClasses.Find((c) => c.Name == "PLC心跳");
                if (mModels1 != null)
                    mModels1.bStatus = false;
                var mModels2 = MainForm1.txClasses.Find((c) => c.Name == "PLC");
                Thread.Sleep(500);
                try
                {
                    if (TaskClass.plcConnState == true)
                    {
                        if (pLCTool.ReadPLC("心跳OK").ToString() == "True")
                        {
                            time.Restart();
                            pLCTool.WritePLC("心跳OK", "False");
                            if (mModels1 != null)
                                mModels1.bStatus = true;            
                            Thread.Sleep(500);
                        }
                        else if (time.Elapsed.TotalSeconds > 3)
                        {
                            time.Restart();
                            if (mModels2 != null)
                                mModels2.bStatus = false;
                            TaskClass.plcConnState = false;
                        }
                    }
                    else
                    {
                        time.Restart();
                    }
                }
                catch (Exception ex)
                {

                }

            }
        }
        public void InitCon()
        {
            //CCD连接
            Task.Factory.StartNew(() =>
            {
                MainForm1.txClasses.Add(new TxClass { Name = "CCD1", txType = TxType.客户端, obj = CCDConnect });
                MainForm1.txClasses.Add(new TxClass { Name = "CCD2", txType = TxType.客户端, obj = CCDConnect1 });
                MainForm1.txClasses.Add(new TxClass { Name = "CCD权限", txType = TxType.客户端, obj = CCDPERConnectB });
                MainForm1.txClasses.Add(new TxClass { Name = "扫码枪", txType = TxType.客户端, obj = ScanConnect });
                //MainForm1.txClasses.Add(new TxClass { Name = "AGV信号交互服务器", txType = TxType.服务端, obj = _AGVServer });
                //MainForm1.txClasses.Add(new TxClass { Name = "AGV信号刷新服务器", txType = TxType.服务端, obj = _AGVServerRes });

                CCDConnect.conn(ClassCANS.OLD.CCDIP, ClassCANS.OLD.CCDPROT, 2000);
                CCDConnect1.conn(ClassCANS.OLD.CCDIP1, ClassCANS.OLD.CCDPROT1, 2000);
                //权限
                CCDPERConnectB.conn(ClassCANS.OLD.PERIP, ClassCANS.OLD.PERPORT, 2000);
                ScanConnect.conn(ClassCANS.OLD.SCANIP, ClassCANS.OLD.SCANPORT, 2000);

                ////AGV服务器
                //_AGVServer.Conn(ClassCANS.OLD.AGVIP, ClassCANS.OLD.AGVPORT, "AGV信号交互服务器", 1);

                //_AGVServerRes.Conn(ClassCANS.OLD.AGVIPRes, ClassCANS.OLD.AGVPORTRes, "AGV信号刷新服务器", 2);
            });
            MainForm1.txClasses.Add(new TxClass { Name = "PLC", txType = TxType.PLC, bStatus = false, bShow = true });
            MainForm1.txClasses.Add(new TxClass { Name = "PLC心跳", txType = TxType.MES, bStatus = false, bShow = true });
            aGVTaskFlow = new AGVTaskFlow("AGV交互服务器", 1, _AGVServer);
            aGVTaskFlowRes = new AGVTaskFlow("AGV信号刷新服务器", 2, _AGVServerRes);
        }
        private void MesTaskFlow_CallBack(Dictionary<string, string> dic, string STR)
        {
            try
            {
                this.Invoke(new Action(() =>
                {
                    List<string> list = new List<string>();
                    foreach (var item in dic)
                    {
                        list.Add(item.Value);
                    }
                    DataGridView1.Rows.Add(list.ToArray());
                    if (DataGridView1.Rows.Count > 50)
                    {
                        DataGridView1.Rows.Clear();
                    }
                    if (STR.Contains("NG"))
                    {
                        //DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[2]. = Color.Red;
                        DataGridView1[3, DataGridView1.Rows.Count - 1].Style.BackColor = Color.Red;
                    }
                    if (STR.Contains("OK"))
                    {
                        //DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[2].Style.BackColor = Color.Green;
                        DataGridView1[3, DataGridView1.Rows.Count - 1].Style.BackColor = Color.Green;
                    }
                }));
            }
            catch (Exception ex)
            {

            }
        }

        private void FirstCheckCCDTaskFlow_CallBack_1(Dictionary<string, string> dic, string STR)
        {
            try
            {
                this.Invoke(new Action(() =>
                {
                    List<string> list = new List<string>();
                    foreach (var item in dic)
                    {
                        list.Add(item.Value);
                    }
                    DataGridView2.Rows.Add(list.ToArray());
                    if (DataGridView2.Rows.Count > 2)
                    {
                        DataGridView2.Rows.RemoveAt(0);
                    }
                }));
            }
            catch (Exception ex)
            {

            }
        }

        private void btn_手动触发扫码_Click(object sender, EventArgs e)
        {
            ScanConnect.Send(ClassCANS.OLD.扫码触发信号);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (lb_小车条码.Text != ClassCANS.OLD.小车码)
                lb_小车条码.Text = ClassCANS.OLD.小车码;
            if (lb_Station0SN.Text != ClassCANS.OLD.模组码)
                lb_Station0SN.Text = ClassCANS.OLD.模组码;
            if (ClassCANS.OLD.启用MES)
            {
                lb_MES.Text = "MES在线:";
                pictureBox1.Image = UpperComputer.Properties.Resources.lamp_green;
            }
            else
            {
                lb_MES.Text = "MES离线:";
                pictureBox1.Image = UpperComputer.Properties.Resources.lamp_red;
            }
        }
        int I = 0;
        int IS = 10;
        private void button1_Click(object sender, EventArgs e)
        {
            I++;
            IS++;
       
            var mModels1 = HisDataClass.Data.list.Find((c) => c.strPoint == I.ToString());
            if (mModels1 != null)
            {
                mModels1.value = IS.ToString();
            }
            else
            {
                HisDataClass.Data.list.Add(new CCDDataClass { strPoint = I.ToString(), value = IS.ToString() });
            }
            ClassXMLGET.xmlset<HisDataClass>("HistroyData.xml", HisDataClass.Data);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (/*txt_胶水A物料条码.Text.Trim().Substring(0, 13) == ClassCANS.OLD.胶水物料规则 &&*/ textBox1.Text.Trim().Length == ClassCANS.OLD.胶水A物料编码长度)
                {
                    ClassCANS.OLD.工位1胶水B物料编码 = textBox1.Text.Trim();
                    ClassXMLGET.xmlset<ClassCANS>("cans.xml", ClassCANS.OLD);
                  
                }
                else
                {
                    MessageBox.Show($"右工位上料失败，原因:胶水条码不符合条码规则: 条码长度:{ClassCANS.OLD.胶水A物料编码长度}", "提示");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"上料失败，原因:[{ex}]", "提示");
            }
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            string strRes = "TRUE";
            if (strRes == "TRUE")
            {
                MessageBox.Show("下料成功", "提示");
            }
            else
            {
                MessageBox.Show($"下料失败，原因：[{strRes}]", "提示");
            }
        }
    }
}
