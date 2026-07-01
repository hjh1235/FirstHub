using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UpperComputer
{
    public class ClassNET
    {
        System.Net.Sockets.TcpClient SOCKE = new TcpClient();
        string IP = "192.168.0.1";
        int PORT = 9600;

        public string EXMESSAGE = "";
        public string BCODE = "";
        public bool bConnect = false;
        public static event Action<String, ClassNET> GETSTR;
        private NetworkStream networke;

        public ClassNET(string ip, int port)
        {
            IP = ip;
            PORT = port;
            //  Task.Run(() => { CONN(); });
        }

        public void COLSE()
        {
            SOCKE?.Close();

        }
        public bool CONN()
        {

            try
            {
                if (SOCKE.Connected)
                {
                    bConnect = true;
                    return true;
                }
                SOCKE?.Close();

                Thread.Sleep(300);
                SOCKE = new TcpClient();


                var A = SOCKE.BeginConnect(IP, PORT, null, null);

                var A1 = A.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(10));
                if (!A1) return false;

                if (!SOCKE.Connected) return false;
                //    SOCKE = new TcpClient(IP,PORT);
                SOCKE.ReceiveBufferSize = 1024;
                SOCKE.ReceiveTimeout = 500;
                SOCKE.SendTimeout = 500;
                networke = SOCKE.GetStream();
                byte[] x = new byte[1024];
                networke.BeginRead(x, 0, x.Length, new AsyncCallback(ReceiveCallBack2), x);
                bConnect = true;
            }
            catch (Exception EX)
            {
                // MessageBox.Show(EX.ToString());
                EXMESSAGE = EX.ToString();
                bConnect = false;
                return false;
            }

            if (SOCKE.Connected) return true;

            return true;
        }


        private void ReceiveCallBack2(IAsyncResult ar)
        {

            try
            {
                byte[] x = new byte[1024];
                networke?.BeginRead(x, 0, x.Length, new AsyncCallback(ReceiveCallBack2), x);
                byte[] BYTES = (byte[])ar.AsyncState;
                if (BYTES.Length > 0)
                {

                    BCODE = Encoding.UTF8.GetString(BYTES).Trim('\0');
                    Log.log($"接收到数据:{BCODE}");
                    GETSTR(BCODE, this);
                }
            }
            catch (Exception ex)
            {
                EXMESSAGE = ex.Message;
                bConnect = false;
                SOCKE?.Close();
            }
        }

        public bool READBCODE(string Str)
        {

            try
            {
                var s = Encoding.UTF8.GetBytes(Str+"\r\n");
                SOCKE.GetStream().Write(s, 0, s.Length);
                return true;
            }

            catch (Exception EX)
            {
                bConnect = false;
                return false;
            }
        }
    }

}
