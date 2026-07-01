using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UpperComputer
{
    public partial class RangValueForm : Form
    {
        int station = 0;
        int index = 1;
        public RangValueForm()
        {
            InitializeComponent();
        }
        public Dictionary<string, PLCinfomartion> dicPLC1 = new Dictionary<string, PLCinfomartion>();
        public Dictionary<string, PLCinfomartion> dicPLC2 = new Dictionary<string, PLCinfomartion>();
        public Dictionary<string, PLCinfomartion> dicPLC3 = new Dictionary<string, PLCinfomartion>();
        public Dictionary<string, PLCinfomartion> dicPLC4 = new Dictionary<string, PLCinfomartion>();

        private void RangValueForm_Load(object sender, EventArgs e)
        {
            dicPLC1.Clear();
            dicPLC2.Clear();
            dicPLC3.Clear();
            dicPLC4.Clear();
            ShowMessage(0, 1);
            ShowMessage(0, 2);
            ShowMessage(1, 1);
            ShowMessage(1, 2);

            dgv_RangValue.Rows.Clear();
            int i = 1;
            foreach (var item in dicPLC1)
            {
                dgv_RangValue.Rows.Add(new object[] { i.ToString(), item.Value.PointAddress, item.Value.PointResult });
                i++;
            }
            ThradTest();
            station = 1;
            timer1.Enabled = true;
        }
        public void ThradTest()
        {
            Thread Thread = new Thread(ReadData);
            Thread.IsBackground = true;
            Thread.Start();
        }
        public void ReadData()
        {
            while (true)
            {
                Thread.Sleep(10);
                try
                {
                    if (station == 1)
                    {
                        foreach (var item in dicPLC1)
                        {
                            item.Value.PointResult = FormStart.pLCTool.ReadPLC(item.Value.PointAddress, "FLOAT").ToString();
                        }
                    }
                    else if (station ==2)
                    {
                        foreach (var item in dicPLC2)
                        {
                            item.Value.PointResult = FormStart.pLCTool.ReadPLC(item.Value.PointAddress, "FLOAT").ToString();
                        }
                    }
                    else if (station ==3)
                    {
                        foreach (var item in dicPLC3)
                        {
                            item.Value.PointResult = FormStart.pLCTool.ReadPLC(item.Value.PointAddress, "FLOAT").ToString();
                        }
                    }
                    else if (station ==4)
                    {
                        foreach (var item in dicPLC4)
                        {
                            item.Value.PointResult = FormStart.pLCTool.ReadPLC(item.Value.PointAddress, "FLOAT").ToString();
                        }
                    }
                }
                catch (Exception)
                {

                }
            }
        }
        /// <summary>
        /// 显示测距值
        /// </summary>
        /// <param name="Station">工位</param>
        /// <param name="index">电芯位置</param>
        public void ShowMessage(int Station, int index)
        {
            try
            {
               
                int Startaddress = 0;
                string strAddress = "";
                int iRead = 0;
                double paramValue = 0;
                int count = Convert.ToInt32(FormStart.dicPLC["单个模组测距点数量"].PointResult);
                if (count <= 0)
                    return;
                for (int pos = 1; pos <= count; pos++)
                {
                    if (Station == 0)
                    {
                        if (index == 1)
                        {
                            Startaddress = 0;
                            iRead = Startaddress + (4 * (pos - 1));
                            strAddress = "DB102." + iRead.ToString();
                            //paramValue = Double.Parse(Form2.ReadPLC(strAddress, "FLOAT").ToString());
                            dicPLC1.Add(pos.ToString(), new PLCinfomartion { PointAddress = strAddress, PointType = "FLOAT", PointName = pos.ToString(), PointResult = "0" });
                        }
                        else
                        {
                            //起始地值
                            Startaddress = 192;
                            iRead = Startaddress + (4 * (pos - 1));
                            strAddress = "DB102." + iRead.ToString();
                            //paramValue = Double.Parse(Form2.ReadPLC(strAddress, "FLOAT").ToString());
                            dicPLC2.Add(pos.ToString(), new PLCinfomartion { PointAddress = strAddress, PointType = "FLOAT", PointName = pos.ToString(), PointResult = "0" });
                        }

                    }
                    else
                    {
                        if (index == 1)
                        {
                            Startaddress = 384;
                            iRead = Startaddress + (4 * (pos - 1));
                            strAddress = "DB102." + iRead.ToString();
                            //paramValue = Double.Parse(Form2.ReadPLC(strAddress, "FLOAT").ToString());
                            dicPLC3.Add(pos.ToString(), new PLCinfomartion { PointAddress = strAddress, PointType = "FLOAT", PointName = pos.ToString(), PointResult = "0" });
                        }
                        else
                        {
                            //起始地值
                            Startaddress = 576;
                            iRead = Startaddress + (4 * (pos - 1));
                            strAddress = "DB102." + iRead.ToString();
                            //paramValue = Double.Parse(Form2.ReadPLC(strAddress, "FLOAT").ToString());
                            dicPLC4.Add(pos.ToString(), new PLCinfomartion { PointAddress = strAddress, PointType = "FLOAT", PointName = pos.ToString(), PointResult = "0" });
                        }
                    }
                }
            }
            catch (Exception)
            {


            }
        }

        private void btn_Online0_1_Click(object sender, EventArgs e)
        {
            station = 1;
        }

        private void btn_Online0_2_Click(object sender, EventArgs e)
        {
            station = 2;
        }

        private void btn_Online1_1_Click(object sender, EventArgs e)
        {
            station = 3;
        }

        private void btn_Online1_2_Click(object sender, EventArgs e)
        {
            station = 4;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (station == 1)
                {
                    btn_Online0_1.BackColor = Color.Green;
                    btn_Online0_2.BackColor = SystemColors.Control;
                    btn_Online1_1.BackColor = SystemColors.Control;
                    btn_Online1_2.BackColor = SystemColors.Control;
                    int i = 0;
                    foreach (var item in dicPLC1)
                    {
                        dgv_RangValue.Rows[i].Cells[1].Value = item.Value.PointAddress;
                        dgv_RangValue.Rows[i].Cells[2].Value = item.Value.PointResult;
                        i++;
                    }
                }
                else if (station == 2)
                {
                    btn_Online0_1.BackColor = SystemColors.Control;
                    btn_Online0_2.BackColor = Color.Green;
                    btn_Online1_1.BackColor = SystemColors.Control;
                    btn_Online1_2.BackColor = SystemColors.Control;
                    int i = 0;
                    foreach (var item in dicPLC2)
                    {
                        dgv_RangValue.Rows[i].Cells[1].Value = item.Value.PointAddress;
                        dgv_RangValue.Rows[i].Cells[2].Value = item.Value.PointResult;
                        i++;
                    }
                }
                else if (station == 3)
                {
                    btn_Online0_1.BackColor = SystemColors.Control;
                    btn_Online0_2.BackColor = SystemColors.Control;
                    btn_Online1_1.BackColor = Color.Green;
                    btn_Online1_2.BackColor = SystemColors.Control;
                    int i = 0;
                    foreach (var item in dicPLC3)
                    {
                        dgv_RangValue.Rows[i].Cells[1].Value = item.Value.PointAddress;
                        dgv_RangValue.Rows[i].Cells[2].Value = item.Value.PointResult;
                        i++;
                    }

                }
                else if (station == 4)
                {
                    btn_Online0_1.BackColor = SystemColors.Control;
                    btn_Online0_2.BackColor = SystemColors.Control;
                    btn_Online1_1.BackColor = SystemColors.Control;
                    btn_Online1_2.BackColor = Color.Green;
                    int i = 0;
                    foreach (var item in dicPLC4)
                    {
                        dgv_RangValue.Rows[i].Cells[1].Value = item.Value.PointAddress;
                        dgv_RangValue.Rows[i].Cells[2].Value = item.Value.PointResult;
                        i++;
                    }
                }
            }
            catch (Exception)
            {
             
            }
           

        }
    }
}
