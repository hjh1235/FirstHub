using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
namespace UpperComputer
{
    public partial class SerialPortForm : Form
    {
        SerialPortCom SerialPort;
        public SerialPortForm()
        {
            InitializeComponent();
            SerialPort = new SerialPortCom();
        }
        public string m_strHardWare_Modle { get; set; }
        public string hardwareName { get; set; }
        public string hardwareTpye { get; set; }
        public string hardwareVender { get; set; }     
        /// <summary>
        /// 界面显示
        /// </summary>
        public void ShowFromMessage()
        {
            label1.Text = hardwareName;
            try
            {
                SerialPortCom SerialPort = (SerialPortCom)Communication_DateLoadData._Communication_DateTool[label1.Text];
                cmbBaudRate.Text = SerialPort.SerialPort_BaudRate;
                cmbPortName.Text = SerialPort.PortName;
                cmbDataBits.Text = SerialPort.cmbDataBits;
                cmbParity.Text = SerialPort._Parity;
                cmbStopBits.Text = SerialPort.cmbStopBits;
                txtSendPort1.Text = SerialPort.SerialPort_txtSendPort;
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// 打开串口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSPopen_Click(object sender, EventArgs e)
        {
            SerialPortCom SerialPort = (SerialPortCom)Communication_DateLoadData._Communication_DateTool[label1.Text];
            if (btnSPopen.Text == "打开串口")
            {
                if (SerialPort.Open() == true)
                {
                    btnSPopen.Text = "关闭串口";
                    lblPortInd.BackColor = Color.Green;
                }
            }
            else
            {
                SerialPort.Close();
                btnSPopen.Text = "打开串口";
                lblPortInd.BackColor = Color.Red;
            }
        }
        /// <summary>
        /// 串口选择项Change事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbPortName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPortName.Text  !="")
            {
                SerialPortCom SerialPort = null;
                SerialPort = (SerialPortCom)Communication_DateLoadData._Communication_DateTool[label1.Text];
                SerialPort.PortName = cmbPortName.Text;
            }
        }
        /// <summary>
        /// 发送按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSend_Click(object sender, EventArgs e)
        {
            SerialPortCom SerialPort = null;
            SerialPort = (SerialPortCom)Communication_DateLoadData._Communication_DateTool[label1.Text];
            SerialPort.Send(SerialPort.SerialPort_txtSendPort);
        }
        /// <summary>
        /// 波特率选择项Change事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbBaudRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBaudRate.Text !="")
            {
                SerialPortCom SerialPort = null;
                SerialPort = (SerialPortCom)Communication_DateLoadData._Communication_DateTool[label1.Text];
                SerialPort.SerialPort_BaudRate = cmbBaudRate.Text;
            }
        }
        /// <summary>
        /// 数据位设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDataBits_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDataBits.Text !="")
            {
                SerialPortCom SerialPort = null;
                SerialPort = (SerialPortCom)Communication_DateLoadData._Communication_DateTool[label1.Text];
                SerialPort.cmbDataBits = cmbDataBits.Text;
            }
        }
        /// <summary>
        /// 奇偶性设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbParity_SelectedIndexChanged(object sender, EventArgs e)
        {
            SerialPortCom SerialPort = null;
            SerialPort = (SerialPortCom)Communication_DateLoadData._Communication_DateTool[label1.Text];
            if (cmbParity.Text !="")
            {
                SerialPort._Parity = cmbParity.Text;          
            }
        }
        /// <summary>
        /// 停止位设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbStopBits_SelectedIndexChanged(object sender, EventArgs e)
        {
            SerialPortCom SerialPort = null;
            SerialPort = (SerialPortCom)Communication_DateLoadData._Communication_DateTool[label1.Text];
            if (cmbStopBits.Text !="")
            {
              SerialPort.cmbStopBits = cmbStopBits.Text;           
            }
        }
        /// <summary>
        /// 发送内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSendPort1_TextChanged(object sender, EventArgs e)
        {
            SerialPortCom SerialPort = null;
            SerialPort = (SerialPortCom)Communication_DateLoadData._Communication_DateTool[label1.Text];
            SerialPort.SerialPort_txtSendPort = txtSendPort1.Text;
        }
        /// <summary>
        /// 已摒弃
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbBaudRate_Validated(object sender, EventArgs e)
        {
            //Hard_Ward_Contral.S_PortDate_Save.m_HardWareDictionary[label1.Text].SerialPort_PCom = cmbPortName.Text;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                SerialPortCom SerialPort = null;
                SerialPort = (SerialPortCom)Communication_DateLoadData._Communication_DateTool[label1.Text];
                foreach (var item in Communication_DateLoadData._Communication_DateTool)
                {
                    try
                    {
                        txtRecPort1.Text = SerialPort.DataReceivedstr;
                        if (item.Value.Communication_DateName == label1.Text)
                        {
                            if (SerialPort.COMM.IsOpen == true)
                            {
                                lblPortInd.BackColor = Color.Green;
                                btnSPopen.Text = "关闭串口";
                            }
                            else
                            {
                                lblPortInd.BackColor = Color.Red;
                                btnSPopen.Text = "打开串口";
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                    }
                }

            }
            catch 
            {

               
            }
          
        }
        private void SerialPortForm_Load(object sender, EventArgs e)
        {
        }
        public void show()
        {
            SerialPort = (SerialPortCom)Communication_DateLoadData._Communication_DateTool["PORT"];
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            SerialPortCom SerialPort = null;
            SerialPort = (SerialPortCom)Communication_DateLoadData._Communication_DateTool[label1.Text];
            SerialPort.ClearData();
        }
        //public 
        private void txtRecPort1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
