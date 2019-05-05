using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace GISecSvc.Monitoring.Process
{
    class ProcessMonitoring
    {
        private static readonly ManagementEventWatcher ProcessStartWatch = new ManagementEventWatcher(new WqlEventQuery("SELECT * FROM Win32_ProcessStartTrace"));
        private static readonly ManagementEventWatcher ProcessStopWatch = new ManagementEventWatcher(new WqlEventQuery("SELECT * FROM Win32_ProcessStopTrace"));
        private static readonly ManagementEventWatcher ModuleLoadWatch = new ManagementEventWatcher(new WqlEventQuery("SELECT * FROM Win32_ModuleLoadTrace "));
        private static readonly ManagementEventWatcher ThreadStartWatch = new ManagementEventWatcher(new WqlEventQuery("SELECT * FROM Win32_ThreadStartTrace"));
        private static readonly ManagementEventWatcher ThreadStopWatch = new ManagementEventWatcher(new WqlEventQuery("SELECT * FROM Win32_ThreadStopTrace"));

        public static void Initialize()
        {
            ProcessStartWatch.EventArrived += startWatch_EventArrived;
            ProcessStartWatch.Start();

            ProcessStopWatch.EventArrived += stopWatch_EventArrived;
            ProcessStopWatch.Start();

            ModuleLoadWatch.EventArrived += ModuleLoad_EventArrived;
            ModuleLoadWatch.Start();

            ThreadStartWatch.EventArrived += ThreadStartWatch_EventArrived;
            ThreadStartWatch.Start();

            ThreadStopWatch.EventArrived += ThreadStopWatch_EventArrived;
            ThreadStopWatch.Start();
        }
        
        public static void TearDown()
        {
            ThreadStopWatch.Stop();
            ThreadStartWatch.Stop();
            ModuleLoadWatch.Stop();
            ProcessStopWatch.Stop();
            ProcessStartWatch.Stop();
        }

        private static void ThreadStopWatch_EventArrived(object sender, EventArrivedEventArgs e)
        {
            CloudSetup.SendMessagesAsync("Win32_ThreadStopTrace", e.NewEvent.Properties);
            Console.WriteLine("Thread Stop: {0}:{1}", e.NewEvent.Properties["ProcessID"].Value, e.NewEvent.Properties["ThreadID"].Value);
        }

        private static void ThreadStartWatch_EventArrived(object sender, EventArrivedEventArgs e)
        {
            CloudSetup.SendMessagesAsync("Win32_ThreadStartTrace", e.NewEvent.Properties);
            Console.WriteLine("Thread Start: {0}:{1}", e.NewEvent.Properties["ProcessID"].Value, e.NewEvent.Properties["ThreadID"].Value);
        }

        private static void ModuleLoad_EventArrived(object sender, EventArrivedEventArgs e)
        {
            CloudSetup.SendMessagesAsync("Win32_ModuleLoadTrace", e.NewEvent.Properties);
            Console.WriteLine("Module Load: {0}:{1}", e.NewEvent.Properties["ProcessID"].Value, e.NewEvent.Properties["FileName"].Value);
        }

        static void stopWatch_EventArrived(object sender, EventArrivedEventArgs e)
        {
            CloudSetup.SendMessagesAsync("Win32_ProcessStopTrace", e.NewEvent.Properties);
            Console.WriteLine("Process Stop: {0}:{1}", e.NewEvent.Properties["ProcessID"].Value, e.NewEvent.Properties["ProcessName"].Value);
        }

        static void startWatch_EventArrived(object sender, EventArrivedEventArgs e)
        {
            CloudSetup.SendMessagesAsync("Win32_ProcessStartTrace", e.NewEvent.Properties);
            Console.WriteLine("Process Start: {0}:{1}", e.NewEvent.Properties["ProcessID"].Value, e.NewEvent.Properties["ProcessName"].Value);
        }
    }
}
