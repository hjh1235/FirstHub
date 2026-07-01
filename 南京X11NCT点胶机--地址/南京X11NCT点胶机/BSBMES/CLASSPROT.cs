using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UpperComputer
{
  public   class CLASSPROT
    {
        public SerialPort COMM = new SerialPort();

        public Action<String> TOSN =(P)=> { }; 

        public bool open(string com, Int32 BIT, int Parity1, int DataBits, int StopBits1)
        {
            if (COMM.IsOpen)
            {
                COMM.Close();
            }
            COMM.ReadBufferSize = 1024;
            COMM.PortName = com;
            COMM.ReadTimeout = 1000;
            COMM.BaudRate = BIT;
            COMM.Parity = (Parity)Parity1;
            COMM.DataBits = DataBits;
            COMM.StopBits = (StopBits)StopBits1;
            COMM.ReadTimeout = 2000;
            return OPEN();
        }

        bool OPEN()
        {
            try
            {
                COMM.Open();
                COMM.DataReceived += (P1, P2) =>
                {
                    if (COMM.IsOpen && COMM.BytesToRead > 0)
                    {
                        int n = COMM.BytesToRead;//先记录下来，避免某种原因，人为的原因，操作几次之间时间长，缓存不一致   ；
                        byte[] buf = new byte[n];//声明一个临时字节数组存储当前来的串口数据   
                                                 //  received_count += n;//增加接收计数                         
                        COMM.Read(buf, 0, n);//读取缓冲数据                                                          
                       var DataReceivedstr_STR = System.Text.Encoding.Default.GetString(buf);
                        if (DataReceivedstr_STR.Length > 10)
                        {
                            TOSN(DataReceivedstr_STR);
                        }    
                        //   }
                    }
                    COMM.DiscardOutBuffer();
                };
            }
            catch
            {
                return false;
            }
            return COMM.IsOpen;
        }

        public Tuple<bool,string> Send(string SendStr)
        {
            try
            {
                COMM.DiscardInBuffer();
                Thread.Sleep(10);
                COMM.Write(SendStr);
                Thread.Sleep(10);
                var  Data = COMM.ReadTo("\r");
                return new Tuple<bool, string>(true, Data);
            }
            catch (Exception EX)
            {
                return new Tuple<bool, string>(false,EX.ToString()) ;
            }
          
        }
     
    }
}
