using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;

using System.Runtime.InteropServices;
using System.Threading;

using CommonController;
using CommonController.db;
using mp3_stream;

namespace RecordingService
{
    public partial class RecordingService : ServiceBase
    {

        [DllImport("kernel32.dll")]
        public static extern bool GetProcessWorkingSetSize(IntPtr proc, out int min, out int max);
        [DllImport("kernel32.dll")]
        public static extern bool SetProcessWorkingSetSize(IntPtr proc, int min, int max);

        private KLoggerCollection loggers = new KLoggerCollection();

        public RecordingService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                Trace.TraceInformation("LOADING LOGGERS");
                loggers.LoadLoggersFromDataBase();
                foreach (KeyValuePair<long, KLogger> kvp in loggers)
                {
                    kvp.Value.OnChunkWritten += new KLogger.ChunkWritterHandler(OnChunkWritten);
                }
                Trace.TraceInformation("STARTING LOGGERS");
                loggers.StartAll();
                Trace.Flush();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                Trace.TraceError(ex.StackTrace);
                Trace.Flush();
            }
        }

        protected override void OnStop()
        {
            try
            {
                loggers.StopAll();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                Trace.TraceError(ex.StackTrace);
                Trace.Flush();
            }
        }

        protected override void OnCustomCommand(int command)
        {
            if (command == ServiceCommands.ReadPendingMessages.GetHashCode())
            {
                foreach (Message m in Message.getPendingMessages())
                {
                    LoggerCustomCommands cmd = m.Command;
                    cmd.Execute(loggers[(int)cmd.stationID]);
                    Message.Update(m.ID);
                }
            }
        }

        private void OnChunkWritten(KLogger k, String filename)
        {
            try
            {
                TaskInfo t = new TaskInfo(k, filename);
                t.onTaskFinish += new TaskInfoHandler(onTaskFinish);
                ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadProc), t);
                Trace.TraceInformation("INDEXING: Device -> " + k.deviceName + " / Line -> " + k.lineName + " / File -> " + filename);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                Trace.TraceError(ex.StackTrace);
                Trace.Flush();
            }
        }

#region " Memory recycling by timer "
        private void FlushMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            Trace.TraceInformation("MEMORY FLUSH: " + System.Diagnostics.Process.GetCurrentProcess().PagedMemorySize64);
            Trace.Flush();
        }

        private void tmrRecycle_Tick(object sender, EventArgs e)
        {
            FlushMemory();
        }
#endregion

#region " Insert to db by threadpool "
        void ThreadProc(Object stateInfo)
        {
            TaskInfo i = (TaskInfo)stateInfo;
            i.ExecuteTaskInfo();
        }

        public void onTaskFinish(object source)
        {
            TaskInfo i = (TaskInfo)source;
            Trace.TraceInformation("INDEXED: Device -> " + i.k.deviceName + " / Line -> " + i.k.lineName + " / File -> " + i.filename);
        }

        public delegate void TaskInfoHandler(object source);
        public class TaskInfo
        {
            public event TaskInfoHandler onTaskFinish;
            public KLogger k;
            public string filename;
            public TaskInfo(KLogger k, String filename)
            {
                this.k = k;
                this.filename = filename;
            }

            public void ExecuteTaskInfo()
            {
                TimeLine.Insert(k, filename);
                if (onTaskFinish != null) onTaskFinish(this);
            }
        }
#endregion

    }
}
