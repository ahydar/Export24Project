using Microsoft.Win32.TaskScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFExportSolution.Scheduler
{
    class MonthlyTask : IMonthlyTask
    {
        private string TaskName;
        private string Path;

        public MonthlyTask()
        {
            TaskName = "Export24";
            Path = String.Format("{0}\\{1}", Environment.CurrentDirectory,AppDomain.CurrentDomain.FriendlyName);
    }
        public bool Exists()
        {
            using (TaskService sched = new TaskService())
            {
                    
                var task = sched.GetTask("\\" + TaskName);
                if(task != null)
                {
                    TaskDefinition taskDefinition = task.Definition;
                    var actions = taskDefinition.Actions.ToArray();
                    var tempPath = actions[0].ToString().Trim();
                    if(task.Name.Equals(TaskName) && tempPath.Equals(Path))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            
        }

        public void CreateTask()
        {
            using (TaskService ts = new TaskService())
            {

                try
                {
                    // Create a new task definition and assign properties
                    TaskDefinition td = ts.NewTask();
                    td.RegistrationInfo.Description = "Export CSV file on the 24th of each month.";

                    // Create a trigger that will fire the task at this time every other day
                    MonthlyTrigger mt = new MonthlyTrigger();
                    if (DateTime.Now.Day <= 24)
                    {
                        mt.StartBoundary = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 24) + TimeSpan.FromHours(9);
                    }
                    else
                    {
                        mt.StartBoundary = new DateTime(DateTime.Now.Year, (DateTime.Now.Month + 1 == 13 ? 1 : DateTime.Now.Month), 24) + TimeSpan.FromHours(9); ;
                    }
                    mt.DaysOfMonth = new int[] { 24 };
                    //wt.Repetition.Duration = TimeSpan.FromMinutes(5);
                    //wt.Repetition.Interval = TimeSpan.FromMinutes(1);
                    td.Triggers.Add(mt);

                    // Create an action that will launch Notepad whenever the trigger fires
                    td.Actions.Add(new ExecAction(Path, null, null));

                    // Register the task in the root folder
                    ts.RootFolder.RegisterTaskDefinition(TaskName, td);
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
