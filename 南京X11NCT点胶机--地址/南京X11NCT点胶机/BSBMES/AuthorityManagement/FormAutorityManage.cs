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
    public partial class FormAutorityManage : Form
    {
        public FormAutorityManage()
        {
            InitializeComponent();
        }

        private void FormAutorityManage_Load(object sender, EventArgs e)
        {
            ckDebug.Checked = Properties.Settings.Default.Ckdebug;
            txtPermissionsLevel.Text = FormUserLogoin.userLevel.ToString();
            txtUserName.Text = FormUserLogoin.userName;
            txtPermissions.Text = FormUserLogoin.userpermissions;
        }              
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string vidwText = treeView1.SelectedNode.Text;

            if (vidwText == "权限信息")
            {
                for (int i = 0; i < panel3.Controls.Count; i++)
                {
                    panel3.Controls.RemoveAt(i);
                }
                FormAuthorityUpdate formAuthorityUpdata = new FormAuthorityUpdate();
                formAuthorityUpdata.TopLevel = false;
                panel3.Controls.Add(formAuthorityUpdata);
                formAuthorityUpdata.Size = panel3.Size;
                formAuthorityUpdata.Show();
            }
            if (vidwText == "界面管理")
            {
                for (int i = 0; i < panel3.Controls.Count; i++)
                {
                    panel3.Controls.RemoveAt(i);
                }
                FormInterfaceManage formInterfaceManage = new FormInterfaceManage();
                formInterfaceManage.TopLevel = false;
                panel3.Controls.Add(formInterfaceManage);
                formInterfaceManage.Size = panel3.Size;
                formInterfaceManage.Show();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (FormUserLogoin.userName != txtUserName.Text || FormUserLogoin.userpermissions != txtPermissions.Text || FormUserLogoin.userLevel != Convert.ToInt32(txtPermissionsLevel.Text))
            {
                txtUserName.Text = FormUserLogoin.userName;
                txtPermissions.Text = FormUserLogoin.userpermissions;
                txtPermissionsLevel.Text = FormUserLogoin.userLevel.ToString();
            }
            if (FormUserLogoin.userpermissions == "超级管理员" || FormUserLogoin.userpermissions == "test")
            {
                ckDebug.Visible = true;
            }
            else
            {
                ckDebug.Visible = false;
            }
        }


        private void ckDebug_CheckedChanged(object sender, EventArgs e)
        {
            if (FormUserLogoin.userName == "超级管理员" ||FormUserLogoin.userName=="test")
            {
                Properties.Settings.Default.Ckdebug = ckDebug.Checked;
                Properties.Settings.Default.Save();
                return;
            }        
        }      
    }
}
