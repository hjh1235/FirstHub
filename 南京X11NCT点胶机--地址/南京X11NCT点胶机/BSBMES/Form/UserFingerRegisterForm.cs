using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using libzkfpcsharp;
using EquipBase;
using BADB;
using System.IO;

namespace UpperComputer
{

    public partial class UserFingerRegisterForm : Form
    {
        #region Value
        private UserInformationParameter UIP = new UserInformationParameter();
        //private FingerReader FR = new FingerReader();
        #region 新增显示
        public delegate void addmessagedelegate(object message);
        public static addmessagedelegate oneaddmessagedelegate = null;
        #endregion
        #endregion
        //public UserFingerRegisterForm()
        //{
        //    InitializeComponent();
        //}
        public UserFingerRegisterForm(string str_UserPower, ref bool result)
        {
            if (FingerPermission.IsCheckFG)//判断指纹验证成功
            {
                if (FormUserLogoin.userLevel<3)
                //指纹登录不是三级权限
                {
                   
                    if (true)
                    {
                        if (FormUserLogoin.userLevel==1)
                        {
                            str_UserPower = "技术员";
                            result = true;
                        }
                        else if (FormUserLogoin.userLevel == 2)
                        {
                            str_UserPower = "工程师";
                            result = true;
                        }
                        else
                        {
                            FingerPermission.isStaionCheck = false;
                            MessageBox.Show("三级账户验证失败");
                            result = false;
                            return;
                        }
                    }
                    else
                    {
                        FingerPermission.isStaionCheck = false;
                        MessageBox.Show("三级账户验证失败");
                        result = false;
                        return;
                    }
                }
                else
                {
                    result = true;
                }
            }
            else
            {

                if (FormUserLogoin.userLevel>=3)
                {
                    //if (fCheck.gLogin.ActGroup.ToUpper() == "PE")
                    //{
                    //    BdFun.User2 = fCheck.UserNo;
                    //    str_UserPower = "工艺";
                    //    result = true;
                    //}
                    if (FormUserLogoin.userLevel==3)
                    {
                        str_UserPower = "管理员";
                        result = true;
                    }
                    //else if (fCheck.gLogin.ActGroup.ToUpper() == "PQE")
                    //{
                    //    BdFun.User2 = fCheck.UserNo;
                    //    str_UserPower = "品质工程师";
                    //    result = true;
                    //}
                    else
                    {
                        FingerPermission.isStaionCheck = false;
                        MessageBox.Show("三级账户验证失败");
                        result = false;
                        return;
                    }
                }
                else
                {
                    FingerPermission.isStaionCheck = false;
                    MessageBox.Show("三级账户验证失败");
                    result = false;
                    return;
                }
            }
            InitializeComponent();
            oneaddmessagedelegate = showamessage;
            if (str_UserPower == "管理员") //只有工艺和品质工程师权限才能进行增加
            {
                Global.UserPower = "管理员";
                dgvUserFingerInformation.Enabled = true;
                btnDelete.Enabled = true;
                
                
            }
            else if (str_UserPower == "工程师")
            {
                Global.UserPower = "工程师";
                dgvUserFingerInformation.Enabled = true;
                btnDelete.Enabled = true;
            }
            else if (str_UserPower == "技术员")
            {
                Global.UserPower = "技术员";
                dgvUserFingerInformation.Enabled = false;
                btnDelete.Enabled = false;
            }
            else
            {
                Global.UserPower = "操作员";
                dgvUserFingerInformation.Enabled = false;
                btnDelete.Enabled = false;
            }
        }
        #region 新增
        private void showamessage(object message)
        {
            Invoke((ThreadStart)delegate
            {
                PromptText1.Text = message.ToString();
                Refresh();
            });
        }
        #endregion
        static Thread TestTh = null;
        private void UserFingerRegisterForm_Load(object sender, EventArgs e) //指纹管理按钮传进来
        {
            string s_Message = "";
            //关闭外部识别线程

            //只打开一次
            //if (FingerReader.OpenDevice(0, out s_Message))
            //{
            //    showamessage(s_Message);
            //}
            ReadParameter();
            TestTh = new Thread(Test);
            TestTh.IsBackground = true;
            TestTh.Start();
        }
        public void Test()
        {
            #region 循环读取
            while (true)
            {
                string s_Message1 = "", str_UserName = "", str_UserLevel = "";
                if (FingerReader.UserIndentify(out str_UserName, out str_UserLevel, out s_Message1))//循环读取指纹
                {
                    oneaddmessagedelegate($"{DateTime.Now.ToString()}:\n指纹信息验证成功\n当前用户为：{str_UserName}\n职位为{ str_UserLevel}");
                }
                if (s_Message1.Contains("识别失败"))
                {
                    Invoke((ThreadStart)delegate
                    {
                        if (txbUserName.Text != "" && cmbUserLevel.SelectedIndex != -1)
                        {
                            string s_Message2 = "";
                            oneaddmessagedelegate("请录入三次指纹信息");
                            Thread.Sleep(1000);
                            if (FingerReader.UserRegister(txbUserName.Text, cmbUserLevel.Text, out s_Message2))//注册
                            {
                                txbUserName.Text = "";
                                cmbUserLevel.SelectedIndex = -1;
                                ReadParameter();
                                return;
                            }
                        }
                        else
                        {
                            oneaddmessagedelegate("用户名和职级都不能为空");
                        }

                    });

                }
                Thread.Sleep(200);
            }
            #endregion
        }
        private void ReadParameter() //每次更新名称和职位时调用刷新界面
        {
            try
            {
                UIP = (UserInformationParameter)BasicArithmetic.LoadFromXml("FingerUserInfo.xml", UIP.GetType());
                dgvUserFingerInformation.Rows.Clear();
                for (int i = 0; i < UIP.ListUserInformation.Count(); i++)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    DataGridViewTextBoxCell Name = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell Level = new DataGridViewTextBoxCell();
                    row.Cells.Add(Name);
                    row.Cells.Add(Level);
                    dgvUserFingerInformation.Rows.Add(row);
                    dgvUserFingerInformation.AllowUserToAddRows = false;
                    dgvUserFingerInformation.Rows[i].Cells[0].Value = UIP.ListUserInformation[i].UserName;
                    dgvUserFingerInformation.Rows[i].Cells[1].Value = UIP.ListUserInformation[i].UserLevel;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e) //删除指纹
        {
            try
            {
                int ErrorCode = zkfp.ZKFP_ERR_OK;
                if (dgvUserFingerInformation.CurrentRow.Index != -1)
                {
                    int i_Index = dgvUserFingerInformation.CurrentRow.Index;
                    if (UIP.ListUserInformation.Count == 0)
                    {
                        ReadParameter();
                        return;
                    }
                    UIP.ListUserInformation.RemoveAt(i_Index);
                    FingerReader.List_UserInfo.ListUserInformation.RemoveAt(i_Index);
                    BasicArithmetic.SaveToXml("FingerUserInfo.xml", UIP, UIP.GetType());

                    #region MyRegion
                    string s_Message = "";
                    FingerReader.ReflashXaml2(0, out s_Message);//刷新计数
                    #endregion
                    ReadParameter();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("删除用户数据失败，请检查指纹仪是否正常连接！");
            }

        }
        private void UserFingerRegisterForm_Close(object sender, FormClosingEventArgs e) //关闭设备
        {
            TestTh.Suspend();
            //FingerReader.CloseDevice();//关闭设备D:\users\S2301030034\桌面\P10\上位机预开发\南京人工位新界面\人工位2024091301\ProcessUpper\ProcessUpper\指纹模块\FingerPermission.cs
            FingerPermission.isStaionCheck = false;
        }
    }

}
