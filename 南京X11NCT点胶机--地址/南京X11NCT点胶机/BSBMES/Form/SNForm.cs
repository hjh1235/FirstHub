using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UpperComputer.TaskUnit;

namespace UpperComputer
{
    public partial class SNForm : Form
    {
        public SNForm()
        {
            InitializeComponent();
        }

        private void btn_手动过站_Click(object sender, EventArgs e)
        {
            MunGust();
        }
        public void MunGust()
        {
            MunLoadMESForm _MunLoadMESForm = new MunLoadMESForm();
            _MunLoadMESForm.ShowDialog();
            if (!_MunLoadMESForm.bSuccess)
            {
                return;
            }
            try
            {
                List<Dictionary<string, string>> testData = new List<Dictionary<string, string>>();
                bool testResult = true; //参数校验判定总结论

                string moduleCode = txtCellCode.Text.Trim();//模组码
                string paramCode = "";
                string paramName = "";
                double paramValue = 0;

                string paramResult = "0"; //判定结果 0：合格  1：NG
                double lowerValue = 0;
                double upperValue = 0;
                string paramUnit = "";
                List<string> errList = new List<string>();
                foreach (var item in InitClass.LoaddicPar)
                {
                    paramName = item.Value.paramName;
                    paramCode = item.Value.paramCode;
                    paramUnit = item.Value.paramUnit;
                    lowerValue = Convert.ToDouble(item.Value.paramFirstLower);
                    upperValue = Convert.ToDouble(item.Value.paramFirstUpper);
                    paramValue = Convert.ToDouble(item.Value.paramFirstLower);
                    if ((Formlogin.dicPar.ContainsKey(paramName) && lowerValue <= paramValue && upperValue >= paramValue)
                        || Formlogin.dicPar.ContainsKey(paramName) == false) //参与校验且合格或者不参与检验（默认合格）
                    {
                        paramResult = "0";
                    }
                    else //结论：不合格
                    {
                        testResult = false;
                        paramResult = "1";
                        string msg = $"MES上传参数:{paramName}超出限制范围,MES上限:{upperValue},MES下限:{lowerValue},当前值:{paramValue}";
                        errList.Add(msg);
                        Log.log(msg);
                    }
                    testData.Add(new Dictionary<string, string>() {
                                            { "paramCode", paramCode }, { "paramName", paramName }, { "paramValue", paramValue.ToString() }, { "paramResult", paramResult }, { "paramUnit", paramUnit }
                                        });
                }

                string str = "";
                List<string> materials = new List<string>();
                materials.Add(ClassCANS.OLD.工位1胶水A物料编码);
                string checkResultUpload = mes.Instance().OutStationCheckData(
                                                 ClassCANS.OLD.groupCode, ClassCANS.OLD.MachineNo, ClassCANS.OLD.SessionId, moduleCode, ClassCANS.OLD.MoNumber, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                 testResult == true ? "0" : "1", "1", testData, materials, ref str);
                if (checkResultUpload.ToUpper().Contains("TRUE"))
                {
                    SaveExcel(moduleCode,"OK", errList);
                    Log.log($"PACK条码[{moduleCode}]手动过站成功");
                    MessageBox.Show($"PACK条码[{moduleCode}]手动过站成功","提示");
                }
                else
                {
                    errList.Add(checkResultUpload);
                    SaveExcel(moduleCode, "NG", errList);
                    string msg = $"PACK条码[{moduleCode}]手动过站失败,原因：{checkResultUpload}";
                    MessageBox.Show(msg, "提示");
                    Log.alarmLog(msg, 1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"手动过站失败，原因：{ex.Message}");

            }
        }

        public void SaveExcel(string code,string status, List<string> errlist)
        {
            try
            {
                string strValue = Data.工位1点胶量.ToString();
                string strTime = Data.工位1点胶时间.ToString();
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("小车条码", "");
                dic.Add("PACK条码", code);
                dic.Add("时间", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                dic.Add("状态", status);
                dic.Add("类型", "出站");
                dic.Add("工位", "1");
                dic.Add("点胶量", strValue);
                dic.Add("点胶时间", strTime);
              
                foreach (var item in InitClass.LoaddicPar)
                {
                    if (item.Value.paramName.Contains("检测面积"))
                    {
                        if (dic.ContainsKey(item.Value.paramName))
                            continue;
                        dic.Add(item.Value.paramName,item.Value.paramCurValue);
                    }
                }
                string msg = "";
                foreach (var item in errlist)
                {
                    msg += item + ";";
                }
                dic.Add("备注", msg);

                MesTaskFlow.Callback(dic, status);
                List<string> list = new List<string>();
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
                Task.Run(() => {
                    int i = Excelhelper.DataTableToExcel(code, listhead, list, path, txtname + ".xls");
                });
            }
            catch (Exception)
            {

               
            }         
        }
    }
}
