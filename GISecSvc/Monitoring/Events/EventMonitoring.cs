
using System;
using System.Collections;
using System.Diagnostics;
using System.Threading;

namespace GISecSvc.Monitoring.Events
{

    static class EventMonitoring
    {
        private static EventLog _logWatcher;
        
        private static void EventLogCallback(object sender, EntryWrittenEventArgs e)
        {
            CloudSetup.SendMessagesAsync("EventLog", e.Entry);
            Console.WriteLine("Event - Source:{0}, ID:{1}", e.Entry.Source, e.Entry.InstanceId);
        }

        /// <summary>
        /// Subscribe to event log for updates 
        /// </summary>
        /// <param name="logName">System, Application, Security, Setup Events</param>
        public static void SubscribeEvents(string logName)
        {
            _logWatcher = new EventLog() {Log = logName};
            _logWatcher.EntryWritten += new EntryWrittenEventHandler(EventLogCallback);
            _logWatcher.EnableRaisingEvents = true;
        }

        public static void Teardown()
        {
            _logWatcher.EnableRaisingEvents = false;
            _logWatcher.EntryWritten -= EventLogCallback;
        }
    }
}