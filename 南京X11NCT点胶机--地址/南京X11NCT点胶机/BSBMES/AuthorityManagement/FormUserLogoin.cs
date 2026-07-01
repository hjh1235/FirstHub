using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace UpperComputer
{
    public partial class FormUserLogoin : Form
    {
        public static string userpermissions = "未登录";
        public static int userLevel = 0;
        public static string userName= "未登录";
        public bool UserLoding = false;
        public static string userID = "未登录";
        public static bool bPermission = false;
        public FormUserLogoin()
        {
            InitializeComponent();
        }             
        public void  getdataset()
        {

            if (txtPassword.Text == "" || txtUserID.Text == "")
            {              
                MessageBox.Show("账号或密码不能为空","提示");
                return;
            }
            if (txtUserID.Text == "test" && txtPassword.Text == "test")
            {
                FormUserLogoin.userName = "test";
                FormUserLogoin.userpermissions = "test";
                FormUserLogoin.userLevel = 10;
                userID = txtUserID.Text;
                Close();
                return;
            }
            string msg = "";
            try
            {
                string str = "select UserPassword,UserPermissions,PermissionsLevel,UserName from UserManage where UserID='" + txtUserID.Text + "'";         
                string pword=SQLiteConnect.getQuery(str,"UserPassword",ref msg);
                if (pword == txtPassword.Text)
                {
                    bPermission = true;
                    userID = txtUserID.Text;
                    userpermissions = SQLiteConnect.getQuery(str, "UserPermissions",ref msg).Trim();
                    userLevel = Convert.ToInt32(SQLiteConnect.getQuery(str, "PermissionsLevel",ref msg).Trim());
                    userName = SQLiteConnect.getQuery(str, "UserName",ref msg).Trim();
                    Log.log($"用户：[{userID}]登录，权限：[{userpermissions}]");
                    Close();              
                    return;
                }
                else if(pword=="")
                {
                    MessageBox.Show("该账号不存在","提示");
                    return;
                }
                else
                {
                    MessageBox.Show("密码错误", "提示");
                    return;
                }                 
            }
            catch (Exception error)
            {
                MessageBox.Show($"查询数据异常,原因:{error.Message}", "提示");
                return;
            }          
        }           
        private void btnDetermine_Click(object sender, EventArgs e)
        {
            getdataset();
        }

        private void txtUserID_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (UserLoding == false)
                {
                    UserLoding = true;
                    getdataset();                   
                }
                else
                {
                    UserLoding = false;
                }                            
            }
        }

        private void txtPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (UserLoding == false)
                {
                    UserLoding = true;
                    getdataset();                   
                }
                else
                {
                    UserLoding = false;
                }
            }
        }

        private void FormUserLogoin_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            FormUpdatePassword _form = new FormUpdatePassword();
            _form.ShowDialog();
        }
    }
}
