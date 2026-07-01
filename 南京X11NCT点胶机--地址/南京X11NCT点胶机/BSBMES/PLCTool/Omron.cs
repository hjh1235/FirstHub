using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace UpperComputer
{

    public class OmronPlCUDP : PLC
    {
        #region 字段定义

        private string _plcIp = "192.168.66.33";
        private int _plcPort = 9600;
        private string _pcIp = "192.168.66.192";
        private Socket server = null;       //Socket Server
        IPEndPoint plcIpEndPoint = null;
        IPEndPoint receiveEndPoint = null;
        EndPoint remote = null;
        private bool _isOpen = false;   //是否打开连接
        private int timeOut = 2000;
        #endregion
        public OmronPlCUDP(string plcIP, int plcPort, string pcIP, int Timeout = 2000)
        {
            _plcIp = plcIP;
            _plcPort = plcPort;
            _pcIp = pcIP;
            timeOut = Timeout;
        }

        #region 枚举定义
        public enum Region
        {
            DM = 0x82,
            WR = 0x71,
            WR_WORD = 0xB1,
            DM_BIT = 0x02,
            CIO_BIT = 0x30,
            WR_BIT = 0x31,
            HR_BIT = 0x32,
            AR_BIT = 0x33,

        }
        #endregion

        #region 属性定义

        /// <summary>
        /// PLC的IP地址
        /// </summary>
        public string PlcIp
        {
            get
            {
                return _plcIp;
            }
        }
        /// <summary>
        /// PLC的端口号
        /// </summary>
        public int PlcPort
        {
            get
            {
                return _plcPort;
            }
        }

        public string PcIp
        {
            get
            {
                return _pcIp;
            }
        }

        #endregion

        #region 实现BaseEquip成员


        //private  bool Read(string block, int start, int len, string type, out object[] buff)
        //{
        //    buff = new object[len];
        //    try
        //    {
        //        if (len > 256)
        //        {
        //            for (int i = 0; i < len; i++)
        //            {
        //                buff[i] = 0;
        //            }
        //            base.State = false;
        //            return false;
        //        }
        //        int maxOneLen = 100;                    //单次允许读取的最大长度，欧姆龙限制为100个字
        //        int count = len / maxOneLen;            //要读取的次数
        //        int mod = len % maxOneLen;              //剩余的长度
        //        bool flag = true;                       //保存读取标志
        //        for (int i = 0; i < count; i++)
        //        {
        //            object[] _buff = new object[maxOneLen];
        //            flag = this.ReadByLen(block, start + i * maxOneLen, maxOneLen, type, out _buff);
        //            if (flag == false)
        //            {
        //                base.State = flag;
        //                return false;
        //            }
        //            for (int k = i * maxOneLen; k < (i + 1) * maxOneLen; k++)
        //            {
        //                buff[k] = _buff[k - i * maxOneLen];
        //            }
        //        }
        //        if (mod > 0)
        //        {
        //            object[] _buff = new object[mod];
        //            flag = this.ReadByLen(block, start + count * maxOneLen, mod, type, out _buff);
        //            if (flag == false)
        //            {
        //                base.State = flag;
        //                return false;
        //            }
        //            for (int k = count * maxOneLen; k < count * maxOneLen + mod; k++)
        //            {
        //                buff[k] = _buff[k - count * maxOneLen];
        //            }
        //        }
        //        base.State = flag;
        //        return flag;
        //    }
        //    catch (Exception ex)
        //    {
        //        base.State = false;
        //        return false;
        //    }
        //}

        ///// <summary>
        ///// 单次读取最长100个字的方法
        ///// </summary>
        ///// <param name="block">块号</param>
        ///// <param name="start">起始字</param>
        ///// <param name="len">长度，最长不超过100</param>
        ///// <param name="buff">数据缓冲区，存放读取的数据</param>
        ///// <returns>读取成功返回true，读取失败返回false</returns>
        //private bool ReadByLen(string block, int start, int len, string type, out object[] buff)
        //{
        //    lock (this)
        //    {
        //        buff = new object[len];
        //        if (!this.Open())
        //        {
        //            return false;
        //        }
        //        int state = len;
        //        byte[] _buff = new byte[len * 2];
        //        int iblock = Convert.ToInt32(block);
        //        byte[] readCmd = this.GetReadCmd(iblock + start, len);
        //        int size = this.server.SendTo(readCmd, readCmd.Length, SocketFlags.None, this.plcIpEndPoint);
        //        byte[] buffer = new byte[len * 2 + 14];
        //        if (size < readCmd.Length)
        //        {
        //            //ICSharpCode.Core.LoggingService.Warn("PLC读取失败：" + this.GetErrInfo(result));
        //            this.State = false;
        //            return false;
        //        }
        //        else
        //        {
        //            int recv = server.ReceiveFrom(buffer, ref this.remote);//返回收到的字节数
        //            if (recv != 0)
        //            {
        //                //for (int i = 0; i < len; i++)
        //                //{
        //                //    byte[] a = new byte[2];
        //                //    a[0] = buffer[15 + i * 2];
        //                //    a[1] = buffer[14 + i * 2];
        //                //    _buff[2 * i + 0] = a[0];
        //                //    _buff[2 * i + 1] = a[1];
        //                //}

        //                for (int i = 0; i < len; i++)
        //                {
        //                    byte[] a = new byte[4];
        //                    a[0] = buffer[17 + i * 2];
        //                    a[1] = buffer[16 + i * 2];
        //                    a[2] = buffer[15 + i * 2];
        //                    a[3] = buffer[14 + i * 2];
        //                    _buff[2 * i + 0] = a[0];
        //                    _buff[2 * i + 1] = a[1];
        //                    _buff[2 * i + 0] = a[2];
        //                    _buff[2 * i + 1] = a[3];
        //                }
        //            }
        //            this.State = true;
        //        }
        //        int iReadLen = len;
        //        if (iReadLen > state)
        //        {
        //            iReadLen = state;
        //        }
        //        if (type == "SHORT")
        //        {
        //            for (int i = 0; i < iReadLen; i++)
        //            {
        //                int value = 0;
        //                value = BitConverter.ToInt16(_buff, 0);
        //                //int.TryParse(_buff[i].ToString(), out value);
        //                //if (value > ushort.MaxValue)
        //                //{
        //                //    value = ushort.MaxValue - value;
        //                //}
        //                buff[i] = value;
        //            }
        //        }
        //        else if (type == "BOOL")
        //        {
        //            for (int i = 0; i < iReadLen; i++)
        //            {
        //                int value = 0;
        //                value = BitConverter.ToInt16(_buff, 0);
        //                //int.TryParse(_buff[i].ToString(), out value);
        //                //if (value > ushort.MaxValue)
        //                //{
        //                //    value = ushort.MaxValue - value;
        //                //}
        //                buff[i] = value;
        //            }
        //        }
        //        else if (type == "FLOAT")
        //        {
        //            for (int i = 0; i < iReadLen; i++)
        //            {
        //                BitConverter.to
        //                //buff[i] = value;
        //            }
        //        }
        //        else if (type == "STRING")
        //        {
        //            for (int i = 0; i < iReadLen; i++)
        //            {
        //                byte[] s = new byte[1];
        //                s[0] = _buff[i];
        //                string str = System.Text.Encoding.ASCII.GetString(s);
        //                buff[i] = str;
        //            }
        //        }
        //        return true;
        //    }
        //}


        //private  bool Write(int block, int start, object[] buff)
        //{
        //    lock (this)
        //    {
        //        byte[] writeCmd = this.GetWriteCmd(block + start, buff);
        //        try
        //        {
        //            int size = this.server.SendTo(writeCmd, writeCmd.Length, SocketFlags.None, this.plcIpEndPoint);
        //            if (size < writeCmd.Length)
        //            {
        //                return false;
        //            }
        //            this.server.ReceiveTimeout = 1000;
        //            byte[] buffer = new byte[20];
        //            int recv = server.ReceiveFrom(buffer, ref remote);
        //            if (recv != 0)
        //            {
        //                return true;
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            return false;
        //        }
        //    }
        //}

        object objRead = new object();
        /// <summary>
        /// 单个读取PLC数据
        /// </summary>
        /// <param name="block"></param>
        /// <param name="start"></param>
        /// <param name="len"></param>
        /// <param name="type"></param>
        /// <param name="buff"></param>
        /// <returns></returns>
        private bool Read(string address, DataType dataType,int len, ref object value)
        {
            lock (objRead)
            {
                System.Threading.Thread.Sleep(1);
                value = "ERROR";
                try
                {
                    if (!State)
                    {
                        return false;
                    }
                    int block = 0, reclen = 0, sendlen = 0;
                    string region = "00";
                    byte bit = 0;
                    if (!GetRegion(address, dataType,len, ref block, ref region, ref bit, ref sendlen, ref reclen))
                    {
                        return false;
                    }
                    byte[] readCmd = this.GetReadCmd(block, sendlen, bit, region);
                    int size = this.server.SendTo(readCmd, readCmd.Length, SocketFlags.None, this.plcIpEndPoint);
                    byte[] buffer = new byte[reclen + 14];
                    byte[] buff = new byte[reclen];
                    if (size < readCmd.Length)
                    {
                        return false;
                    }
                    else
                    {
                        int recv = server.ReceiveFrom(buffer, ref this.remote);//返回收到的字节数
                        if (recv != 0 && recv > 14)
                        {
                            string rt = ByteArrayToHexString(buffer);
                            string strResult = rt.Substring(24,4);
                            string strRead = rt.Substring(20, 4);
                            if (strRead != "0101")
                            {
                                return false;
                            }
                            //12,13位表示读取结果，成功为0000
                            if (!(strResult=="0000"||strResult=="0040"))
                            {
                                return false;
                            }
                            string str = "";
                            //bool 类型
                            if (dataType == DataType.BOOL)
                            {
                                buff[0] = buffer[14];
                                if (!(buffer[15] == 0 || buffer[15] == 1))
                                {
                                    return false;
                                }
                                if (buffer[15] == 1)
                                {
                                    return false;
                                }
                            }
                            else
                            {

                                for (int i = 0; i < sendlen; i++)
                                {
                                    buff[i * 2] = buffer[15 + i * 2];
                                    buff[i * 2 + 1] = buffer[14 + i * 2];
                                }
                            }
                            //解析接收到的数据
                            if (!byteToValue(dataType, buff, ref value))
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
              
                return true;
            }
        }
        object objWrite = new object();
        private bool Write(string address, DataType dataType, int len, object value)
        {
            lock (objWrite)
            {
                try
                {
                    int block = 0, sendlen = 0, reclen = 0;
                    byte bit = 0;
                    string region = "";
                    if (!GetRegion(address, dataType,len,ref block, ref region, ref bit, ref sendlen, ref reclen))
                    {
                        return false;
                    }
                    byte[] b = new byte[1];
                    if (!GetStrValue(dataType, value,sendlen, ref b))
                    {
                        return false;
                    }
                    byte[] writeCmd = this.GetWriteCmd(block, region, bit, dataType, sendlen, b);
                    try
                    {
                        int size = this.server.SendTo(writeCmd, writeCmd.Length, SocketFlags.None, this.plcIpEndPoint);
                        if (size < writeCmd.Length)
                        {
                            return false;
                        }
                        byte[] buffer = new byte[20];
                        int recv = server.ReceiveFrom(buffer, ref remote);
                        if (recv != 0 && recv >= 14)
                        {
                            string rt = ByteArrayToHexString(buffer);
                            string strResult = rt.Substring(24, 4);
                            string strWrite= rt.Substring(20, 4);
                            if (strWrite != "0102")
                            {
                                return false;
                            }
                            //12,13位表示读取结果，成功为0000
                            if (!(strResult == "0000" || strResult == "0040"))
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }              
                return true;
            }
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 获取IP地址中最后一部分的值，比如：192.168.1.50，则返回50
        /// </summary>
        /// <param name="ip">ip地址</param>
        /// <returns>返回IP地址中最后一部分的值</returns>
        private byte GetLastIpScopeValue(string ip)
        {
            try
            {
                IPAddress ipAddress = IPAddress.Parse(ip);
                byte[] ipValues = ipAddress.GetAddressBytes();
                return ipValues[ipValues.Length - 1];
                //return ipValues;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// 获取读取数据的指令
        /// </summary>
        /// <param name="start">起始地址</param>
        /// <param name="len">要读取的长度</param>
        /// <param name="region">区块区域</param>
        /// <returns>返回合法的读取指令</returns>
        private byte[] GetReadCmd(int start, int len,byte bit, string region)
        {
            //开始地址
            byte[] starts = BitConverter.GetBytes((short)start);
                   
            string blockStr = starts[1].ToString("X2");
            blockStr += starts[0].ToString("X2");

            //位
            string bitStr =bit.ToString("X2").PadLeft(2,'0');

            //本机IP
            string pcIp = this.GetLastIpScopeValue(this.PcIp).ToString();
            pcIp = BitConverter.ToString(new byte[] { Convert.ToByte(pcIp) }, 0);

            //PLCIP
            string plcIp = this.GetLastIpScopeValue(this.PlcIp).ToString();
            plcIp = BitConverter.ToString(new byte[] { Convert.ToByte(plcIp) }, 0);

            //长度
            byte[] lens = BitConverter.GetBytes((short)len);
            string lensStr = lens[1].ToString("X2");
            lensStr += lens[0].ToString("X2");



            //byte[] readCmd = new byte[18];
            //readCmd[0] = 0X80;
            //readCmd[1] = 0X00;
            //readCmd[2] = 0X02;
            //readCmd[3] = 0X00;
            ////下标从0开始，第4个字节表示PLC的IP地址（IP地址4部分的最后一部分的值）
            //readCmd[4] = this.GetLastIpScopeValue(this.PlcIp);        //对方IP地址，即PLC
            ////readCmd[4] = 0x21;
            //readCmd[5] = 0X00;
            //readCmd[6] = 0X00;
            ////下标从0开始，第7个字节表示PC的IP地址（IP地址4部分的最后一部分的值）
            ////readCmd[7] = 0X01;  // this.GetLastIpScopeValue(this.PcIp);           //本机IP地址，即上位机电脑，只要是大于0的值就行
            //readCmd[7] = this.GetLastIpScopeValue(this.PcIp);
            //readCmd[8] = 0X00;
            //readCmd[9] = 0X00;
            //readCmd[10] = 0X01;
            //readCmd[11] = 0X01;         //0101 读取数据
            //readCmd[12] = region;         //82 地址区域DM区
            //                                    //下标从0开始，第13和14这2个字节表示起始地址
            //readCmd[13] = starts[1];    //读/写数据区的起始地址
            //readCmd[14] = starts[0];    //读/写数据区的起始地址
            //readCmd[15] = bit;
            ////下标从0开始，第16和17这2个字节表示读取数据的长度
            //readCmd[16] = lens[1];      //读/写数据个数
            //readCmd[17] = lens[0];      //读/写数据个数

            string sendValue = $"80000200{plcIp}0000{pcIp}00000101{region}{blockStr}{bitStr}{lensStr}";
            return HexStringToByteArray(sendValue);
        }

        /// <summary>
        /// 获取写入PLC的指令
        /// </summary>
        /// <param name="start"></param>
        /// <param name="buff"></param>
        /// <returns></returns>
        private byte[] GetWriteCmd(int block, string region, byte b, DataType dataType,int sendlen, byte[] buff)
        {
            Random ra = new Random(unchecked((int)DateTime.Now.Ticks));
            int SID = ra.Next(1, 100);
          
            byte[] startBytes = new byte[2];
            string value = "";
            //块
            byte[] blockArr = BitConverter.GetBytes((short)block);
            string blockStr = blockArr[1].ToString("X2");
            blockStr += blockArr[0].ToString("X2");

            //位
            string bitStr = b.ToString("X2").PadLeft(2,'0');
            //长度
            byte[] lens = BitConverter.GetBytes((short)sendlen);
            string lensStr = lens[1].ToString("X2");
            lensStr += lens[0].ToString("X2");

            //本机IP
            string pcIp = this.GetLastIpScopeValue(this.PcIp).ToString();
            pcIp = BitConverter.ToString(new byte[] { Convert.ToByte(pcIp) }, 0);

            //PLCIP
            string plcIp = this.GetLastIpScopeValue(this.PlcIp).ToString();
            plcIp = BitConverter.ToString(new byte[] { Convert.ToByte(plcIp) }, 0);

            if (dataType == DataType.BOOL)
            {
                byte[] writeCmda = new byte[1];
                for (int i = 0; i < sendlen; i++)
                {
                    writeCmda[0] = buff[0];
                    value += ByteArrayToHexString(writeCmda);
                }
            }
            else
            {
              
                for (int i = 0; i < sendlen; i++)
                {
                    byte[] writeCmda = new byte[2];
                    writeCmda[0] = buff[i * 2 + 1];
                    writeCmda[1] = buff[i * 2];
                    value += ByteArrayToHexString(writeCmda);
                }
            }
          
            string sendValue = $"80000200{plcIp}0000{pcIp}00000102{region}{blockStr}{bitStr}{lensStr}{value}";
            return HexStringToByteArray(sendValue);

            //writeCmd[0] = 0X80;             //ICF
            //writeCmd[1] = 0X00;             //RSV
            //writeCmd[2] = 0X02;             //GCT
            //writeCmd[3] = 0X00;             //DNA
            //                                //下标从0开始，第4个字节表示PLC的IP地址（IP地址4部分的最后一部分的值）
            //writeCmd[4] = this.GetLastIpScopeValue(this.PlcIp);          //DA1，对方IP地址，即PLC
            //writeCmd[5] = 0X00;             //DA2
            //writeCmd[6] = 0X00;             //SNA
            //                                //下标从0开始，第7个字节表示PC的IP地址（IP地址4部分的最后一部分的值）
            //writeCmd[7] = 0X01;     // this.GetLastIpScopeValue(this.PcIp);           //SA1，本机IP地址，即上位机电脑，只要是大于0的值就行
            //writeCmd[8] = 0X00;              //SA2
            //writeCmd[9] = Convert.ToByte(SID.ToString(), 16);//SID //0x00;
            //writeCmd[10] = 0X01;            //Command code
            //writeCmd[11] = 0X02;            //Command code，0101表示读取，0102 写入数据
            //writeCmd[12] = region;            //82 地址区域DM区
            //                                        //下标从0开始，第13和14这2个字节表示起始地址
            //writeCmd[13] = startBytes[1];
            //writeCmd[14] = startBytes[0];
            //writeCmd[15] = b;//bit位
            //writeCmd[16] = lenBytes[1];
            //writeCmd[17] = lenBytes[0];           
          
        }

        ///// <summary>
        ///// 获取写入PLC的指令
        ///// </summary>
        ///// <param name="start"></param>
        ///// <param name="buff"></param>
        ///// <returns></returns>
        //private byte[] GetWriteCmd(int start, int region)
        //{
        //    Random ra = new Random(unchecked((int)DateTime.Now.Ticks));
        //    int SID = ra.Next(1, 100);
        //    int num = buff.Length;
        //    int amount = 18 + num * 2;
        //    byte[] writeCmd = new byte[amount];
        //    byte[] startBytes = new byte[2];
        //    startBytes = BitConverter.GetBytes((short)start);
        //    byte[] lenBytes = new byte[2];
        //    lenBytes = BitConverter.GetBytes((short)num);

        //    writeCmd[0] = 0X80;             //ICF
        //    writeCmd[1] = 0X00;             //RSV
        //    writeCmd[2] = 0X02;             //GCT
        //    writeCmd[3] = 0X00;             //DNA
        //                                    //下标从0开始，第4个字节表示PLC的IP地址（IP地址4部分的最后一部分的值）
        //    writeCmd[4] = this.GetLastIpScopeValue(this.PlcIp);          //DA1，对方IP地址，即PLC
        //    writeCmd[5] = 0X00;             //DA2
        //    writeCmd[6] = 0X00;             //SNA
        //                                    //下标从0开始，第7个字节表示PC的IP地址（IP地址4部分的最后一部分的值）
        //    writeCmd[7] = 0X01;     // this.GetLastIpScopeValue(this.PcIp);           //SA1，本机IP地址，即上位机电脑，只要是大于0的值就行
        //    writeCmd[8] = 0X00;              //SA2
        //    writeCmd[9] = Convert.ToByte(SID.ToString(), 16);//SID //0x00;
        //    writeCmd[10] = 0X01;            //Command code
        //    writeCmd[11] = 0X02;            //Command code，0101表示读取，0102 写入数据
        //    writeCmd[12] = 0X82;            //82 地址区域DM区
        //                                    //下标从0开始，第13和14这2个字节表示起始地址
        //    writeCmd[13] = startBytes[1];
        //    writeCmd[14] = startBytes[0];
        //    writeCmd[15] = 0x00;
        //    writeCmd[16] = lenBytes[1];
        //    writeCmd[17] = lenBytes[0];
        //    for (int i = 0; i < buff.Length; i++)
        //    {
        //        int value = 0;
        //        if (buff[i] != null)
        //        {
        //            int.TryParse(buff[i].ToString(), out value);
        //        }
        //        byte[] byteValues = BitConverter.GetBytes((short)value);
        //        writeCmd[18 + i * 2] = byteValues[1];
        //        writeCmd[18 + i * 2 + 1] = byteValues[0];
        //    }
        //    Console.WriteLine(BitConverter.ToString(writeCmd).Replace("-", String.Empty));
        //    return writeCmd;
        //}
        /// <summary>
        /// 连接测试
        /// </summary>
        /// <returns></returns>
        private bool Connection()
        {
            byte[] BT = new byte[] { (byte)Region.DM };
            string bt = ByteArrayToHexString(BT);
            //连接时,读取D0作为成功标志
            byte[] readCmd = this.GetReadCmd(0, 1,0x00,bt);
            int size = this.server.SendTo(readCmd, readCmd.Length, SocketFlags.None, this.plcIpEndPoint);
            if (size < readCmd.Length)
            {
                this.State = false;
                return this.State;
            }
            byte[] buffer = new byte[256 + 14];
            int recv = server.ReceiveFrom(buffer, ref this.remote);//返回收到的字节数
            if (recv != 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 获取数据、类型、块号区、区域、位和大小
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="dataType">类型</param>
        /// <param name="block">区块</param>
        /// <param name="region">起始区域</param>
        /// <param name="bit">位</param>
        /// <param name="size">大小</param>
        /// <returns></returns>
        private bool GetRegion(string address, DataType dataType,int len, ref int block, ref string region, ref byte bit,ref int sendlen, ref int reclen)
        {
            if (address.Length < 1)
                return false;
            try
            {
                block = 0;
                sendlen = 0;
                reclen = 0;
                string strfirst = address.Substring(0, 1);
                string strSec = address.Substring(1, address.Length - 1);
                switch (strfirst)
                {
                    case "D":
                        if (dataType == DataType.BOOL)
                        {
                            int index = strSec.IndexOf(".");
                            string strblock = strSec.Substring(0, index);
                            string strbit = strSec.Substring(index + 1, strSec.Length - index - 1);
                            if (!int.TryParse(strblock, out block))
                            {
                                return false;
                            }                          
                            if (!byte.TryParse(strbit, out bit))
                            {
                                return false;
                            }
                            byte[] BT = new byte[] { (byte)Region.DM_BIT};
                            region = ByteArrayToHexString(BT);
                        }
                        else
                        {
                            if (!int.TryParse(strSec, out block))
                            {
                                return false;
                            }
                            byte[] BT = new byte[] { (byte)Region.DM };
                            region = ByteArrayToHexString(BT);
                        }
                        break;
                    case "W":
                        if (dataType == DataType.BOOL)
                        {
                            int index = strSec.IndexOf(".");
                            string strblock = strSec.Substring(0, index);
                            string strbit = strSec.Substring(index, strSec.Length - index);
                            if (!int.TryParse(strblock, out block))
                            {
                                return false;
                            }
                            if (!byte.TryParse(strbit, out bit))
                            {
                                return false;
                            }
                            byte[] BT = new byte[] { (byte)Region.WR_BIT };
                            region = ByteArrayToHexString(BT);
                        }
                        else
                        {
                            if (!int.TryParse(strSec, out block))
                            {
                                return false;
                            }
                            byte[] BT = new byte[] { (byte)Region.WR_WORD };
                            region = ByteArrayToHexString(BT);
                        }
                        break;
                    default:
                        return false;
                }
                switch (dataType)
                {
                    case DataType.SHORT:
                        sendlen = 1;
                        reclen = 2;
                        break;
                    case DataType.INT:
                        sendlen = 2;
                        reclen = 4;
                        break;
                    case DataType.LONG:
                        sendlen = 4;
                        reclen = 8;
                        break;
                    case DataType.BOOL:
                        sendlen = 1;
                        reclen = 2;
                        break;
                    case DataType.STRING:
                        sendlen = len;
                        reclen = len*2;
                        break;
                    case DataType.FLOAT:
                        reclen = 4;
                        sendlen = 2;
                        break;
                    case DataType.DOUBLE:
                        sendlen = 4;
                        reclen = 8;
                        break;
                    default:
                        return false;
                }
                if (block == 0 || reclen == 0 || sendlen == 0)
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
         
            return true;
        }
        private bool GetStrValue(DataType dataType, object value,int sendlen, ref byte[] str)
        {
            try
            {
                switch (dataType)
                {
                    case DataType.SHORT:
                        str = BitConverter.GetBytes(Convert.ToInt16(value));
                        break;
                    case DataType.INT:
                        str = BitConverter.GetBytes(Convert.ToInt32(value));
                        break;
                    case DataType.LONG:
                        str = BitConverter.GetBytes(Convert.ToInt64(value));
                        break;
                    case DataType.BOOL:
                        if (value.ToString().ToUpper() == "TRUE")
                        {
                            str[0] = 1;
                        }
                        else
                        {
                            str[0] = 0;
                        }
                        break;
                    case DataType.STRING:
                        //str = System.Text.Encoding.ASCII.GetBytes(value.ToString());
                        str = new byte[sendlen * 2];
                        byte[] bt = System.Text.Encoding.ASCII.GetBytes(value.ToString());
                        if (bt.Length <= str.Length)
                        {
                            Array.Copy(bt, str, bt.Length);
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    case DataType.FLOAT:
                        str = BitConverter.GetBytes(Convert.ToSingle(value));
                        break;
                    case DataType.DOUBLE:
                        str = BitConverter.GetBytes(Convert.ToDouble(value));
                        break;
                    default:
                        return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 解析接收到的数据
        /// </summary>
        /// <returns></returns>
        private bool byteToValue(DataType dataType, byte[] buff, ref object value)
        {
            try
            {
                switch (dataType)
                {
                    case DataType.SHORT:
                        value = BitConverter.ToInt16(buff, 0);
                        break;
                    case DataType.SHORTBIT:
                        object strValue = BitConverter.ToInt16(buff, 0);
                        value = Convert.ToString((short)strValue, 2).PadLeft(16, '0');
                        break;
                    case DataType.INT:
                        value = BitConverter.ToInt32(buff, 0);
                        break;
                    case DataType.LONG:
                        value = BitConverter.ToInt64(buff, 0);
                        break;
                    case DataType.BOOL:
                        if (buff[0] == 1)
                        {
                            value = true;
                        }
                        else if (buff[0] == 0)
                        {
                            value = false;
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    case DataType.STRING:
                        value = System.Text.Encoding.ASCII.GetString(buff).Replace("\0","");
                        break;
                    case DataType.FLOAT:
                        value = BitConverter.ToSingle(buff, 0);
                        break;
                    case DataType.DOUBLE:
                        value = BitConverter.ToDouble(buff, 0);
                        break;
                    default:
                        return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        private string ByteArrayToHexString(byte[] data)
        {
            //字节数组转化为16进制字符串
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0'));
            return sb.ToString().ToUpper();
        }
        private byte[] HexStringToByteArray(string s)
        {
            //16进制字符串转化为字节数组
            s = s.Replace(" ", "");
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
        }
        #endregion


        #region 实现PLC成员方法
        public override bool Open()
        {
            try
            {

                if (this.State == true && (server != null))
                {
                    return true;
                }
                this.State = false;
                this.server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                this.plcIpEndPoint = new IPEndPoint(IPAddress.Parse(this.PlcIp),this.PlcPort);
                this.receiveEndPoint = new IPEndPoint(IPAddress.Any, 0);
                this.remote = (EndPoint)this.receiveEndPoint;
                server.ReceiveTimeout = timeOut;
                this.State = this.Connection();
                if (!this.State)
                {
                    return this.State;
                }
                else
                {
                    this.State = true;
                    this._isOpen = true;
                    return this.State;
                }

            }
            catch (Exception ex)
            {
                this.State = false;
                this._isOpen = false;
                return this.State;
            }
        }
        public override bool ReadValue(string address, DataType type, ref object value,int len = 1)
        {
            return Read(address, type, len, ref value);           
        }
        public override bool WriteValue(string address, DataType type, object value,int len=1)
        {
            return Write(address, type, len, value);
         
        }       
        public override void Close()
        {
            try
            {
                if (this.server != null)
                {
                    this.server.Close();
                    this.server = null;
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}
