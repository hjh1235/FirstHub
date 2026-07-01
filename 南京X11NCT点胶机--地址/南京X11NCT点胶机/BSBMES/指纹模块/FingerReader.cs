using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using libzkfpcsharp;
using System.IO;
using System.Threading;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;

namespace UpperComputer
{
    public static class FingerReader
    {
        static IntPtr mDevHandle = IntPtr.Zero; //指纹设备句柄
        static IntPtr mDBHandle = IntPtr.Zero;  //指纹识别算法句柄
        static byte[] FPBuffer;
        static int RegisterCount = 0;  //当前注册次数（指纹注册时需输入3次）
        const int REGISTER_FINGER_COUNT = 3; //指纹注册时需输入的次数
        static byte[][] RegTmps = new byte[3][]; //注册指纹信息缓存数组(每个指纹图片一个byte数组)
        static byte[] RegTmp = new byte[2048];   //登记指纹信息缓存数组
        static byte[] CapTmp = new byte[2048];   //设备扫描指纹信息缓存数组
        static int cbCapTmp = 2048;
        static int cbRegTmp = 0;
        private static int DBCount = 1; //算法句柄当前添加指纹计数
        private static int mfpWidth = 0;
        private static int mfpHeight = 0;
        private static UserInformationParameter list_UserInfo = new UserInformationParameter();

        public static UserInformationParameter List_UserInfo
        {
            get { return list_UserInfo; }
            set { list_UserInfo = value; }
        }

