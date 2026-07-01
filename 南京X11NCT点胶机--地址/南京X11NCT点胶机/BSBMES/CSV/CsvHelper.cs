using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.POIFS.FileSystem;
using NPOI.HSSF.UserModel;
using System.Data;
using System.Windows.Forms;

namespace UpperComputer
{
    public static class CsvHelper
    {
        /// <summary>
        /// 保存csv文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="content"></param>
        public static bool SaveAsCSV<T>(string fileName, IList<T> listModel) where T : class, new()
        {
            bool flag = false;

            try
            {
                string dir =@"D:\生产记录\";
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                string fullName = Path.Combine(dir, fileName);


                StringBuilder sb = new StringBuilder();

                //通过反射 显示要显示的列
                BindingFlags bf = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static;//反射标识
                Type objType = typeof(T);
                PropertyInfo[] propInfoArr = objType.GetProperties(bf);
                string header = string.Empty;
                List<string> listPropertys = new List<string>();
                foreach (PropertyInfo info in propInfoArr)
                {
                    if (string.Compare(info.Name.ToUpper(), "ID") != 0) //不考虑自增长的id或者自动生成的guid等
                    {
                        if (!listPropertys.Contains(info.Name))
                        {
                            listPropertys.Add(info.Name);
                        }
                        if (!File.Exists(fullName)) //不存在执行
                            header += info.Name + ",";
                    }
                }
                if (!File.Exists(fullName)) //不存在执行
                    sb.AppendLine(header.Trim(',')); //csv头

                foreach (T model in listModel)
                {
                    string strModel = string.Empty;
                    foreach (string strProp in listPropertys)
                    {
                        foreach (PropertyInfo propInfo in propInfoArr)
                        {
                            if (string.Compare(propInfo.Name.ToUpper(), strProp.ToUpper()) == 0)
                            {
                                PropertyInfo modelProperty = model.GetType().GetProperty(propInfo.Name);
                                if (modelProperty != null)
                                {
                                    object objResult = modelProperty.GetValue(model, null);
                                    string result = ((objResult == null) ? string.Empty : objResult).ToString().Trim();
                                    if (result.IndexOf(',') != -1)
                                    {
                                        result = "\"" + result.Replace("\"", "\"\"") + "\""; //特殊字符处理 ？
                                        //result = result.Replace("\"", "“").Replace(',', '，') + "\"";
                                    }
                                    if (!string.IsNullOrEmpty(result))
                                    {
                                        Type valueType = modelProperty.PropertyType;
                                        if (valueType.Equals(typeof(Nullable<decimal>)))
                                        {
                                            result = decimal.Parse(result).ToString("#.#");
                                        }
                                        else if (valueType.Equals(typeof(decimal)))
                                        {
                                            result = decimal.Parse(result).ToString("#.#");
                                        }
                                        else if (valueType.Equals(typeof(Nullable<double>)))
                                        {
                                            result = double.Parse(result).ToString("#.#");
                                        }
                                        else if (valueType.Equals(typeof(double)))
                                        {
                                            result = double.Parse(result).ToString("#.#");
                                        }
                                        else if (valueType.Equals(typeof(Nullable<float>)))
                                        {
                                            result = float.Parse(result).ToString("#.#");
                                        }
                                        else if (valueType.Equals(typeof(float)))
                                        {
                                            result = float.Parse(result).ToString("#.#");
                                        }
                                    }
                                    strModel += result + ",";
                                }
                                else
                                {
                                    strModel += ",";
                                }
                                break;
                            }
                        }
                    }
                    strModel = strModel.Substring(0, strModel.Length - 1);
                    sb.AppendLine(strModel);
                }
                string content = sb.ToString();

                using (FileStream fs = new FileStream(fullName, FileMode.Append, FileAccess.Write))
                {
                    StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                    sw.Flush();
                    sw.Write(content);
                    sw.Flush();
                    sw.Close();
                }
                flag = true;
            }
            catch
            {
                flag = false;
            }
            return flag;
        }
    }

    public static class Excelhelper
    {

