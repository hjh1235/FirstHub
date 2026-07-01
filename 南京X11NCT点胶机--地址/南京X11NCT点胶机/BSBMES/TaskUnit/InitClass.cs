using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UpperComputer.TaskUnit
{
    public class InitClass
    {
        public static Dictionary<string, LaserPar> dic1 = new Dictionary<string, LaserPar>();
        public static Dictionary<string, LaserPar> dic2 = new Dictionary<string, LaserPar>();

        public static Dictionary<string, Dictionary<string, string>> keyValuePairs1 = new Dictionary<string, Dictionary<string, string>>();
        public static Dictionary<string, Dictionary<string, string>> keyValuePairs2 = new Dictionary<string, Dictionary<string, string>>();

        public static Dictionary<string, ResultPar> LoaddicPar = new Dictionary<string, ResultPar>();
        public static List<double> CCDArea = new List<double>();   
        public static List<string> Proportion = new List<string>();   
        public static List<double> CCDArea2 = new List<double>();   
        public static List<string> Proportion2 = new List<string>();   
        //public static object ReadPLC(string name)
        //{
        //    try
        //    {
        //        Thread.Sleep(1);
        //        var address = FormStart.pLCTool.dicPLC[name].PointAddress;
        //        var type = FormStart.pLCTool.dicPLC[name].PointType;
        //        if (type.ToUpper() == "INT")
        //        {
        //            var result = FormStart.pLCTool.PLC.ReadInt32(address);
        //            if (result.IsSuccess == true)
        //            {
        //                return result.Content;
        //            }
        //            else
        //            {
        //                return "-1";
        //            }
        //        }
        //        else if (type.ToUpper() == "SHORT")
        //        {
        //            var result = FormStart.pLCTool.PLC.ReadInt16(address);
        //            if (result.IsSuccess == true)
        //            {
        //                string i = result.Content.ToString();
        //                return result.Content.ToString();
        //            }
        //            else
        //            {
        //                return "-1";
        //            }
        //        }
        //        else if (type.ToUpper() == "USHORT")
        //        {
        //            var result = FormStart.pLCTool.PLC.ReadUInt16(address);
        //            if (result.IsSuccess == true)
        //            {
        //                return result.Content;
        //            }
        //            else
        //            {
        //                return "-1";
        //            }
        //        }
        //        else if (type.ToUpper() == "BOOL")
        //        {
        //            var result = FormStart.pLCTool.PLC.ReadBool(address);
        //            if (result.IsSuccess == true)
        //            {
        //                return result.Content;
        //            }
        //            else
        //            {
        //                return "-1";
        //            }
        //        }
        //        else if (type.ToUpper() == "DOUBLE")
        //        {
        //            var result = FormStart.pLCTool.PLC.ReadDouble(address);
        //            if (result.IsSuccess == true)
        //            {
        //                return result.Content;
        //            }
        //            else
        //            {
        //                return "-1";
        //            }
        //        }
        //        else if (type.ToUpper() == "STRING")
        //        {
        //            var result = FormStart.pLCTool.PLC.ReadString(address, 32);
        //            if (result.IsSuccess == true)
        //            {
        //                return result.Content.ToString();
        //            }
        //            else
        //            {
        //                return "-1";
        //            }
        //        }
        //        else if (type.ToUpper() == "FLOAT")
        //        {
        //            var result = FormStart.pLCTool.PLC.ReadFloat(address);
        //            if (result.IsSuccess == true)
        //            {
        //                return result.Content;
        //            }
        //            else
        //            {
        //                return "-1";
        //            }
        //        }
        //        else
        //        {
        //            return "-1";
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        return "-1";
        //    }
        //}
        public void plcInit()
        {
            while (true)
            {
                Thread.Sleep(50);
                var mModels = MainForm1.txClasses.Find((c) => c.Name == "初始化");
                if (FormStart.pLCTool.ReadPLC("初始化").ToString() == "1")
                {
                    mModels.bStatus = false;
                    FormStart.bInit = false;
                    foreach (var item in TaskClass.taskThreadList)
                    {
                        item.action?.BeginInvoke(item.GetType().ToString(), callback =>
                        {

                        }, null);
                    }
                    mModels.bStatus = true;
                    FormStart.pLCTool.WritePLC("初始化", "2");
                    FormStart.pLCTool.WritePLC("初始化完成", "1");
                    FormStart.bInit = true;
                    Log.log("所有流程初始化完成");
                }

            }
        }
        string point1 = "0";
        int npoint1 = 0;

        public void Gather1()
        {
            while (true)
            {
                Thread.Sleep(500);
                try
                {
                    point1 = FormStart.pLCTool.ReadPLC("1#机器人_测高点位1").ToString();
                    if (point1 == "0" || point1 == "999")
                        continue;
                    if (int.TryParse(point1, out npoint1))
                    {
                        GetLaser1(npoint1);
                        //InitClass.dic1.Clear();
                    }
                }
                catch (Exception ex)
                {


                }

            }
        }
        string point2 = "0";
        int npoint2 = 0;

        public void Gather2()
        {
            while (true)
            {
                Thread.Sleep(500);
                try
                {
                    point2 = FormStart.pLCTool.ReadPLC("2#机器人_测高点位1").ToString();

                    if (point2 == "0" || point2 == "999")
                        continue;
                    if (int.TryParse(point2, out npoint2))
                    {
                        GetLaser2(npoint2);
                    }
                }
                catch (Exception ex)
                {


                }

            }
        }
        public void GetLaser1(int iPoint)
        {
            //保护气 DB101.16-DB101.492  8,9,24,25
            string strAir = "DB101.";
            int nStartAir = 16;

            //极差 DB101.976-DB101.1454
            string strRange = "DB101.";
            int nStartRange = 976;

            //离焦补偿 DB101.1456-DB101.1934
            string strCompensate = "DB101.";
            int nStartCompenstate = 1456;

            //中心功率 DB101.1936-DB101.2414
            string strCenterPower = "DB101.";
            int nStartCenterPower = 1936;

            //外环功率 DB101.2416-DB101.2894
            string strOutPower = "DB101.";
            int nStartOutPower = 2416;

            if (iPoint == 8 || iPoint == 9 || iPoint == 24 || iPoint == 25)
            {
                int nPoint1 = 0, nPoint2 = 0;
                //29,30
                if (iPoint == 8)
                {
                    //点位
                    nPoint1 = (4 * (iPoint - 1)) + 1;
                    nPoint2 = (4 * (iPoint - 1)) + 2;
                }
                //31,32
                else if (iPoint == 9)
                {
                    nPoint1 = (4 * (iPoint - 1)) - 1;
                    nPoint2 = (4 * (iPoint - 1));
                }
                //89,90
                if (iPoint == 24)
                {
                    //点位
                    nPoint1 = (4 * (iPoint - 1)) + 1 - 4;
                    nPoint2 = (4 * (iPoint - 1)) + 2 - 4;
                }
                //91,92
                else if (iPoint == 25)
                {
                    //点位
                    nPoint1 = (4 * (iPoint - 1)) - 1 - 4;
                    nPoint2 = (4 * (iPoint - 1)) - 4;
                }
                //保护气
                int iPoint1 = (nPoint1 - 1) * 4;
                int iPoint2 = (nPoint2 - 1) * 4;

                //极差、离焦补偿、中心功率、外环功率
                int iAdd1 = (nPoint1 - 1) * 2;
                int iAdd2 = (nPoint2 - 1) * 2;

                //保护气
                string strAirAddress1 = strAir + (nStartAir + iPoint1);
                string strAirAddress2 = strAir + (nStartAir + iPoint1);
                string strAirValue1 = FormStart.pLCTool.ReadPLC(strAirAddress1, "float").ToString();
                string strAirValue2 = FormStart.pLCTool.ReadPLC(strAirAddress2, "float").ToString();

                //极差
                string strRangeAddress3 = strRange + (nStartRange + iAdd1);
                string strRangeAddress4 = strRange + (nStartRange + iAdd2);
                string strRangeValue1 = FormStart.pLCTool.ReadPLC(strRangeAddress3, "Short").ToString();
                string strRangeValue2 = FormStart.pLCTool.ReadPLC(strRangeAddress4, "Short").ToString();
                double dRangeValue1 = 0, dRangeValue2 = 0;
                if (double.TryParse(strRangeValue1, out dRangeValue1) && double.TryParse(strRangeValue2, out dRangeValue2))
                {
                    dRangeValue1 = dRangeValue1 / 100;
                    dRangeValue2 = dRangeValue2 / 100;
                }
                //离焦补偿
                string strComAddress5 = strCompensate + (nStartCompenstate + iAdd1);
                string strComAddress6 = strCompensate + (nStartCompenstate + iAdd2);

                string strComValue1 = FormStart.pLCTool.ReadPLC(strComAddress5, "Short").ToString();
                string strComValue2 = FormStart.pLCTool.ReadPLC(strComAddress6, "Short").ToString();
                double dComValue1 = 0, dComValue2 = 0;
                if (double.TryParse(strComValue1, out dComValue1) && double.TryParse(strComValue2, out dComValue2))
                {
                    dComValue1 = dComValue1 / 100;
                    dComValue2 = dComValue2 / 100;
                }

                //中心功率
                string strCenterAddress7 = strCenterPower + (nStartCenterPower + iAdd1);
                string strCenterAddress8 = strCenterPower + (nStartCenterPower + iAdd2);
                string strCenterValue1 = FormStart.pLCTool.ReadPLC(strCenterAddress7, "Short").ToString();
                string strCenterValue2 = FormStart.pLCTool.ReadPLC(strCenterAddress8, "Short").ToString();

                //外环功率
                string strOutAddress9 = strOutPower + (nStartOutPower + iAdd1);
                string strOutAddress10 = strOutPower + (nStartOutPower + iAdd2);
                string strOutValue1 = FormStart.pLCTool.ReadPLC(strOutAddress9, "Short").ToString();
                string strOutValue2 = FormStart.pLCTool.ReadPLC(strOutAddress10, "Short").ToString();

                LaserPar laserPar1 = new LaserPar { 组 = iPoint.ToString(),点位=nPoint1.ToString(), 保护气 = strAirValue1, 极差值 = dRangeValue1.ToString(), 离焦补偿 = dComValue1.ToString(), 中心实际功率 = strCenterValue1, 外环实际功率 = strOutValue1 };
                LaserPar laserPar2 = new LaserPar { 组 = iPoint.ToString(), 点位 = nPoint2.ToString(), 保护气 = strAirValue2, 极差值 = dRangeValue2.ToString(), 离焦补偿 = dComValue2.ToString(), 中心实际功率 = strCenterValue2, 外环实际功率 = strOutValue2 };

                if (!dic1.ContainsKey(nPoint1.ToString()))
                {
                    dic1.Add(nPoint1.ToString(), laserPar1);
                }
                else
                {
                    dic1[nPoint1.ToString()] = laserPar1;
                }
                if (!dic1.ContainsKey(nPoint2.ToString()))
                {
                    dic1.Add(nPoint2.ToString(), laserPar2);
                }
                else
                {
                    dic1[nPoint2.ToString()] = laserPar2;
                }


                //if (strAirValue1 != "0" && strAirValue2 != "0" && strRangeValue1 != "0" && strRangeValue2 != "0" && strOutValue1 != "0" && strOutValue2 != "0" && strCenterValue1 != "0" && strCenterValue2 != "0")
                //{
                //    LaserPar laserPar1 = new LaserPar { strPoint = iPoint.ToString(), 保护气 = strAirValue1, 极差值 = strRangeValue1, 离焦补偿 = strComValue1, 中心实际功率 = strCenterValue1, 外环实际功率 = strOutValue1 };
                //    LaserPar laserPar2 = new LaserPar { strPoint = iPoint.ToString(), 保护气 = strAirValue2, 极差值 = strRangeValue2, 离焦补偿 = strComValue2, 中心实际功率 = strCenterValue2, 外环实际功率 = strOutValue2 };

                //    if (!dic1.ContainsKey(nPoint1.ToString()))
                //    {
                //        dic1.Add(nPoint1.ToString(), laserPar1);
                //    }
                //    else
                //    {
                //        dic1[nPoint1.ToString()] = laserPar1;
                //    }
                //    if (!dic1.ContainsKey(nPoint2.ToString()))
                //    {
                //        dic1.Add(nPoint2.ToString(), laserPar2);
                //    }
                //    else
                //    {
                //        dic1[nPoint2.ToString()] = laserPar2;
                //    }
                //}
            }
            else
            {
                int nPoint1 = 0, nPoint2 = 0, nPoint3 = 0, nPoint4 = 0;
                //点位
                if (iPoint < 8 && iPoint > 0)
                {
                    nPoint1 = (4 * (iPoint - 1)) + 1;
                    nPoint2 = (4 * (iPoint - 1)) + 2;
                    nPoint3 = (4 * (iPoint - 1)) + 3;
                    nPoint4 = (4 * (iPoint - 1)) + 4;
                }
                else if (iPoint > 9 && iPoint < 24)
                {
                    nPoint1 = (4 * (iPoint - 1)) + 1 - 4;
                    nPoint2 = (4 * (iPoint - 1)) + 2 - 4;
                    nPoint3 = (4 * (iPoint - 1)) + 3 - 4;
                    nPoint4 = (4 * (iPoint - 1)) + 4 - 4;
                }
                else if (iPoint > 24)
                {
                    nPoint1 = (4 * (iPoint - 1)) + 1 - 8;
                    nPoint2 = (4 * (iPoint - 1)) + 2 - 8;
                    nPoint3 = (4 * (iPoint - 1)) + 3 - 8;
                    nPoint4 = (4 * (iPoint - 1)) + 4 - 8;
                }
                else
                {
                    return;
                }
                //保护气、PLC地址偏移加4
                int iPoint1 = (nPoint1 - 1) * 4;
                int iPoint2 = (nPoint2 - 1) * 4;
                int iPoint3 = (nPoint3 - 1) * 4;
                int iPoint4 = (nPoint4 - 1) * 4;

                //极差、离焦补偿、中心功率、外环功率、PLC地址偏移加2
                int iAdd1 = (nPoint1 - 1) * 2;
                int iAdd2 = (nPoint2 - 1) * 2;
                int iAdd3 = (nPoint3 - 1) * 2;
                int iAdd4 = (nPoint4 - 1) * 2;

                //保护气
                string strAirAddress1 = strAir + (nStartAir + iPoint1);
                string strAirAddress2 = strAir + (nStartAir + iPoint2);
                string strAirAddress3 = strAir + (nStartAir + iPoint3);
                string strAirAddress4 = strAir + (nStartAir + iPoint4);
                string strAirValue1 = FormStart.pLCTool.ReadPLC(strAirAddress1, "float").ToString();
                string strAirValue2 = FormStart.pLCTool.ReadPLC(strAirAddress2, "float").ToString();
                string strAirValue3 = FormStart.pLCTool.ReadPLC(strAirAddress3, "float").ToString();
                string strAirValue4 = FormStart.pLCTool.ReadPLC(strAirAddress4, "float").ToString();

                //极差
                string strRangeAddress1 = strRange + (nStartRange + iAdd1);
                string strRangeAddress2 = strRange + (nStartRange + iAdd2);
                string strRangeAddress3 = strRange + (nStartRange + iAdd3);
                string strRangeAddress4 = strRange + (nStartRange + iAdd4);
                string strRangeValue1 = FormStart.pLCTool.ReadPLC(strRangeAddress1, "Short").ToString();
                string strRangeValue2 = FormStart.pLCTool.ReadPLC(strRangeAddress2, "Short").ToString();
                string strRangeValue3 = FormStart.pLCTool.ReadPLC(strRangeAddress3, "Short").ToString();
                string strRangeValue4 = FormStart.pLCTool.ReadPLC(strRangeAddress4, "Short").ToString();
                double dRangeValue1 = 0, dRangeValue2 = 0, dRangeValue3 = 0, dRangeValue4 = 0;
                if (double.TryParse(strRangeValue1, out dRangeValue1) && double.TryParse(strRangeValue2, out dRangeValue2)&& double.TryParse(strRangeValue3, out dRangeValue3) && double.TryParse(strRangeValue4, out dRangeValue4))
                {
                    dRangeValue1 = dRangeValue1 / 100;
                    dRangeValue2 = dRangeValue2 / 100;
                    dRangeValue3 = dRangeValue3 / 100;
                    dRangeValue4 = dRangeValue4 / 100;
                }

                //离焦补偿
                string strComAddress1 = strCompensate + (nStartCompenstate + iAdd1);
                string strComAddress2 = strCompensate + (nStartCompenstate + iAdd2);
                string strComAddress3 = strCompensate + (nStartCompenstate + iAdd3);
                string strComAddress4 = strCompensate + (nStartCompenstate + iAdd4);
                string strComValue1 = FormStart.pLCTool.ReadPLC(strComAddress1, "Short").ToString();
                string strComValue2 = FormStart.pLCTool.ReadPLC(strComAddress2, "Short").ToString();
                string strComValue3 = FormStart.pLCTool.ReadPLC(strComAddress3, "Short").ToString();
                string strComValue4 = FormStart.pLCTool.ReadPLC(strComAddress4, "Short").ToString();
                double dComValue1 = 0, dComValue2 = 0, dComValue3 = 0, dComValue4 = 0;
                if (double.TryParse(strComValue1, out dComValue1) && double.TryParse(strComValue2, out dComValue2)&& double.TryParse(strComValue3, out dComValue3) && double.TryParse(strComValue4, out dComValue4))
                {
                    dComValue1 = dComValue1 / 100;
                    dComValue2 = dComValue2 / 100;
                    dComValue3 = dComValue3 / 100;
                    dComValue4 = dComValue4 / 100;
                }

                //中心功率
                string strCenterAddress1 = strCenterPower + (nStartCenterPower + iAdd1);
                string strCenterAddress2 = strCenterPower + (nStartCenterPower + iAdd2);
                string strCenterAddress3 = strCenterPower + (nStartCenterPower + iAdd3);
                string strCenterAddress4 = strCenterPower + (nStartCenterPower + iAdd4);
                string strCenterValue1 = FormStart.pLCTool.ReadPLC(strCenterAddress1, "Short").ToString();
                string strCenterValue2 = FormStart.pLCTool.ReadPLC(strCenterAddress2, "Short").ToString();
                string strCenterValue3 = FormStart.pLCTool.ReadPLC(strCenterAddress3, "Short").ToString();
                string strCenterValue4 = FormStart.pLCTool.ReadPLC(strCenterAddress4, "Short").ToString();

                //外环功率
                string strOutAddress1 = strOutPower + (nStartOutPower + iAdd1);
                string strOutAddress2 = strOutPower + (nStartOutPower + iAdd2);
                string strOutAddress3 = strOutPower + (nStartOutPower + iAdd3);
                string strOutAddress4 = strOutPower + (nStartOutPower + iAdd4);
                string strOutValue1 = FormStart.pLCTool.ReadPLC(strOutAddress1, "Short").ToString();
                string strOutValue2 = FormStart.pLCTool.ReadPLC(strOutAddress2, "Short").ToString();
                string strOutValue3 = FormStart.pLCTool.ReadPLC(strOutAddress3, "Short").ToString();
                string strOutValue4 = FormStart.pLCTool.ReadPLC(strOutAddress4, "Short").ToString();
                LaserPar laserPar1 = new LaserPar { 组 = iPoint.ToString(),点位=nPoint1.ToString(), 保护气 = strAirValue1, 极差值 = dRangeValue1.ToString(), 离焦补偿 = dComValue1.ToString(), 中心实际功率 = strCenterValue1, 外环实际功率 = strOutValue1 };
                LaserPar laserPar2 = new LaserPar { 组 = iPoint.ToString(),点位=nPoint2.ToString(), 保护气 = strAirValue2, 极差值 = dRangeValue2.ToString(), 离焦补偿 = dComValue2.ToString(), 中心实际功率 = strCenterValue2, 外环实际功率 = strOutValue2 };
                LaserPar laserPar3 = new LaserPar { 组 = iPoint.ToString(),点位=nPoint3.ToString(), 保护气 = strAirValue3, 极差值 = dRangeValue3.ToString(), 离焦补偿 = dComValue3.ToString(), 中心实际功率 = strCenterValue3, 外环实际功率 = strOutValue3 };
                LaserPar laserPar4 = new LaserPar { 组 = iPoint.ToString(),点位=nPoint4.ToString(), 保护气 = strAirValue4, 极差值 = dRangeValue4.ToString(), 离焦补偿 = dComValue4.ToString(), 中心实际功率 = strCenterValue4, 外环实际功率 = strOutValue4 };

                if (!dic1.ContainsKey(nPoint1.ToString()))
                {
                    dic1.Add(nPoint1.ToString(), laserPar1);
                }
                else
                {
                    dic1[nPoint1.ToString()] = laserPar1;
                }
                if (!dic1.ContainsKey(nPoint2.ToString()))
                {
                    dic1.Add(nPoint2.ToString(), laserPar2);
                }
                else
                {
                    dic1[nPoint2.ToString()] = laserPar2;
                }
                if (!dic1.ContainsKey(nPoint3.ToString()))
                {
                    dic1.Add(nPoint3.ToString(), laserPar3);
                }
                else
                {
                    dic1[nPoint3.ToString()] = laserPar3;
                }
                if (!dic1.ContainsKey(nPoint4.ToString()))
                {
                    dic1.Add(nPoint4.ToString(), laserPar4);
                }
                else
                {
                    dic1[nPoint4.ToString()] = laserPar4;
                }
                //if (strAirValue1 != "0" && strAirValue2 != "0" && strAirValue3 != "0" && strAirValue4 != "0" &&
                //    strRangeValue1 != "0" && strRangeValue2 != "0" && strRangeValue3 != "0" && strRangeValue4 != "0" &&
                //    strOutValue1 != "0" && strOutValue2 != "0" && strOutValue3 != "0" && strOutValue4 != "0" &&
                //    strCenterValue1 != "0" && strCenterValue2 != "0" && strCenterValue3 != "0" && strCenterValue4 != "0")
                //{

                //}
            }
        }
        public void GetLaser2(int iPoint)
        {
            //保护气 DB101.16-DB101.492  8,9,24,25
            string strAir = "DB102.";
            int nStartAir = 496;

            //极差 DB101.976-DB101.1454
            string strRange = "DB102.";
            int nStartRange = 1216;

            //离焦补偿 DB101.1456-DB101.1934
            string strCompensate = "DB102.";
            int nStartCompenstate = 1696;

            //中心功率 DB101.1936-DB101.2414
            string strCenterPower = "DB102.";
            int nStartCenterPower = 2176;

            //外环功率 DB101.2416-DB101.2894
            string strOutPower = "DB102.";
            int nStartOutPower = 2656;

            if (iPoint == 8 || iPoint == 9 || iPoint == 24 || iPoint == 25)
            {
                int nPoint1 = 0, nPoint2 = 0;
                //29,30
                if (iPoint == 8)
                {
                    //点位
                    nPoint1 = (4 * (iPoint - 1)) + 1;
                    nPoint2 = (4 * (iPoint - 1)) + 2;
                }
                //31,32
                else if (iPoint == 9)
                {
                    nPoint1 = (4 * (iPoint - 1)) - 1;
                    nPoint2 = (4 * (iPoint - 1));
                }
                //89,90
                if (iPoint == 24)
                {
                    //点位
                    nPoint1 = (4 * (iPoint - 1)) + 1 - 4;
                    nPoint2 = (4 * (iPoint - 1)) + 2 - 4;
                }
                //91,92
                else if (iPoint == 25)
                {
                    //点位
                    nPoint1 = (4 * (iPoint - 1)) - 1 - 4;
                    nPoint2 = (4 * (iPoint - 1)) - 4;
                }


                //保护气
                int iPoint1 = (nPoint1 - 1) * 4;
                int iPoint2 = (nPoint2 - 1) * 4;

                //极差、离焦补偿、中心功率、外环功率
                int iAdd1 = (nPoint1 - 1) * 2;
                int iAdd2 = (nPoint2 - 1) * 2;

                //保护气
                string strAirAddress1 = strAir + (nStartAir + iPoint1);
                string strAirAddress2 = strAir + (nStartAir + iPoint2);
                string strAirValue1 = FormStart.pLCTool.ReadPLC(strAirAddress1, "float").ToString();
                string strAirValue2 = FormStart.pLCTool.ReadPLC(strAirAddress2, "float").ToString();

                //极差
                string strRangeAddress3 = strRange + (nStartRange + iAdd1);
                string strRangeAddress4 = strRange + (nStartRange + iAdd2);
                string strRangeValue1 = FormStart.pLCTool.ReadPLC(strRangeAddress3, "Short").ToString();
                string strRangeValue2 = FormStart.pLCTool.ReadPLC(strRangeAddress4, "Short").ToString();
                double dRangeValue1 = 0, dRangeValue2 = 0;
                if (double.TryParse(strRangeValue1, out dRangeValue1) && double.TryParse(strRangeValue2, out dRangeValue2))
                {
                    dRangeValue1 = dRangeValue1 / 100;
                    dRangeValue2 = dRangeValue2 / 100;
                }
                //离焦补偿
                string strComAddress5 = strCompensate + (nStartCompenstate + iAdd1);
                string strComAddress6 = strCompensate + (nStartCompenstate + iAdd2);
                string strComValue1 = FormStart.pLCTool.ReadPLC(strComAddress5, "Short").ToString();
                string strComValue2 = FormStart.pLCTool.ReadPLC(strComAddress6, "Short").ToString();
                double dComValue1 = 0, dComValue2 = 0;
                if (double.TryParse(strComValue1, out dComValue1) && double.TryParse(strComValue2, out dComValue2))
                {
                    dComValue1 = dComValue1 / 100;
                    dComValue2 = dComValue2 / 100;
                }

                //中心功率
                string strCenterAddress7 = strCenterPower + (nStartCenterPower + iAdd1);
                string strCenterAddress8 = strCenterPower + (nStartCenterPower + iAdd2);
                string strCenterValue1 = FormStart.pLCTool.ReadPLC(strCenterAddress7, "Short").ToString();
                string strCenterValue2 = FormStart.pLCTool.ReadPLC(strCenterAddress8, "Short").ToString();

                //外环功率
                string strOutAddress9 = strOutPower + (nStartOutPower + iAdd1);
                string strOutAddress10 = strOutPower + (nStartOutPower + iAdd2);
                string strOutValue1 = FormStart.pLCTool.ReadPLC(strOutAddress9, "Short").ToString();
                string strOutValue2 = FormStart.pLCTool.ReadPLC(strOutAddress10, "Short").ToString();

                LaserPar laserPar1 = new LaserPar {组 = iPoint.ToString(),点位=iPoint1.ToString(), 保护气 = strAirValue1, 极差值 = dRangeValue1.ToString(), 离焦补偿 = dComValue1.ToString(), 中心实际功率 = strCenterValue1, 外环实际功率 = strOutValue1 };
                LaserPar laserPar2 = new LaserPar { 组 = iPoint.ToString(),点位=iPoint2.ToString(), 保护气 = strAirValue2, 极差值 = dRangeValue2.ToString(), 离焦补偿 =dComValue2.ToString(), 中心实际功率 = strCenterValue2, 外环实际功率 = strOutValue2 };

                if (!dic2.ContainsKey(nPoint1.ToString()))
                {
                    dic2.Add(nPoint1.ToString(), laserPar1);
                }
                else
                {
                    dic2[nPoint1.ToString()] = laserPar1;
                }
                if (!dic2.ContainsKey(nPoint2.ToString()))
                {
                    dic2.Add(nPoint2.ToString(), laserPar2);
                }
                else
                {
                    dic2[nPoint2.ToString()] = laserPar2;
                }
            }
            else
            {
                int nPoint1 = 0, nPoint2 = 0, nPoint3 = 0, nPoint4 = 0;
                //点位
                if (iPoint < 8 && iPoint > 0)
                {
                    nPoint1 = (4 * (iPoint - 1)) + 1;
                    nPoint2 = (4 * (iPoint - 1)) + 2;
                    nPoint3 = (4 * (iPoint - 1)) + 3;
                    nPoint4 = (4 * (iPoint - 1)) + 4;
                }
                else if (iPoint > 9 && iPoint < 24)
                {
                    nPoint1 = (4 * (iPoint - 1)) + 1 - 4;
                    nPoint2 = (4 * (iPoint - 1)) + 2 - 4;
                    nPoint3 = (4 * (iPoint - 1)) + 3 - 4;
                    nPoint4 = (4 * (iPoint - 1)) + 4 - 4;
                }
                else if (iPoint > 24)
                {
                    nPoint1 = (4 * (iPoint - 1)) + 1 - 8;
                    nPoint2 = (4 * (iPoint - 1)) + 2 - 8;
                    nPoint3 = (4 * (iPoint - 1)) + 3 - 8;
                    nPoint4 = (4 * (iPoint - 1)) + 4 - 8;
                }
                else
                {
                    return;
                }

                //保护气、PLC地址偏移加4
                int iPoint1 = (nPoint1 - 1) * 4;
                int iPoint2 = (nPoint2 - 1) * 4;
                int iPoint3 = (nPoint3 - 1) * 4;
                int iPoint4 = (nPoint4 - 1) * 4;

                //极差、离焦补偿、中心功率、外环功率、PLC地址偏移加2
                int iAdd1 = (nPoint1 - 1) * 2;
                int iAdd2 = (nPoint2 - 1) * 2;
                int iAdd3 = (nPoint3 - 1) * 2;
                int iAdd4 = (nPoint4 - 1) * 2;

                //保护气
                string strAirAddress1 = strAir + (nStartAir + iPoint1);
                string strAirAddress2 = strAir + (nStartAir + iPoint2);
                string strAirAddress3 = strAir + (nStartAir + iPoint3);
                string strAirAddress4 = strAir + (nStartAir + iPoint4);
                string strAirValue1 = FormStart.pLCTool.ReadPLC(strAirAddress1, "float").ToString();
                string strAirValue2 = FormStart.pLCTool.ReadPLC(strAirAddress2, "float").ToString();
                string strAirValue3 = FormStart.pLCTool.ReadPLC(strAirAddress3, "float").ToString();
                string strAirValue4 = FormStart.pLCTool.ReadPLC(strAirAddress4, "float").ToString();

                //极差
                string strRangeAddress1 = strRange + (nStartRange + iAdd1);
                string strRangeAddress2 = strRange + (nStartRange + iAdd2);
                string strRangeAddress3 = strRange + (nStartRange + iAdd3);
                string strRangeAddress4 = strRange + (nStartRange + iAdd4);
                string strRangeValue1 = FormStart.pLCTool.ReadPLC(strRangeAddress1, "Short").ToString();
                string strRangeValue2 = FormStart.pLCTool.ReadPLC(strRangeAddress2, "Short").ToString();
                string strRangeValue3 = FormStart.pLCTool.ReadPLC(strRangeAddress3, "Short").ToString();
                string strRangeValue4 = FormStart.pLCTool.ReadPLC(strRangeAddress4, "Short").ToString();
                double dRangeValue1 = 0, dRangeValue2 = 0, dRangeValue3 = 0, dRangeValue4 = 0;
                if (double.TryParse(strRangeValue1, out dRangeValue1) && double.TryParse(strRangeValue2, out dRangeValue2) && double.TryParse(strRangeValue3, out dRangeValue3) && double.TryParse(strRangeValue4, out dRangeValue4))
                {
                    dRangeValue1 = dRangeValue1 / 100;
                    dRangeValue2 = dRangeValue2 / 100;
                    dRangeValue3 = dRangeValue3 / 100;
                    dRangeValue4 = dRangeValue4 / 100;
                }

                //离焦补偿
                string strComAddress1 = strCompensate + (nStartCompenstate + iAdd1);
                string strComAddress2 = strCompensate + (nStartCompenstate + iAdd2);
                string strComAddress3 = strCompensate + (nStartCompenstate + iAdd3);
                string strComAddress4 = strCompensate + (nStartCompenstate + iAdd4);
                string strComValue1 = FormStart.pLCTool.ReadPLC(strComAddress1, "Short").ToString();
                string strComValue2 = FormStart.pLCTool.ReadPLC(strComAddress2, "Short").ToString();
                string strComValue3 = FormStart.pLCTool.ReadPLC(strComAddress3, "Short").ToString();
                string strComValue4 = FormStart.pLCTool.ReadPLC(strComAddress4, "Short").ToString();
                double dComValue1 = 0, dComValue2 = 0, dComValue3 = 0, dComValue4 = 0;
                if (double.TryParse(strComValue1, out dComValue1) && double.TryParse(strComValue2, out dComValue2) && double.TryParse(strComValue3, out dComValue3) && double.TryParse(strComValue4, out dComValue4))
                {
                    dComValue1 = dComValue1 / 100;
                    dComValue2 = dComValue2 / 100;
                    dComValue3 = dComValue3 / 100;
                    dComValue4 = dComValue4 / 100;
                }

                //中心功率
                string strCenterAddress1 = strCenterPower + (nStartCenterPower + iAdd1);
                string strCenterAddress2 = strCenterPower + (nStartCenterPower + iAdd2);
                string strCenterAddress3 = strCenterPower + (nStartCenterPower + iAdd3);
                string strCenterAddress4 = strCenterPower + (nStartCenterPower + iAdd4);
                string strCenterValue1 = FormStart.pLCTool.ReadPLC(strCenterAddress1, "Short").ToString();
                string strCenterValue2 = FormStart.pLCTool.ReadPLC(strCenterAddress2, "Short").ToString();
                string strCenterValue3 = FormStart.pLCTool.ReadPLC(strCenterAddress3, "Short").ToString();
                string strCenterValue4 = FormStart.pLCTool.ReadPLC(strCenterAddress4, "Short").ToString();

                //外环功率
                string strOutAddress1 = strOutPower + (nStartOutPower + iAdd1);
                string strOutAddress2 = strOutPower + (nStartOutPower + iAdd2);
                string strOutAddress3 = strOutPower + (nStartOutPower + iAdd3);
                string strOutAddress4 = strOutPower + (nStartOutPower + iAdd4);
                string strOutValue1 = FormStart.pLCTool.ReadPLC(strOutAddress1, "Short").ToString();
                string strOutValue2 = FormStart.pLCTool.ReadPLC(strOutAddress2, "Short").ToString();
                string strOutValue3 = FormStart.pLCTool.ReadPLC(strOutAddress3, "Short").ToString();
                string strOutValue4 = FormStart.pLCTool.ReadPLC(strOutAddress4, "Short").ToString();

                LaserPar laserPar1 = new LaserPar { 组 = iPoint.ToString(), 点位=nPoint1.ToString(), 保护气 = strAirValue1, 极差值 = dRangeValue1.ToString(), 离焦补偿 = dComValue1.ToString(), 中心实际功率 = strCenterValue1, 外环实际功率 = strOutValue1 };
                LaserPar laserPar2 = new LaserPar { 组 = iPoint.ToString(),点位=nPoint2.ToString(), 保护气 = strAirValue2, 极差值 = dRangeValue2.ToString(), 离焦补偿 = dComValue2.ToString(), 中心实际功率 = strCenterValue2, 外环实际功率 = strOutValue2 };
                LaserPar laserPar3 = new LaserPar { 组 = iPoint.ToString(),点位=nPoint3.ToString(), 保护气 = strAirValue3, 极差值 = dRangeValue3.ToString(), 离焦补偿 = dComValue3.ToString(), 中心实际功率 = strCenterValue3, 外环实际功率 = strOutValue3 };
                LaserPar laserPar4 = new LaserPar { 组 = iPoint.ToString(),点位=nPoint4.ToString(), 保护气 = strAirValue4, 极差值 = dRangeValue4.ToString(), 离焦补偿 = dComValue4.ToString(), 中心实际功率 = strCenterValue4, 外环实际功率 = strOutValue4 };

                if (!dic2.ContainsKey(nPoint1.ToString()))
                {
                    dic2.Add(nPoint1.ToString(), laserPar1);
                }
                else
                {
                    dic2[nPoint1.ToString()] = laserPar1;
                }
                if (!dic2.ContainsKey(nPoint2.ToString()))
                {
                    dic2.Add(nPoint2.ToString(), laserPar2);
                }
                else
                {
                    dic2[nPoint2.ToString()] = laserPar2;
                }
                if (!dic2.ContainsKey(nPoint3.ToString()))
                {
                    dic2.Add(nPoint3.ToString(), laserPar3);
                }
                else
                {
                    dic2[nPoint3.ToString()] = laserPar3;
                }
                if (!dic2.ContainsKey(nPoint4.ToString()))
                {
                    dic2.Add(nPoint4.ToString(), laserPar4);
                }
                else
                {
                    dic2[nPoint4.ToString()] = laserPar4;
                }
            }
        }
    }
    public class CheckData
    {
        public string station { get; set; }
        public string value { get; set; }
        public string status { get; set; }

        public List<string> errlist = new List<string>();
    }

    public class FirstCheckData
    {
        public string station = "";
        public string value = "";

        public string status1 = "";
        public string status2 = "";
        public string status3 = "";
        public string status4 = "";
        public string status5 = "";

        public string value1 = "";
        public string value2 = "";
        public string value3 = "";
        public string value4= "";
        public string value5= "";
        
        public List<string> errlist = new List<string>();
    }

    public class LaserPar
    {
        public string 点位 { get; set; }
        public string 组 { get; set; }
        public string 保护气 { get; set; }
        public string 极差值 { get; set; }
        public string 离焦补偿 { get; set; }

        public string 中心实际功率 { get; set; }

        public string 外环实际功率 { get; set; }
    }
}
