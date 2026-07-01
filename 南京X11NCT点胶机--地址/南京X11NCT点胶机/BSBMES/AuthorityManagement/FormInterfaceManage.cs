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
    public partial class FormInterfaceManage : Form
    {
        string SelectName;
        List<string> CountList = new List<string> { };       
        public FormInterfaceManage()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 查询
        /// </summary>
        public void getdataset()
        {
            int level = 0;
            try
            {
                //清除dataview数据
                dataviewInterface.Columns.Clear();
                //查询需要查询的权限等级
                string strLevel = "select distinct PermissionsLevel from UserManage where UserPermissions='" + cbPermissions.Text + "'";
                level =Convert.ToInt32(SQLiteConnect.getQuery(strLevel,"PermissionsLevel"));         
                //如果需要查询的权限等级小于当前登录权限或者权限等级等于10,则显示所有的界面管理 
                if (level < FormUserLogoin.userLevel || level==10)
                {
                    string str = "select InterfaceName as 界面名称  from InterfaceManage where Permissions= '" + cbPermissions.Text + "'";
                    DataSet ds = SQLiteConnect.getDataSet(str);
                    if (ds == null)
                    {
                        MainForm.m_formAlarm.InsertAlarmMessage(ERecvResult.查询数据异常.ToString());
                        return;
                    }
                    dataviewInterface.DataSource = ds.Tables[0];
                    dataviewInterface.AllowUserToAddRows = false;
                    DrawOutput();
                }
                else
                {
                    //如果需要查询的权限等级大于当前登录权限或者权限等级不等于10,则只显示当前登录权限启用的界面管理
                    string str = "select InterfaceName as 界面名称  from InterfaceManage where Permissions= '" + cbPermissions.Text + "'and Enable='true'";
                    DataSet ds = SQLiteConnect.getDataSet(str);
                    if (ds == null)
                    {
                        MainForm.m_formAlarm.InsertAlarmMessage(ERecvResult.查询数据异常.ToString());
                        return;
                    }
                    dataviewInterface.DataSource = ds.Tables[0];
                    dataviewInterface.AllowUserToAddRows = false;
                    DrawOutput();
                }
              
                getEnable();
            }
            catch (Exception error)
            {
                MainForm.m_formAlarm.InsertAlarmMessage(ERecvResult.查询数据异常.ToString() + "\r\n" + error);
            }

        }
        private void tenSelect_Click(object sender, EventArgs e)
        {        
             getdataset();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (FormUserLogoin.userLevel < 10)
            {
                MainForm.m_formAlarm.InsertAlarmMessage(ERecvResult.系统权限不足.ToString());
                return;
            }
            //界面名不能为空
            if (Booltext() == false)
            {
                return;
            }
            try
            {
                ///添加界面名称前，查询是否已经存在该界面
                string strName = "select InterfaceName from InterfaceManage where InterfaceName='" + txtInterfaceName.Text + "'";
                DataSet ds = SQLiteConnect.getDataSet(strName);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    MainForm.m_formAlarm.InsertAlarmMessage(ERecvResult.已存在该界面.ToString());
                    return;
                }
                CountList.Clear();
                //如果不存在，则查询存在哪些权限
                string strface = "select distinct Permissions from InterfaceManage";
                //把查询到的所有权限添加到List中
                CountList = SQLiteConnect.getQueryList(strface, "Permissions");

               //把添加的界面名称和存在的每个权限都绑定到一起
                for (int i = 0; i < CountList.Count; i++)
                {
                    string str = "insert into InterfaceManage (InterfaceName,Permissions,Enable)values('" + txtInterfaceName.Text + "','" + CountList[i] + "','false')";
                    int number = SQLiteConnect.executeSQL(str);
                    if (number <= 0)
                    {
                        MainForm.m_formAlarm.InsertAlarmMessage(ERecvResult.改变数据库数据异常.ToString());                       
                    }
                }
                CountList.Clear();
                getdataset();
            }
            catch (Exception error)
            {
                CountList.Clear();
                MainForm.m_formAlarm.InsertAlarmMessage(ERecvResult.改变数据库数据异常.ToString() + "\r\n"+error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (FormUserLogoin.userLevel < 10)
            {
                MainForm.m_formAlarm.InsertAlarmMessage(ERecvResult.系统权限不足.ToString());
                return;
            }
            //删除的界面名不能为空
            if (Booltext() == false)
            {
                return;
            }
            try
            {
                string str = "delete from InterfaceManage where InterfaceName='" + txtInterfaceName.Text + "'";
                SQLiteConnect.executeSQL(str);
                getdataset();
            }
            catch (Exception error)
            {

                MainForm.m_formAlarm.InsertAlarmMessage(ERecvResult.改变数据库数据异常.ToString() + "\r\n" + error);
            }
        }
        /// <summary>
        /// 点击dataview界面checkbox控件改变控件显示和修改数据库数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataviewInterface_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                SelectName = dataviewInterface.SelectedRows[0].Cells["界面名称"].Value.ToString();
                txtInterfaceName.Text = dataviewInterface.SelectedRows[0].Cells["界面名称"].Value.ToString();
                //查询CheckBox控件的启用状态
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)this.dataviewInterface.Rows[e.RowIndex].Cells["cb_check"];
                Boolean flag = Convert.ToBoolean(checkCell.Value);
             
                if (flag == true)
                {
                    //如果已经启用，则改变为不启用
                    checkCell.Value = false;                                      
                        string str = "update InterfaceManage set Enable='false' where InterfaceName='" + SelectName + "' and Permissions='"+cbPermissions.Text+"'";
                        int index=  SQLiteConnect.executeSQL(str);
                        if (index <= 0)
                        {
                            MainForm.m_formAlarm.InsertAlarmMessage(ERecvResult.修改启用状态失败.ToString());
                            return;
                        }                  
                }
                else
                {
                    //如果没启用，则启用
                        checkCell.Value = true;                   
                        string str = "update InterfaceManage set Enable='true' where InterfaceName='" + SelectName + "'and Permissions='" + cbPermissions.Text + "'";
                        int index = SQLiteConnect.executeSQL(str);
                        if (index <= 0)
                        {
                            MainForm.m_formAlarm.InsertAlarmMessage(ERecvResult.修改启用状态失败.ToString());
                            return;
                        }                 
                }
            }
            catch (Exception error)
            {
                MainForm.m_formAlarm.InsertAlarmMessage(ERecvResult.改变数据库数据异常.ToString() + "\r\n" + error);
            }
        }

        private void FormInterfaceManage_Load(object sender, EventArgs e)
        {
            try
            {
                cbPermissions.Items.Clear();
                //查询表UserManage中小于当前登录权限等级的所有权限名称
                string strface = "select distinct UserPermissions from UserManage where PermissionsLevel<='"+FormUserLogoin.userLevel+"'";
                //把查询到的值添加到List中
                CountList= SQLiteConnect.getQueryList(strface, "UserPermissions");
                for (int i = 0; i < CountList.Count; i++)
                {
                    //把查询到的权限名称添加到combox控件中
                    cbPermissions.Items.Add(CountList[i]);
                }
                
                if (cbPermissions.Items.Count > 0)
                {
                    //如果combox的数量大于0，则默认选择第一项
                    cbPermissions.SelectedIndex = 0;
                }
                CountList.Clear();
            }
            catch (Exception error)
            {
                CountList.Clear();
                MainForm.m_formAlarm.InsertAlarmMessage(ERecvResult.查询数据异常.ToString() + "\r\n" + error);
            }

        }
        public bool Booltext()
        {
            if (txtInterfaceName.Text == "")
            {
                MessageBox.Show("界面名称不能为空", "提示");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 添加CheckBox控件
        /// </summary>
        void DrawOutput()
        {

            DataGridViewCheckBoxColumn ignore = new DataGridViewCheckBoxColumn();
            ignore.DataPropertyName = "bignore";
            ignore.Name = "cb_check";
            ignore.HeaderText = "是否启用";
            dataviewInterface.Columns.Add(ignore);

        }
        /// <summary>
        /// 在界面显示该权限是否可以操作此界面
        /// </summary>
        public void getEnable()
        {
            int level = 0;
            int count = 0;
            CountList.Clear();
            try
            {
                string strLevel = "select distinct PermissionsLevel from UserManage where UserPermissions=='" + cbPermissions.Text + "'";
                level=Convert.ToInt32(SQLiteConnect.getQuery(strLevel, "PermissionsLevel"));
              
                if (level < FormUserLogoin.userLevel) //如果查询的权限等于当前权限，则不能修改Enable值
                {
                    string strEb = "select Enable  from InterfaceManage where Permissions='" + cbPermissions.Text + "'";
                    CountList= SQLiteConnect.getQueryList(strEb, "Enable");
                    for (int i = 0; i < CountList.Count; i++)
                    {
                        DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)this.dataviewInterface.Rows[count].Cells["cb_check"];
                        checkCell.Value = CountList[i];
                        count++; //依次增加，把list值赋给Enable控件
                    }
                  
                }
                else //如果查询的权限小于当前权限，则能修改Enable值
                {
                    string str = "select Enable  from InterfaceManage where Permissions='" + cbPermissions.Text + "'and Enable='true'";
                    CountList = SQLiteConnect.getQueryList(str, "Enable");
                    for (int i = 0; i < CountList.Count; i++)
                    {
                        DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)this.dataviewInterface.Rows[count].Cells["cb_check"];
                        checkCell.Value = CountList[i];
                        count++;                    //依次增加，把list值赋给Enable控件
                    }                  
                }
                CountList.Clear();
            }
            catch (Exception error)
            {
                CountList.Clear();
                MainForm.m_formAlarm.InsertAlarmMessage(ERecvResult.查询数据异常.ToString() + "\r\n" + error);
            }
        }
        private void dataviewInterface_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


    }
}