        /// <summary>
        /// 初始化设备
        /// </summary>
        /// <returns>大于0为设备数量，0未找到设备，小于0为报错信息</returns>
        private static int InitializeDevice()
        {

            int ret = zkfperrdef.ZKFP_ERR_OK;
            if ((ret = zkfp2.Init()) == zkfperrdef.ZKFP_ERR_OK)//初始化库
            {
                int nCount = zkfp2.GetDeviceCount();//获取连接设备数
                if (nCount > 0)
                    return nCount;
                else
                {
                    zkfp2.Terminate();//释放库资源
                    return 0;
                }
            }
            else
                return ret;
        }
        /// <summary>
        /// 检查连接的设备数
        /// </summary>
        /// <returns></returns>
        public static bool TestDevice()
        {
            int ret = 0;
            zkfp2.Init();//初始化库
            ret = zkfp2.GetDeviceCount(); //获取连接设备数
            if (ret < 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 打开设备
        /// </summary>
        /// <param name="iNum">设备索引</param>
        /// <param name="sMessage">返回信息</param>
        /// <returns></returns>
        public static bool OpenDevice(int iNum, out string sMessage)
        {
            bool b_Add = true;
            byte[] FingerTemp;
            int ret = zkfp.ZKFP_ERR_OK;
            ret = InitializeDevice();
            if (ret > 0)
            {
                if (IntPtr.Zero == (mDevHandle = zkfp2.OpenDevice(iNum)))//连接设备 返回设备句柄
                {
                    sMessage = "NG:打开指纹识别器失败，请检查线缆是否安装正确";
                    return false;
                }
                if (IntPtr.Zero == (mDBHandle = zkfp2.DBInit()))//初始化算法库
                {
                    zkfp2.CloseDevice(mDevHandle);//关闭设备
                    //zkfp2.Terminate();
                    mDevHandle = IntPtr.Zero;
                    sMessage = "NG:初始化指纹识别器数据库失败，请重启一下软件";
                    return false;
                }
                RegisterCount = 0;
                cbRegTmp = 0;
                for (int i = 0; i < 3; i++)
                {
                    RegTmps[i] = new byte[2048];
                }
                byte[] paramValue = new byte[4];
                int size = 4;
                zkfp2.GetParameters(mDevHandle, 1, paramValue, ref size);//获取参数
                zkfp2.ByteArray2Int(paramValue, ref mfpWidth);
                size = 4;
                zkfp2.GetParameters(mDevHandle, 2, paramValue, ref size);//获取参数
                zkfp2.ByteArray2Int(paramValue, ref mfpHeight);
                FPBuffer = new byte[mfpWidth * mfpHeight];
                if (File.Exists("FingerUserInfo.xml"))
                {
                    List_UserInfo = (UserInformationParameter)BasicArithmetic.LoadFromXml("FingerUserInfo.xml", List_UserInfo.GetType());
                    try
                    {
                        foreach (UserInformation ui in List_UserInfo.ListUserInformation)
                        {
                            FingerTemp = zkfp2.Base64ToBlob(ui.FingerInfo);
                            if (zkfp.ZKFP_ERR_OK != (ret = zkfp2.DBAdd(mDBHandle, DBCount, FingerTemp))) //从本地添加登记模板到内存
                                b_Add = false;
                            DBCount++;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
                if (b_Add)
                {
                    sMessage = "指纹仪打开成功!";
                    return true;
                }
                else
                {
                    sMessage = "NG:指纹识别器数据库打开失败";
                    return false;
                }
            }
            else
            {
                sMessage = "NG:初始化指纹识别器失败";
                return false;
            }
        }
        public static bool OpenDevice1(int iNum, out string sMessage)
        {
            bool b_Add = true;
            byte[] FingerTemp;
            int ret = zkfp.ZKFP_ERR_OK;
            ret = InitializeDevice();
            if (ret > 0)
            {
                if (IntPtr.Zero == (mDevHandle = zkfp2.OpenDevice(iNum)))//连接设备 返回设备句柄
                {
                    sMessage = "NG:打开指纹识别器失败，请检查线缆是否安装正确";
                    return false;
                }
                if (IntPtr.Zero == (mDBHandle = zkfp2.DBInit()))//初始化算法库
                {
                    zkfp2.CloseDevice(mDevHandle);//关闭设备
                    zkfp2.Terminate();
                    mDevHandle = IntPtr.Zero;
                    sMessage = "NG:初始化指纹识别器数据库失败，请重启一下软件";
                    return false;
                }
                RegisterCount = 0;
                cbRegTmp = 0;
                for (int i = 0; i < 3; i++)
                {
                    RegTmps[i] = new byte[2048];
                }
                byte[] paramValue = new byte[4];
                int size = 4;
                zkfp2.GetParameters(mDevHandle, 1, paramValue, ref size);//获取参数
                zkfp2.ByteArray2Int(paramValue, ref mfpWidth);
                size = 4;
                zkfp2.GetParameters(mDevHandle, 2, paramValue, ref size);//获取参数
                zkfp2.ByteArray2Int(paramValue, ref mfpHeight);
                FPBuffer = new byte[mfpWidth * mfpHeight];
                if (File.Exists("FingerUserInfo.xml"))
                {
                    List_UserInfo = (UserInformationParameter)BasicArithmetic.LoadFromXml("FingerUserInfo.xml", List_UserInfo.GetType());
                    try
                    {
                        foreach (UserInformation ui in List_UserInfo.ListUserInformation)
                        {
                            FingerTemp = zkfp2.Base64ToBlob(ui.FingerInfo);
                            if (zkfp.ZKFP_ERR_OK != (ret = zkfp2.DBAdd(mDBHandle, DBCount, FingerTemp))) //从本地添加登记模板到内存
                                b_Add = false;
                            DBCount++;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
                if (b_Add)
                {
                    sMessage = "指纹仪打开成功!";
                    return true;
                }
                else
                {
                    sMessage = "NG:指纹识别器数据库打开失败";
                    return false;
                }
            }
            else
            {
                sMessage = "NG:初始化指纹识别器失败";
                return false;
            }
        }
        /// <summary>
        /// 本地指纹数据重新编号并加载进指纹仪
        /// </summary>
        /// <param name="iNum"></param>
        /// <param name="sMessage"></param>
        /// <returns></returns>
        public static bool ReflashXaml2(int iNum, out string sMessage)
        {
            bool b_Add = true;
            byte[] FingerTemp;
            int ret = zkfp.ZKFP_ERR_OK;
            ret = InitializeDevice();
            if (ret > 0)
            {
                if (IntPtr.Zero == (mDevHandle = zkfp2.OpenDevice(iNum)))//连接设备 返回设备句柄
                {
                    sMessage = "NG:打开指纹识别器失败，请检查线缆是否安装正确";
                    return false;
                }
                if (IntPtr.Zero == (mDBHandle = zkfp2.DBInit()))//初始化算法库
                {
                    zkfp2.CloseDevice(mDevHandle);//关闭设备
                    mDevHandle = IntPtr.Zero;
                    sMessage = "NG:初始化指纹识别器数据库失败，请重启一下软件";
                    return false;
                }
                RegisterCount = 0;
                cbRegTmp = 0;
                for (int i = 0; i < 3; i++)
                {
                    RegTmps[i] = new byte[2048];
                }
                byte[] paramValue = new byte[4];
                int size = 4;
                zkfp2.GetParameters(mDevHandle, 1, paramValue, ref size);//获取参数
                zkfp2.ByteArray2Int(paramValue, ref mfpWidth);
                size = 4;
                zkfp2.GetParameters(mDevHandle, 2, paramValue, ref size);//获取参数
                zkfp2.ByteArray2Int(paramValue, ref mfpHeight);
                FPBuffer = new byte[mfpWidth * mfpHeight];
                if (File.Exists("FingerUserInfo.xml"))
                {
                    List_UserInfo = (UserInformationParameter)BasicArithmetic.LoadFromXml("FingerUserInfo.xml", List_UserInfo.GetType());
                    try
                    {
                        //if (Parames.CheckStatus == true)
                        //{
                        int BeginNum = 1;
                        XmlDocument doc = new XmlDocument(); //声明XmlDocument对象，加载XML文件
                        doc.Load("FingerUserInfo.xml");
                        XmlNode xn = doc.SelectSingleNode("UserInformationParameter"); //得到根节点UserInformationParameter
                        XmlNodeList xnl = xn.ChildNodes; //得到根节点的所有子节点
                        foreach (XmlNode xNode in xnl)
                        {
                            XmlElement xm = (XmlElement)xNode; //将节点转换为元素，便于得到节点属性值
                            XmlNodeList xm1 = xm.ChildNodes; //得到二层节点的所有子节点
                            foreach (XmlNode xMode in xm1)
                            {
                                XmlElement xnm = (XmlElement)xMode; //将节点转换为元素，便于得到节点属性值
                                XmlNodeList xnm1 = xnm.ChildNodes; //得到三层节点的所有子节点
                                string body = xnm1.Item(0).InnerText;
                                xnm1.Item(0).InnerText = BeginNum.ToString();
                                doc.Save("FingerUserInfo.xml");
                                BeginNum++;
                            }
                        }
                        int declearR = zkfp2.DBClear(mDBHandle);//清空内存中所有指纹模板
                        if (declearR == 0)
                        {
                            int DBCount2 = 1;
                            foreach (UserInformation ui in List_UserInfo.ListUserInformation)//异常 DBCount=2
                            {
                                FingerTemp = zkfp2.Base64ToBlob(ui.FingerInfo);
                                if (zkfp.ZKFP_ERR_OK != (ret = zkfp2.DBAdd(mDBHandle, DBCount2, FingerTemp))) //从本地添加登记模板到内存
                                    b_Add = false;
                                DBCount2++;
                            }
                        }
                        else
                        {

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("删除用户数据失败，请检查指纹仪是否正常连接！");
                        //throw;
                    }
                }
                if (b_Add)
                {
                    sMessage = "指纹仪打开成功!";
                    return true;
                }
                else
                {
                    sMessage = "NG:指纹识别器数据库打开失败";
                    return false;
                }
            }
            else
            {
                sMessage = "NG:初始化指纹识别器失败";
                return false;
            }
        }
        /// <summary>
        /// 关闭设备
        /// </summary>
        public static void CloseDevice()
        {
            if (IntPtr.Zero != mDevHandle)
            {
                //Thread.Sleep(3000);
                zkfp2.CloseDevice(mDevHandle);//关闭设备
                mDevHandle = IntPtr.Zero;
                RegisterCount = 0;
            }
            FingerPermission.isStaionCheck = false;
        }
        /// <summary>
        /// 释放算法库
        /// </summary>
        public static void DBFree()
        {
            zkfp2.DBFree(mDevHandle);//释放算法库
            Thread.Sleep(1000);
        }
        /// <summary>
        /// 清空内存中所有指纹模板
        /// </summary>
        public static void DBClear()
        {
            zkfp2.DBClear(mDevHandle);//清空内存中所有指纹模板
            Thread.Sleep(1000);
        }
        /// <summary>
        /// 用户注册 修改
        /// </summary>
        /// <param name="sMessage">注册结果信息</param>
        /// <returns></returns>
        public static bool UserRegister(string str_UserName, string str_UserLevel, out string s_Message)
        {
            s_Message = "";
            bool b_Result = false;
            int i_Count = 0;
            #region 新
            while (true)
            {
                cbCapTmp = 2048;
                int ret = zkfp2.AcquireFingerprint(FingerReader.mDevHandle, FPBuffer, CapTmp, ref cbCapTmp);//采集指纹
                if (ret == zkfp.ZKFP_ERR_OK)
                {
                    try
                    {
                        FingerRegister(str_UserName, str_UserLevel, out s_Message);//指纹注册和保存
                        if (s_Message.Contains("OK"))
                        {
                            UserFingerRegisterForm.oneaddmessagedelegate(s_Message);
                            b_Result = true;
                            break;
                        }
                        else if (s_Message.Contains("继续"))
                        {
                            UserFingerRegisterForm.oneaddmessagedelegate(s_Message);
                            i_Count++;
                            if (i_Count >= 3)
                            {
                                b_Result = false;
                                break;
                            }
                        }
                        else if (s_Message.Contains("相同的手指"))
                        {
                            UserFingerRegisterForm.oneaddmessagedelegate(s_Message);
                            b_Result = false;
                            break;

                        }
                        else if (s_Message.Contains("已经被注册"))
                        {
                            UserFingerRegisterForm.oneaddmessagedelegate(s_Message);
                            b_Result = false;
                            break;
                        }
                        else
                        {
                            UserFingerRegisterForm.oneaddmessagedelegate(s_Message);
                            b_Result = false;
                            break;
                        }
                    }
                    catch
                    {
                        UserFingerRegisterForm.oneaddmessagedelegate(s_Message);
                        b_Result = false;
                        break;
                    }
                }
                Thread.Sleep(200);
            }
            #endregion
            return b_Result;
        }

        /// <summary>
        /// 用户识别
        /// </summary>
        /// <param name="sMessage">识别结果信息</param>
        /// <returns></returns>
        public static bool UserIndentify(out string str_UserName, out string str_UserLevel, out string sMessage)
        {
            str_UserName = "";
            str_UserLevel = "";
            string s_Message = "";
            bool b_Result = false;
            cbCapTmp = 2048;
            int ret = zkfp2.AcquireFingerprint(FingerReader.mDevHandle, FPBuffer, CapTmp, ref cbCapTmp);//采集指纹
            if (ret == zkfp.ZKFP_ERR_OK) //感应是否有手指，有则先读取到信息
            {
                ret = zkfp.ZKFP_ERR_OK;
                int fid = 0, score = 0;
                ret = zkfp2.DBIdentify(mDBHandle, CapTmp, ref fid, ref score); //返回fid是从1开始 获得指纹ID
                if (zkfp.ZKFP_ERR_OK == ret) //通过比对找到符合先前存储的指纹数据
                {
                    b_Result = true;
                    //s_Message = "OK:识别成功, fid= " + fid + ",score=" + score + "!";
                    s_Message = fid.ToString();
                    UserInformationParameter ListData = new UserInformationParameter();
                    ListData = (UserInformationParameter)BasicArithmetic.LoadFromXml("FingerUserInfo.xml", List_UserInfo.GetType());
                    foreach (UserInformation update in ListData.ListUserInformation)
                    {
                        if (update.IDKey == fid)
                        {
                           
                            str_UserName = update.UserName;
                            str_UserLevel = update.UserLevel;
                        }
                    }
                }
                else
                {
                    b_Result = false;
                    s_Message = "识别失败, " + fid;
                }
            }
            else if (ret == zkfp.ZKFP_ERR_CAPTURE)//没有识别到指纹
            {
                b_Result = false;
                s_Message = "没有识别到";
            }
            sMessage = s_Message;
            return b_Result;
        }
        /// <summary>
        /// 指纹注册
        /// </summary>
        /// <param name="sMessage"></param>
        private static void FingerRegister(string str_UserName, string str_UserLever, out string sMessage)
        {
            sMessage = "";
            //MemoryStream ms = new MemoryStream();
            //BitmapFormat.GetBitmap(FPBuffer, mfpWidth, mfpHeight, ref ms);
            //Bitmap bmp = new Bitmap(ms);
            int ret = zkfp.ZKFP_ERR_OK;
            int fid = 0, score = 0;
            ret = zkfp2.DBIdentify(mDBHandle, CapTmp, ref fid, ref score); //返回fid是从1开始 获得指纹ID
            if (zkfp.ZKFP_ERR_OK == ret)
            {
                sMessage = "NG:这个指纹已经被注册 " + fid + "!";
                return;
            }
            if (RegisterCount > 0 && zkfp2.DBMatch(mDBHandle, CapTmp, RegTmps[RegisterCount - 1]) <= 0)//比对两枚指纹
            {
                sMessage = "NG:请用相同的手指录入3次";
                RegisterCount = 0;//新增
                return;
            }
            Array.Copy(CapTmp, RegTmps[RegisterCount], cbCapTmp);
            String strBase64 = zkfp2.BlobToBase64(CapTmp, cbCapTmp);
            byte[] blob = zkfp2.Base64ToBlob(strBase64);
            RegisterCount++;

            if (RegisterCount >= REGISTER_FINGER_COUNT)
            {
                RegisterCount = 0;
                int DBCount1 = 1;
                #region 获取本地的值 DBCount
                if (File.Exists("FingerUserInfo.xml"))
                {
                    List_UserInfo = (UserInformationParameter)BasicArithmetic.LoadFromXml("FingerUserInfo.xml", List_UserInfo.GetType());
                    try
                    {
                        int BeginNum = 1;
                        XmlDocument doc = new XmlDocument(); //声明XmlDocument对象，加载XML文件
                        doc.Load("FingerUserInfo.xml");
                        XmlNode xn = doc.SelectSingleNode("UserInformationParameter"); //得到根节点UserInformationParameter
                        XmlNodeList xnl = xn.ChildNodes; //得到根节点的所有子节点
                        foreach (XmlNode xNode in xnl)
                        {
                            XmlElement xm = (XmlElement)xNode; //将节点转换为元素，便于得到节点属性值
                            XmlNodeList xm1 = xm.ChildNodes; //得到二层节点的所有子节点
                            foreach (XmlNode xMode in xm1)
                            {
                                XmlElement xnm = (XmlElement)xMode; //将节点转换为元素，便于得到节点属性值
                                XmlNodeList xnm1 = xnm.ChildNodes; //得到三层节点的所有子节点
                                string body = xnm1.Item(0).InnerText;
                                xnm1.Item(0).InnerText = BeginNum.ToString();
                                doc.Save("FingerUserInfo.xml");
                                BeginNum++;
                            }
                        }
                        int declearR = zkfp2.DBClear(mDBHandle);//清空内存中所有指纹模板
                        if (declearR == 0)
                        {
                            int DBCount2 = 1;
                            foreach (UserInformation ui in List_UserInfo.ListUserInformation)//异常 DBCount=2
                            {
                                byte[] FingerTemp;
                                FingerTemp = zkfp2.Base64ToBlob(ui.FingerInfo);
                                if (zkfp.ZKFP_ERR_OK == (ret = zkfp2.DBAdd(mDBHandle, DBCount2, FingerTemp)))////从本地添加登记模板到内存
                                {
                                    DBCount2++;
                                }
                                else
                                {
                                    sMessage = "NG:指纹库重置失败！";
                                    return;
                                }//从本地添加登记模板到内存

                            }
                        }

                    }

                    catch (Exception ex)
                    {
                        throw;
                    }
                }
                #endregion
                DBCount1 = List_UserInfo.ListUserInformation.Count;
                if (zkfp.ZKFP_ERR_OK == (ret = zkfp2.DBMerge(mDBHandle, RegTmps[0], RegTmps[1], RegTmps[2], RegTmp, ref cbRegTmp)) && zkfp.ZKFP_ERR_OK == (ret = zkfp2.DBAdd(mDBHandle, DBCount1 + 1, RegTmp))) //指纹三合一录入内存及保存在本地
                {
                    string strBase = "";
                    zkfp.Blob2Base64String(RegTmp, RegTmp.Length, ref strBase);
                   
                    List_UserInfo.ListUserInformation.Add(new UserInformation
                    {
                        IDKey = DBCount1 + 1,
                        UserName = str_UserName,
                        UserLevel = str_UserLever,
                        FingerInfo = strBase
                    });
                    BasicArithmetic.SaveToXml("FingerUserInfo.xml", List_UserInfo, List_UserInfo.GetType());
                    DBCount1++;
                    sMessage = "OK:注册成功";
                    return;
                }
                else
                    sMessage = "NG:注册失败,错误代码：" + ret;
                return;
            }
            else
                sMessage = "继续:你需要按下 " + (REGISTER_FINGER_COUNT - RegisterCount) + " 次指纹录入";
        }
    }
    public class Global
    {
        /// <summary>
        /// MES登录窗体通过标记位
        /// </summary>
        public static string MESLoginOKFlag = "";
        /// <summary>
        /// 数据上传标记位
        /// </summary>
        public static string UpLoad = "";
        /// <summary>
        /// 软件用户登录权限
        /// </summary>
        public static string UserPower = "";
    }
    public class UserInformationParameter
    {
        private List<UserInformation> list_UserInfo = new List<UserInformation>();
        public List<UserInformation> ListUserInformation
        {
            get { return list_UserInfo; }
            set { list_UserInfo = value; }
        }
    }
    public class UserInformation
    {
        private int i_IDKey = 113;
        /// <summary>
        /// ID键值
        /// </summary>
        public int IDKey
        {
            get { return i_IDKey; }
            set { i_IDKey = value; }
        }
        private string str_UserName = "张三";
        /// <summary>
        /// 用户名字
        /// </summary>
        public string UserName
        {
            get { return str_UserName; }
            set { str_UserName = value; }
        }
        private string str_UserLevel = "IPQC";
        /// <summary>
        /// 用户等级，IPQC，技术员，工艺
        /// </summary>
        public string UserLevel
        {
            get { return str_UserLevel; }
            set { str_UserLevel = value; }
        }
        private string str_FingerInfo = "";
        /// <summary>
        /// 指纹信息
        /// </summary>
        public string FingerInfo
        {
            get { return str_FingerInfo; }
            set { str_FingerInfo = value; }
        }
    }
    //public class BitmapFormat
    //{
    //    public struct BITMAPFILEHEADER
    //    {
    //        public ushort bfType;
    //        public int bfSize;
    //        public ushort bfReserved1;
    //        public ushort bfReserved2;
    //        public int bfOffBits;
    //    }
    //    public struct MASK
    //    {
    //        public byte redmask;
    //        public byte greenmask;
    //        public byte bluemask;
    //        public byte rgbReserved;
    //    }
    //    public struct BITMAPINFOHEADER
    //    {
    //        public int biSize;
    //        public int biWidth;
    //        public int biHeight;
    //        public ushort biPlanes;
    //        public ushort biBitCount;
    //        public int biCompression;
    //        public int biSizeImage;
    //        public int biXPelsPerMeter;
    //        public int biYPelsPerMeter;
    //        public int biClrUsed;
    //        public int biClrImportant;
    //    }
    //    /*******************************************
    //    * 函数名称：RotatePic       
    //    * 函数功能：旋转图片，目的是保存和显示的图片与按的指纹方向不同     
    //    * 函数入参：BmpBuf---旋转前的指纹字符串
    //    * 函数出参：ResBuf---旋转后的指纹字符串
    //    * 函数返回：无
    //    *********************************************/
    //    public static void RotatePic(byte[] BmpBuf, int width, int height, ref byte[] ResBuf)
    //    {
    //        int RowLoop = 0;
    //        int ColLoop = 0;
    //        int BmpBuflen = width * height;
    //        try
    //        {
    //            for (RowLoop = 0; RowLoop < BmpBuflen;)
    //            {
    //                for (ColLoop = 0; ColLoop < width; ColLoop++)
    //                {
    //                    ResBuf[RowLoop + ColLoop] = BmpBuf[BmpBuflen - RowLoop - width + ColLoop];
    //                }

    //                RowLoop = RowLoop + width;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            //ZKCE.SysException.ZKCELogger logger = new ZKCE.SysException.ZKCELogger(ex);
    //            //logger.Append();
    //        }
    //    }
    //    /*******************************************
    //    * 函数名称：StructToBytes       
    //    * 函数功能：将结构体转化成无符号字符串数组     
    //    * 函数入参：StructObj---被转化的结构体
    //    *           Size---被转化的结构体的大小
    //    * 函数出参：无
    //    * 函数返回：结构体转化后的数组
    //    *********************************************/
    //    public static byte[] StructToBytes(object StructObj, int Size)
    //    {
    //        int StructSize = Marshal.SizeOf(StructObj);
    //        byte[] GetBytes = new byte[StructSize];
    //        try
    //        {
    //            IntPtr StructPtr = Marshal.AllocHGlobal(StructSize);
    //            Marshal.StructureToPtr(StructObj, StructPtr, false);
    //            Marshal.Copy(StructPtr, GetBytes, 0, StructSize);
    //            Marshal.FreeHGlobal(StructPtr);
    //            if (Size == 14)
    //            {
    //                byte[] NewBytes = new byte[Size];
    //                int Count = 0;
    //                int Loop = 0;
    //                for (Loop = 0; Loop < StructSize; Loop++)
    //                {
    //                    if (Loop != 2 && Loop != 3)
    //                    {
    //                        NewBytes[Count] = GetBytes[Loop];
    //                        Count++;
    //                    }
    //                }
    //                return NewBytes;
    //            }
    //            else
    //            {
    //                return GetBytes;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            //ZKCE.SysException.ZKCELogger logger = new ZKCE.SysException.ZKCELogger(ex);
    //            //logger.Append();
    //            return GetBytes;
    //        }
    //    }
    //    /*******************************************
    //    * 函数名称：GetBitmap       
    //    * 函数功能：将传进来的数据保存为图片     
    //    * 函数入参：buffer---图片数据
    //    *           nWidth---图片的宽度
    //    *           nHeight---图片的高度
    //    * 函数出参：无
    //    * 函数返回：无
    //    *********************************************/
    //    public static void GetBitmap(byte[] buffer, int nWidth, int nHeight, ref MemoryStream ms)
    //    {
    //        int ColorIndex = 0;
    //        ushort m_nBitCount = 8;
    //        int m_nColorTableEntries = 256;
    //        byte[] ResBuf = new byte[nWidth * nHeight * 2];
    //        try
    //        {
    //            BITMAPFILEHEADER BmpHeader = new BITMAPFILEHEADER();
    //            BITMAPINFOHEADER BmpInfoHeader = new BITMAPINFOHEADER();
    //            MASK[] ColorMask = new MASK[m_nColorTableEntries];
    //            int w = (((nWidth + 3) / 4) * 4);
    //            //图片头信息
    //            BmpInfoHeader.biSize = Marshal.SizeOf(BmpInfoHeader);
    //            BmpInfoHeader.biWidth = nWidth;
    //            BmpInfoHeader.biHeight = nHeight;
    //            BmpInfoHeader.biPlanes = 1;
    //            BmpInfoHeader.biBitCount = m_nBitCount;
    //            BmpInfoHeader.biCompression = 0;
    //            BmpInfoHeader.biSizeImage = 0;
    //            BmpInfoHeader.biXPelsPerMeter = 0;
    //            BmpInfoHeader.biYPelsPerMeter = 0;
    //            BmpInfoHeader.biClrUsed = m_nColorTableEntries;
    //            BmpInfoHeader.biClrImportant = m_nColorTableEntries;
    //            //文件头信息
    //            BmpHeader.bfType = 0x4D42;
    //            BmpHeader.bfOffBits = 14 + Marshal.SizeOf(BmpInfoHeader) + BmpInfoHeader.biClrUsed * 4;
    //            BmpHeader.bfSize = BmpHeader.bfOffBits + ((((w * BmpInfoHeader.biBitCount + 31) / 32) * 4) * BmpInfoHeader.biHeight);
    //            BmpHeader.bfReserved1 = 0;
    //            BmpHeader.bfReserved2 = 0;
    //            ms.Write(StructToBytes(BmpHeader, 14), 0, 14);
    //            ms.Write(StructToBytes(BmpInfoHeader, Marshal.SizeOf(BmpInfoHeader)), 0, Marshal.SizeOf(BmpInfoHeader));
    //            //调试板信息
    //            for (ColorIndex = 0; ColorIndex < m_nColorTableEntries; ColorIndex++)
    //            {
    //                ColorMask[ColorIndex].redmask = (byte)ColorIndex;
    //                ColorMask[ColorIndex].greenmask = (byte)ColorIndex;
    //                ColorMask[ColorIndex].bluemask = (byte)ColorIndex;
    //                ColorMask[ColorIndex].rgbReserved = 0;
    //                ms.Write(StructToBytes(ColorMask[ColorIndex], Marshal.SizeOf(ColorMask[ColorIndex])), 0, Marshal.SizeOf(ColorMask[ColorIndex]));
    //            }
    //            //图片旋转，解决指纹图片倒立的问题
    //            RotatePic(buffer, nWidth, nHeight, ref ResBuf);

    //            byte[] filter = null;
    //            if (w - nWidth > 0)
    //            {
    //                filter = new byte[w - nWidth];
    //            }
    //            for (int i = 0; i < nHeight; i++)
    //            {
    //                ms.Write(ResBuf, i * nWidth, nWidth);
    //                if (w - nWidth > 0)
    //                {
    //                    ms.Write(ResBuf, 0, w - nWidth);
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            // ZKCE.SysException.ZKCELogger logger = new ZKCE.SysException.ZKCELogger(ex);
    //            // logger.Append();
    //        }
    //    }
    //    /*******************************************
    //    * 函数名称：WriteBitmap       
    //    * 函数功能：将传进来的数据保存为图片     
    //    * 函数入参：buffer---图片数据
    //    *           nWidth---图片的宽度
    //    *           nHeight---图片的高度
    //    * 函数出参：无
    //    * 函数返回：无
    //    *********************************************/
    //    public static void WriteBitmap(byte[] buffer, int nWidth, int nHeight)
    //    {
    //        int ColorIndex = 0;
    //        ushort m_nBitCount = 8;
    //        int m_nColorTableEntries = 256;
    //        byte[] ResBuf = new byte[nWidth * nHeight];

    //        try
    //        {
    //            BITMAPFILEHEADER BmpHeader = new BITMAPFILEHEADER();
    //            BITMAPINFOHEADER BmpInfoHeader = new BITMAPINFOHEADER();
    //            MASK[] ColorMask = new MASK[m_nColorTableEntries];
    //            int w = (((nWidth + 3) / 4) * 4);
    //            //图片头信息
    //            BmpInfoHeader.biSize = Marshal.SizeOf(BmpInfoHeader);
    //            BmpInfoHeader.biWidth = nWidth;
    //            BmpInfoHeader.biHeight = nHeight;
    //            BmpInfoHeader.biPlanes = 1;
    //            BmpInfoHeader.biBitCount = m_nBitCount;
    //            BmpInfoHeader.biCompression = 0;
    //            BmpInfoHeader.biSizeImage = 0;
    //            BmpInfoHeader.biXPelsPerMeter = 0;
    //            BmpInfoHeader.biYPelsPerMeter = 0;
    //            BmpInfoHeader.biClrUsed = m_nColorTableEntries;
    //            BmpInfoHeader.biClrImportant = m_nColorTableEntries;
    //            //文件头信息
    //            BmpHeader.bfType = 0x4D42;
    //            BmpHeader.bfOffBits = 14 + Marshal.SizeOf(BmpInfoHeader) + BmpInfoHeader.biClrUsed * 4;
    //            BmpHeader.bfSize = BmpHeader.bfOffBits + ((((w * BmpInfoHeader.biBitCount + 31) / 32) * 4) * BmpInfoHeader.biHeight);
    //            BmpHeader.bfReserved1 = 0;
    //            BmpHeader.bfReserved2 = 0;
    //            Stream FileStream = File.Open("finger.bmp", FileMode.Create, FileAccess.Write);
    //            BinaryWriter TmpBinaryWriter = new BinaryWriter(FileStream);
    //            TmpBinaryWriter.Write(StructToBytes(BmpHeader, 14));
    //            TmpBinaryWriter.Write(StructToBytes(BmpInfoHeader, Marshal.SizeOf(BmpInfoHeader)));
    //            //调试板信息
    //            for (ColorIndex = 0; ColorIndex < m_nColorTableEntries; ColorIndex++)
    //            {
    //                ColorMask[ColorIndex].redmask = (byte)ColorIndex;
    //                ColorMask[ColorIndex].greenmask = (byte)ColorIndex;
    //                ColorMask[ColorIndex].bluemask = (byte)ColorIndex;
    //                ColorMask[ColorIndex].rgbReserved = 0;

    //                TmpBinaryWriter.Write(StructToBytes(ColorMask[ColorIndex], Marshal.SizeOf(ColorMask[ColorIndex])));
    //            }
    //            //图片旋转，解决指纹图片倒立的问题
    //            RotatePic(buffer, nWidth, nHeight, ref ResBuf);

    //            //写图片
    //            byte[] filter = null;
    //            if (w - nWidth > 0)
    //            {
    //                filter = new byte[w - nWidth];
    //            }
    //            for (int i = 0; i < nHeight; i++)
    //            {
    //                TmpBinaryWriter.Write(ResBuf, i * nWidth, nWidth);
    //                if (w - nWidth > 0)
    //                {
    //                    TmpBinaryWriter.Write(ResBuf, 0, w - nWidth);
    //                }
    //            }
    //            FileStream.Close();
    //            TmpBinaryWriter.Close();
    //        }
    //        catch (Exception ex)
    //        {

    //        }
    //    }
    //}
}
