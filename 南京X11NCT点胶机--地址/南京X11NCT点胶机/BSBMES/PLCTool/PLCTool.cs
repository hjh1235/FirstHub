using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpperComputer
{
    public class PLCTool
    {
        public Dictionary<string, PLCinfomartion> dicPLC = new Dictionary<string, PLCinfomartion>();
        //public Object PLC;
        public PLC PLC;
        public bool canListen = false;
        public string strSta = "";
        public PLCTool()
        {

        }
        public virtual void intance()
        { }
        public virtual void ResPLCData()
        { }
        public virtual void ResPLCData2()
        { }
        public virtual object ReadPLC(string address, string type)
        {
            return null;
        }
       
        public virtual bool WritePLC(string address, string type, string value)
        {
            return false;
        }
        public virtual object ReadPLC(string name)
        {
            return new object();
        }
       
       
        public virtual bool WritePLC(string name, object value)
        {
            return false;
        }
        public virtual bool WritePLCData(string address, object value)
        {
            return false;
        }
        public virtual bool connectToPlc()
        {
            return false;
        }
    }
}
