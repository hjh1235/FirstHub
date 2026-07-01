using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UpperComputer;

namespace UpperComputer
{
    public partial class SettingForm : Form
    {
        string path = System.Environment.CurrentDirectory + "\\Parameter\\Communication_Date.xml";
        public SettingForm()
        {
            InitializeComponent();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (FormUserLogoin.userLevel < 3)
            {
                MessageBox.Show("权限不足", "温馨提示");
                return;
            }
            int aLen = 0, bLen = 0;
            if (!int.TryParse(txt_胶水A编码长度.Text, out aLen))
            {
                MessageBox.Show("保存失败，胶水A编码长度格式不正确", "提示");
                return;
            }
            //if (!int.TryParse(txt_胶水B编码长度.Text, out aLen))
            //{
            //    MessageBox.Show("保存失败，胶水B编码长度格式不正确", "提示");
            //    return;
            //}
            try
            {
                Communication_DateLoadData.SaveFile();
                ClassCANS.OLD.屏蔽扫码 = ck_Scan.Checked;
                ClassCANS.OLD.当前配方 = cmb_ModeName.Text;
                ClassCANS.OLD.nLen = int.Parse(txt_Len.Text);
                ClassCANS.OLD.胶水A物料编码长度 = aLen;
                ClassCANS.OLD.检测NG放行 = ck_检测NG放行.Checked;
                //ClassCANS.OLD.胶水B物料编码长度 = bLen;
                bool rt = ClassXMLGET.xmlset<ClassCANS>("cans.xml", ClassCANS.OLD);

                if (rt == true)
                {
                    MessageBox.Show("保存成功");
                }
                else
                {
                    MessageBox.Show("保存失败");
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("保存失败,原因:" + error.Message);
            }

        }

        private void SettingForm_Load(object sender, EventArgs e)
        {
            //MesLoadParForm._CallBack += MesLoadParForm__CallBack;
            GetXMLName();
            ck_Scan.Checked = ClassCANS.OLD.屏蔽扫码;
            txt_Station0.Text = ClassCANS.OLD.工位固定条码;
            cmb_ModeName.Text = ClassCANS.OLD.当前配方;
            txt_Len.Text = ClassCANS.OLD.nLen.ToString();
            txt_胶水A编码长度.Text = ClassCANS.OLD.胶水A物料编码长度.ToString();
            ck_检测NG放行.Checked= ClassCANS.OLD.检测NG放行;
            //txt_胶水B编码长度.Text = ClassCANS.OLD.胶水B物料编码长度.ToString();

            try
            {
                //显示串口列表
                foreach (var item in Communication_DateLoadData._Communication_DateTool.Values)
                {
                    if (item.Communication_DateType == "串口")
                    {
                        串口列表.Items.Add(item.Communication_DateName);
                    }
                }
            }
            catch
            {

            }
        }

        public void GetXMLName()
        {
            cmb_ModeName.Items.Clear();
            string strPath = MesLoadParForm.strPath;
            if (!Directory.Exists(strPath))
            {
                Directory.CreateDirectory(strPath);
            }
            var filesLeft = Directory.GetFiles(strPath, "*.xml");
            foreach (var file in filesLeft)
            {
                string fileNameExt = file.Substring(file.LastIndexOf("\\") + 1); //获取文件名，不带路径
                string filePath = file.Substring(0, file.LastIndexOf("\\"));//获取文件路径，不带文件名   
                string name = fileNameExt.Substring(0, fileNameExt.LastIndexOf(".")); //获取文件名，不带后缀
                cmb_ModeName.Items.Add(name);
            }
            if (cmb_ModeName.Items.Contains(ClassCANS.OLD.当前配方))
            {
                cmb_ModeName.Text = ClassCANS.OLD.当前配方;
            }
            else
            {
                if (cmb_ModeName.Items.Count > 0)
                    cmb_ModeName.SelectedIndex = 0;
            }
        }
        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void txt_Statiton0_TextChanged(object sender, EventArgs e)
        {
            ClassCANS.OLD.工位固定条码 = txt_Station0.Text;
        }

        private void txt_Station1_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmb_ModeName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button_添加_Click(object sender, EventArgs e)
        {
            串口列表.Items.Add(textBox_串口名称.Text);
            SerialPortCom _SerialPortCom = new SerialPortCom();
            _SerialPortCom.Communication_DateName = textBox_串口名称.Text;
            _SerialPortCom.Communication_DateType = "串口";
            _SerialPortCom.Communication_DateVender = textBox_串口名称.Text;
            _SerialPortCom.Communication_DatePath = path;
            try
            {
                Communication_DateLoadData.tool.Add(_SerialPortCom);
                Communication_DateLoadData._Communication_DateTool = Communication_DateLoadData.tool.ToDictionary(p => p.Communication_DateName);
            }
            catch (Exception EX)
            {
            }
        }

        private void 串口列表_Click(object sender, EventArgs e)
        {
        }

        private void 串口列表_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ListBox listBox = (ListBox)sender;
                SerialPortForm serialPortForm = new SerialPortForm();
                serialPortForm.hardwareName = 串口列表.SelectedItem.ToString();
                if (panel_串口.Controls.Count > 0)
                {
                    panel_串口.Controls.RemoveAt(0);
                }
                serialPortForm.TopLevel = false;
                panel_串口.Controls.Add(serialPortForm);
                serialPortForm.Size = panel_串口.Size;
                serialPortForm.Show();
                serialPortForm.ShowFromMessage();
            }
            catch
            {
                panel_串口.Controls.RemoveAt(0);
            }
        }

        private void button_删除串口_Click(object sender, EventArgs e)
        {
            Communication_DateLoadData._Communication_DateTool.Remove(串口列表.SelectedItems[0].ToString());
            Communication_DateLoadData.tool.RemoveAt(串口列表.SelectedIndex);
            串口列表.Items.Remove(串口列表.SelectedItem);
        }

        private void ck_Scan_CheckedChanged(object sender, EventArgs e)
        {
            if (ck_Scan.Checked)
            {
                txt_Station0.Enabled = true;
            }
            else
            {
                txt_Station0.Enabled = false;
            }
        }
    }
}
