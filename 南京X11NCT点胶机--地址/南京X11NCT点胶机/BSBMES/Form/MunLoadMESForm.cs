using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UpperComputer.TaskUnit;

namespace UpperComputer
{
    public partial class MunLoadMESForm : Form
    {
        public bool bSuccess = false;
        public MunLoadMESForm()
        {
            InitializeComponent();
        }

        private void MunLoadMESForm_Load(object sender, EventArgs e)
        {
            HisDataClass.Data = ClassXMLGET.xmlget<HisDataClass>("HistroyData.xml");
            LoadMESData();
        }
        public void LoadMESData()
        {
            try
            {
                dgv_MES.Rows.Clear();
                int i = 1;
                foreach (var item in Formlogin.dicPar)
                {
                    if (item.Value.paramName == null)
                    {
                        continue;
                    }
                    string value = "0";
                    if (item.Value.paramName.Contains("通道"))
                    {
                        value = "1";
                    }
                    //if (item.Value.paramName.Contains("是否超出涂胶区域"))
                    //{
                    //    double dVlaue = 0;
                    //    double.TryParse(item.Value.paramFirstLower, out dVlaue);
                    //    value = (dVlaue).ToString();
                    //}
                    //else if (item.Value.paramName.Contains("检测面积"))
                    //{
                    //    var mModels1 = HisDataClass.Data.list.Find((c) => c.strPoint == i.ToString());
                    //    if (mModels1 != null)
                    //    {
                    //        double dVlaue = 0;
                    //        double.TryParse(mModels1.value, out dVlaue);
                    //        value = dVlaue.ToString();
                    //    }
                    //    else
                    //    {
                    //        double dVlaue = 0;
                    //        double.TryParse(item.Value.paramFirstLower, out dVlaue);
                    //        value = (dVlaue+1).ToString();
                    //    }
                    //    i++;
                    //}
                    else
                    {
                        double dVlaue = 0;
                        double.TryParse(item.Value.paramFirstLower, out dVlaue);
                        value = (dVlaue + 1).ToString();
                    }
                    dgv_MES.Rows.Add(new object[] { item.Value.paramName, item.Value.paramCode, item.Value.paramFirstUpper, item.Value.paramFirstLower, item.Value.paramReTestUpper, item.Value.paramReTestLower, item.Value.paramUnit, value.ToString() });
                }
            }
            catch (Exception ee)
            {


            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            dgv_MES.EndEdit();
            try
            {
                InitClass.LoaddicPar.Clear();
                int i = 0;
                foreach (var item in dgv_MES.Rows)
                {
                    string name = dgv_MES.Rows[i].Cells[0].Value.ToString();
                    string value = dgv_MES.Rows[i].Cells[7].Value.ToString();
                    string Upvalue = dgv_MES.Rows[i].Cells[2].Value.ToString();
                    string Lowvalue = dgv_MES.Rows[i].Cells[3].Value.ToString();
                    //string code = dgv_MES.Rows[i].Cells[1].Value.ToString();


                    InitClass.LoaddicPar.Add(name, new ResultPar { paramCurValue = value, paramName = name, paramFirstUpper = Upvalue, paramFirstLower = Lowvalue });
                    i++;
                }
                bSuccess = true;
                this.Close();
            }
            catch (Exception ex)
            {


            }

        }
        private void btn_Exit_Click(object sender, EventArgs e)
        {
            bSuccess = false;
            this.Close();
        }
    }
}
