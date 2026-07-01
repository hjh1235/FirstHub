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
    public partial class MESParForm : Form
    {
        public MESParForm()
        {
            InitializeComponent();
        }

        private void btn_Get_Click(object sender, EventArgs e)
        {
            GetMesPar();
        }
        public void GetMesPar()
        {
            if (!ClassCANS.OLD.启用MES == true)
            {
                return;
            }
            string rt = mes.Instance().GetParameter(ClassCANS.OLD.groupCode, ClassCANS.OLD.MachineNo, ClassCANS.OLD.SessionId, "", ClassCANS.OLD.MoNumber, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ref Formlogin.dicPar);

            if (rt.ToUpper() != "TRUE")
            {
                MessageBox.Show($"调用参数下发接口MES返回失败,原因:{rt}");
                return;
            }
            ShowView();
        }
        public void ShowView()
        {
            try
            {
                dgv_MES.Rows.Clear();
                foreach (var item in Formlogin.dicPar)
                {
                    dgv_MES.Rows.Add(new object[] { item.Value.paramName, item.Value.paramCode, item.Value.paramFirstUpper, item.Value.paramFirstLower, item.Value.paramReTestUpper, item.Value.paramReTestLower, item.Value.paramUnit });
                }
            }
            catch (Exception)
            {

            }

        }
    }
}
