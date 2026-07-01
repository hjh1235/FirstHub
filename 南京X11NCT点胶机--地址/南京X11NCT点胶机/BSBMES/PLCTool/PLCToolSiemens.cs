using HslCommunication.Profinet.Siemens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UpperComputer
{
    class PLCToolSiemens : PLCTool
    {
        /// <summary>
        /// 状态
        /// </summary>
        public new HslCommunication.Profinet.Siemens.SiemensS7Net PLC;
        public override void ResPLCData()
        {
            while (true)
            {
                Thread.Sleep(50);
                try
                {
                    if (canListen == false)
                        continue;
                    foreach (var item in dicPLC)
                    {
                        var result = ReadPLC(item.Value.PointAddress, item.Value.PointType);
                        item.Value.PointResult = result.ToString();
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
                    var result = PLC.ReadInt32(address);
                    if (result.IsSuccess == true)
                    {
                        return result.Content;
                    }
                    else
                    {
                        return "-1";
                    }
                }
                else if (type.ToUpper() == "SHORT")
                {
                    var result = PLC.ReadInt16(address);
                    if (result.IsSuccess == true)
                    {
                        string i = result.Content.ToString();
                        return result.Content.ToString();
                    }
                    else
                    {
                        return "-1";
                    }
                }
                else if (type.ToUpper() == "USHORT")
                {
                    var result = PLC.ReadUInt16(address);
                    if (result.IsSuccess == true)
                    {
                        return result.Content;
                    }
                    else
                    {
                        return "-1";
                    }
                }
                else if (type.ToUpper() == "BOOL")
                {
                    var result = PLC.ReadBool(address);
                    if (result.IsSuccess == true)
                    {
                        return result.Content;
                    }
                    else
                    {
                        return "-1";
                    }
                }
                else if (type.ToUpper() == "DOUBLE")
                {
                    var result = PLC.ReadDouble(address);
                    if (result.IsSuccess == true)
                    {
                        return result.Content;
                    }
                    else
                    {
                        return "-1";
                    }
                }
                else if (type.ToUpper() == "STRING")
                {
                    var result = PLC.ReadString(address);
                    if (result.IsSuccess == true)
                    {
                        return result.Content;
                    }
                    else
                    {
                        return "-1";
                    }
                }
                else if (type.ToUpper() == "FLOAT")
                {
                    var result = PLC.ReadFloat(address);
                    if (result.IsSuccess == true)
                    {
                        return (float)result.Content;
                    }
                    else
                    {
                        return "-1";
                    }
                }
                else
                {
                    return "-1";
                }
            }
            catch (Exception)
            {

                return "-1";
            }
        }

        public object obj = new object();
        public override bool WritePLC(string address, string type, string value)
        {
            try
            {
                for (int i = 0; i < 5; i++)
                {
                    if (type.ToUpper() == "INT")
                    {
                        var result = PLC.Write(address, int.Parse(value));
                        if (result.IsSuccess == true && ReadPLC(address, type).ToString() == value.ToString())
                        {
                            return true;
                        }
                    }
                    else if (type.ToUpper() == "SHORT")
                    {
                        var result = PLC.Write(address, Int16.Parse(value));
                        if (result.IsSuccess == true && ReadPLC(address, type).ToString() == value.ToString())
                        {
                            return true;
                        }

                    }
                    else if (type.ToUpper() == "USHORT")
                    {
                        var result = PLC.Write(address, uint.Parse(value));
                        if (result.IsSuccess == true)
                        {
                            return true;
                        }

                    }
                    else if (type.ToUpper() == "BOOL")
                    {
                        var result = PLC.Write(address, Convert.ToBoolean(value));
                        if (result.IsSuccess == true)
                        {
                            return true;
                        }

                    }
                    else if (type.ToUpper() == "DOUBLE")
                    {
                        var result = PLC.Write(address, Convert.ToDouble(value));
                        if (result.IsSuccess == true)
                        {
                            return true;
                        }
                    }
                    else if (type.ToUpper() == "STRING")
                    {
                        var result = PLC.Write(address, value);
                        if (result.IsSuccess == true)
                        {
                            return true;
                        }
                    }
                    else if (type.ToUpper() == "FLOAT")
                    {
                        var result = PLC.Write(address, Convert.ToSingle(value));
                        if (result.IsSuccess == true)
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

        public override object ReadPLC(string name)
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
                            var result = PLC.ReadInt32(address);
                            var result1 = PLC.ReadInt32(address);
                            bool valueEqual = result.Content.ToString() == result1.Content.ToString();
                            if (result.IsSuccess == true && valueEqual)
                            {
                                return result.Content;
                            }
                            else
                            {
                                Log.alarmLog($"读取失败地址{address},结果{result},{result1}",1);
                            }
                        }
                        else if (type.ToUpper() == "SHORT")
                        {
                            var result = PLC.ReadInt16(address);
                            var result1 = PLC.ReadInt16(address);
                            bool valueEqual = result.Content.ToString() == result1.Content.ToString();
                            if (result.IsSuccess == true && valueEqual)
                            {
                                return result.Content;
                            }

                        }
                        else if (type.ToUpper() == "USHORT")
                        {
                            var result = PLC.ReadUInt16(address);
                            var result1 = PLC.ReadUInt16(address);
                            bool valueEqual = result.Content.ToString() == result1.Content.ToString();
                            if (result.IsSuccess == true && valueEqual)
                            {
                                return result.Content;
                            }

                        }
                        else if (type.ToUpper() == "BOOL")
                        {
                            var result = PLC.ReadBool(address);
                            var result1 = PLC.ReadBool(address);
                            bool valueEqual = result.Content.ToString() == result1.Content.ToString();
                            if (result.IsSuccess == true && valueEqual)
                            {
                                return result.Content;
                            }

                        }
                        else if (type.ToUpper() == "DOUBLE")
                        {
                            var result = PLC.ReadDouble(address);
                            var result1 = PLC.ReadDouble(address);
                            bool valueEqual = result.Content.ToString() == result1.Content.ToString();
                            if (result.IsSuccess == true && valueEqual)
                            {
                                return result.Content;
                            }
                            else
                            {
                                Log.alarmLog($"读取失败地址{address},结果{result},{result1}",1);
                            }
                        }
                        else if (type.ToUpper() == "STRING")
                        {
                            var result = PLC.ReadString(address, 24);
                            var result1 = PLC.ReadString(address, 24);
                            bool valueEqual = result.Content.ToString() == result1.Content.ToString();
                            if (result.IsSuccess == true && valueEqual)
                            {
                                return result.Content;
                            }
                            else
                            {
                                Log.alarmLog($"读取失败地址{address},结果{result},{result1}",1);
                            }
                        }
                        else if (type.ToUpper() == "FLOAT")
                        {
                            var result = PLC.ReadFloat(address);
                            var result1 = PLC.ReadFloat(address);
                            bool valueEqual = result.Content.ToString() == result1.Content.ToString();
                            if (result.IsSuccess == true && valueEqual)
                            {
                                return result.Content;
                            }
                            else
                            {
                                Log.alarmLog($"读取失败地址{address},结果{result},{result1}",1);
                            }
                        }
                        else
                        {
                            return "-1";
                        }
                    }
                    return "-1";
                }
                catch (Exception)
                {
                    return "-1";
                }
            }
        }
        static object objData = new object();
       
        public override bool WritePLC(string name, object value)
        {
            //lock (obj)
            //{
            try
            {
                var address = dicPLC[name].PointAddress;
                var type = dicPLC[name].PointType;
                for (int i = 0; i < 5; i++)
                {
                    if (type.ToUpper() == "INT")
                    {
                        var result = PLC.Write(address, int.Parse(value.ToString()));
                        if (result.IsSuccess == true && ReadPLC(address, type).ToString() == value.ToString())
                        {
                            return true;
                        }
                        else
                        {
                            Log.log("失败" + address);
                        }
                    }
                    else if (type.ToUpper() == "SHORT")
                    {
                        var result = PLC.Write(address, short.Parse(value.ToString()));
                        if (result.IsSuccess == true && ReadPLC(address, type).ToString() == value.ToString())
                        {
                            return true;
                        }
                        else
                        {
                            Log.log("失败" + address);
                        }
                    }
                    else if (type.ToUpper() == "USHORT")
                    {
                        var result = PLC.Write(address, ushort.Parse(value.ToString()));
                        if (result.IsSuccess == true && ReadPLC(address, type).ToString() == value.ToString())
                        {
                            return true;
                        }

                    }
                    else if (type.ToUpper() == "BOOL")
                    {
                        var result = PLC.Write(address, Convert.ToBoolean(value));
                        if (result.IsSuccess == true && ReadPLC(address, type).ToString() == value.ToString())
                        {
                            return true;
                        }

                    }
                    else if (type.ToUpper() == "DOUBLE")
                    {
                        var result = PLC.Write(address, Convert.ToDouble(value));
                        if (result.IsSuccess == true && ReadPLC(address, type).ToString() == value.ToString())
                        {
                            return true;
                        }
                    }
                    else if (type.ToUpper() == "FLOAT")
                    {
                        var result = PLC.Write(address, float.Parse(value.ToString()));
                        if (result.IsSuccess == true && ReadPLC(address, type).ToString() == value.ToString())
                        {
                            return true;
                        }
                    }
                    else if (type.ToUpper() == "STRING")
                    {
                        var result = PLC.Write(address, value.ToString());
                        if (result.IsSuccess == true && ReadPLC(address, type).ToString() == value.ToString())
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
                Log.log($"当前PLC地址字典中不存在{name}");
                return false;
            }
            //}
        }
        static object objRead = new object();
        public override bool WritePLCData(string address, object value)
        {
            lock (objRead)
            {
                var type = "INT";
                try
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (type.ToUpper() == "INT")
                        {
                            var result = PLC.Write(address, int.Parse(value.ToString()));
                            if (result.IsSuccess == true)
                            {
                                return true;
                            }
                        }
                        else if (type.ToUpper() == "SHORT")
                        {
                            var result = PLC.Write(address, short.Parse(value.ToString()));
                            if (result.IsSuccess == true)
                            {
                                return true;
                            }
                        }
                        else if (type.ToUpper() == "USHORT")
                        {
                            var result = PLC.Write(address, ushort.Parse(value.ToString()));
                            if (result.IsSuccess == true)
                            {
                                return true;
                            }

                        }
                        else if (type.ToUpper() == "BOOL")
                        {
                            var result = PLC.Write(address, Convert.ToBoolean(value));
                            if (result.IsSuccess == true)
                            {
                                return true;
                            }

                        }
                        else if (type.ToUpper() == "DOUBLE")
                        {
                            var result = PLC.Write(address, Convert.ToDouble(value));
                            if (result.IsSuccess == true)
                            {
                                return true;
                            }
                        }
                        else if (type.ToUpper() == "FLOAT")
                        {
                            var result = PLC.Write(address, float.Parse(value.ToString()));
                            if (result.IsSuccess == true)
                            {
                                return true;
                            }
                        }
                        else if (type.ToUpper() == "STRING")
                        {
                            var result = PLC.Write(address, value.ToString());
                            if (result.IsSuccess == true)
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
        }
        public override bool connectToPlc()
        {
            if (PLC.ConnectServer().IsSuccess)
            {
                return true;
            }
            return false;
        }

        public override void intance()
        {
            ///PLC地址
            PLC = new HslCommunication.Profinet.Siemens.SiemensS7Net(SiemensPLCS.S1500, ClassCANS.OLD.PLCip1);
            PLC.Port = ClassCANS.OLD.PROT; //PLC 端口
            PLC.Slot = 0;
            PLC.Rack = 0;
            PLC.ConnectTimeOut = 2000;
            PLC.ReceiveTimeOut = 500;
        }
    }
}
