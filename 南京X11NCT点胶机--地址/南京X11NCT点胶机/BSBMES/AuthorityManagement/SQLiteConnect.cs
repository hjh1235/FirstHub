using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using NPOI;
using NPOI.POIFS.FileSystem;

namespace  UpperComputer

{


    public static class SQLiteConnect
    {
        public static SQLiteConnection scWebshop;
        public static SQLiteDataReader sqlReader;
        public static DataSet ds;
        public static SQLiteDataAdapter myAdapter;
        public static SQLiteCommand myCommand;

        public static void OpenSqliteConnection(ref string msg)
        {
            string strConString = @"Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "Parameter\\Productiondata.db" + ";Version=3";
            try
            {
                scWebshop = new SQLiteConnection(strConString);
                scWebshop.Open();

            }
            catch (Exception error)
            {               
                msg = $"连接数据库异常,原因:"+error.Message;
            }
        }
        /// <summary>
        /// 数据查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataSet getDataSet(string sql,ref string msg)
        {
            object objget = new object();
            lock (objget)
            {
                OpenSqliteConnection(ref msg);
                myAdapter = new SQLiteDataAdapter(sql, scWebshop);
                ds = new DataSet();
                int i = myAdapter.Fill(ds);
                try
                {
                    myAdapter.Dispose();
                    scWebshop.Close();
                }
                catch (Exception error)
                {                  
                    msg = $"查询数据异常,原因:" + error.Message;
                }
                if (i >= 0)
                    return ds;
                else
                    return null;
            }

        }
        /// <summary>
        /// 数据增删
        /// </summary>
        /// <param name="Sql"></param>
        /// <returns></returns>
        public static int executeSQL(string Sql,ref string msg)
        {
            object obj = new object();
            SQLiteCommand MyCommand = null;
            lock (obj)
            {
                int intRows = 0;
                try
                {
                    OpenSqliteConnection(ref  msg);
                    MyCommand = new SQLiteCommand(Sql, scWebshop);
                    intRows = MyCommand.ExecuteNonQuery();
                    MyCommand.Dispose();
                    scWebshop.Close();
                }
                catch (Exception error)
                {
                    MyCommand.Dispose();
                    scWebshop.Close();
                    //MainForm.m_formAlarm.InsertAlarmMessage(ERecvResult.改变数据库数据异常.ToString() + "\r\n" + error);
                }
                return intRows;
            }
        }
        /// <summary>
        /// 数据按条件查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static string getQuery(string sql, string res,ref string msg)
        {
            string result = "";
            object obj = new object();
            lock (obj)
            {
                try
                {
                    OpenSqliteConnection(ref msg);
                    myCommand = new SQLiteCommand(sql, scWebshop);
                    sqlReader = myCommand.ExecuteReader();
                    while (sqlReader.Read())
                    {
                        result = sqlReader[res].ToString();
                    }
                    myCommand.Dispose();
                    scWebshop.Close();
                    return result;
                }
                catch (Exception error)
                {
                    msg = $"查询数据异常,原因:" + error.Message;                  
                    myCommand.Dispose();
                    scWebshop.Close();
                   
                    return result;
                }
            }


        }

        public static List<string> getQueryList(string sql, string res,ref string msg)
        {
            List<string> resultList = new List<string> { };

            object obj = new object();
            lock (obj)
            {
                try
                {
                    OpenSqliteConnection(ref msg);
                    myCommand = new SQLiteCommand(sql, scWebshop);
                    sqlReader = myCommand.ExecuteReader();
                    while (sqlReader.Read())
                    {
                        resultList.Add(sqlReader[res].ToString());
                    }
                    myCommand.Dispose();
                    scWebshop.Close();
                    return resultList;
                }
                catch (Exception error)
                {
                    resultList.Clear();
                  
                    msg = $"查询数据异常,原因:" + error.Message;
                    myCommand.Dispose();
                    scWebshop.Close();
                    return resultList;
                }
            }


        }

        public static bool getResult(string txt,ref string msg)
        {
            bool result = false;
            try
            {
                string str = "select Enable from InterfaceManage where InterfaceName='" + txt + "' and Permissions='" + FormUserLogoin.userpermissions + "'";
                result = Convert.ToBoolean(getQuery(str, "Enable",ref msg));
                return result;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public static void InsertoProducion(string sn, string state, string workstation,ref string msg)
        {
            object intoPro = new object();
            lock (intoPro)
            {
                DateTime time = DateTime.Now;
                try
                {
                    string str = "insert into productiondata (SN,State,WorkStation,Time) value('" + sn + "','" + state + "','" + workstation + "','" + time.ToString("s") + "')";
                    int i = executeSQL(str,ref msg);
                    //if (!(i > 0))
                    //{
                    //    MainForm.m_formAlarm.InsertAlarmMessage(ERecvResult.改变数据库数据异常.ToString());
                    //}
                }
                catch (Exception error)
                {                  
                    msg = $"改变数据库数据异常,原因:" + error.Message;
                }
            }

        }
        /// <summary>
        /// 添加报警信息到数据库
        /// </summary>
        /// <param name="Alarmlevel"></param>
        /// <param name="AlarmMessage"></param>
        public static void InsertoAlarm(string Alarmlevel, string AlarmMessage,ref string msg)
        {
            object intoAlaram = new object();
            lock (intoAlaram)
            {
                DateTime time = DateTime.Now;
                try
                {
                    string str = "insert into Alarm(AlarmLevel,AlarmMessage,StartTime)values('" + Alarmlevel + "','" + AlarmMessage + "','" + time.ToString("s") + "')";
                    int i = executeSQL(str,ref  msg);
                    if (!(i > 0))
                    {
                       
                        msg = $"改变数据库数据异常";
                    }
                }
                catch (Exception error)
                {                  
                    msg = $"改变数据库数据异常,原因:" + error.Message;
                }
            }

        }
    }
   



}
