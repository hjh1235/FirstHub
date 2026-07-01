using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UpperComputer
{
    public partial class FormUpdatePassword : Form
    {
        public FormUpdatePassword()
        {
            InitializeComponent();
        }

        private void 修改_Click(object sender, EventArgs e)
        {
            string msg = "";
            try
            {
               
                string str = "select UserPassword,UserPermissions,PermissionsLevel,UserName from UserManage where UserID='" + 账号.Text + "'";
                string pword = SQLiteConnect.getQuery(str, "UserPassword",ref msg);
                if (pword == 原密码.Text)
                {
                    if (新密码.Text == 二次确认新密码.Text)
                    {
                        string strS = "update UserManage set UserPassword='" + 新密码.Text + "'where UserID='" + 账号.Text + "'";
                        int result = SQLiteConnect.executeSQL(strS,ref msg);
                        if (result <= 0)
                        {
                            //MainForm.m_formAlarm.InsertAlarmMessage("修改密码失败,请重试");
                            MessageBox.Show($"修改密码失败,请重试,原因:{msg}");
                        }
                        else
                        {
                            MessageBox.Show("修改成功","提示");
                            this.Close();
                        }
                    }
                }
                else
                {                   
                    MessageBox.Show($"账号不存在或密码不正确","提示");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"修改密码失败,请重试,原因:{ex.Message}");

            }
          
        }

        private void 取消_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
