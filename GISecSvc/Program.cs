using System;
using System.Collections;
using System.Threading;
using GISecSvc.Monitoring.Events;
using System.Diagnostics;
using GISecSvc.Monitoring.Process;

namespace GISecSvc
{
    // 1: File Upload
    // 2: DDNA Analysis
    // 3: Process Lifecycle
    // 4: Network Connections
    // 5: Event Monitoring
    // 6: Registry Monitoring
    class Program
    {
        static void Main(string[] args)
        {
            CloudSetup.Initialize();

            EventLog[] eventLogs = EventLog.GetEventLogs(".");
            foreach (EventLog evt in eventLogs)
            {
                EventMonitoring.SubscribeEvents(evt.Log);
            }

            ProcessMonitoring.Initialize();
            
            //Wait for KeyPress to teardown
            Console.ReadLine();

            EventMonitoring.Teardown();
            ProcessMonitoring.TearDown();

        }
    }
}
