using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UpperComputer
{
    public class TCP_IP_Connect
    {
        Thread threadWatch = null; //负责监听客户端的线程
        Socket socketWatch = null;  //负责监听客户端的套接字   
                                    //创建一个负责和客户端通信的套接字 
        Socket socConnection = null;
        public bool bConStatus = false;
        public string msg = "";
        string Name = "";
        int station = 1;

        public bool Conn(string IP, int Port, string name, int istation)
        {
            try
            {
                //定义一个套接字用于监听客户端发来的信息  包含3个参数(IP4寻址协议,流式连接,TCP协议)
                socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //服务端发送信息 需要1个IP地址和端口号
                IPAddress ipaddress = IPAddress.Parse(IP); //获取文本框输入的IP地址
                                                           //将IP地址和端口号绑定到网络节点endpoint上 
                IPEndPoint endpoint = new IPEndPoint(ipaddress, Port); //获取文本框上输入的端口号

                Name = name;
                station = istation;
                //监听绑定的网络节点
                socketWatch.Bind(endpoint);
                //将套接字的监听队列长度限制为20
                socketWatch.Listen(5);
                //创建一个监听线程 
                threadWatch = new Thread(WatchConnecting);
                //将窗体线程设置为与后台同步
                threadWatch.IsBackground = true;
                //启动线程
                threadWatch.Start();
                //启动线程后 txtMsg文本框显示相应提示
                log($"{Name}-开始监听客户端传来的信息,等待AGV连接...");
            }
            catch (Exception ex)
            {
                bConStatus = false;
                log($"{Name}-AGV服务端启动服务失败!");
            }
            return true;
        }
        private void WatchConnecting()
        {
            while (true)  //持续不断监听客户端发来的请求
            {
                socConnection = socketWatch.Accept();
                log($"{Name}-AGV客户端连接成功! ");
                bConStatus = true;
                //创建一个通信线程 
                ParameterizedThreadStart pts = new ParameterizedThreadStart(ServerRecMsg);
                Thread thr = new Thread(pts);
                thr.IsBackground = true;
                //启动线程
                thr.Start(socConnection);
            }
        }
        /// <summary>
        /// 发送信息到客户端的方法
        /// </summary>
        /// <param name="sendMsg">发送的字符串信息</param>
        public bool ServerSendMsg(string sendMsg)
        {
            try
            {
                //if (bConStatus == false)
                //{
                //    log($"{Name}-AGV客户端已断开连接,无法发送信息！");
                //    return false;
                //}

                //将输入的字符串转换成 机器可以识别的字节数组
                byte[] arrSendMsg = Encoding.UTF8.GetBytes(sendMsg);

                //向客户端发送字节数组信息
                socConnection.Send(arrSendMsg);
                //将发送的字符串信息附加到文本框txtMsg上

                log($"{Name}-上位机发送\r\n" + sendMsg);

            }
            catch (Exception ex)
            {
                bConStatus = false;
                log($"{Name}-AGV客户端已断开连接,无法发送信息！");
                return false;
            }
            return true;
        }
        /// <summary>
        /// 接收客户端发来的信息 
        /// </summary>
        /// <param name="socketClientPara">客户端套接字对象</param>
        private void ServerRecMsg(object socketClientPara)
        {
            Socket socketServer = socketClientPara as Socket;
            while (true)
            {
                //创建一个内存缓冲区 其大小为1024*1024字节  即1M
                byte[] arrServerRecMsg = new byte[1024 * 1024];
                try
                {
                    //将接收到的信息存入到内存缓冲区,并返回其字节数组的长度
                    int length = socketServer.Receive(arrServerRecMsg);
                    if (length > 0)
                    {
                        //将机器接受到的字节数组转换为人可以读懂的字符串
                        string strSRecMsg = Encoding.UTF8.GetString(arrServerRecMsg, 0, length);
                        msg = strSRecMsg;
                        //将发送的字符串信息附加到文本框txtMsg上 
                        bConStatus = true;
                        log($"{Name}-接收到AGV客户端发送\r\n" + strSRecMsg);
                    }
                    else
                    {
                        bConStatus = false;
                        log($"{Name}-AGV客户端已断开连接！");
                        break;
                    }
                }
                catch (Exception ex)
                {
                    bConStatus = false;
                    log($"{Name}-AGV客户端已断开连接！");
                    break;
                }
            }
        }
        public void log(string str)
        {
            if (station == 1)
            {
                Log.logAGV(str);
            }
            else
            {
                Log.logAGVRes(str);
            }
        }
        /// <summary>
        /// 获取当前系统时间的方法
        /// </summary>
        /// <returns>当前时间</returns>
        private DateTime GetCurrentTime()
        {
            DateTime currentTime = new DateTime();
            currentTime = DateTime.Now;
            return currentTime;
        }
    }
}
