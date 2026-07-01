using HslCommunication.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UpperComputer.TaskUnit;

namespace UpperComputer
{
    public class PLCToolOmron_UDP : PLCTool
    {
        //public new HslCommunication.Profinet.Omron.OmronFinsUdp PLC;

        public override void ResPLCData()
        {
            while (true)
            {
                Thread.Sleep(1);
                try
                {
                    //if (canListen == false)
                    //{
                    //    Thread.Sleep(1000);
                    //    continue;
                    //}

                    foreach (var item in dicPLC)
                    {
                        if (item.Value.PointFlag == "0")
                        {
                            var result = ReadPLC(item.Value.PointAddress, item.Value.PointType);
                            item.Value.PointResult = result.ToString();
                        }                      
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        public override void ResPLCData2()
        {
            while (true)
            {
                Thread.Sleep(100);
                try
                {
                    if (canListen == false)
                    {
                        Thread.Sleep(1000);
                        continue;
                    }
                    foreach (var item in dicPLC)
                    {
                        if (item.Value.PointFlag != "0")
                        {
                            var result = ReadPLC(item.Value.PointAddress, item.Value.PointType);
                            item.Value.PointResult = result.ToString();
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
        }
     
        public override object ReadPLC(string address, string type)
        {
            try
            {
                if (type.ToUpper() == "INT")
                {
                    object value = 0;
                    var result = PLC.ReadValue(address, UpperComputer.PLC.DataType.INT, ref value);
                    if (result == true)
                    {
                        return value;
                    }
                }
                else if (type.ToUpper() == "SHORT")
                {
                    object value = 0;
                    var result = PLC.ReadValue(address, UpperComputer.PLC.DataType.SHORT, ref value);
                    if (result == true)
                    {
                        return value;
                    }
                }
                else if (type.ToUpper() == "BOOL")
                {
                    object value = 0;
                    var result = PLC.ReadValue(address, UpperComputer.PLC.DataType.BOOL, ref value);
                    if (result == true)
                    {
                        return value;
                    }
                }
                else if (type.ToUpper() == "DOUBLE")
                {
                    object value = 0;
                    var result = PLC.ReadValue(address, UpperComputer.PLC.DataType.DOUBLE, ref value);
                    if (result == true)
                    {
                        return value;
                    }
                }
                else if (type.ToUpper() == "STRING")
                {
                    object value = 0;
                    var result = PLC.ReadValue(address, UpperComputer.PLC.DataType.STRING, ref value, 12);
                    if (result == true)
                    {
                        return value;
                    }
                }
                else if (type.ToUpper() == "FLOAT")
                {
                    object value = 0;
                    var result = PLC.ReadValue(address, UpperComputer.PLC.DataType.FLOAT, ref value);
                    if (result == true)
                    {
                        return value;
                    }
                }
                return "ERROR";
            }
            catch (Exception)
            {

                return "ERROR";
            }

        }
        public override bool WritePLC(string address, string type, string value)
        {
            try
            {
                for (int i = 0; i < 5; i++)
                {
                    if (type.ToUpper() == "INT")
                    {
                        bool result = PLC.WriteValue(address, UpperComputer.PLC.DataType.INT, Convert.ToInt32(value));
                        if (result == true)
                        {
                            return true;
                        }
                    }
                    else if (type.ToUpper() == "SHORT")
                    {
                        bool result = PLC.WriteValue(address, UpperComputer.PLC.DataType.SHORT, Convert.ToInt16(value));
                        if (result == true)
                        {
                            return true;
                        }
                    }
                    else if (type.ToUpper() == "BOOL")
                    {
                        bool result = PLC.WriteValue(address, UpperComputer.PLC.DataType.BOOL, Convert.ToBoolean(value));
                        if (result == true)
                        {
                            return true;
                        }
                    }
                    else if (type.ToUpper() == "DOUBLE")
                    {
                        bool result = PLC.WriteValue(address, UpperComputer.PLC.DataType.DOUBLE, Convert.ToDouble(value));
                        if (result == true)
                        {
                            return true;
                        }
                    }
                    else if (type.ToUpper() == "STRING")
                    {
                        bool result = PLC.WriteValue(address, UpperComputer.PLC.DataType.STRING, value, 12);
                        if (result == true)
                        {
                            return true;
                        }
                    }
                    else if (type.ToUpper() == "FLOAT")
                    {
                        bool result = PLC.WriteValue(address, UpperComputer.PLC.DataType.FLOAT, Convert.ToSingle(value));
                        if (result == true)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        int i = 0;
        object objRead = new object();
        public override object ReadPLC(string name)
        {
            lock (objRead)
            {
                try
                {
                    return dicPLC[name].PointResult;
                }
                catch (Exception)
                {
                    return "ERROR";
                }
                try
                {
                    var address = dicPLC[name].PointAddress;
                    var type = dicPLC[name].PointType;
                    i = 0;
                    for (int i = 0; i < 3; i++)
                    {
                        if (type.ToUpper() == "INT")
                        {
                            object value1 = 0, value2 = 0;
                            bool result1 = PLC.ReadValue(address, UpperComputer.PLC.DataType.INT, ref value1);
                            bool result2 = PLC.ReadValue(address, UpperComputer.PLC.DataType.INT, ref value2);
                            bool valueEqual = result1 == result2;
                            if (valueEqual)
                            {
                                return value1.ToString();
                            }
                            else
                            {
                                //Log.alarmLog($"读取失败地址{address},结果{result.Content},{result1.Content}", 1);
                            }
                        }
                        else if (type.ToUpper() == "SHORT")
                        {
                            object value1 = 0, value2 = 0;
                            bool result1 = PLC.ReadValue(address, UpperComputer.PLC.DataType.SHORT, ref value1);
                            bool result2 = PLC.ReadValue(address, UpperComputer.PLC.DataType.SHORT, ref value2);
                            bool valueEqual = result1 == result2;
                            if (valueEqual)
                            {
                                return value1.ToString();
                            }
                            else
                            {
                                //Log.alarmLog($"读取失败地址{address},结果{result.Content},{result1.Content}", 1);
                            }
                        }
                        else if (type.ToUpper() == "BOOL")
                        {
                            object value1 = 0, value2 = 0;
                            bool result1 = PLC.ReadValue(address, UpperComputer.PLC.DataType.BOOL, ref value1);
                            bool result2 = PLC.ReadValue(address, UpperComputer.PLC.DataType.BOOL, ref value2);
                            bool valueEqual = result1 == result2;
                            if (valueEqual)
                            {
                                return value1.ToString();
                            }
                            else
                            {
                                //Log.alarmLog($"读取失败地址{address},结果{result.Content},{result1.Content}", 1);
                            }
                        }
                        else if (type.ToUpper() == "DOUBLE")
                        {
                            object value1 = 0, value2 = 0;
                            bool result1 = PLC.ReadValue(address, UpperComputer.PLC.DataType.DOUBLE, ref value1);
                            bool result2 = PLC.ReadValue(address, UpperComputer.PLC.DataType.DOUBLE, ref value2);
                            bool valueEqual = result1 == result2;
                            if (valueEqual)
                            {
                                return value1.ToString();
                            }
                            else
                            {
                                //Log.alarmLog($"读取失败地址{address},结果{result.Content},{result1.Content}", 1);
                            }
                        }
                        else if (type.ToUpper() == "STRING")
                        {
                            object value1 = 0, value2 = 0;
                            bool result1 = PLC.ReadValue(address, UpperComputer.PLC.DataType.STRING, ref value1, 12);
                            bool result2 = PLC.ReadValue(address, UpperComputer.PLC.DataType.STRING, ref value2, 12);
                            bool valueEqual = result1 == result2;
                            if (valueEqual)
                            {
                                return value1.ToString();
                            }
                            else
                            {
                                //Log.alarmLog($"读取失败地址{address},结果{result.Content},{result1.Content}", 1);
                            }
                        }
                        else if (type.ToUpper() == "FLOAT")
                        {
                            object value1 = 0, value2 = 0;
                            bool result1 = PLC.ReadValue(address, UpperComputer.PLC.DataType.FLOAT, ref value1);
                            bool result2 = PLC.ReadValue(address, UpperComputer.PLC.DataType.FLOAT, ref value2);
                            bool valueEqual = result1 == result2;
                            if (valueEqual)
                            {
                                return value1.ToString();
                            }
                            else
                            {
                                //Log.alarmLog($"读取失败地址{address},结果{result.Content},{result1.Content}", 1);
                            }
                        }
                        else
                        {
                            return "ERROR";
                        }
                    }
                    return "ERROR";
                }
                catch (Exception ex)
                {
                    return "ERROR";
                }
            }
        }

        public object obj = new object();
        public override bool WritePLC(string name, object value)
        {
            lock (obj)
            {
                try
                {
                    var address = dicPLC[name].PointAddress;
                    var type = dicPLC[name].PointType;
                    for (int i = 0; i < 8; i++)
                    {
                        if (type.ToUpper() == "INT")
                        {
                            var result = PLC.WriteValue(address, UpperComputer.PLC.DataType.INT, int.Parse(value.ToString()));
                            if (result == true && ReadPLC(address, type).ToString() == value.ToString())
                            {
                                return true;
                            }
                        }
                        else if (type.ToUpper() == "SHORT")
                        {
                            var result = PLC.WriteValue(address, UpperComputer.PLC.DataType.SHORT, short.Parse(value.ToString()));
                            string value1 = ReadPLC(address, type).ToString();
                            if (result == true && value1 == value.ToString())
                            {
                                return true;
                            }
                            else
                            {
                                //Log.alarmLog("失败" + address, 1);
                            }
                        }
                        else if (type.ToUpper() == "USHORT")
                        {
                            var result = PLC.WriteValue(address, UpperComputer.PLC.DataType.SHORT, ushort.Parse(value.ToString()));
                            if (result == true && ReadPLC(address, type).ToString() == value.ToString())
                            {
                                return true;
                            }
                        }
                        else if (type.ToUpper() == "BOOL")
                        {
                            var result = PLC.WriteValue(address, UpperComputer.PLC.DataType.BOOL, Convert.ToBoolean(value));
                            if (result == true && ReadPLC(address, type).ToString().ToLower() == value.ToString().ToLower())
                            {
                                return true;
                            }

                        }
                        else if (type.ToUpper() == "DOUBLE")
                        {
                            var result = PLC.WriteValue(address, UpperComputer.PLC.DataType.DOUBLE, Convert.ToDouble(value));
                            if (result == true && ReadPLC(address, type).ToString() == value.ToString())
                            {
                                return true;
                            }
                        }
                        else if (type.ToUpper() == "FLOAT")
                        {
                            var result = PLC.WriteValue(address, UpperComputer.PLC.DataType.FLOAT, float.Parse(value.ToString()));
                            if (result == true && ReadPLC(address, type).ToString() == value.ToString())
                            {
                                return true;
                            }
                        }
                        else if (type.ToUpper() == "STRING")
                        {
                            var result = PLC.WriteValue(address, UpperComputer.PLC.DataType.STRING, value.ToString(), 12);
                            if (result == true /*&& ReadPLC(address, type).ToString() == value.ToString()*/)
                            {
                                return true;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    return false;
                }
                catch (Exception)
                {
                    Log.alarmLog($"当前PLC地址字典中不存在{name}", 1);
                    return false;
                }
            }
        }

        public override bool connectToPlc()
        {
            return PLC.Open();
        }

        public override void intance()
        {
            //PLC = new HslCommunication.Profinet.Omron.OmronFinsUdp();
            //PLC.IpAddress = ClassCANS.OLD.PLCip1;
            //PLC.Port = ClassCANS.OLD.PROT;
            //PLC.SA1 = 192;
            //PLC.DA2 = 0;
            //PLC.ReceiveTimeout = 2000;
            //PLC.ByteTransform.DataFormat = (DataFormat)2;
            PLC = new OmronPlCUDP(ClassCANS.OLD.PLCip1, ClassCANS.OLD.PROT, ClassCANS.OLD.s_PCIP, 2000);
        }
    }
}
