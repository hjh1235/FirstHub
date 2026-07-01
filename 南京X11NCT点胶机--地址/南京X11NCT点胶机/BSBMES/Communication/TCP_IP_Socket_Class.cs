using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UpperComputer
{
    public class TcpServer
    {
        string IP = "";
        string Port = "";
         public  string recMsg = "";
        public TcpServer(string ip, string port)
        {
            IP = ip;
            Port = port;
        }
        public Dictionary<string, Socket> clientList = new Dictionary<string, Socket>();
        public Dictionary<string, SocketDataClass> dicList = new Dictionary<string, SocketDataClass>();
        //①:创建一个用于监听连接的Socket对象；
        //②:用指定的端口号和服务器的Ip建立一个EndPoint对象；
        //③:用Socket对象的Bind()方法绑定EndPoint；
        //④:用Socket对象的Listen()方法开始监听；
        //⑤:接收到客户端的连接，用Socket对象的Accept()方法创建一个新的用于和客户端进行通信的Socket对象；
        //⑥:通信结束后一定记得关闭Socket。
        public void MySocket()
        {
            //1.创建一个用于监听连接的Socket对象；
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            //2.用指定的端口号和服务器的Ip建立一个EndPoint对象；
            IPAddress iP = IPAddress.Parse(IP);
            IPEndPoint endPoint = new IPEndPoint(iP, int.Parse(Port));
            //3.用Socket对象的Bind()方法绑定EndPoint；
            server.Bind(endPoint);
            //4.用Socket对象的Listen()方法开始监听；
            //同一时刻内允许同时加入链接的最大数量
            server.Listen(20);
            //5.接收到客户端的连接，用Socket对象的Accept()方法创建一个新的用于和客户端进行通信的Socket对象；
            while (true)
            {
                Thread.Sleep(1);
                //接受接入的一个客户端
                Socket connectClient = server.Accept();
                if (connectClient != null)
                {
                    string infor = connectClient.RemoteEndPoint.ToString();
                    clientList.Add(infor, connectClient);
                    SocketDataClass ts = new SocketDataClass();
                    ts.str = "";
                    ts.point = infor;
                    ts.socket = connectClient;
                    dicList.Add(infor, ts);
                    //每有一个客户端接入时，需要有一个线程进行服务
                    Thread threadClient = new Thread(ReciveMsg);//带参的方法可以把传递的参数放到start中
                    threadClient.IsBackground = true;
                    //创建的新的对应的Socket和客户端Socket进行通信
                    threadClient.Start(connectClient);                  
                }
            }

        }
        public void ReciveMsg(object o)
        {
            Socket connectClient = o as Socket;//connectClient负责客户端的通信
            IPEndPoint endPoint = null;
            int consuccess = 0;
            Stopwatch sw = new Stopwatch();
            while (true)
            {
                try
                {
                    ///定义服务器接收的字节大小
                    byte[] arrMsg = new byte[1024 * 1024];
                    ///接收到的信息大小(所占字节数)
                    int length = connectClient.Receive(arrMsg);
                    if (length > 0)
                    {
                        recMsg = Encoding.UTF8.GetString(arrMsg, 0, length);
                        //获取客户端的端口号
                        endPoint = connectClient.RemoteEndPoint as IPEndPoint;
                        dicList[endPoint.ToString()].str = recMsg;
                        //服务器显示客户端的端口号和消息
                        consuccess = 1;
                        Log.log("接收到上位机数据:\r\n"+recMsg);
                    }
                    else
                    {
                        //获取客户端的端口号
                        endPoint = connectClient.RemoteEndPoint as IPEndPoint;
                        clientList.Remove(endPoint.ToString());
                        dicList.Remove(endPoint.ToString());
                        //list.RemoveAt(list.IndexOf(endPoint.ToString()));
                        connectClient.Dispose();
                        break;
                    }
                }
                catch (Exception)
                {
                    if (endPoint != null)
                    {
                        endPoint = connectClient.RemoteEndPoint as IPEndPoint;
                        ///移除添加在字典中的服务器和客户端之间的线程
                        clientList.Remove(endPoint.ToString());
                        dicList.Remove(endPoint.ToString());
                        connectClient.Dispose();
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
        /// 服务器发送消息，客户端接收
        public bool SendMsg(string str)
        {
            try
            {
                if (!(clientList.Count> 0))
                {
                    Log.log("发送数据失败，与上位机连接异常,请检查，当前连接数目:"+clientList.Count);
                    return false;
                }
                ///遍历出字典中的所有线程
                foreach (var item in clientList)
                {
                    byte[] arrMsg = Encoding.UTF8.GetBytes(str + "\r\n");

                    ///获取键值(服务器），发送消息
                    item.Value.Send(arrMsg);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
           
        }
    }
    #region
    public class TCPClientClass
    {
        /// 创建客户端
        private Socket client;
        public string IP = "";
        public string Port = "";
        public string msg = ""; //接收到的数据
        public bool bConnect = false;

        public TCPClientClass(string ip, string port)
        {
            IP = ip;
            Port = port;
        }
        public void Connect()
        {
            if (client != null && client.Connected == true)
            {
                return;
            }

            ///创建客户端
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            ///IP地址
            IPAddress ip = IPAddress.Parse(IP);
            ///端口号
            IPEndPoint endPoint = new IPEndPoint(ip, int.Parse(Port));

            ///建立与服务器的远程连接
            try
            {
                client.Connect(endPoint);
                bConnect = true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("地址或端口错误！！！！");
                client.Close();
                bConnect = false;
                return;
            }
            ///线程问题
            Thread thread = new Thread(ReciveMsg);
            thread.IsBackground = true;
            thread.Start(client);
        }

        /// 客户端接收到服务器发送的消息
        private void ReciveMsg(object o)
        {
            Socket client = o as Socket;
            while (true)
            {
                try
                {
                    ///定义客户端接收到的信息大小
                    byte[] arrList = new byte[1024 * 1024];
                    ///接收到的信息大小(所占字节数)
                    int length = client.Receive(arrList);
                    msg = Encoding.UTF8.GetString(arrList, 0, length);
                    //listBox_log.Items.Add(msg);
                }
                catch (Exception ex)
                {
                    ///关闭客户端
                    client.Close();
                    bConnect = false;
                    break;
                }
            }
        }

        /// 客户端发送消息给服务端
        //private void button_send_Click(object sender, EventArgs e)
        //{
        //    if (textBox_message.Text != "")
        //    {
        //        SendMsg(textBox_message.Text);
        //    }
        //}

        /// 客户端发送消息，服务端接收
        public bool Send(string str)
        {
            try
            {
                byte[] arrMsg = Encoding.UTF8.GetBytes(str+"\r\n");
                client.Send(arrMsg);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }


        //private void Client_FormClosed(object sender, FormClosedEventArgs e)
        //{
        //    if (client != null) client.Close();
        //}
    }

    public class SocketClient
    {
        public event Action<string> EventReceive;
        public event Func<string, bool> EventSend;
        public event Action<string> EventconnectState; //1:链接  0：断开
        public Socket NewScocket = null;
        byte[] byteBuffer = new byte[1024*1024];
        string beginMsg = string.Empty;
        public int isconnect = 0; //0未连接 1成功 2连接失败 3plc断开
                                  //string endMsg = "\r\n";
                                  //Log log = new Log();
        public string IP = "";
        public string Port = "";
        public string Name = "";
        public string msg = "";
        public bool bConnect = false;
        public SocketClient(string ip, string port,string name)
        {

            //EventSend += Send;

            //System.Windows.Forms.Timer nTime = new System.Windows.Forms.Timer();
            //nTime.Interval = 500;
            //nTime.Tick += NTime_Tick;
            //nTime.Enabled = true;

            IP = ip;
            Port = port;
            Name = name;
        }

        private void NTime_Tick(object sender, EventArgs e)
        {
            if (NewScocket != null && NewScocket.Connected)
            {
                if (EventconnectState != null)
                {
                    EventconnectState("1");
                }
            }
            else
            {
                if (EventconnectState != null)
                {
                    EventconnectState("0");
                }
            }
        }

        public bool Connect()
        {
            try
            {
                if (NewScocket != null && NewScocket.Connected) //只有一个client
                {
                    Close(NewScocket);
                }
                NewScocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                NewScocket.Blocking = false;
                IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(IP), int.Parse(Port));
                //绑定  异步链接
                NewScocket.BeginConnect(ipe, new AsyncCallback(OnConnectRequest), NewScocket);
              
                return true;
            }
            catch (Exception)
            {
                //log.WriteLog("链接异常");
                //MessageBox.Show("链接异常", "链接提示");
                bConnect = false;
                return false;
            }
        }

        private void OnConnectRequest(IAsyncResult ar)
        {
            //获取链接状态
            Socket _Socket = (Socket)ar.AsyncState;//
            try
            {
                //判断链接是否成功
                if (_Socket.Connected)
                {
                    SetReceiveCallback(_Socket);
                    // MessageBox.Show("链接成功");
                    //log.WriteLog("链接成功");
                    isconnect = 1;
                    bConnect = true;
                    //Log.log(Name + "连接成功");

                }
                else
                {
                    //log.WriteLog("链接失败,请检查网线");
                    //MessageBox.Show("链接失败,请检查网线", "链接提示");
                    isconnect = 2;
                    bConnect = false;
                    //Log.log(Name + "连接失败,请检查IP或端口是否正确");
                }
            }
            catch (Exception)
            {
                //log.WriteLog("链接异常");
                //MessageBox.Show("链接异常", "链接提示");
                bConnect = false;
                isconnect = 2;
                //Log.log(Name + "连接异常,请检查IP或端口是否正确");
            }
        }

        /// <summary>
        /// 回调法法
        /// </summary>
        private void SetReceiveCallback(Socket _Sock)
        {
            AsyncCallback receive = new AsyncCallback(OnReceiveData);
            _Sock.BeginReceive(byteBuffer, 0, byteBuffer.Length, SocketFlags.None, OnReceiveData, _Sock);
        }

        /// <summary>
        /// 接受数据
        /// </summary>
        /// <param name="ar"></param>
        private void OnReceiveData(IAsyncResult ar)
        {
            Socket _Sock = (Socket)ar.AsyncState;
            try
            {
                int receiveLengthData = _Sock.EndReceive(ar);
                //大于0接受到报文数据
                if (receiveLengthData > 0)
                {
                    // if (m_ByteBuffer[0]==0)
                    //接受到的数据
                    msg = Encoding.UTF8.GetString(byteBuffer, 0, receiveLengthData);
                    //传值
                    if (EventReceive != null)
                    {
                        EventReceive(msg);
                    }
                    //重复调用
                    SetReceiveCallback(_Sock);
                }
                else
                {
                    //光闭Socket
                    //MessageBox.Show("plc已关闭");
                    //Log.log(Name + "-断开连接");
                    isconnect = 3;
                    bConnect = false;
                    Close(_Sock);
                }
            }
            catch (Exception)
            {
                isconnect = 3;
                bConnect = false;
                //Log.log(Name + "-连接异常,断开连接");
            }
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        public bool Send(string str)//字符串转字节发送
        {
            //把字符转换字节
            byte[] _ByteSend = Encoding.UTF8.GetBytes(str+"\r\n");
            //发送数据
            try
            {
                NewScocket.Send(_ByteSend, _ByteSend.Length, 0);
                Log.log(Name + "-发送数据成功");
                //if (EventReceive != null)
                //{
                //    EventReceive.Invoke("客户端："+str);
                //}
                return true;
            }
            catch (Exception)
            {
                isconnect = 3;
                bConnect = false;
                Log.log(Name + "-发送数据异常，连接失败");
                return false;

            }
        }


        public bool strToHexByte(string hexString)//字符串转成16进制发送
        {

            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                //转字节发送
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2).Replace(" ", ""), 10);//十进制

            //转字发送
            byte[] bytes = new byte[12];
            // UInt16 a = 54543;
            for (int j = 0; j < returnBytes.Length; j++)
            {
                byte[] tempbyte = new byte[2];
                tempbyte = BitConverter.GetBytes(returnBytes[j]);
                bytes[j * 2] = tempbyte[1];
                bytes[j * 2 + 1] = tempbyte[0];
            }
            try
            {
                NewScocket.Send(bytes, bytes.Length, 0);
                //if (EventReceive != null)//客户端发送后单纯的只是为了显示再客户端的界面上而已
                //{
                //    EventReceive.Invoke("客户端：" + hexString);//客户端发送的数据
                //    //EventReceive.Invoke(hexString);
                //}
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SendDM(string str)//字符串转字节发送 
        {
            //把字符转换字节
            byte[] _ByteSend = Encoding.ASCII.GetBytes(str);
            //发送数据
            try
            {
                NewScocket.Send(_ByteSend, _ByteSend.Length, 0);
                //if (EventReceive != null)
                //{
                //    EventReceive.Invoke("客户端："+str);
                //}
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        /// <summary>
        /// 关闭Sockte
        /// </summary>
        /// <param name="sockte"></param>
        public void Close(Socket socket)
        {
            if (socket != null && socket.Connected)
            {
                socket.Shutdown(SocketShutdown.Both);
                Thread.Sleep(10);
                socket.Close();
            }
        }
    }
    #endregion
    public class SocketDataClass
    {
        public Socket socket;
        /// <summary>
        /// 字符串
        /// </summary>
        public string str;
        /// <summary>
        /// 端口
        /// </summary>
        public string point;
    }
}
