using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UpperComputer
{
    public class ClassCANS
    {
        public static ClassCANS OLD = new ClassCANS();

        //扫码串口
        public String COM口 = "COM1";
        public int 波特率 = 9600;
        public int 数据位 = 8;
        public int 停止位 = 1;
        public int 校验 = 0;
        public string 小车码 = "";
        public string 模组码 = "";
        public double 打胶间隔时间管控 = 10;

        //PLC交互
        public string PLC触发扫码信号 = "SM";
        public string 扫码触发信号 = "T";
        public string 扫码回复信号 = "OK";
        public String PLC出站信号 = "END";
        public string CCD拍照信号 = "Stat";
        public string CCD完成信号 = "OK";
        public string 极柱寻址数据请求 = "ASK";
        public string CCD拍照结束信号 = "END";
        public string plc可焊接信号 = "Y";

        //测距
        public string 测距命令 = "M1";

        //plc连接地址
        public string PLCip1 = "192.168.0.1";
        public Int16 PROT = 102;
        public string s_PCIP = "192.168.66.192";
        //CCD1连接地址
        public string CCDIP = "127.0.0.1";
        public Int16 CCDPROT = 8000;

        //CCD2连接地址
        public string CCDIP1 = "127.0.0.1";
        public Int16 CCDPROT1 = 8000;

        //权限连接地址
        public string PERIP = "127.0.0.1";
        public Int16 PERPORT = 9001;

        //3D线扫连接地址
        public string CCDIP3D = "127.0.0.1";
        public Int16 CCDPROT3D = 8000;

        //扫码枪连接地址
        public string SCANIP = "127.0.0.1";
        public Int16 SCANPORT = 8000;
        //AGV连接地址
        public string AGVIP = "197.168.2.17";
        public Int16 AGVPORT = 9600;

        //AGV连接地址
        public string AGVIPRes = "197.168.2.17";
        public Int16 AGVPORTRes = 9000;

        public int 条码长度 = 10;
        public String 错误条码 = "ERCC";
        public List<string> OLCDSN = new List<string>();
        public String OLCANS = "";
        public bool 极柱寻址 = false;
        public bool 屏蔽扫码 = false;
        public string 工位固定条码 = "";
        public bool 启用MES = false;
        public string 当前配方 = "";
        public int nLen = 10;
        public int 胶水A物料编码长度 = 25;
        public int 胶水B物料编码长度 = 25;
        public string 工位1胶水A物料编码 = "";
        public string 工位1胶水B物料编码 = "";
        public string 工位2胶水A物料编码 = "";
        public string 工位2胶水B物料编码 = "";
        public string 胶水物料规则 = "";
        public bool 检测NG放行 = false;

        //MES参数      
        public string name = "";
        public string pasword = "";
        public string MachineNo = "";
        public string SessionId = "";
        public string url = "";
        public string MoNumber = "";
        public string groupCode = "";
        public string ftpurl;
        public int nOffLineTime = 1;
        public List<PLCinfomartion> PLClist = new List<PLCinfomartion>();
    }
    //没用上
    public class info
    {
        public static info ss = new info();
        public string i;
        public string b;
        public List<PLCinfomartion> PLClist = new List<PLCinfomartion>();
        [NonSerialized]
        public Dictionary<string, PLCinfomartion> dicPLC = new Dictionary<string, PLCinfomartion>();
    }
    public class PLCinfomartion
    {
        /// <summary>
        /// PLC点位名称
        /// </summary>
        public string PointName;
        /// <summary>
        /// PLC点位地址
        /// </summary>
        public string PointAddress;
        /// <summary>
        /// PLC点位类型
        /// </summary>
        public string PointType;
        /// <summary>
        /// PLC读取标志位
        /// </summary>
        public string PointFlag;
        /// <summary>
        /// PLC点位结果
        /// </summary>
        public string PointResult;
    }
    public class CCDOffer
    {
        //振镜要求，补偿值需要乘以1000
        public int X;
        public int Y;
    }
}
