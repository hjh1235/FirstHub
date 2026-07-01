using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UpperComputer;
using UpperComputer.TaskUnit;

namespace UpperComputer
{
    public partial class MainForm1 : Form
    {
        int WM_SYSCOMMAND = 0x0112,
SC_MOVE = 0xF010,
WM_NCHITTEST = 0x84,
HTCAPTION = 2;//命中标题栏
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int iParam);
        public static List<TxClass> txClasses = new List<TxClass>();

        /// <summary>
        /// 权限计时
        /// </summary>
        public Stopwatch sw = new Stopwatch();
        private DateTime? _targetEndTime; // 倒计时截止时间（可空，未开始时为null）
        private readonly string _configPath; // 本地配置文件路径（保存截止时间）
        public MainForm1()
        {
            InitializeComponent();
            //设置窗体最大化（不遮挡任务栏)
            //MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width + 10, Screen.PrimaryScreen.WorkingArea.Height + 1);
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            _configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "countdown_config.json");
        }
        private void MainForm1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            InitForm();
            AddForm();
            InitListVist();

            timer_Update.Enabled = true;
            LoadTargetEndTimeFromLocal(); // 读取本地配置
            //timer1.Start(); // 启动计时器（即使未开始，也先启动刷新）
            UpdateCountdownDisplay(); // 初始刷新一次显示

        }
        public void InitListVist()
        {
            ListViewItem lv1 = new ListViewItem("胶水物料编码");
            lv1.SubItems.Add("");
            ListViewItem lv2 = new ListViewItem("工位1点胶量(g)");
            lv2.SubItems.Add("");
            ListViewItem lv3 = new ListViewItem("工位1点胶时间(ms)");
            lv3.SubItems.Add("");
            ListViewItem lv4 = new ListViewItem("工位2点胶量(g)");
            lv4.SubItems.Add("");
            ListViewItem lv5 = new ListViewItem("工位2点胶时间(ms)");
            lv5.SubItems.Add("");
            ListViewItem lv6 = new ListViewItem("胶水保质期时间");
            lv6.SubItems.Add("");

            lv_Glue.Items.Add(lv1);
            lv_Glue.Items.Add(lv2);
            lv_Glue.Items.Add(lv3);
            lv_Glue.Items.Add(lv4);
            lv_Glue.Items.Add(lv5);
            lv_Glue.Items.Add(lv6);
        }
        /// <summary>
        /// 初始化日志框位置
        /// </summary>
        public void InitForm()
        {
            int rchAlarm = rchtxt_Alarm.Size.Width;
            int lineAlarm = line_Alarm.Size.Width;

            int rchAlarmloc = rchtxt_Alarm.Location.X;

            int panl = panel2.Size.Width;

            int start = rchAlarm + rchAlarmloc;

            int len = (panel2.Width - start) / 2 - 3;
            rch_txtStation1.Size = new Size(len, rch_txtStation1.Size.Height);

            line_Station1.Size = new Size(len, line_Station1.Size.Height);

            rch_txtStation1.Location = new Point(start + 3, rch_txtStation1.Location.Y);

            line_Station1.Location = new Point(start + 3, line_Station1.Location.Y);

            int iStartX = rch_txtStation1.Location.X + len;


            rch_txtStation2.Size = new Size(len, rch_txtStation2.Size.Height);

            line_Station2.Size = new Size(len, line_Station2.Size.Height);

            rch_txtStation2.Location = new Point(iStartX + 3, rch_txtStation2.Location.Y);

            line_Station2.Location = new Point(iStartX + 3, line_Station2.Location.Y);


        }
        public void AddForm()
        {
            MainForm1.txClasses.Add(new TxClass { Name = "初始化", txType = TxType.初始化, bStatus = false, bShow = true });
            Log.initLog(rch_txtStation1);
            Log.initLogMes(rch_txtStation2);
            Log.initLogAlaram(rchtxt_Alarm);
            Log.initLogAGV(rch_txtAGV);
            Log.initLogAGVRes(rch_txtAGVRes);
            Formlogin _Formlogin = new Formlogin();
            _Formlogin.ShowDialog();

            var mainform = new FormStart();
            mainform.FormBorderStyle = FormBorderStyle.None;
            mainform.TopLevel = false;
            mainform.Size = panel_StartForm.Size;
            panel_StartForm.Controls.Add(mainform);
            mainform.Dock = DockStyle.Fill;
            mainform.Show();
        }
        private void btn_Set_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Point p = new Point();
            p.X = Location.X + panel3.Location.X + button.Location.X + 2;
            p.Y = Location.Y + panel3.Location.Y + button.Location.Y + 22;
            cms_Setting.Show(p);
        }

        private void btn_Closed_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Maximized_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
                this.WindowState = FormWindowState.Maximized;
            else
                this.WindowState = FormWindowState.Normal;
        }

        private void btn_Minimized_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


        int index = 0;
        private void timer_Update_Tick(object sender, EventArgs e)
        {
            try
            {
                if (tls_当前配方.Text != "当前配方：" + ClassCANS.OLD.当前配方)
                    tls_当前配方.Text = "当前配方：" + ClassCANS.OLD.当前配方;
                if (tls_当前用户.Text != "当前用户：" + FormUserLogoin.userID)
                    tls_当前用户.Text = "当前用户：" + FormUserLogoin.userID;
                if (tls_当前权限.Text != "当前权限：" + FormUserLogoin.userpermissions)
                    tls_当前权限.Text = "当前权限：" + FormUserLogoin.userpermissions;
                if (tls_制令单.Text != "制令单：" + ClassCANS.OLD.MoNumber)
                    tls_制令单.Text = "制令单：" + ClassCANS.OLD.MoNumber;
                tls_时间.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");


            }
            catch (Exception ex)
            {

            }
            UpdateListView();

            UpdatePermission();

            UpdateData();
            UpdateCountdownDisplay();
        }
        /// <summary>
        /// 刷新ListView控件状态
        /// </summary>
        public void UpdateListView()
        {
            #region 更新状态
            try
            {
                foreach (var item in txClasses)
                {
                    if (item.txType == TxType.服务端)
                    {
                        if (item.obj != null)
                        {
                            var MODE = (TCP_IP_Connect)item.obj;
                            if (MODE.bConStatus == true)
                            {
                                item.nImageIndex = 1;
                                item.strTxt = "已连接";
                            }
                            else
                            {
                                item.nImageIndex = 0;
                                item.strTxt = "未连接";
                            }
                        }
                    }
                    else if (item.Name == "扫码枪")
                    {
                        if (item.obj != null)
                        {
                            var MODE = (_Client)item.obj;
                            if (MODE.ConnStr == "已连接服务器")
                            {
                                if (ClassCANS.OLD.屏蔽扫码)
                                {
                                    item.nImageIndex = 1;
                                    item.strTxt = "已连接   已屏蔽扫码";
                                }
                                else
                                {
                                    item.nImageIndex = 1;
                                    item.strTxt = "已连接";
                                }

                            }
                            else
                            {
                                if (ClassCANS.OLD.屏蔽扫码)
                                {
                                    item.nImageIndex = 0;
                                    item.strTxt = "未连接   已屏蔽扫码";
                                }
                                else
                                {
                                    item.nImageIndex = 0;
                                    item.strTxt = "未连接";
                                }
                            }
                        }
                    }
                    else if (item.txType == TxType.客户端)
                    {
                        if (item.obj != null)
                        {
                            var MODE = (_Client)item.obj;
                            if (MODE.ConnStr == "已连接服务器")
                            {
                                item.nImageIndex = 1;
                                item.strTxt = "已连接";
                            }
                            else
                            {
                                item.nImageIndex = 0;
                                item.strTxt = "未连接";
                            }
                        }
                    }
                    else if (item.txType == TxType.串口)
                    {
                        if (item.obj != null)
                        {
                            var MODE = (SerialPortCom)item.obj;
                            if (MODE.flag == true)
                            {
                                item.nImageIndex = 1;
                                item.strTxt = "已连接";
                            }
                            else
                            {
                                item.nImageIndex = 0;
                                item.strTxt = "未连接";
                            }
                        }
                    }
                    else if (item.txType == TxType.PLC)
                    {
                        if (item.bStatus == true)
                        {
                            item.nImageIndex = 1;
                            item.strTxt = "在线";
                        }
                        else
                        {
                            item.nImageIndex = 0;
                            item.strTxt = "离线";
                        }
                    }
                    else if (item.txType == TxType.MES)
                    {
                        if (item.bStatus == true)
                        {
                            item.nImageIndex = 1;
                            item.strTxt = "在线";
                        }
                        else
                        {
                            item.nImageIndex = 0;
                            item.strTxt = "离线";
                        }
                    }
                    else if (item.txType == TxType.初始化)
                    {
                        if (item.bStatus == true)
                        {
                            item.nImageIndex = 1;
                            item.strTxt = "完成";
                        }
                        else
                        {
                            item.nImageIndex = 0;
                            item.strTxt = "未完成";
                        }
                    }
                    else if (item.txType == TxType.PLC心跳)
                    {
                        if (item.bStatus == true)
                        {
                            item.nImageIndex = 1;
                        }
                        else
                        {
                            item.nImageIndex = 0;
                        }
                    }
                }
                if (listView_Status.Items.Count < txClasses.Count)
                {
                    listView_Status.Items.Clear();
                    int i = 0;
                    foreach (var item in txClasses)
                    {
                        if (item.Name == "PLC心跳")
                        {
                            ListViewItem lv = new ListViewItem(item.Name);
                            listView_Status.Items.Add(lv);
                            if (item.bShow)
                                listView_Status.Items[i].ImageIndex = 0;
                        }
                        else
                        {
                            ListViewItem lv = new ListViewItem(item.Name + item.strTxt);
                            listView_Status.Items.Add(lv);
                            if (item.bShow)
                                listView_Status.Items[i].ImageIndex = 0;
                        }

                        i++;
                    }
                }

                index = 0;
                if (listView_Status.Items.Count >= txClasses.Count)
                {
                    foreach (var item in txClasses)
                    {
                        if (item.Name == "激光图档")
                        {
                            if (item.strTxt != listView_Status.Items[index].Text)
                                listView_Status.Items[index].Text = item.strTxt;
                        }
                        else if (item.Name == "PLC心跳")
                        {
                            if (listView_Status.Items[index].ImageIndex != item.nImageIndex)
                            {
                                listView_Status.Items[index].Text = item.Name;
                                listView_Status.Items[index].ImageIndex = item.nImageIndex;
                            }
                        }
                        else if (listView_Status.Items[index].ImageIndex != item.nImageIndex || listView_Status.Items[index].Text != item.Name + item.strTxt)
                        {
                            listView_Status.Items[index].Text = item.Name + item.strTxt;
                            listView_Status.Items[index].ImageIndex = item.nImageIndex;
                        }
                        index++;
                    }
                }
            }
            catch (Exception)
            {


            }

            #endregion

        }
        bool bOffLine = false;
        public void UpdatePermission()
        {
            #region 限时注销权限
            if (FormUserLogoin.bPermission && bOffLine == false)
            {
                sw.Restart();
                bOffLine = true;
            }
            else if (FormUserLogoin.bPermission && bOffLine && sw.Elapsed.TotalMinutes > ClassCANS.OLD.nOffLineTime)
            {
                Log.log($"自动注销用户：[{FormUserLogoin.userID}]，权限：[{FormUserLogoin.userpermissions}]");
                FormUserLogoin.userLevel = 0;
                FormUserLogoin.userpermissions = "未登录";
                FormUserLogoin.userName = "未登录";
                FormUserLogoin.bPermission = false;
                bOffLine = false;
                sw.Stop();
            }
            if (UserPerClass.bUser)
            {
                bOffLine = false;
                FormUserLogoin.bPermission = false;
                sw.Stop();
            }
            #endregion
        }
        /// <summary>
        /// 更新点胶参数
        /// </summary>
        int i = 0;
        public void UpdateData()
        {
            try
            {
                i = 0;
                if (lv_Glue.Items[i].SubItems[1].Text != ClassCANS.OLD.工位1胶水A物料编码)
                    lv_Glue.Items[i].SubItems[1].Text = ClassCANS.OLD.工位1胶水A物料编码;
                i++;
                if (lv_Glue.Items[i].SubItems[1].Text != Data.工位1点胶量.ToString())
                    lv_Glue.Items[i].SubItems[1].Text = Data.工位1点胶量.ToString();
                i++;
                if (lv_Glue.Items[i].SubItems[1].Text != Data.工位1点胶时间.ToString())
                    lv_Glue.Items[i].SubItems[1].Text = Data.工位1点胶时间.ToString();
                i++;
                if (lv_Glue.Items[i].SubItems[1].Text != Data.工位2点胶量.ToString())
                    lv_Glue.Items[i].SubItems[1].Text = Data.工位2点胶量.ToString();
                i++;
                if (lv_Glue.Items[i].SubItems[1].Text != Data.工位2点胶时间.ToString())
                    lv_Glue.Items[i].SubItems[1].Text = Data.工位2点胶时间.ToString();
                i++;
                if (lv_Glue.Items[i].SubItems[1].Text != Data.胶水保质期时间.ToString())
                    lv_Glue.Items[i].SubItems[1].Text = Data.胶水保质期时间.ToString();
                if (Program.GlueTime == "当前胶水已过期，请及时更换！")
                {
                    lv_Glue.Items[i].SubItems[1].Text = "当前胶水已过期，请及时更换！";
                    lv_Glue.Items[i].BackColor = Color.Red;
                }
                else
                {
                    lv_Glue.Items[i].BackColor = SystemColors.Window;
                }
            }
            catch (Exception)
            {


            }
        }
        private void 用户登录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUserLogoin formUserLogoin = new FormUserLogoin();
            formUserLogoin.ShowDialog();
        }

        private void 用户管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormUserLogoin.userLevel < 3)
            {
                MessageBox.Show("权限不足", "温馨提示");
                return;
            }
            FormUserManage formUserManage = new FormUserManage();
            formUserManage.ShowDialog();
        }

        private void 用户注销ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Log.log($"手动注销用户：[{FormUserLogoin.userID}]，权限：[{FormUserLogoin.userpermissions}]");
            FormUserLogoin.userLevel = 0;
            FormUserLogoin.userpermissions = "未登录";
            FormUserLogoin.userName = "未登录";
            FormUserLogoin.bPermission = false;
        }

        private void btn_User_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Point p = new Point();
            p.X = Location.X + panel3.Location.X + button.Location.X + 2;
            p.Y = Location.Y + panel3.Location.Y + button.Location.Y + 22;
            cms_User.Show(p);
        }

        private void MainForm1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("是否退出上位机？重启后无法继续当前流程。", "退出程序提醒", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result != DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void MainForm1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.DoEvents();
            //Application.ExitThread();
            //Application.Exit();
            //System.Environment.Exit(0);
            Process.GetCurrentProcess().Kill();
        }

        private void 参数设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormUserLogoin.userLevel < 2)
            {
                MessageBox.Show("权限不足", "温馨提示");
                return;
            }
            ManageForm _ManageForm = new ManageForm();
            _ManageForm.Show();
        }

        private void pLC监控ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormUserLogoin.userLevel < 3)
            {
                MessageBox.Show("权限不足", "温馨提示");
                return;
            }
            ManageShowForm _ManageShowForm = new ManageShowForm();
            _ManageShowForm.ShowDialog();
        }

        private void mES登录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (FormUserLogoin.userLevel < 1)
            //{
            //    MessageBox.Show("权限不足", "温馨提示");
            //    return;
            //}
            Formlogin _Formlogin = new Formlogin();
            _Formlogin.ShowDialog();
        }
        private void 初始化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("是否进行初始化操作", "提示", MessageBoxButtons.OKCancel))
            {

                foreach (var item in TaskClass.taskThreadList)
                {
                    item.action?.BeginInvoke(item.GetType().ToString(), callback =>
                    {

                    }, null);
                }
                FormStart.bInit = true;
                var mModels = MainForm1.txClasses.Find((c) => c.Name == "初始化");
                mModels.bStatus = true;

            }
        }

        private void btn_上料A_Click(object sender, EventArgs e)
        {
            try
            {
                if (/*txt_胶水A物料条码.Text.Trim().Substring(0, 13) == ClassCANS.OLD.胶水物料规则 &&*/ txt_胶水A物料条码.Text.Trim().Length == ClassCANS.OLD.胶水A物料编码长度)
                {
                    ClassCANS.OLD.工位1胶水A物料编码 = txt_胶水A物料条码.Text.Trim();
                    ClassXMLGET.xmlset<ClassCANS>("cans.xml", ClassCANS.OLD);
                    MessageBox.Show("上料成功", "提示");
                    // 1. 重新计算倒计时截止时间：当前时间 + 90天
                    _targetEndTime = DateTime.Now.AddDays(90);

                    // 2. 将新的截止时间保存到本地文件（确保重启后能续跑）
                    SaveTargetEndTimeToLocal();

                    // 3. 立即刷新界面显示，让用户看到重置后的倒计时
                    UpdateCountdownDisplay();
                }
                else
                {
                    MessageBox.Show($"上料失败，原因:胶水条码不符合条码规则: 条码长度:{ClassCANS.OLD.胶水A物料编码长度}", "提示");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"上料失败，原因:[{ex}]", "提示");
            }
        }
        public void UpdateCountdownDisplay()
        {
            if (_targetEndTime == null)
            {
                Program.GlueTime = "未开始倒计时，点击上料按钮启动！";
                return;
            }

            TimeSpan remainingTime = _targetEndTime.Value - DateTime.Now;

            if (remainingTime.TotalMilliseconds <= 0)
            {
                Program.GlueTime = "当前胶水已过期，请及时更换！";
                //lblCountdown.BackColor = Color.Red;

                //_targetEndTime = null; // 重置，避免重复显示结束
                //SaveTargetEndTimeToLocal(); // 同步删除本地配置
                return;
            }

            Program.GlueTime = $"{remainingTime.Days:D2}天 " +
                               $"{remainingTime.Hours:D2}时 " +
                               $"{remainingTime.Minutes:D2}分 ";
        }
        public void SaveTargetEndTimeToLocal()
        {
            try
            {
                string json = JsonConvert.SerializeObject(_targetEndTime);
                File.WriteAllText(_configPath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存倒计时配置失败：{ex.Message}");
            }
        }

        public void LoadTargetEndTimeFromLocal()
        {
            try
            {
                if (!File.Exists(_configPath))
                {
                    _targetEndTime = null;
                    return;
                }

                string json = File.ReadAllText(_configPath);
                _targetEndTime = JsonConvert.DeserializeObject<DateTime?>(json);

                // 若读取的截止时间已过期，直接重置
                //if (_targetEndTime != null && _targetEndTime.Value < DateTime.Now)
                //{
                //    _targetEndTime = null;
                //    //SaveTargetEndTimeToLocal(); // 同步删除过期配置
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show($"读取倒计时配置失败：{ex.Message}");
                _targetEndTime = null;
            }
        }
        private void btn_下料A_Click(object sender, EventArgs e)
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

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();

            //SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void 手动过站ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormUserLogoin.userLevel < 3)
            {
                MessageBox.Show("权限不足", "温馨提示");
                return;
            }
            SNForm sNForm = new SNForm();
            sNForm.ShowDialog();
        }

        private void mES下发参数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormUserLogoin.userLevel < 3)
            {
                MessageBox.Show("权限不足", "温馨提示");
                return;
            }
            MESParForm mESParForm = new MESParForm();
            mESParForm.ShowDialog();
        }

        private void 指纹模块ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormUserLogoin.userLevel < 3)
            {
                MessageBox.Show("权限不足", "温馨提示");
                return;
            }
            bool result = false;
            UserFingerRegisterForm FingerRegisterForm = new UserFingerRegisterForm(FormUserLogoin.userpermissions, ref result);
            FingerRegisterForm.ShowDialog();
        }

        private void btn_上料B_Click(object sender, EventArgs e)
        {

        }

        private void btn_下料B_Click(object sender, EventArgs e)
        {

        }

        private void 生产记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", Log.strProPath);
        }
    }
}
