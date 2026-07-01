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
    public partial class ManageForm : Form
    {
        public ManageForm()
        {
            InitializeComponent();
        }    
        private void ManageForm_Load(object sender, EventArgs e)
        {

        }
        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (panel_Form.Controls.Count > 0)
                panel_Form.Controls.Clear();
            try
            {
                if (treeView.SelectedNode.Text == "功能参数")
                {
                    SettingForm settingForm = new SettingForm();
                    settingForm.FormBorderStyle = FormBorderStyle.None;
                    settingForm.Size = panel_Form.Size;
                    settingForm.TopLevel = false;
                    panel_Form.Controls.Add(settingForm);
                    settingForm.Show();
                }
                else if (treeView.SelectedNode.Text == "配方设置")
                {
                    MesLoadParForm mesLoadParForm = new MesLoadParForm();
                    mesLoadParForm.FormBorderStyle = FormBorderStyle.None;
                    mesLoadParForm.Size = panel_Form.Size;
                    mesLoadParForm.TopLevel = false;
                    panel_Form.Controls.Add(mesLoadParForm);
                    mesLoadParForm.Show();
                }              
            }
            catch (Exception)
            {

               
            }           
        }
    }
}
