//using Newtonsoft.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NPOI.Util.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace UpperComputer
{
    public partial class mes
    {
        private static mes mesDate;
        public static mes Instance()
        {
            if (mesDate == null)
            {
                mesDate = new mes();
            }
            return mesDate;
        }
        public ImgFtpUpd ImageUpDate = new ImgFtpUpd();
        public HttpPost mespost = null;
        public FtpUpd theFtp = null;
        /// <summary>
        /// 工序代码
        /// </summary>
        public string userCode = "";
        /// <summary>
        /// 设备ID
        /// </summary>
        public string deviceCode = "";
        /// <summary>
        /// 工序代码
        /// </summary>
        public string groupCode = "";
        /// <summary>
        /// 制令单号
        /// </summary>
        public string moNumber = "";
        public string DataReceivedstrSN = "";
        public mes()
        {
            //   mespost = new HttpPost("http://172.30.7.22:7081/mesproject/mesinterface/");
        }
        public void Load(string Ls)
        {
            mespost = new HttpPost(Ls);
        }
        public string ImageUpLoad(string soucePath, string SN, string imgPath, string imType)
        {
            string isok = ImageUpDate.Upload(soucePath);//将图片上传到服务器
            string UpLoadok = "";
            if (isok == "OK")
            {
                UpLoadok = UploadImagePath(SN.Replace("\r\n", ""), imgPath, imType);//将图片数据传送到MES
            }
            return UpLoadok;
        }

        public static Dictionary<string, string> ConverFromJson(string JsonStr)
        {
            Dictionary<string, string> dcValues = new Dictionary<string, string>();
            string[] strs = JsonStr.Trim().Trim(',').TrimStart('{').TrimEnd('}').Split(',');
            if (strs != null && strs.Length > 0)
            {
                for (int i = 0; i < strs.Length; i++)
                {
                    int idx1 = strs[i].IndexOf('\"');
                    if (idx1 >= 0 && strs[i].Length > idx1 + 1)
                    {
                        int idx2 = strs[i].IndexOf('\"', idx1 + 1);
                        if (idx2 > idx1)
                        {
                            string tKey = strs[i].Substring(idx1 + 1, idx2 - idx1 - 1);
                            if (strs[i].Length > idx2 + 1)
                            {
                                int idx3 = strs[i].IndexOf(':', idx2 + 1);
                                if (idx3 > idx2)
                                {
                                    int idx4 = strs[i].IndexOf('\"', idx3);
                                    if (idx4 > idx3 && strs[i].Length > idx4 + 1)
                                    {
                                        int idx5 = strs[i].IndexOf('\"', idx4 + 1);
                                        if (idx5 > idx4)
                                        {
                                            string tValue = strs[i].Substring(idx4 + 1, idx5 - idx4 - 1);
                                            dcValues.Add(tKey, tValue);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return dcValues;
        }

        /// <summary>
        /// 解析返回的模组条码数据。具体看文档格式
        /// </summary>
        /// <param name="ResponseJson"></param>
        /// <returns></returns>
        #region old
        //private string GetModuleBarcodes(string ResponseJson, ref List<SNInformtion> str)
        //{
        //    try
        //    {
        //        JObject jo = (JObject)JsonConvert.DeserializeObject(ResponseJson);
        //        List<SNInformtion> productSNList = new List<SNInformtion>() { };
        //        if (jo["status"].ToString().ToUpper() == "TRUE")
        //        {
        //            JToken jt = jo["testResultDetails"];
        //            foreach (var item in jt)
        //            {
        //                productSNList.Add(new SNInformtion { sn = item["productSn"].ToString(), position = item["position"].ToString() });
        //            }
        //            str = productSNList;
        //            return "true";
        //        }
        //        else
        //        {
        //            str = productSNList;
        //            return jo["result"].ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return "false," + ex.Message;
        //    }

        //}
        #endregion
        private string GetModuleBarcodes(string ResponseJson, ref List<SNInformtion> str)
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(ResponseJson);
                var firstChild = xml.FirstChild;
                ResponseJson = firstChild.NextSibling.InnerText;
                JObject jo = (JObject)JsonConvert.DeserializeObject(ResponseJson);
                List<SNInformtion> productSNList = new List<SNInformtion>() { };
                if (jo["status"].ToString().ToUpper() == "TRUE")
                {
                    JToken jt = jo["testResultDetails"];
                    foreach (var item in jt)
                    {
                        productSNList.Add(new SNInformtion { sn = item.ToString() });
                    }
                    str = productSNList;
                    return "true";
                }
                else
                {
                    str = productSNList;
                    return jo["result"].ToString();
                }
            }
            catch (Exception ex)
            {
                return "false," + ex.Message;
            }

        }
        /// <summary>
        /// 解析返回的模组条码数据。具体看文档格式
        /// </summary>
        /// <param name="ResponseJson"></param>
        /// <returns></returns>
        #region old
        //private string GetCheckModule(string ResponseJson, ref string index)
        //{
        //    try
        //    {
        //        index = "0";
        //        JObject jo = (JObject)JsonConvert.DeserializeObject(ResponseJson);

        //        if (jo["status"].ToString().ToUpper() == "TRUE")
        //        {
        //            JToken jt = jo["testResultDetails"];
        //            foreach (var item in jt)
        //            {
        //                index = item["productMode"].ToString();
        //            }
        //            return "true";
        //        }
        //        else
        //        {
        //            return jo["result"].ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return "false," + ex.Message;
        //    }

        //}
        #endregion
        private string GetCheckModule(string ResponseJson, ref string index)
        {
            try
            {
                index = "0";
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(ResponseJson);
                var firstChild = xml.FirstChild;
                ResponseJson = firstChild.NextSibling.InnerText;
                JObject jo = (JObject)JsonConvert.DeserializeObject(ResponseJson);

                if (jo["status"].ToString().ToUpper() == "TRUE")
                {
                    return "true";
                }
                else
                {
                    return jo["description"].ToString();
                }
            }
            catch (Exception ex)
            {
                return "false," + ex.Message;
            }

        }
        //解析返回的条码字典
        public static List<string> ConverFromJson_s(string JsonStr)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(JsonStr);

            var firstChild = xml.FirstChild;
            string str = firstChild.NextSibling.InnerText;
            JObject dcValues = JsonConvert.DeserializeObject<JObject>(str);
            Type a = dcValues.GetType();
            JToken token = dcValues.GetValue("testResultDetails");
            List<string> list = new List<string>() {
                token.First.ToString(),
                token.Last.ToString()};
            return list;
        }

        protected string getMsg_mes(string result)
        {
            Dictionary<string, string> dcValue = ConverFromJson(result);
            if (dcValue.ContainsKey("result"))
            {
                if (dcValue["result"].ToUpper() == "TRUE")
                {
                    if (dcValue.ContainsKey("message"))
                    {
                        return dcValue["message"];
                    }
                    //return "OK";
                }
                else
                {
                    return "";
                }

            }
            return "登陆失败";

        }
        //protected string getMsg(string result)
        //{
        //    Dictionary<string, string> dcValue = ConverFromJson(result);
        //    if (dcValue.ContainsKey("result"))
        //    {
        //        if (dcValue["result"].ToUpper() == "TRUE")
        //        {
        //            return "TRUE";
        //        }

        //        if (dcValue.ContainsKey("message"))
        //        {
        //            return dcValue["message"];
        //        }
        //    }
        //    return "登陆失败";
        //    //Dictionary<string, string> dcValue = ConverFromJson(result);
        //    //if (dcValue.ContainsKey("result"))
        //    //{
        //    //    if (dcValue["result"].ToUpper() == "TRUE")
        //    //        return "OK";
        //    //    else
        //    //    {
        //    //        if (dcValue.ContainsKey("message"))
        //    //            return dcValue["message"];
        //    //    }
        //    //}
        //    //return "登陆失败";
        //}

        /// <summary>
        /// 南昌登录MES验证结果解析，成功保存SessionId，返回TRUE，失败返回错误信息
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        #region old
        //protected string getLoginMsg(string result)
        //{
        //    JObject jo = (JObject)JsonConvert.DeserializeObject(result);
        //    if (jo["status"].ToString().ToUpper() == "TRUE")
        //    {
        //        ClassCANS.OLD.SessionId = jo["result"].ToString(); //登录成功时保存sessionId
        //        Properties.Settings.Default.Save();
        //        return "TRUE";
        //    }
        //    else
        //    {
        //        return jo["result"].ToString();
        //    }
        //}
        #endregion
        protected string getLoginMsg(string result)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(result);
            var firstChild = xml.FirstChild;
            result = firstChild.NextSibling.InnerText;
            JObject jo = (JObject)JsonConvert.DeserializeObject(result);
            if (jo["status"].ToString().ToUpper() == "TRUE")
            {
                ClassCANS.OLD.SessionId = jo["sessionid"].ToString(); //登录成功时保存sessionId
                Properties.Settings.Default.Save();
                return "TRUE";
            }
            else
            {
                return jo["description"].ToString();
            }
        }
        /// <summary>
        /// 南昌极柱激光清洗机MES返回数据解析,成功返回"TRUE",失败返回出错信息
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected string getMsg(string result)
        {
            JObject jo = (JObject)JsonConvert.DeserializeObject(result);
            if (jo["status"].ToString().ToUpper() == "TRUE")
            {

                return "TRUE";
            }
            else  //失败，则返回出错信息
            {

                return jo["result"].ToString();
            }
        }
        /// <summary>
        /// 南昌极柱激光清洗机MES返回数据解析,成功返回"TRUE",失败返回出错信息
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected string getMsgWest(string result, out string rt)
        {
            rt = "";
            JObject jo = (JObject)JsonConvert.DeserializeObject(result);
            if (jo["status"].ToString().ToUpper() == "TRUE")
            {
                return "TRUE";
            }
            else  //失败，则返回出错信息
            {
                rt = jo["status"].ToString();
                return jo["result"].ToString();
            }
        }

        /// <summary>
        /// 登录MES验证,成功返回TRUE，失败返回错误信息
        /// </summary>
        /// <param name="userNo"></param>
        /// <param name="passWord"></param>
        /// <param name="machineNo"></param>
        /// <param name="moNumber"></param>
        /// <returns></returns>
        public string Login(string userNo, string passWord, string machineNo, string moNumber)
        {
            string mesResult = "";
            try
            {
                var loginData = new
                {
                    operatorID = userNo, //工号
                    passWord = passWord,//密码
                    machineID = machineNo,//设备号
                    moNumber = moNumber,//指令单号
                };
                //string jsonData = "jsonData=" + JsonConvert.SerializeObject(loginData);//发送MES的字符串
                string jsonData = "LoginInfo=" + JsonConvert.SerializeObject(loginData);//发送MES的字符串
                mesResult = mespost.getResult("LoginCheck", jsonData, 10000);
                Log.logMES($"请求接口:LoginCheck",1);
                Log.logMES($"请求Json:{jsonData}",1);
                Log.logMES($"MES返回Json:{mesResult}",1);
                return getLoginMsg(mesResult);
            }
            catch (Exception ex1)
            {
                return mesResult.Replace("[", "【").Replace("]", "】").Replace("\"", "“").Replace(":", "：").Replace("{", "｛").Replace("}", "｝").Replace(",", "，") + "；" + ex1.Message;
            }
        }
        /// <summary>
        /// 参数下发
        /// </summary>
        /// <param name="groupCode"></param>
        /// <param name="machineNo"></param>
        /// <param name="operatorId"></param>
        /// <param name="productSn"></param>
        /// <param name="moNumber"></param>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public string GetParameter(string groupCode, string machineNo, string operatorId, string productSn, string moNumber, string timeStamp, ref Dictionary<string, ResultPar> dicPar)
        {
            string mesResult = "";
            try
            {
                var parameterData = new
                {
                    groupCode = groupCode, //工序代码
                    deviceSn = machineNo,//设备号
                    operatorId = operatorId,//SessionID
                    productSn = productSn,//产品条码
                    moNumber = moNumber,//制令单号
                    timeStamp = timeStamp //上传时间
                };
                string jsonData = "jsonData=" + JsonConvert.SerializeObject(parameterData);//发送MES的字符串
                mesResult = mespost.getResult("GetSpecifications", jsonData, 10000);
                Log.logMES($"请求接口:GetSpecifications",1);
                Log.logMES($"请求Json:{jsonData}",1);
                Log.logMES($"MES返回Json:{mesResult}",1);
                return GetModulePar(mesResult, ref dicPar);
            }
            catch (Exception ex1)
            {
                return "false" + ex1.Message;
            }
        }
        /// <summary>
        /// 解析返回的参数数据。具体看文档格式
        /// </summary>
        /// <param name="ResponseJson"></param>
        /// <returns></returns>
        private string GetModulePar(string ResponseJson, ref Dictionary<string, ResultPar> str)
        {
            try
            {
                JObject jo = (JObject)JsonConvert.DeserializeObject(ResponseJson);
                str = new Dictionary<string, ResultPar>();
                if (jo["status"].ToString().ToUpper() == "TRUE")
                {
                    JToken jt = jo["testResultDetails"];
                    int i = 0;
                    foreach (var item in jt)
                    {
                        //str.Add(item["paramName"].ToString(), new ResultPar { paramCode = item["paramCode"].ToString(), paramName = item["paramName"].ToString(), paramFirstUpper = item["paramFirstUpper"].ToString(), paramFirstLower = item["paramFirstLower"].ToString(), paramReTestUpper = item["paramReTestUpper"].ToString(), paramReTestLower = item["paramReTestLower"].ToString(), paramUnit = item["paramUnit"].ToString() });
                        str.Add(item["paramName"].ToString(), new ResultPar { paramCode = item["paramCode"].ToString(), paramName = item["paramName"].ToString(), paramFirstUpper = item["paramFirstUpper"].ToString(), paramFirstLower = item["paramFirstLower"].ToString(), paramReTestUpper = item["paramReTestUpper"].ToString(), paramReTestLower = item["paramReTestLower"].ToString() });
                        i++;
                    }
                    //if (i == 0)
                    //{
                    //    return jo["result"].ToString();
                    //}
                    return "true";
                }
                else
                {
                    return jo["result"].ToString();
                }
            }
            catch (Exception ex)
            {
                return "false," + ex.Message;
            }

        }
       

        /// <summary>
        /// 南昌极柱激光清洗机 由工装板和工序序号，设备条码，获取两个模组条码
        /// </summary>
        /// <param name="trayNo">工装板条码</param>
        /// <param name="deviceSn">设备号</param>
        /// <param name="groupCode">工序代码</param>
        /// <param name="timeStamp">上传时间</param>
        /// <returns></returns>
        public string GetTaryBarcode(string trayNo, string deviceSn, string groupCode, string timeStamp, ref List<SNInformtion> snList)
        {
            string mesResult = "";
            try
            {
                //var modelData = new
                //{
                //    deviceSn = deviceSn,//设备号
                //    groupCode = groupCode,//工序代码
                //    taryNo = trayNo, //工装板条码
                //    timeStamp = timeStamp//上传时间
                //};
                //string jsonData = "jsonData=" + JsonConvert.SerializeObject(modelData);
                var modelData = new
                {
                    SN = trayNo, //工装板条码
                };
                string jsonData = "jsonData=" + JsonConvert.SerializeObject(modelData);
                mesResult = mespost.getResult("GetModuleByBoard", jsonData, 10000);
                Log.logMES($"请求接口:GetModuleByBoard",1);
                Log.logMES($"请求Json:{jsonData}",1);
                Log.logMES($"MES返回Json:{mesResult}",1);
                return GetModuleBarcodes(mesResult, ref snList);
            }
            catch (Exception ex1)
            {
                return "false," + ex1.Message;
            }
        }
        /// <summary>
        /// 防错点检
        /// </summary>
        /// <param name="trayNo">工装板条码</param>
        /// <param name="deviceSn">设备号</param>
        /// <param name="groupCode">工序代码</param>
        /// <param name="timeStamp">上传时间</param>
        /// <returns></returns>
        public string SampleDataUpload(string operatorId, string groupCode, string productSn, string testResult, List<Dictionary<string, string>> testData, List<Dictionary<string, string>> stepData,ref string str)
        {
            string mesResult = "";
            try
            {
                var parameterData = new
                {
                    operatorId = operatorId,//SessionID
                    groupCode = groupCode, //工序代码
                    productSn = productSn,//产品条码
                    testResult = testResult,//判定结果
                    testData = testData,
                    stepData = stepData
                };
                string jsonData = "jsonData=" + JsonConvert.SerializeObject(parameterData);
                mesResult = mespost.getResult("SampleDataUpload", jsonData, 10000);
                Log.logMES($"请求接口:SampleDataUpload", 1);
                Log.logMES($"请求Json:{jsonData}", 1);
                return getMsgWest(mesResult,out str);
            }
            catch (Exception ex1)
            {
                return "false," + ex1.Message;
            }
        }
        /// <summary>
        /// 进站检查,成功返回"TRUE"，失败返回具体的出错信息
        /// </summary>
        /// <param name="groupCode">工序代码</param>
        /// <param name="deviceSn">设备编号</param>
        /// <param name="sessionid">SessionID：登录时返回的Result的值</param>
        /// <param name="productSn">产品条码</param>
        /// <param name="moNumber">制令单号</param>
        /// <param name="timeStamp">上传时间</param>
        /// <param name="IsAssemblySn">是否组件条码，0是，1否，若为0，通过组件条码找到产品条码进工序检查及过站</param>
        /// <returns></returns>
        public string StationCheck(string groupCode, string deviceSn, string sessionId, string productSn, string moNumber, string timeStamp, string IsAssemblySn, ref string index)
        {
            string mesResult = "";
            try
            {
                //var modelData = new
                //{
                //    groupCode = groupCode,//工序代码
                //    deviceSn = deviceSn,//设备号
                //    operatorId = sessionId, //SessionId
                //    productSn = productSn, //产品条码
                //    moNumber = moNumber,//制令单号
                //    timeStamp = timeStamp,//上传时间
                //    IsAssemblySn = IsAssemblySn //是否组件条码，0是，1否，若为0，通过组件条码找到产品条码进工序检查及过站
                //};
                //string jsonData = "jsonData=" + JsonConvert.SerializeObject(modelData);
                var modelData = new
                {
                    groupcode = groupCode,//工序代码
                    sessionid = sessionId, //SessionId
                    productSn = productSn, //产品条码
                };
                string jsonData = "GroupInfo=" + JsonConvert.SerializeObject(modelData);

                mesResult = mespost.getResult("StationCheck", jsonData, 10000);
                Log.logMES($"请求接口:StationCheck",1);
                Log.logMES($"请求Json:{jsonData}",1);
                Log.logMES($"MES返回Json:{mesResult}",1);
                return GetCheckModule(mesResult, ref index);
            }
            catch (Exception ex1)
            {
                return mesResult.Replace("[", "【").Replace("]", "】").Replace("\"", "“").Replace(":", "：").Replace("{", "｛").Replace("}", "｝").Replace(",", "，") + "；" + ex1.Message;
            }
        }

        /// <summary>
        /// 过站上传参数,成功返回"TRUE"，失败返回具体的出错信息
        /// </summary>
        /// <param name="groupCode">工序代码</param>
        /// <param name="deviceSn">设备编号</param>
        /// <param name="sessionId">SessionID：登录时返回的Result的值</param>
        /// <param name="productSn">产品条码</param>
        /// <param name="moNumber">制令单号</param>
        /// <param name="timeStamp">上传时间</param>
        /// <param name="testResult">通过参数规格判定,0为OK,1为NG</param>
        /// <param name="IsAssemblySn">是否组件条码，0是，1否，若为0，通过组件条码找到产品条码进工序检查及过站</param>
        /// <param name="testData">产品条码的生产数据</param>
        /// <returns></returns>
        public string OutStationCheckData(string groupCode, string deviceSn, string sessionId, string productSn, string moNumber, string timeStamp, string testResult, string IsAssemblySn, List<Dictionary<string, string>> testData, List<string> materials, ref string str)//过站验证  校验工序
        {
            string mesResult = "";
            try
            {
                //var modelData = new
                //{
                //    groupCode = groupCode,//工序代码
                //    deviceSn = deviceSn,//设备号
                //    operatorId = sessionId, //SessionId
                //    productSn = productSn, //产品条码
                //    moNumber = moNumber,//制令单号
                //    timeStamp = timeStamp,//上传时间

                //    testResult = testResult, //通过参数规格判定,0为OK,1为NG
                //    IsAssemblySn = IsAssemblySn, //是否组件条码，0是，1否，若为0，通过组件条码找到产品条码进工序检查及过站
                //    testData = testData, // 产品条码的生产数据
                //    environment = new List<object> { }, //为空
                //    stepData = new List<object> { } //为空
                //};
                //string jsonData = "jsonData=" + JsonConvert.SerializeObject(modelData);
                var modelData = new
                {
                    productSn = productSn,//SN
                    groupcode = groupCode,//工序代码
                    sessionid = sessionId, //SessionId
                    materials = materials, //materials
                    testData = testData,//生产数据
                    result = testResult, //结果  
                };
                string jsonData = "ProductUploadInfo=" + JsonConvert.SerializeObject(modelData);
                mesResult = mespost.getResult("ProductUpload", jsonData, 10000);
                Log.logMES($"请求接口:ProductUpload",1);
                Log.logMES($"请求Json:{jsonData}",1);
                Log.logMES($"MES返回Json:{mesResult}",1);
                return getMsgWest(mesResult, out str);
            }
            catch (Exception ex1)
            {
                return mesResult.Replace("[", "【").Replace("]", "】").Replace("\"", "“").Replace(":", "：").Replace("{", "｛").Replace("}", "｝").Replace(",", "，") + "；" + ex1.Message;
            }
        }
        
        /// <summary>
        /// 产品上料校验
        /// </summary>
        /// <param name="groupCode">工序代码</param>
        /// <param name="deviceSn">设备编号</param>
        /// <param name="operatorId">SessionID：登录时返回的Result的值</param>
        /// <param name="moNumber">制令单号</param>
        /// <param name="batchCode">物料编号</param>
        /// <param name="assemblyNo">上料顺序</param>
        /// <param name="batchCount">上料数量</param>
        /// <param name="timeStamp">上传时间</param>
        /// <param name="loadItemFlag">1：上料 3：下料</param>
        /// <returns>成功返回TRUE</returns>
        public string GetMaterialControl(string groupCode, string deviceSn, string operatorId, string moNumber, string batchCode,
            string assemblyNo, string batchCount, string timeStamp, string loadItemFlag)
        {
            string mesResult = "";
            try
            {
                var modelData = new
                {
                    groupCode = groupCode, //工序代码
                    deviceSn = deviceSn, //设备号
                    operatorId = operatorId, //SessionID
                    moNumber = moNumber, //指令单号
                    batchCode = batchCode, //物料批次号
                    assemblyNo = assemblyNo, //上料顺序
                    batchCount = batchCount, //上料数量
                    timeStamp = timeStamp, //上传时间
                    loadItemFlag = loadItemFlag //1.上料 2.备料 3.下料
                };
                string jsonData = "jsonData=" + JsonConvert.SerializeObject(modelData);
                mesResult = mespost.getResult("MaterialControl", jsonData, 10000);
                Log.logMES($"请求接口:MaterialControl", 1);
                Log.logMES($"请求Json:{jsonData}", 1);
                Log.logMES($"MES返回Json:{mesResult}", 1);
                return getMsg(mesResult); ;
            }
            catch (Exception ex1)
            {
                return mesResult.Replace("[", "【").Replace("]", "】").Replace("\"", "“").Replace(":", "：").Replace("{", "｛").Replace("}", "｝").Replace(",", "，") + "；" + ex1.Message;
            }
        }
        public string GroupTest(string productSn, string userCode, string deviceCode)//过站验证  校验工序
        {
            string mesResult = "";
            try
            {
                StringBuilder sbJsonData = new StringBuilder();
                sbJsonData.Append("jsonData={\"productSn\":\"");
                sbJsonData.Append(productSn);
                sbJsonData.Append("\",\"userCode\":\"");
                sbJsonData.Append(userCode);
                sbJsonData.Append("\",\"deviceCode\":\"");
                sbJsonData.Append(deviceCode);
                sbJsonData.Append("\",\"res\":null}");
                mesResult = mespost.getResult("GroupTest.action?", sbJsonData.ToString(), 10000);
                return getMsg(mesResult);
            }
            catch (Exception ex1)
            {
                return mesResult.Replace("[", "【").Replace("]", "】").Replace("\"", "“").Replace(":", "：").Replace("{", "｛").Replace("}", "｝").Replace(",", "，") + "；" + ex1.Message;
            }
        }
      
        public string WipTest(string productSn, string result, string userCode, string deviceCode, string errorCodeAllData, string itemValue)//上传数据
        {
            string mesResult = "";
            try
            {
                StringBuilder sbJsonData = new StringBuilder();
                sbJsonData.Append("jsonData={\"productSn\":\"");
                sbJsonData.Append(productSn);
                sbJsonData.Append("\",\"errorCodeAllData\":\"");
                sbJsonData.Append(errorCodeAllData);
                sbJsonData.Append("\",\"userCode\":\"");
                sbJsonData.Append(userCode);
                sbJsonData.Append("\",\"deviceCode\":\"");
                sbJsonData.Append(deviceCode);
                sbJsonData.Append("\",\"itemValue\":\"");
                sbJsonData.Append(itemValue);
                sbJsonData.Append("\",\"result\":\"");
                sbJsonData.Append(result);
                sbJsonData.Append("\",\"res\":null}");
                mesResult = mespost.getResult("WipTest.action?", sbJsonData.ToString(), 10000);
                return getMsg(mesResult);
            }
            catch (Exception ex1)
            {
                return mesResult.Replace("[", "【").Replace("]", "】").Replace("\"", "“").Replace(":", "：").Replace("{", "｛").Replace("}", "｝").Replace(",", "，") + "；" + ex1.Message;
            }
        }
      
        public string OfflineUploadData(string productSn, string groupCode, string errCode, string result, string userCode, string deviceCode, string errorCodeAllData, string itemValue)//上传数据
        {
            string mesResult = "";
            try
            {
                StringBuilder sbJsonData = new StringBuilder();
                sbJsonData.Append("jsonData={\"productSn\":\"");
                sbJsonData.Append(productSn.Replace("\r\n", ""));
                sbJsonData.Append("\",\"groupCode\":\"");
                sbJsonData.Append(groupCode);
                sbJsonData.Append("\",\"errorCode\":\"");
                sbJsonData.Append(errCode);
                sbJsonData.Append("\",\"userCode\":\"");
                sbJsonData.Append(userCode);
                sbJsonData.Append("\",\"deviceCode\":\"");
                sbJsonData.Append(deviceCode);
                sbJsonData.Append("\",\"errorCodeAllData\":\"");
                sbJsonData.Append(errorCodeAllData);
                sbJsonData.Append("\",\"itemValue\":\"");
                sbJsonData.Append(itemValue);
                sbJsonData.Append("\",\"result\":\"");
                sbJsonData.Append(result);
                sbJsonData.Append("\",\"res\":null}");
                mesResult = mespost.getResult("OfflineUploadData.action?", sbJsonData.ToString(), 10000);
                return getMsg(mesResult);
            }
            catch (Exception ex1)
            {
                return mesResult.Replace("[", "【").Replace("]", "】").Replace("\"", "“").Replace(":", "：").Replace("{", "｛").Replace("}", "｝").Replace(",", "，") + "；" + ex1.Message;
            }
        }
        public string UploadImagePath(string productSn, string imgPath, string imType)//过站验证  校验工序
        {
            string mesResult = "";
            try
            {
                StringBuilder sbJsonData = new StringBuilder();
                sbJsonData.Append("jsonData=[{\"productSn\":\"");
                sbJsonData.Append(productSn);
                sbJsonData.Append("\",\"imgPath\":\"");
                sbJsonData.Append(imgPath);
                sbJsonData.Append("\",\"imgType\":\"");
                sbJsonData.Append(imType);
                //sbJsonData.Append("\",\"res\":null}");
                sbJsonData.Append("\"}]");


                mesResult = mespost.getResult("UploadImgPath.action?", sbJsonData.ToString(), 10000);
                return getMsg(mesResult);
            }
            catch (Exception ex1)
            {
                return mesResult.Replace("[", "【").Replace("]", "】").Replace("\"", "“").Replace(":", "：").Replace("{", "｛").Replace("}", "｝").Replace(",", "，") + "；" + ex1.Message;
            }
        }

        public string CellToolingPlate(string MeProductSn)//过站验证  校验工序
        {
            string mesResult = "";
            try
            {
                StringBuilder sbJsonData = new StringBuilder();
                sbJsonData.Append("jsonData={\"plateSn\":\"");
                sbJsonData.Append(MeProductSn);
                sbJsonData.Append("\",\"productSn\":\"");
                sbJsonData.Append(" ");
                sbJsonData.Append("\",\"linkType\":\"");
                sbJsonData.Append("1");
                //sbJsonData.Append("\",\"res\":null}");
                sbJsonData.Append("\"}");
                mesResult = mespost.getResult("CellToolingPlate.action?", sbJsonData.ToString(), 10000);
                return getMsg_mes(mesResult);
            }
            catch (Exception ex1)
            {
                return mesResult.Replace("[", "【").Replace("]", "】").Replace("\"", "“").Replace(":", "：").Replace("{", "｛").Replace("}", "｝").Replace(",", "，") + "；" + ex1.Message;
            }
        }
      

        //进站
        //mes校验返回，二维码，工序代码(南京二期)
        //public string StationCheck(string sessionid, string productSn, string groupcode)//过站验证  校验工序
        //{
        //    string mesResult = "";
        //    try
        //    {
        //        StringBuilder sbJsonData = new StringBuilder();
        //        sbJsonData.Append("StationCheck={\"sessionid\":\"");
        //        sbJsonData.Append(sessionid);
        //        sbJsonData.Append("\",\"productSn\":\"");
        //        sbJsonData.Append(productSn);
        //        sbJsonData.Append("\",\"groupcode\":\"");
        //        sbJsonData.Append(groupcode);
        //        sbJsonData.Append("\"}");
        //        mesResult = mespost.getResult("StationCheck", sbJsonData.ToString(), 10000);
        //        return getMsg(mesResult);
        //    }
        //    catch (Exception ex1)
        //    {
        //        return mesResult.Replace("[", "【").Replace("]", "】").Replace("\"", "“").Replace(":", "：").Replace("{", "｛").Replace("}", "｝").Replace(",", "，") + "；" + ex1.Message;
        //    }
        //}

    } 
    public class ResultPar
    {
        /// <summary>
        /// 参数代码
        /// </summary>
        public string paramCode { get; set; }
        /// <summary>
        /// 参数名称
        /// </summary>
        public string paramName { get; set; }
        /// <summary>
        /// 参数首测上限
        /// </summary>
        public string paramFirstUpper { get; set; }
        /// <summary>
        /// 参数首测下限
        /// </summary>
        public string paramFirstLower { get; set; }
        /// <summary>
        /// 参数复测上限
        /// </summary>
        public string paramReTestUpper { get; set; }
        /// <summary>
        /// 参数复测下限
        /// </summary>
        public string paramReTestLower { get; set; }
        /// <summary>
        /// 参数单位
        /// </summary>
        public string paramUnit { get; set; }
        /// <summary>
        /// 实时值
        /// </summary>
        public string paramCurValue { get; set; }

    }
    public class SNInformtion
    {
        public string sn;
        public string position;
    }
    public class testdata
    {
        public string testItem;
        public string testValue;
        public string unit = " ";
        public string testResult = "PASS";
    }

    public class 生产数据
    {
        public string ItemCode { get; set; }
        public string Value { get; set; }

        public string ItemResult = "0";

    }
}
