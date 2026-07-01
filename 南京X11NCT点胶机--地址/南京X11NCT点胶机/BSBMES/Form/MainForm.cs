using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MetroFramework.Forms;
using System.Threading.Tasks;
using System.Xml;
using UpperComputer.TaskUnit;

namespace UpperComputer
{
    public partial class MainForm :MetroFramework.Forms.MetroForm
    {
        TaskClass task = new TaskClass();
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            Formlogin _Formlogin = new Formlogin();
            _Formlogin.ShowDialog();

            Log.initLog(uiRichTextBox2);
            Log.initLogAlaram(uiRichTextBox1);
            var mainform = new FormStart();
            mainform.FormBorderStyle = FormBorderStyle.None;
            mainform.TopLevel = false;
            mainform.Size = panel6.Size;
            panel6.Controls.Add(mainform);
            mainform.Dock = DockStyle.Fill;
            mainform.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否退出上位机？重启后无法继续当前流程。","退出程序提醒",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
            if (result.ToString().ToUpper() == "YES")
            {
                this.Close();
                Application.ExitThread();
                Application.Exit();
                System.Environment.Exit(0);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString();
            try
            {
                btn_ModeName.Text = ClassCANS.OLD.当前配方;
            }
            catch (Exception)
            {
               
            }          
            try
            {
                if (FormStart.Station0SN != btn_Scan.Text)
                {
                    btn_Scan.Text = FormStart.Station0SN;
                }       
            }
            catch (Exception)
            {
            }

            ///PLC连接状态
            if (TaskClass.plcConnState == true)
            {
                btn_PLCStatus.BackColor = Color.Green;
                btn_PLCStatus.Text = "PLC在线";
            }
            else
            {
                btn_PLCStatus.BackColor = Color.Red;
                btn_PLCStatus.Text = "PLC离线";
            }

            //CCD连接状态
            if (FormStart.CCDConnect.ConnStr == "已连接服务器")
            {
                btn_CCD0.BackColor = Color.Green;
                btn_CCD0.Text = "CCD1在线";
            }
            else
            {
                btn_CCD0.BackColor = Color.Red;
                btn_CCD0.Text = "CCD1离线";
            }

            //CCD连接状态
            if (FormStart.CCDConnect1.ConnStr == "已连接服务器")
            {
                btn_CCD1.BackColor = Color.Green;
                btn_CCD1.Text = "CCD2在线";
            }
            else
            {
                btn_CCD1.BackColor = Color.Red;
                btn_CCD1.Text = "CCD2离线";
            }

            //扫码串口连接状态
            if (FormStart.ScanConnect.ConnStr == "已连接服务器")
            {
                btn_Scan.BackColor = Color.Green;
                btn_Scan.Text = "扫码枪在线";
            }
            else
            {
                btn_Scan.BackColor = Color.Red;
                btn_Scan.Text = "扫码枪离线";
            }
            
            ///MES连接状态
            if (ClassCANS.OLD.启用MES == true)
            {
                button_mesState.BackColor = Color.Green;
                button_mesState.Text = "MES启用中";
            }
            else
            {
                button_mesState.BackColor = Color.Red;
                button_mesState.Text = "MES离线";
            }
            label_制令单.Text = Properties.Settings.Default.MoNumber;
        }  
            
        private void btn_Add_Click(object sender, EventArgs e)
        {
            //if (FormUserLogoin.userLevel < 3)
            //{
            //    MessageBox.Show("权限不足", "温馨提示");
            //    return;
            //}
           
            ManageShowForm _ManageShowForm = new ManageShowForm();
            _ManageShowForm.ShowDialog();
        }

        //初始化
        private void btn_Inital_Click(object sender, EventArgs e)
        {
            if(DialogResult.OK==MessageBox.Show("是否进行初始化操作","提示",MessageBoxButtons.OKCancel))
            {
                //FormStart.pLCTool.WritePLC("报警", "0");
                foreach (var item in TaskClass.taskThreadList)
                {
                    item.action?.BeginInvoke(item.GetType().ToString(),callback=> {

                    },null);
                }
            }
        }

        private void btnOutStation_Click(object sender, EventArgs e)
        {
            //string code = txtModuleCode.Text;
            //string index = "";
            //string checkResult = mes.Instance().GetInStationCheck(
            //                   ClassCANS.OLD.工序代码,
            //                   Properties.Settings.Default.MachineNo,
            //                   Properties.Settings.Default.SessionId,
            //                   code, Properties.Settings.Default.MoNumber, "1", ref index
            //                   );

            //if (checkResult.ToUpper().Contains("TRUE"))
            //{
            //    Log.log($"校验{ClassCANS.OLD.模组码}成功！");
            //}
            //List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            //if (!handMesPara( ref list))
            //{
            //    Log.alarmLog($"处理条码{code}数据失败！");
            //}
            //if (!uploadProduct(code, list))
            //{
            //    Log.alarmLog($"上传MES失败！");
            //}
            //upadteMainFormData(code, "OK");
        }

        private void btn_Setting_Click(object sender, EventArgs e)
        {
            //if (FormUserLogoin.userLevel < 3)
            //{
            //    MessageBox.Show("权限不足", "温馨提示");
            //    return;
            //}
          
            ManageForm _SettingForm = new ManageForm();
            _SettingForm.Show();
        }

