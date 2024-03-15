using AutomationTesting_CorConnect.applicationContext;
using Microsoft.Win32.TaskScheduler;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Helper
{
    internal class JobHelper
    {
        public static void ExecuteJob(string jobName,string server)
        {
            string user = ApplicationContext.RemoteUser;
            string domain = ApplicationContext.RemoteDomain;
            string password = ApplicationContext.RemotePassword;
            using (TaskService tasksrvc = new TaskService(@"\\" + server + "", "" + user + "", "" + domain + "", "" + password + ""))
            {
                try
                {
                    Microsoft.Win32.TaskScheduler.Task task = tasksrvc.FindTask(jobName);
                    task.Run();
                    while (tasksrvc.GetRunningTasks(true).FirstOrDefault(t => t.Name == jobName) != null)
                    {
                        System.Threading.Thread.Sleep(TimeSpan.FromSeconds(20));
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        public static DateTime? GetJobLastRunTime(string jobName, string server)
        {
            string user = ApplicationContext.RemoteUser;
            string domain = ApplicationContext.RemoteDomain;
            string password = ApplicationContext.RemotePassword;
            bool taskStatus = true;
            Microsoft.Win32.TaskScheduler.Task task = null;
            DateTime? jobStartDateTime = null;
            using (TaskService tasksrvc = new TaskService(@"\\" + server + "", "" + user + "", "" + domain + "", "" + password + ""))
            {
                try
                {
                    task = tasksrvc.FindTask(jobName);
                    taskStatus = task.Enabled;
                    if (!task.Enabled) 
                    {
                        task.Enabled = true;
                    }

                    task.Run();
                    while (task.State == TaskState.Running)
                    {
                        System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
                    }
                    jobStartDateTime = task.LastRunTime;

                                
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while executing the job. Last Run Time is not available.");
                }
                finally
                {
                    //Reverting into original position
                    if (!taskStatus && task!=null)
                    {
                        task.Enabled = false;
                    }
                }
                return jobStartDateTime;
            }
        }
    }
}
