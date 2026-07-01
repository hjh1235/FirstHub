using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CCWin.SkinClass;

namespace UpperComputer.TaskUnit
{
    public class MesTaskFlow : TaskClass
    {
        /// <summary>
        /// Mes流程步骤
        /// </summary>
        int MesRunStep = 0;
        string strMes = "";
        string strMes1 = "";
        int count = 0;
        int count2 = 0;
    
        public static event Action<Dictionary<string, string>, string> CallBack;

        public enum MesFlowFlag
        {
            等待焊接完成,
            MES过站,
            MES过站成功,
            MES过站失败,
            等待复位焊接完成
        }
      
        public static void Callback(Dictionary<string, string> dic, string str)
        {
            CallBack?.Invoke(dic, str);
        }
        //MES流程
        public override void TaskRun()
        {
            while (true)
            {
                Thread.Sleep(10);
                if (TaskClass.plcConnState == false || !FormStart.bInit)
                {
                    continue;
                }
                switch (MesRunStep)
                {
                    case (int)MesFlowFlag.等待焊接完成:
                        strMes = FormStart.pLCTool.ReadPLC("请求上传").ToString();
                        if (strMes== "True")
                        {
                            Thread.Sleep(2000);
                            strMes1 = FormStart.pLCTool.ReadPLC("请求上传").ToString();
                            Log.log($"接收到PLC请求上传MES信号[{strMes},{strMes1}]");
                            if (strMes != strMes1)
                                break;
                            Log.log($"点胶完成，接收到PLC请求上传MES信号");
                            //string code = FormStart.pLCTool.ReadPLC("模组条码").ToString().Replace("\r", "").Replace("\0", "");//通过PLC获取到的模组码
                            string code = ClassCANS.OLD.模组码;
                            Log.log($"读取模组码成功:[{code}]");
                                                                                           
                            List<string> errList = new List<string>();
                            Dictionary<string, string> dic = new Dictionary<string, string>();
                            Dictionary<string, string> dic1 = new Dictionary<string, string>();
                            Dictionary<string, string> MESdic = new Dictionary<string, string>();
                            //获取参数
                            if (!dealWithMeltData(code, ref dic, ref MESdic))
                            {
                                //FormStart.pLCTool.WritePLC("报警", "1");
                                MesRunStep = (int)MesFlowFlag.MES过站失败;
                                continue;
                            }
                            Program.模组数据.Remove(code);
                            Program.模组数据.Add(code, dic);
                            Program.模组数据MES.Remove(code);
                            Program.模组数据MES.Add(code, MESdic);
                            if (ClassCANS.OLD.启用MES)
                            {
                                List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
                                bool bResult = true;
                                if (!dealWithMesPara(code, MESdic, ref list, ref errList, ref bResult))
                                {
                                    upadteMainFormData(dic, "出站", "NG", errList);
                                    SaveExcel(code);
                                    MesRunStep = (int)MesFlowFlag.MES过站失败;
                                    continue;
                                }
                                if (!uploadProduct(code, list, bResult, ref errList))
                                {
                                    upadteMainFormData(dic, "出站" ,"NG", errList);
                                    SaveExcel(code);
                                    MesRunStep = (int)MesFlowFlag.MES过站失败;
                                    continue;
                                }
                                ClassCANS.OLD.模组码 = "";
                                ClassCANS.OLD.小车码 = "";
                                upadteMainFormData(dic,"出站", "OK", errList);
                                MesRunStep = (int)MesFlowFlag.MES过站成功;
                            }
                            else
                            {
                                upadteMainFormData(dic,"屏蔽MES", "OK", errList);
                                MesRunStep = (int)MesFlowFlag.MES过站成功;
                            }                          
                            SaveExcel(code);
                        }
                        break;
                    case (int)MesFlowFlag.MES过站成功:
                        if (FormStart.pLCTool.WritePLC("上传OK", "True"))
                        {
                            MesRunStep = (int)MesFlowFlag.等待复位焊接完成;
                        }
                        break;
                    case (int)MesFlowFlag.MES过站失败:
                        if (FormStart.pLCTool.WritePLC("上传NG", "True"))
                        {
                            MesRunStep = (int)MesFlowFlag.等待复位焊接完成;
                        }
                        break;
                    case (int)MesFlowFlag.等待复位焊接完成:
                        if (FormStart.pLCTool.ReadPLC("请求上传").ToString() == "False")
                        {
                            if (FormStart.pLCTool.WritePLC("上传OK", "False") && FormStart.pLCTool.WritePLC("上传NG", "False"))
                            {
                                MesRunStep = (int)MesFlowFlag.等待焊接完成;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 处理MES上传参数
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private bool dealWithMesPara(string code, Dictionary<string, string> dic, ref List<Dictionary<string, string>> list, ref List<string> errList, ref bool bResult)
        {
            string name = "";
            try
            {
                list.Clear();
                string paramValue = "";
                string testResult = "PASS";
                int i = 1;
                int i2 = 1;
                
                foreach (var item in Formlogin.dicPar.Values)
                {
                    if (item.paramName==null)
                    {
                        continue;
                    }
                    name = item.paramName;
                    paramValue = Program.模组数据MES[code][item.paramName];
                    if (item.paramName.Contains("面积比"))
                    {
                        paramValue = "100";
                    }
                    
                    double paramFirstLower = Convert.ToDouble(item.paramFirstLower);
                    double paramFirstUpper = Convert.ToDouble(item.paramFirstUpper);
                    if (Convert.ToDouble(paramValue) > paramFirstUpper ||
                       Convert.ToDouble(paramValue) < paramFirstLower)
                    {
                        if (!ClassCANS.OLD.检测NG放行)
                        {
                            testResult = "FAIL";
                            bResult = false;
                            errList.Add($"参数名称:[{item.paramName}]当前值:[{paramValue}]超出管控范围[{paramFirstLower},{paramFirstUpper}]");
                        }
                        else
                        {
                            Log.log($"参数名称:[{item.paramName}]当前值:[{paramValue}]超出管控范围[{paramFirstLower},{paramFirstUpper}]，开启检测NG放行");
                            paramValue = paramFirstLower.ToString();
                        }
                    }
                    else
                    {
                        testResult = "PASS";
                    }
                    list.Add(new Dictionary<string, string>
                    {
                        ["testItem"] = item.paramName,
                        ["testValue"] = paramValue,
                        ["unit"] = item.paramUnit,
                        ["testResult"] = testResult
                    });
                }
                list.Add(new Dictionary<string, string>
                {
                    ["testItem"] = "点胶时间",
                    ["testValue"] = DateTime.Now.ToString("yyyy/MM/dd/HH/mm/ss"),
                    ["unit"] = "",
                    ["testResult"] = "PASS"
                });
            }
            catch (Exception ex)
            {
                errList.Add($"处理MES数据【{name}】异常,异常原因:{ex.Message}");
                Log.alarmLog($"处理MES数据【{name}】异常,异常原因:{ex.Message}",2);
                return false;
            }
            return true;
        }


        /// <summary>
        /// 上传参数过站
        /// </summary>
        /// <param name="code">产品二维码</param>
        /// <param name="testList">测试项数据</param>
        /// <returns></returns>
        public bool uploadProduct(string code, List<Dictionary<string, string>> testList, bool bResult, ref List<string> errList)
        {
            string str = "";
            List<string> materials = new List<string>();
            materials.Add(ClassCANS.OLD.工位1胶水A物料编码);
            string checkResultUpload = mes.Instance().OutStationCheckData(
                                             ClassCANS.OLD.groupCode, ClassCANS.OLD.MachineNo, ClassCANS.OLD.SessionId, code, ClassCANS.OLD.MoNumber, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                             bResult == true ? "PASS" : "TRUE", "1", testList, materials, ref str);
            if (checkResultUpload.ToUpper().Contains("TRUE"))
            {
                Log.log($"模组组条码[{code}]过站成功");
            }
            else
            {
                string msg = $"模组条码[{code}]过站失败,原因：{checkResultUpload}";
                errList.Add(checkResultUpload);
                Log.alarmLog(msg,1);
                return false;
            }
            return true;
        }

        public bool dealWithMeltData(string code, ref Dictionary<string, string> dic, ref Dictionary<string, string> MESdic)
        {
            try
            {
                if (code == "")
                {
                    Log.log("获取plc模组条码出错，条码为空");
                    return false;
                }
                string str = "";
                string strValue = "";
                string 左胶水面积 = "";
                string 右胶水面积 = "";
                string 左胶水面积比 = "";
                string 右胶水面积比 = "";
                double strTime = 0;
                double strTime2 = 0;
                count = 1;
                count2 = 1;
                strTime =double.Parse( (FormStart.pLCTool.ReadPLC("工位1点胶时间").ToString()))/100;
                strTime2 =double.Parse( (FormStart.pLCTool.ReadPLC("工位2点胶时间").ToString()))/100;
                //if (Data.bUserFlag1&&Data.bUserFlag2)
                //{
                //    str = "1\\2";
                //    //strValue = Data.工位1点胶量.ToString();
                //    strTime = Data.工位1点胶时间.ToString();
                //}
                //else if(Data.bUserFlag1)
                //{
                //    str = "1";
                //    //strValue = Data.工位1点胶量.ToString();
                //    strTime = Data.工位1点胶时间.ToString();
                //}
                //else if (Data.bUserFlag2)
                //{
                //    str = "2";
                //    //strValue = Data.工位2点胶量.ToString();
                //    strTime = Data.工位2点胶时间.ToString();
                //}
                //测试
                //for (int i = 1; i < 16; i++)
                // {
                //     FormStart.ccdData.Add(i.ToString(), new CCDDataClass { strPoint = i.ToString(), status = "OK", value = "90", value2 = "20" });

                // }
                //double 点胶 = double.Parse(strTime) * 0.6;

                dic.Add("小车条码", ClassCANS.OLD.小车码);
                dic.Add("PACK条码", code);
                dic.Add("时间", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                dic.Add("状态", "");
                dic.Add("类型", "");
                //dic.Add("工位", str);
                //dic.Add("点胶量", 点胶.ToString());
                dic.Add("左打胶时间", (strTime*1000).ToString());
                dic.Add("右打胶时间", (strTime2*1000).ToString());
                MESdic.Add("左打胶时间", (strTime * 1000).ToString());
                MESdic.Add("右打胶时间", (strTime2 * 1000).ToString());

                //test
                //for (int i = 1; i <= 12; i++)
                //{
                //    MESdic.Add($"左面积{i}", "100");
                //    左胶水面积 = $"左面积{i}【100】";
                //    MESdic.Add($"左面积比{i}", "100");
                //    左胶水面积比 = $"左面积比{i}【100】";
                //}
                //for (int i = 1; i <= 15; i++)
                //{
                //    MESdic.Add($"右面积{i}", "100");
                //    右胶水面积 = $"右面积{i}【100】";
                //    MESdic.Add($"右面积比{i}", "100");
                //    右胶水面积比 = $"右面积比{i}【100】";
                //}

                foreach (var item in InitClass.CCDArea)
                {
                    MESdic.Add($"左面积{count}", item.ToString());
                    左胶水面积 = $"左面积{count}【{item}】";
                    count++;
                }
                foreach (var item in InitClass.CCDArea2)
                {
                    MESdic.Add($"右面积{count2}", item.ToString());
                    右胶水面积 = $"右面积{count}【{item}】";
                    count2 ++;
                }
                count = 1;
                count2 = 1;
                foreach (var item in InitClass.Proportion)
                {
                    MESdic.Add($"左面积比{count}", item.ToString());
                    左胶水面积比 = $"左面积比{count}【{item}】";
                    count++;
                }
                foreach (var item in InitClass.Proportion2)
                {
                    MESdic.Add($"右面积比{count2}", item.ToString());
                    右胶水面积比 = $"右面积比{count}【{item}】";
                    count2++;
                }
                dic.Add("左胶水面积", 左胶水面积);
                dic.Add("右胶水面积", 右胶水面积);
                dic.Add("左胶水面积比", 左胶水面积比);
                dic.Add("右胶水面积比", 右胶水面积比);
                //for (int count = 1; count <= 15; count++)
                //{
                //    dic.Add($"检测面积{count}", FormStart.ccdData.ContainsKey(count.ToString()) == true ? FormStart.ccdData[count.ToString()].value.Trim() : "0");
                //}
                //for (int count = 1; count <= 15; count++)
                //{
                //    dic.Add($"离防爆阀最小距离{count}", FormStart.ccdData.ContainsKey(count.ToString()) == true ? FormStart.ccdData[count.ToString()].value2.Trim() : "0");
                //}
                dic.Add("备注", "");

            }
            catch (Exception ex)
            {
                Log.alarmLog("获取模组数据出错" + ex,1);
                return false;
            }
            return true;
        }
        /// <summary>
        /// 更新主页面中的表格
        /// </summary>
        public void upadteMainFormData(Dictionary<string, string> dic,string status, string result, List<string> errList)
        {
            try
            {
                string str = "";
                foreach (var item in errList)
                {
                    str += item + ";";
                }
                if (dic.ContainsKey("类型"))
                    dic["类型"] = status;
                if (dic.ContainsKey("状态"))
                    dic["状态"] = result;
                if (dic.ContainsKey("备注"))
                    dic["备注"] = str;
                Callback(dic, result);
                //FormStart.formStart.更新界面(code, dic, result);
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// 按条数据导入Excel表格清洗数据
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool SaveExcel(string code)
        {
            try
            {
                List<string> list = new List<string>();
                Dictionary<string, string> dic = Program.模组数据[code];
                DateTime time = DateTime.Now;
                string year = time.Year.ToString();
                string month = time.Month.ToString();
                string day = time.Day.ToString();
                string path = "D:\\生产记录\\生产信息\\" + year + "\\" + month;
                string txtname = year + month + day;
                //添加标题名称
                List<string> listhead = new List<string>();              
                foreach (var item in dic)
                {
                    listhead.Add(item.Key);
                    list.Add(item.Value);
                }
                Task.Run(()=> {
                    int i = Excelhelper.DataTableToExcel( code, listhead, list, path, txtname + ".xls");
                });
                //
                //if (i == -1)
                //{
                //    return false;
                //}
                //else
                //{
                //    return true;
                //}
            }
            catch (Exception ex)
            {
                Log.alarmLog("保存EXCEL数据出错："+ex.ToString(),1);
                return false;
            }
            return true;
        }
        public override void iniPlc(string str)
        {
            FormStart.pLCTool.WritePLC("上传OK", "False");
            FormStart.pLCTool.WritePLC("上传NG", "False");
            MesRunStep = (int)MesFlowFlag.等待焊接完成;
            Log.log("初始化MES流程完成。");
        }
    }
}
