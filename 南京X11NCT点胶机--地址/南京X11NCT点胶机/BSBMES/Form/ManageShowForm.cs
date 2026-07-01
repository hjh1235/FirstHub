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
    public partial class ManageShowForm : Form
    {
        PointAddForm _PointAddForm = new PointAddForm();
        RangValueForm _RangValueForm = new RangValueForm();
        public ManageShowForm()
        {
            InitializeComponent();
        }

        

        private void ManageShowForm_Load(object sender, EventArgs e)
        {
            ShowMessage("信号监控");
           
        }
        private void btn_IO_Click(object sender, EventArgs e)
        {
            ShowMessage("信号监控");
        }

        private void btn_RangValue_Click(object sender, EventArgs e)
        {
            ShowMessage("测距值监控");
        }

        private void btn_RangPos_Click(object sender, EventArgs e)
        {
            ShowMessage("测距坐标监控");
        }
        public void ShowMessage(string name)
        {
            panel_Form.Controls.Clear();
            switch (name)
            {
                case "信号监控":
                    //PointAddForm _PointAddForm = new PointAddForm();
                    _RangValueForm.Hide();
                    _PointAddForm.Size = panel_Form.Size;
                    _PointAddForm.TopLevel = false;
                    panel_Form.Controls.Add(_PointAddForm);
                    _PointAddForm.Show();
                    btn_IO.BackColor = Color.Green;                   
                    btn_RangValue.BackColor = SystemColors.Control;
                    btn_RangPos.BackColor = SystemColors.Control;
                    break;
                case "测距值监控":
                    _PointAddForm.Hide();
                    _RangValueForm.Size = panel_Form.Size;
                    _RangValueForm.TopLevel = false;
                    panel_Form.Controls.Add(_RangValueForm);
                    _RangValueForm.Show();

                    btn_IO.BackColor = SystemColors.Control ;
                    btn_RangValue.BackColor = Color.Green;
                    btn_RangPos.BackColor = SystemColors.Control;                 
                    break;
                case "测距坐标监控":
                    btn_IO.BackColor = SystemColors.Control;                  
                    btn_RangValue.BackColor = SystemColors.Control;
                    btn_RangPos.BackColor = Color.Green;
                    break;
                default:
                    break;
            }
        }

        private void ManageShowForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormStart.pLCTool.canListen = false;
        }
    }
}
