using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static UpperComputer.mes;

namespace UpperComputer
{
    public partial class Formlogin : Form
    {
        string name = "";
        int WM_SYSCOMMAND = 0x0112,
 SC_MOVE = 0xF010,
 WM_NCHITTEST = 0x84,
 HTCAPTION = 2;//命中标题栏
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int iParam);

        [DllImport("Gdi32.DLL", EntryPoint = "CreateRoundRectRgn")]


        private extern static IntPtr CreateRoundRectRgn
            (
             int nLeftRect,
             int nTopRect,
             int nRightRect,
             int nBottonRect,
             int nWidthEllipse,
             int nHeightEllipse
            );
        public Formlogin()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
        }
        public static Dictionary<string, ResultPar> dicPar = new Dictionary<string, ResultPar>();
        private void button3_Click(object sender, EventArgs e)
        {
            bool bSTA = false;
            var mModels1 = MainForm1.txClasses.Find((c) => c.Name == "MES");
           
            if (!chkMes.Checked) //不连MES
            {
                if (mModels1 != null)
                    mModels1.bStatus = false;
                ClassCANS.OLD.启用MES = false;
                bSTA = true;              
            }
            else
            {
                if (mModels1 != null)
                    mModels1.bStatus = true;
                ClassCANS.OLD.启用MES = true;
                ClassCANS.OLD.name = txtUserName.Text;
                ClassCANS.OLD.pasword = txtPassWord.Text;
                ClassCANS.OLD.MachineNo = txtMachineNo.Text;
                ClassCANS.OLD.groupCode = txtGroupCode.Text;
                ClassCANS.OLD.MoNumber = txtMoNumber.Text;
                ClassCANS.OLD.url = txtUrl.Text;
                button3.Enabled = false;
                button3.Text = "登录中！";
                mes.Instance().Load(txtUrl.Text);
                string rt = mes.Instance().Login(ClassCANS.OLD.name,ClassCANS.OLD.pasword,ClassCANS.OLD.MachineNo,ClassCANS.OLD.MoNumber);
                if (rt.ToUpper() == "TRUE")
                {
                    bSTA = true;
                    //string rt2 = mes.Instance().GetParameter(ClassCANS.OLD.groupCode, ClassCANS.OLD.MachineNo, ClassCANS.OLD.SessionId, "", ClassCANS.OLD.MoNumber, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ref dicPar);
                    bool rt2 = GetParameterControl(ref dicPar);
                    if (!rt2)
                    {
                        bSTA = false;
                        MessageBox.Show($"登录失败，未获取到参数，原因：{rt2}");
                        button3.Enabled = true;
                        button3.Text = "登录";
                    }
                }
                else
                {
                    GetParameterControl(ref dicPar);
                    MessageBox.Show($"登录失败，原因:[{rt}]");
                    button3.Enabled = true;
                    button3.Text = "登录";
                }             
            }
            if (bSTA)
                this.Close();
            ClassXMLGET.xmlset<ClassCANS>("cans.xml", ClassCANS.OLD);
        }

        private bool GetParameterControl(ref Dictionary<string, ResultPar> dicPar)
        {
            try
            {
                Dictionary<string, ResultPar> dicTem = new Dictionary<string, ResultPar>();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Application.StartupPath + "//" + "ParamConfigure.xml");//加载本地xml文件
                XmlNodeList nodeList = xmlDoc.SelectSingleNode("ParamList").ChildNodes; //获取ParamList节点的所有子节点
                foreach (XmlNode xn in nodeList)//遍历所有子节点   多个Param节点（包含下面的子孙节点）数据
                {
                    XmlElement xe = (XmlElement)xn;//将子节点类型转换为XmlElement类型
                    dicTem.Add(xe.GetAttribute("name"), new ResultPar());
                    string name = xe.GetAttribute("name");
                    if (name == "左面积"|| name == "左面积比")
                    {
                        for (int i = 1; i <= 12; i++)
                        {
                            dicTem.Add(name + i, new ResultPar());
                            XmlNodeList nls = xe.ChildNodes;//继续获取xe子节点的所有子节点
                            foreach (XmlNode xn1 in nls)//遍历
                            {
                                XmlElement xe2 = (XmlElement)xn1;//转换类型
                                dicTem[name + i].paramName = name + i;
                                switch (xe2.Name)
                                {
                                    case "ParamFirstLower":
                                        dicTem[name + i].paramFirstLower = xe2.InnerText;//修改
                                        break;
                                    case "ParamFirstUpper":
                                        dicTem[name + i].paramFirstUpper = xe2.InnerText;//修改
                                        break;
                                    case "ParamUnit":
                                        dicTem[name + i].paramUnit = xe2.InnerText;//修改
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                    else if (name == "右面积"||name == "右面积比")
                    {
                        for (int i = 1; i <= 15; i++)
                        {
                            dicTem.Add(name + i, new ResultPar());
                            XmlNodeList nls = xe.ChildNodes;//继续获取xe子节点的所有子节点
                            foreach (XmlNode xn1 in nls)//遍历
                            {
                                XmlElement xe2 = (XmlElement)xn1;//转换类型
                                dicTem[name + i].paramName = name + i;
                                switch (xe2.Name)
                                {
                                    case "ParamFirstLower":
                                        dicTem[name + i].paramFirstLower = xe2.InnerText;//修改
                                        break;
                                    case "ParamFirstUpper":
                                        dicTem[name + i].paramFirstUpper = xe2.InnerText;//修改
                                        break;
                                    case "ParamUnit":
                                        dicTem[name + i].paramUnit = xe2.InnerText;//修改
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                    else
                    {

                        XmlNodeList nls = xe.ChildNodes;//继续获取xe子节点的所有子节点
                        foreach (XmlNode xn1 in nls)//遍历
                        {
                            XmlElement xe2 = (XmlElement)xn1;//转换类型
                            dicTem[name].paramName = name;
                            switch (xe2.Name)
                            {
                                case "ParamFirstLower":
                                    dicTem[name].paramFirstLower = xe2.InnerText;//修改
                                    break;
                                case "ParamFirstUpper":
                                    dicTem[name].paramFirstUpper = xe2.InnerText;//修改
                                    break;
                                case "ParamUnit":
                                    dicTem[name].paramUnit = xe2.InnerText;//修改
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
                dicPar = dicTem;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        private void Formlogin_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();

            //SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void Formlogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mes.Instance().mespost == null)
                ClassCANS.OLD.启用MES = false;
        }

        private void Formlogin_Load(object sender, EventArgs e)
        {
            ClassCANS.OLD = ClassXMLGET.xmlget<ClassCANS>("cans.xml");
            //FormStart.dicPLC = ClassCANS.OLD.PLClist.ToDictionary(key => key.PointName, value => value); //加载数据
            txtUserName.Text = ClassCANS.OLD.name;
            txtPassWord.Text = ClassCANS.OLD.pasword;
            txtMachineNo.Text = ClassCANS.OLD.MachineNo;
            txtMoNumber.Text = ClassCANS.OLD.MoNumber;
            txtGroupCode.Text = ClassCANS.OLD.groupCode;
            txtUrl.Text = ClassCANS.OLD.url;
            chkMes.Checked = ClassCANS.OLD.启用MES;
        }

        private void chkMes_CheckedChanged(object sender, EventArgs e)
        {

        }


        private void btn_Closed_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
