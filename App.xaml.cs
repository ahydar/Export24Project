using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WPFExportSolution.Scheduler;

namespace WPFExportSolution
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            MonthlyTask task = new MonthlyTask();
            
            if (!task.Exists())
            {
                task.CreateTask();
            }
            base.OnStartup(e);
        }
    }
}
