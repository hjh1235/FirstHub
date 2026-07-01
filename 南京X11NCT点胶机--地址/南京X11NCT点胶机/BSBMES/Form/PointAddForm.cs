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
    public partial class PointAddForm : Form
    {
        #region 定义变量
        /// <summary>
        /// 选中行的索引
        /// </summary>
        int iRows = 0;
        /// <summary>
        /// 选中行的首单元格内容
        /// </summary>
        string strRowsTxt = "";
        #endregion
        public PointAddForm()
        {
            InitializeComponent();
        }
        private void PointAddForm_Load(object sender, EventArgs e)
        {
            FormStart.pLCTool.canListen = true;
            ShowView();
            timer1.Enabled = true;
        }
        private void btn_AddPointToRight_Click(object sender, EventArgs e)
        {
            try
            {
                PLCinfomartion info = new PLCinfomartion();
                info.PointName = txt_PLCPointName.Text;
                info.PointAddress = txt_PLCPointAddress.Text;
                info.PointType = cmb_PLCPointType.Text;
                info.PointResult = "0";
                ClassCANS.OLD.PLClist.Add(info);
                FormStart.pLCTool.dicPLC.Add(txt_PLCPointName.Text, info);
                ShowView();
            }
            catch (Exception ex)
            {

                MessageBox.Show("添加失败,"+ex.Message);
            }
                     
        }

        private void btn_DeletePLCPoint_Click(object sender, EventArgs e)
        {
            try
            {
                FormStart.pLCTool.dicPLC.Remove(strRowsTxt);
                ClassCANS.OLD.PLClist.RemoveAt(iRows);
                ShowView();
            }
            catch (Exception)
            {
               
            }        
        }

        private void btn_ReadPLCPoint_Click(object sender, EventArgs e)
        {
            object result = "";
            string name = txt_PLCPointAddress.Text;
            string type = cmb_PLCPointType.Text;
            Task task =Task.Run(()=> {
               result = FormStart.pLCTool.ReadPLC(name,type);
            });
            task.ContinueWith(t=> {
                txt_ReadTxt.Text = result.ToString();
            },TaskScheduler.FromCurrentSynchronizationContext());
        
        }

        private void btn_WritePLCPoint_Click(object sender, EventArgs e)
        {

            bool result = false;
            string name = txt_PLCPointAddress.Text;
            string type = cmb_PLCPointType.Text;
            string value = txt_WriteTxt.Text;
            Task task = Task.Run(() => {
                result = FormStart.pLCTool.WritePLC(name, type, value);
            });
            task.ContinueWith(t => {
                if (result == true)
                {
                    MessageBox.Show("写入成功");
                }
                else
                {
                    MessageBox.Show("写入失败");
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());

            //var result=  FormStart.pLCTool.WritePLC(txt_PLCPointAddress.Text,cmb_PLCPointType.Text,txt_WriteTxt.Text);
            //if (result == true)
            //{
            //    MessageBox.Show("写入成功");
            //}
            //else
            //{
            //    MessageBox.Show("写入失败");
            //}
        }

        private void dgv_PLC_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void dgv_PLC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //获得当前选中行索引
                iRows = dgv_PLC.CurrentCell.RowIndex;
                strRowsTxt = dgv_PLC.Rows[iRows].Cells[0].Value.ToString();
            }
            catch (Exception)
            {
                
            }
        }
        public void ShowView()
        {
            dgv_PLC.Rows.Clear();
            foreach (var item in FormStart.pLCTool.dicPLC)
            {
                dgv_PLC.Rows.Add(new object[] {item.Value.PointName,item.Value.PointAddress,item.Value.PointType,item.Value.PointFlag,item.Value.PointResult });
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                //if (ClassCANS.OLD.PLClist.Count != dgv_PLC.Rows.Count)
                //{
                //    ShowView();
                //}
                int i = 0;
                foreach (var item in FormStart.pLCTool.dicPLC)
                {
                  //  Form2.ReadPLC(item.Value.PointName);
                    dgv_PLC.Rows[i].Cells[3].Value = item.Value.PointResult;
                    i++;
                }
            }
            catch (Exception)
            {
            
            }
        }
        //保存
        private void button1_Click(object sender, EventArgs e)
        {
            //info.ss.i = "1";
            //info.ss.b = "2";
            //PLCinfomartion ins = new PLCinfomartion();
            //ins.PointName = "222";
            ////info.ss.dicPLC.Add("1",ins);
            //info.ss.PLClist.Add(ins);
            //ClassCANS.OLD.PLClist.Add(ins);
            //ClassXMLGET.xmlset<info>("can.xml",info.ss );
           
            bool rt=  ClassXMLGET.xmlset<ClassCANS>("cans.xml", ClassCANS.OLD);
            if (rt == true)
            {
                MessageBox.Show("保存成功");
            }
            else
            {
                MessageBox.Show("保存失败");
            }
        }

       

      
    } 
}
