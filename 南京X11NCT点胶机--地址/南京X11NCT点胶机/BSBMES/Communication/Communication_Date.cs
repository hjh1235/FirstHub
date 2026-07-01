
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace UpperComputer
{
    [Serializable]
    public class Communication_DateTool
    {
        List<Communication_DateTool> _Communication_DateTool;
        public long timer;
        /// <summary>     
        /// 对象名
        /// </summary>
        public string Communication_DateName { get; set; }
        /// <summary>     
        /// 对象名类型
        /// </summary>
        public string Communication_DateType { get; set; }
        /// <summary>     
        /// 对象名说明
        /// </summary>
        public string Communication_DateVender { get; set; }

        /// <summary>     
        /// 对象文件路径
        /// </summary>
        public string Communication_DatePath { get; set; }
        /// <summary>     
        /// 接收值
        /// </summary>
        public string RecvResult { get; set; }
    }

    public class Communication_DateBLL
    {
        /// <summary>
        /// 反序列化对象
        /// </summary>
        /// <param name="str_path"></param>
        /// <returns></returns>
        public static object Read(string str_path)
        {
            return Communication_DateDAL.Read(str_path);
        }
        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <param name="o"></param>
        /// <param name="str_path"></param>
        /// <returns></returns>
        public static bool Write(object o, string str_path)
        {
            return Communication_DateDAL.Write(o, str_path);
        }
       
    }

    public class Communication_DateDAL
    {
        /// <summary>
        /// 序列化所有工具
        /// </summary>
        /// <param name="o">面對對象</param>
        /// <param name="str_path"></param>
        public static bool Write(object o, string str_path)
        {
            try
            {
                using (FileStream fs = new FileStream(str_path, FileMode.Create, FileAccess.Write))//保存数据
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(fs, o);//序列化所有工具
                }
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 反序列化所有工具
        /// </summary>
        /// <param name="str_path"></param>
        /// <returns></returns>
        public static object Read(string str_path)
        {
            try
            {
                using (FileStream fs = new FileStream(str_path, FileMode.Open, FileAccess.Read))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    return bf.Deserialize(fs);//反序列化所有工具
                }
            }
            catch (System.Exception ex)
            {
                //MessageBox.Show("数据读取失败!" + ex.Message);
                return null;
            }
        }
    }

    public class Communication_DateLoadData
    {
        public static List<Communication_DateTool> tool;
        public static Dictionary<string, Communication_DateTool> _Communication_DateTool = new Dictionary<string, Communication_DateTool>();
        static string path = System.Environment.CurrentDirectory + "\\Parameter\\Communication_Date.xml";
        public Communication_DateLoadData()
        {
        }
        public static Dictionary<string, Communication_DateTool> ReadData(string Path)
        {
            try
            {
                _Communication_DateTool.Clear();
                 tool.Clear();
                 tool = Communication_DateBLL.Read(path/*@".//Parameter/FlowChar_Control" + ".xml"*/) as List<Communication_DateTool>;//反序列化所有工具
                 Communication_DateLoadData._Communication_DateTool = Communication_DateLoadData.tool.ToDictionary(p => p.Communication_DateName);
            }
            catch 
            {
            }
           
            return _Communication_DateTool;
        }

        public static void Load()
        {
            try
            {
                tool = Communication_DateBLL.Read(path) as List<Communication_DateTool>;//反序列化所有工具
                Communication_DateLoadData._Communication_DateTool = Communication_DateLoadData.tool.ToDictionary(p => p.Communication_DateName);   
            }
            catch (System.Exception ex)
            {
                tool = new List<Communication_DateTool>();
            }
            if (tool == null)
            {
                tool = new List<Communication_DateTool>();
            }
        }

        public static void SaveFile(/*string path*/)
        {
            try
            {
              bool ok=   Communication_DateDAL.Write(tool, path);
            }
            catch
            {
            }
        }
    }

    public partial class SerialPortDate_Save
    {
        public List<SerialPortDate> m_HardWareList;
        [XmlIgnore]
        
        public Dictionary<string, SerialPortDate> m_HardWareDictionary;
        public SerialPortDate_Save()
        {
            m_HardWareList = new List<SerialPortDate>();
            m_HardWareDictionary = new Dictionary<string, SerialPortDate>();
        }
        /// <summary>
        /// 通讯数据加载
        /// </summary>
        /// <returns></returns>
        public static SerialPortDate_Save LoadObj()
        {
            SerialPortDate_Save pDoc;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(SerialPortDate_Save));
            FileStream fsReader = null;
            try
            {
                fsReader = File.OpenRead(@".//Parameter/SerialPortDate_Save" + ".xml");
                pDoc = (SerialPortDate_Save)xmlSerializer.Deserialize(fsReader);
                fsReader.Close(); 
            }
            catch //(Exception eMy)
            {
                if (fsReader != null)
                {
                    fsReader.Close();
                }
                pDoc = new SerialPortDate_Save();
            }
            return pDoc;
        }
        /// <summary>
        /// 通讯数据保存
        /// </summary>
        /// <returns></returns>
        public bool SaveDoc()
        {
            if (!Directory.Exists(@".//Parameter/"))
            {
                Directory.CreateDirectory(@".//Parameter/");
            }
            FileStream fsWriter = new FileStream(@".//Parameter/SerialPortDate_Save" + ".xml", FileMode.Create, FileAccess.Write, FileShare.Read);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(SerialPortDate_Save));
            xmlSerializer.Serialize(fsWriter, this);
            fsWriter.Close();

            return true;
        }
    }
    /// <summary>
    /// 通讯数据类
    /// </summary>
    public partial class SerialPortDate
    {
        public string m_strHardWare_Modle { get; set; }
        public string hardwareName { get; set; }
        public string hardwareTpye { get; set; }
        public string hardwareVender { get; set; }
        /// <summary>
        ///  串口号数据
        /// </summary>
        public string SerialPort_PCom { get; set; }
        /// <summary>
        ///  串口波特率数据
        /// </summary>
        public int  SerialPort_BaudRate { get; set; }
        /// <summary>
        ///  串口数据位数据
        /// </summary>
        public int  SerialPort_DataBits { get; set; }
        /// <summary>
        ///  串口校验位数据
        /// </summary>
        public string SerialPort_Parity { get; set; }
        /// <summary>
        ///  串口停止位数据
        /// </summary>
        public string SerialPort_StopBits { get; set; }
        /// <summary>
        ///  串口发送数据
        /// </summary>
        public string SerialPort_txtSendPort { get; set; }
        //串口接受数据
        public string SerialPort_RecPort { get; set; }
        /// <summary>
        ///  通讯链接状态
        /// </summary>
        public bool _Conn { get; set; }
        /// <summary>
        ///  网络服务端客户端IP数据
        /// </summary>
        public string Socket_client_IP { get; set; }
        /// <summary>
        ///  网络服务端客户端端口号数据
        /// </summary>
        public int  Socket_client_Port { get; set; }
        /// <summary>
        ///  网络服务端客户端发送数据
        /// </summary>
        public string Socket_client_txtSend { get; set; }
        /// <summary>
        ///  网络服务端客户端界接受数据
        /// </summary>
        public string Socket_client_Rec { get; set; }


    }





}
