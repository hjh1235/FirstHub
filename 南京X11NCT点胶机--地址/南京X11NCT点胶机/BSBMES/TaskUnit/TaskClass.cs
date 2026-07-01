using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace UpperComputer.TaskUnit
{
    public class TaskClass
    {
        public Action<string> action;
        public static List<TaskClass> taskThreadList = new List<TaskClass>();

        /// <summary>
        /// 流程0计时器
        /// </summary>
        public Stopwatch sw0 = new Stopwatch();
        /// <summary>
        /// 工位0拍照流程计时器
        /// </summary>
        public Stopwatch sw1 = new Stopwatch();
        /// <summary>
        /// 工位1拍照流程计时器
        /// </summary>
        public Stopwatch sw2 = new Stopwatch();
        public static bool plcConnState = false;
        public TaskClass()
        {
            taskThreadList.Add(this);
            action += iniPlc;
        }

        public virtual void TaskRun()
        {

        }

        public void start()
        {
            //Thread taskThread = new Thread(TaskRun);
            //taskThread.Start();
            //taskThread.Name = this.GetType().ToString();

            ThreadPool.QueueUserWorkItem((x) => { TaskRun(); });
        }

        public virtual void iniPlc(string str)
        {

        }
    }
}