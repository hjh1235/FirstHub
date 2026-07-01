using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpperComputer
{

    public abstract class PLC
    {

        /// <summary>
        /// 打开设备
        /// </summary>
        /// <returns>成功返回true，失败返回false</returns>
        public abstract bool Open();
        /// <summary>
        /// 读取bool类型
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public abstract bool ReadValue(string address, DataType dataType,ref object value, int len = 1);
        
       /// <summary>
       /// 写入信息
       /// </summary>
       /// <param name="address"></param>
       /// <param name="type"></param>
       /// <param name="value"></param>
       /// <returns></returns>
        public abstract bool WriteValue(string address,DataType dataType, object value,int len = 1);
        /// <summary>
        /// 关闭设备
        /// </summary>
        public abstract void Close();

        /// <summary>
        /// 连接标志
        /// </summary>
        public bool State;

        public enum DataType
        {
            SHORT,
            SHORTBIT,
            INT,
            LONG,
            BOOL,
            STRING,
            FLOAT,
            DOUBLE
        }
    }



}
