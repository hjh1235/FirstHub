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
using System.Threading;

namespace UpperComputer
{
    public partial class FormAuthorityUpdate : Form
    {
        string SelectID;           
        List<string> CountList = new List<string> { };
        public FormAuthorityUpdate()
        {
            InitializeComponent();
        }
        private void FormAuthorityUpdate_Load(object sender, EventArgs e)
        {

            GetPer();
            getdataset();

        }

        public  void GetPer()
        {
            try
            {
                cbPermissions.Items.Clear();
                string strface = "select distinct Permissions from InterfaceManage where PermissionsLevel<='" + FormUserLogoin.userLevel + "'";
                CountList = SQLiteConnect.getQueryList(strface, "Permissions");
                for (int i = 0; i < CountList.Count; i++)
                {
                    cbPermissions.Items.Add(CountList[i]);
                }
                if (cbPermissions.Items.Count > 0)
                {
                    cbPermissions.SelectedIndex = 0;
                }
                CountList.Clear();
            }
            catch (Exception error)
            {
                MainForm.m_formAlarm.InsertAlarmMessage(ERecvResult.查询数据异常.ToString() + "\r\n" + error);
                CountList.Clear();
            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        public void getdataset()
        {
            try
            {
                string str = "select UserID as 工号,UserName as 用户名称,UserPermissions as 用户权限,UserPassword as 用户密码 ,PermissionsLevel as 权限等级 from UserManage where PermissionsLevel<='" + FormUserLogoin.userLevel+ "'";
                DataSet ds = SQLiteConnect.getDataSet(str);
                if (ds == null)
                {
                    MessageBox.Show("查询的值不存在");
                    return;
                }
                dataGridSource.DataSource = ds.Tables[0];
                dataGridSource.AllowUserToAddRows = false;//去掉dataGridSource最后一行
            }
            catch (Exception error)
            {
                MainForm.m_formAlarm.InsertAlarmMessage(ERecvResult.查询数据异常.ToString() + "\r\n" + error);
            }

        }

        private void dataGridSource_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                SelectID = dataGridSource.SelectedRows[0].Cells["工号"].Value.ToString();
                txtUserID.Text = dataGridSource.SelectedRows[0].Cells["工号"].Value.ToString();
                txtUserName.Text = dataGridSource.SelectedRows[0].Cells["用户名称"].Value.ToString();
                cbPermissions.Text = dataGridSource.SelectedRows[0].Cells["用户权限"].Value.ToString();
                txtUserPassword.Text = dataGridSource.SelectedRows[0].Cells["用户密码"].Value.ToString();
                txtPermissionsLevel.Text= dataGridSource.SelectedRows[0].Cells["权限等级"].Value.ToString();
            }
            catch (Exception ex)
            {
               
            }
        }

