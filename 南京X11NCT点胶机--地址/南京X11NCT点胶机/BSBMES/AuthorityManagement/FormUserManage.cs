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
    public partial class FormUserManage : Form
    {
        public FormUserManage()
        {
            InitializeComponent();
        }
        public string SelectID="";
        public enum UserLevel
        {
            技术员=1,
            工程师=2,
            管理员=3,          
        }
        public void getdataset(ref string msg)
        {
           
            try
            {
                string str = "select UserID as 账号,UserName as 用户名称,UserPermissions as 用户权限,UserPassword as 用户密码 ,PermissionsLevel as 权限等级 from UserManage where PermissionsLevel<='" + FormUserLogoin.userLevel + "'";
                DataSet ds = SQLiteConnect.getDataSet(str,ref msg);
                if (ds == null)
                {
                    MessageBox.Show("查询的值不存在","提示");
                    return;
                }
                dgv_UserManage.DataSource = ds.Tables[0];
                dgv_UserManage.AllowUserToAddRows = false;//去掉dataGridSource最后一行
                dgv_UserManage.Columns[1].Visible = false;
                dgv_UserManage.Columns[4].Visible = false;
            }
            catch (Exception error)
            {             
                msg = $"查询数据异常,原因:{error.Message}";
            }

        }
        private void FormUserManage_Load(object sender, EventArgs e)
        {
            string msg = "";
            权限.SelectedIndex = 0;
            getdataset(ref msg);
        }

        private void 添加_Click(object sender, EventArgs e)
        {
            string msg = "";
            string strID = "select * from UserManage where UserID='" + 用户名.Text + "'";
            DataSet dsID = SQLiteConnect.getDataSet(strID,ref msg);
            int intRowsID = dsID.Tables[0].Rows.Count;
            if (intRowsID > 0)
            {
                MessageBox.Show($"已存在该账号！","提示");
                return;
            }
            int value = 0;
            value= (int)Enum.Parse(typeof(UserLevel), 权限.Text);
            //如果权限存在
            string str = "insert into UserManage (UserID ,UserName ,UserPermissions ,UserPassword,PermissionsLevel)values('" + 用户名.Text + "','" + 1 + "','" + 权限.Text + "','" + 密码.Text + "','" +value + "')";
            int number = SQLiteConnect.executeSQL(str,ref msg);
            if (number <= 0)
            {               
                MessageBox.Show("改变数据库数据异常","提示");
            }
            getdataset(ref msg);
        }

        private void dgv_UserManage_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                SelectID = dgv_UserManage.SelectedRows[0].Cells["账号"].Value.ToString();
            }
            catch (Exception)
            {

              
            }
            
        }

        private void dgv_UserManage_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void 删除_Click(object sender, EventArgs e)
        {
            string msg = "";
            if(SelectID=="")
            {
                return;
            }
            try
            {
                string str = "delete from UserManage where UserID='" + SelectID + "'";
                SQLiteConnect.executeSQL(str,ref msg);
                getdataset(ref msg);
            }
            catch (Exception error)
            {              
                MessageBox.Show("改变数据库数据异常","提示");
            }
        }
    }
}
