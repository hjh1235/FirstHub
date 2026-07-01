using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace UpperComputer
{
    //
    //客户端
    //

    public class _Client
{
    public string ip = "";
    public int Port = 0;
    public string msg = "";
    public string ConnStr = "";
    public int ReConnectionTime = 0;
    public string SendStr = "";
    public NetWorkHelper.TCP.ITcpClient ITcpClient = new NetWorkHelper.TCP.ITcpClient();
    public bool ConnOK = false;
    
    // ✅ 添加发送确认相关字段
    private bool _waitForAck = false;
    private bool _ackReceived = false;
    private string _lastSendData = "";
    
    public void conn(string ip, int Port, int ReConnectionTime)
    {
        ITcpClient.ServerIp = ip;
        ITcpClient.ServerPort = Port;
        ITcpClient.ReConnectionTime = ReConnectionTime;
        ITcpClient.IsReconnection = true;
        
        // ✅ 订阅错误事件，连接异常时更新状态
        ITcpClient.OnErrorMsg += ITcpClient_OnErrorMsg;
        ITcpClient.OnRecevice += 客户端_OnRecevice;
        ITcpClient.OnStateInfo += 客户端_OnStateInfo;
        
        ITcpClient.StartConnect();
    }
    
    // ✅ 新增：捕获发送错误事件
    private void ITcpClient_OnErrorMsg(object sender, NetWorkHelper.ICommond.TcpClientErrorEventArgs e)
    {
        Log.log($"[TCP 错误] {e.ErrorMsg}");
        // 错误发生时连接可能已断开
        if (e.ErrorMsg.Contains("发送"))
        {
            ConnStr = "连接断开";
        }
    }
    
    private void 客户端_OnStateInfo(object sender, NetWorkHelper.ICommond.TcpClientStateEventArgs e)
    {
        ConnStr = e.StateInfo;
        Log.log($"[连接状态] {ConnStr}");
    }

    private void 客户端_OnRecevice(object sender, NetWorkHelper.ICommond.TcpClientReceviceEventArgs e)
    {
        msg = System.Text.Encoding.ASCII.GetString(e.Data);
        
        // ✅ 检查是否是 ACK 确认
        if (_waitForAck && msg.Contains("ACK"))
        {
            _ackReceived = true;
            _waitForAck = false;
            Log.log($"[发送确认] 收到 ACK，数据：{_lastSendData}");
        }
        
        Log.log($"[接收数据] {msg}");
    }

    /// <summary>
    /// 发送数据字符串（带应用层确认）
    /// </summary>
    public bool Send(string SendStr)
    {
        // ✅ 1. 检查连接状态
        if (ConnStr != "已连接服务器")
        {
            Log.log($"[发送失败] 连接状态={ConnStr}");
            return false;
        }
        
        // ✅ 2. 检查底层 Socket 是否可用
        if (ITcpClient.Client == null || ITcpClient.Client.WorkSocket == null)
        {
            Log.log("[发送失败] 底层 Socket 为空");
            ConnStr = "连接断开";
            return false;
        }
        
        // ✅ 3. 检查 Socket 连接状态
        if (!ITcpClient.Client.WorkSocket.Connected)
        {
            Log.log("[发送失败] Socket 未连接");
            ConnStr = "连接断开";
            return false;
        }
        
        try
        {
            // ✅ 4. 设置发送确认标志
            _waitForAck = true;
            _ackReceived = false;
            _lastSendData = SendStr;
            
            var byteStr = Encoding.UTF8.GetBytes(SendStr + "\r\n");
            ITcpClient.SendData(byteStr);
            
            Log.log($"[已发送] 数据：{SendStr}，长度：{byteStr.Length}");
            
            // ✅ 5. 等待 ACK 确认（超时 3 秒）
            int timeout = 3000;
            int elapsed = 0;
            while (_waitForAck && elapsed < timeout)
            {
                Thread.Sleep(50);
                elapsed += 50;
            }
            
            // ✅ 6. 检查是否收到确认
            if (_waitForAck)  // 超时未收到 ACK
            {
                Log.log($"[发送超时] 未收到 ACK，数据：{SendStr}");
                _waitForAck = false;
                return false;
            }
            
            if (!_ackReceived)
            {
                Log.log($"[发送失败] 未收到有效 ACK");
                return false;
            }
            
            Log.log($"[发送成功] {SendStr}");
            return true;
        }
        catch (Exception ex)
        {
            Log.log($"[发送异常] {ex.Message}");
            ConnStr = "连接断开";
            return false;
        }

           
    }
        public void ClearData()
        {
            msg = "";
            SendStr = "";
            ConnOK = false;
        }

    // ... 其他方法保持不变
}

    //
    //服务端
    //
    public class _Server
    {
        //public string ip = "";
        //public int Port = 0;
        public string SendStr = "";
        [NonSerialized]
        public NetWorkHelper.TCP.ITcpServer ser = new NetWorkHelper.TCP.ITcpServer();
        [NonSerialized]
        public List<NetWorkHelper.IModels.IClient> IClientList = new List<NetWorkHelper.IModels.IClient>();
        [NonSerialized]
        public static _Server _Server_;
        public static _Server Instance()
        {
            if (_Server_ == null)
            {
                _Server_ = new _Server();
            }
            return _Server_;
        }

        public bool Open(string ip, int Port)
        {
            ser.ServerIp = ip;
            ser.ServerPort = Port;
            ser.OnRecevice += 服务端_OnRecevice;
            ser.OnOnlineClient += 服务端_OnOnlineClient;
            ser.OnOfflineClient += 服务端_OnOfflineClient;
            ser.Start();
            return true;
        }

        private void 服务端_OnOfflineClient(object sender, NetWorkHelper.ICommond.TcpServerClientEventArgs e)
        {
            IClientList.Remove(e.IClient);
        }

        private void 服务端_OnOnlineClient(object sender, NetWorkHelper.ICommond.TcpServerClientEventArgs e)
        {
            IClientList.Add(e.IClient);
        }

        public bool Send(string SendStr)
        {
            for (int i = 0; i < IClientList.Count; i++)
            {
                ser.SendData(IClientList[i], System.Text.Encoding.ASCII.GetBytes(SendStr));
            }
            return true;
        }

        public string str = "";
        private void 服务端_OnRecevice(object sender, NetWorkHelper.ICommond.TcpServerReceviceaEventArgs e)
        {
            NetWorkHelper.IModels.IClient IClient = e.IClient;
            str = System.Text.Encoding.ASCII.GetString(e.Data);
            Log.log("接收数据:" + e.IClient.Ip + "：" + str);
        }
        public bool Open(string objName)
        {
            throw new NotImplementedException();
        }
        public void RecvResult(out string Data)
        {
            Data = "";
            Data = str;
        }

        public void ClearData()
        {
            if (str == null)
            {
                str = "";
            }
            str = "";
        }
        public void Close()
        {

        }
    }
}