        private void btn_Scan_Click(object sender, EventArgs e)
        {

        }

        private void button_登录_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 处理MES上传参数
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private bool dealWithMesPara(string code, ref List<Dictionary<string, string>> list)
        {
            try
            {
                list.Clear();
                string paramValue = "";
                string testResult = "0";//0OK,1NG
                foreach (var item in Formlogin.dicPar.Values)
                {
                    paramValue = Program.模组数据[code][item.paramName];
                    if (Convert.ToDouble(paramValue) > Convert.ToDouble(item.paramFirstUpper) ||
                        Convert.ToDouble(paramValue) < Convert.ToDouble(item.paramFirstLower))
                    {
                        testResult = "1";
                    }
                    else
                    {
                        testResult = "0";
                    }
                    list.Add(new Dictionary<string, string>
                    {
                        ["paramName"] = item.paramName,
                        ["paramCode"] = item.paramCode,
                        ["paramValue"] = paramValue,
                        ["paramResult"] = testResult,
                        ["paramUnit"] = item.paramUnit
                    });
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

       
        /// <summary>
        /// 更新主页面中的表格
        /// </summary>
        public void upadteMainFormData(string code, string result)
        {
            try
            {
                //double outerRingPower1 = Convert.ToDouble(FormStart.pLCTool.ReadPLC("外环激光功率1"));//外环功率
                //double outerRingPower2 = Convert.ToDouble(FormStart.pLCTool.ReadPLC("外环激光功率2"));
                //double outerRingPower3 = Convert.ToDouble(FormStart.pLCTool.ReadPLC("外环激光功率3"));
                //double outerRingPower4 = Convert.ToDouble(FormStart.pLCTool.ReadPLC("外环激光功率4"));
                //double innerRingPower1 = Convert.ToDouble(FormStart.pLCTool.ReadPLC("内环激光功率1"));//内环功率
                //double innerRingPower2 = Convert.ToDouble(FormStart.pLCTool.ReadPLC("内环激光功率2"));
                //double innerRingPower3 = Convert.ToDouble(FormStart.pLCTool.ReadPLC("内环激光功率3"));
                //double innerRingPower4 = Convert.ToDouble(FormStart.pLCTool.ReadPLC("内环激光功率4"));
                //List<mes.testdata> data = new List<mes.testdata>();
                //double 离焦量1 = Convert.ToDouble(FormStart.pLCTool.ReadPLC("离焦量1")) / 100.0;//离焦量1
                //data.Add(new mes.testdata() { testValue = innerRingPower1.ToString() });
                //data.Add(new mes.testdata() { testValue = innerRingPower2.ToString() });
                //data.Add(new mes.testdata() { testValue = innerRingPower3.ToString() });
                //data.Add(new mes.testdata() { testValue = innerRingPower4.ToString() });
                //data.Add(new mes.testdata() { testValue = outerRingPower1.ToString() });
                //data.Add(new mes.testdata() { testValue = outerRingPower2.ToString() });
                //data.Add(new mes.testdata() { testValue = outerRingPower3.ToString() });
                //data.Add(new mes.testdata() { testValue = outerRingPower4.ToString() });
                //data.Add(new mes.testdata() { testValue = 离焦量1.ToString() });
                //FormStart.formStart.更新界面(code, data, result);
            }
            catch (Exception)
            {

            }
        }
        public bool handMesPara(ref List<Dictionary<string, string>> list)
        {
            try
            {
                list.Clear();
                string paramValue = "";
                string testResult = "0";//0OK,1NG
                foreach (var item in Formlogin.dicPar.Values)
                {
                    paramValue = NextFloat((float)Convert.ToDouble(item.paramFirstLower), (float)Convert.ToDouble(item.paramFirstUpper)).ToString();
                    if (Convert.ToDouble(paramValue) > Convert.ToDouble(item.paramFirstUpper) ||
                        Convert.ToDouble(paramValue) < Convert.ToDouble(item.paramFirstLower))
                    {
                        testResult = "1";
                    }
                    else
                    {
                        testResult = "0";
                    }
                    list.Add(new Dictionary<string, string>
                    {
                        ["paramName"] = item.paramName,
                        ["paramCode"] = item.paramCode,
                        ["paramValue"] = paramValue,
                        ["paramResult"] = testResult,
                        ["paramUnit"] = item.paramUnit
                    });
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        static float NextFloat(float min, float max)
        {
            System.Random random = new System.Random();
            double val = (random.NextDouble() * (max - min) + min);
            return (float)val;
        }
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("是否退出上位机？重启后无法继续当前流程。", "退出程序提醒", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                Application.ExitThread();
                Application.Exit();
                System.Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FormUserLogoin.userLevel = 0;
            FormUserLogoin.userID = "未登录";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormUserLogoin _form = new FormUserLogoin();
            _form.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (FormUserLogoin.userLevel < 3)
            {
                MessageBox.Show("权限不足", "温馨提示");
                return;
            }
            FormUserManage _form = new FormUserManage();
            _form.ShowDialog();
        }

        private void btn_MESSetting_Click(object sender, EventArgs e)
        {
            //if (FormUserLogoin.userLevel < 1)
            //{
            //    MessageBox.Show("权限不足", "温馨提示");
            //    return;
            //}
            Formlogin _Formlogin = new Formlogin();
            _Formlogin.ShowDialog();
        }
    }
}