        private void tenSelect_Click(object sender, EventArgs e)
        {
            getdataset();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {          
            if (Booltext() == false)
            {
                return;
            }           
            try
            {
                //查询添加的账号是否存在
                CountList.Clear();
                string strID = "select * from UserManage where UserID='"+ txtUserID.Text+ "'";
                DataSet dsID= SQLiteConnect.getDataSet(strID);
                int intRowsID = dsID.Tables[0].Rows.Count;
                if (intRowsID > 0)
                {
                    MainForm.m_formAlarm.InsertAlarmMessage(ERecvResult.已存在该账号.ToString());
                    return;
                }
                //查询权限是否存在
                string strPer = "select * from InterfaceManage where Permissions='" + cbPermissions.Text + "'";
                DataSet ds = SQLiteConnect.getDataSet(strPer);
                int intRows = ds.Tables[0].Rows.Count;
                if (intRows > 0)
                {
                   //如果权限存在
                    string str = "insert into UserManage (UserID ,UserName ,UserPermissions ,UserPassword,PermissionsLevel)values('" + txtUserID.Text + "','" + txtUserName.Text + "','" + cbPermissions.Text + "','" + txtUserPassword.Text + "','" + txtPermissionsLevel.Text+ "')";

                    int number = SQLiteConnect.executeSQL(str);
                    if (number <= 0)
                    {
                        MainForm.m_formAlarm.InsertAlarmMessage(ERecvResult.改变数据库数据异常.ToString());
                    }
                    getdataset();
                }
                else
                {
                    //如果权限不存在，查询有多少个管理界面
                    string str = "select distinct InterfaceName from InterfaceManage";
                    CountList = SQLiteConnect.getQueryList(str, "InterfaceName");
                    //然后把当前权限和存在的每个界面绑定，添加进InterfaceManage        
                    for (int i = 0; i < CountList.Count; i++)
                    {
                        string strinter = "insert into InterfaceManage (InterfaceName,Permissions,Enable,PermissionsLevel)values('" + CountList[i] + "','" + cbPermissions.Text + "','true','"+txtPermissionsLevel.Text+"')";
                        int number = SQLiteConnect.executeSQL(strinter);
                        if (number <= 0)
                        {
                            MainForm.m_formAlarm.InsertAlarmMessage(ERecvResult.改变数据库数据异常.ToString());
                            return;
                        }
                    }
                    //添加这个权限进UserManage表
                    string strinto = "insert into UserManage (UserID ,UserName ,UserPermissions ,UserPassword,PermissionsLevel)values('" + txtUserID.Text + "','" + txtUserName.Text + "','" + cbPermissions.Text + "','" + txtUserPassword.Text + "','" + txtPermissionsLevel.Text + "')";
                    int numberS = SQLiteConnect.executeSQL(strinto);
                    if (numberS <= 0)
                    {
                        MainForm.m_formAlarm.InsertAlarmMessage(ERecvResult.改变数据库数据异常.ToString());
                        return;
                    }
                    getdataset();
                    CountList.Clear();
                }
            }
            catch (Exception error)
            {
                CountList.Clear();
                MainForm.m_formAlarm.InsertAlarmMessage(ERecvResult.改变数据库数据异常.ToString() + "\r\n" + error);
            }
        }
        public bool Booltext()
        {
            if (txtUserID.Text == "")
            {
                MainForm.m_formAlarm.InsertAlarmMessage(ERecvResult.数据不能为空.ToString());
                return false;
            }
            else if (txtUserName.Text == "")
            {
                MainForm.m_formAlarm.InsertAlarmMessage(ERecvResult.数据不能为空.ToString());
                return false;
            }
            else if (cbPermissions.Text == "")
            {
                MainForm.m_formAlarm.InsertAlarmMessage(ERecvResult.数据不能为空.ToString());
                return false;
            }
            else if (txtUserPassword.Text == "")
            {
                MainForm.m_formAlarm.InsertAlarmMessage(ERecvResult.数据不能为空.ToString());
                return false;
            }
            else if (txtPermissionsLevel.Text == "")
            {
                MainForm.m_formAlarm.InsertAlarmMessage(ERecvResult.数据不能为空.ToString());
                return false;
            }
            else
            {
                int i;
                if (int.TryParse(txtPermissionsLevel.Text, out i))
                {
                    if (i > FormUserLogoin.userLevel)
                    {
                        MainForm.m_formAlarm.InsertAlarmMessage(ERecvResult.数据不能为空.ToString());
                        return false;
                    }
                }
                else
                {
                    MainForm.m_formAlarm.InsertAlarmMessage(ERecvResult.数据不能为空.ToString());
                    return false;
                }
            }
            return true;
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Booltext() == false)
            {
                return;
            }
            try
            {
                 //修改
                string str = "update UserManage set UserName='" + txtUserName.Text + "', UserPermissions='" + cbPermissions.Text + "',UserPassword='" + txtUserPassword.Text + "',PermissionsLevel='"+txtPermissionsLevel.Text+"' where UserID='" + txtUserID.Text + "'";
                int result = SQLiteConnect.executeSQL(str);
                if (result > 0)
                {
                    //修改表UserManage当前所有权限的等级
                    string strUpdate = "update UserManage set PermissionsLevel='" + txtPermissionsLevel.Text + "'where UserPermissions='"+ cbPermissions.Text + "' ";
                    SQLiteConnect.executeSQL(strUpdate);         
                    //修改表InterfaceManage当前权限的等级
                    string strInterface = "update InterfaceManage set PermissionsLevel='" + txtPermissionsLevel.Text + "' where Permissions='" + cbPermissions.Text + "'";
                    SQLiteConnect.executeSQL(strInterface);               
                    getdataset();
                    MessageBox.Show("修改成功！", "提示");
                }
            }
            catch (Exception error)
            {
                MainForm.m_formAlarm.InsertAlarmMessage(ERecvResult.改变数据库数据异常.ToString() + "\r\n" + error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (Booltext() == false)
            {
                return;
            }
            try
            {
                string str = "delete from UserManage where UserID='" + SelectID + "'";
                SQLiteConnect.executeSQL(str);
                getdataset();
            }
            catch (Exception error)
            {
                MainForm.m_formAlarm.InsertAlarmMessage(ERecvResult.改变数据库数据异常.ToString() + "\r\n" + error);

            }
        }

        private void btnDelPermissions_Click(object sender, EventArgs e)
        {
            if (FormUserLogoin.userLevel < 10)
            {
                MainForm.m_formAlarm.InsertAlarmMessage(ERecvResult.系统权限不足.ToString());
                return;
            }
            if (Booltext() == false)
            {
                return;
            }
            try
            {
                //删除UserManage表中该所有该权限用户
                string str = "delete from UserManage where UserPermissions='" + cbPermissions.Text + "'";
                SQLiteConnect.executeSQL(str);
                //删除InterfaceManage表中所有与该权限绑定的的界面数据
                string strIntreface = "delete from InterfaceManage where Permissions='" + cbPermissions.Text + "'";
                int result = SQLiteConnect.executeSQL(strIntreface);
                if (result > 0)
                {
                    //移除cbPermissions控件中的数据
                    int index = cbPermissions.Items.IndexOf(cbPermissions.Text);
                    cbPermissions.Items.RemoveAt(index);
                    if (cbPermissions.Items.Count > 0)
                    {
                        cbPermissions.SelectedIndex = 0;
                    }                   
                    MessageBox.Show("删除成功！", "提示");
                }

            }
            catch (Exception error)
            {
                MainForm.m_formAlarm.InsertAlarmMessage(ERecvResult.改变数据库数据异常.ToString() + "\r\n" + error);
            }
        }
    }
}