        private static IWorkbook workbook = null;
        private static FileStream fs = null;
        private static bool disposed;

        private static object obj = new object();

        /// <summary>
        /// 将DataTable数据导入到excel中
        /// </summary>
        /// <param name="data">要导入的数据</param>
        /// <param name="isColumnWritten">DataTable的列名是否要导入</param>
        /// <param name="sheetName">要导入的excel的sheet的名称</param>      
        /// <param name="fileName">要导入的excel的sheet的路径</param>      
        public static int DataTableToExcel(string code, List<string> listHeard, List<string> list, string Path, string fileName)
        {
            lock (obj)
            {
                int i = 0;
                int j = 0;
                int count = 0;
                ISheet sheet = null;
                bool isColumnWritten = false;
                IRow Rows;
                try
                {
                    if (!Directory.Exists(Path))
                    {
                        Directory.CreateDirectory(Path);
                    }
                    string creatPath = Path + "\\" + fileName;
                    if (!File.Exists(creatPath))
                    {
                        isColumnWritten = true;
                    }
                    if (isColumnWritten == true)
                    {
                        using (fs = new FileStream(creatPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                        {

                            if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                                workbook = new XSSFWorkbook();
                            else if (fileName.IndexOf(".xls") > 0) // 2003版本
                                workbook = new HSSFWorkbook();

                            if (workbook != null)
                            {
                                sheet = workbook.CreateSheet("生产信息");
                            }
                            else
                            {
                                return -1;
                            }
                            IRow rows = sheet.CreateRow(0);
                            for (j = 0; j < listHeard.Count; ++j)
                            {
                                rows.CreateCell(j).SetCellValue(listHeard[j]);
                            }
                            count = 1;
                            Rows = sheet.CreateRow(sheet.LastRowNum + 1);
                            for (j = 0; j < list.Count; ++j)
                            {
                                Rows.CreateCell(j).SetCellValue(list[j].ToString());
                            }
                            workbook.Write(fs); //写入到excel
                            return count;

                        }
                    }
                    else
                    {
                        //读取流
                        using (FileStream fs = new FileStream(creatPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        {
                            POIFSFileSystem ps = new POIFSFileSystem(fs);//需using NPOI.POIFS.FileSystem;
                            IWorkbook workbook = new HSSFWorkbook(ps);
                            sheet = workbook.GetSheetAt(0);//获取工作表
                            IRow row = sheet.GetRow(0); //得到表头
                                                        //写入流
                            using (FileStream fout = new FileStream(creatPath, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
                            {
                                row = sheet.CreateRow((sheet.LastRowNum + 1));//在工作表中添加一行
                                for (j = 0; j < list.Count; ++j)
                                {
                                    row.CreateCell(j).SetCellValue(list[j].ToString());
                                }
                                workbook.Write(fout);//写入文件
                                workbook = null;
                                return 0;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.log($"保存条码[{code}]数据失败,原因:{ex.Message}");
                    //MessageBox.Show(ex.Message);
                    return -1;
                }
            }
            
        }

        public static int DataTableToExcel(DataTable data, string sheetName, bool isColumnWritten, string fileName)
        {
            int i = 0;
            int j = 0;
            int count = 0;
            ISheet sheet = null;
            try
            {

                fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook();
                else if (fileName.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook();


                if (workbook != null)
                {
                    sheet = workbook.CreateSheet(sheetName);
                }
                else
                {
                    return -1;
                }

                if (isColumnWritten == true) //写入DataTable的列名
                {
                    IRow row = sheet.CreateRow(0);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        row.CreateCell(j).SetCellValue(data.Columns[j].ColumnName);
                    }
                    count = 1;
                }
                else
                {
                    count = 0;
                }

                for (i = 0; i < data.Rows.Count; ++i)
                {
                    IRow row = sheet.CreateRow(count);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        row.CreateCell(j).SetCellValue(data.Rows[i][j].ToString());
                    }
                    ++count;
                }
                workbook.Write(fs); //写入到excel
                return count;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
        }
    }
}
