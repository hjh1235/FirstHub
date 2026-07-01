using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpperComputer
{
    public interface  Commu_Interface
    {
        bool Open();      
        bool Send(string SendStr);
        void RecvResult(out string Data);
        void ClearData();
        void Close();
    }
}
