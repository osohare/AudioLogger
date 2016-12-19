using AudioLogger.Recorder.CommandLineClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AudioLogger.Recorder.WindowsService
{
    partial class RecorderService : ServiceBase
    {
        Task backgroundTask = null;
        CommandLineProcessor commandLineProcessor;

        public RecorderService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
#if DEBUG
            System.Diagnostics.Debugger.Launch();
#endif
            commandLineProcessor = new CommandLineProcessor();
            backgroundTask = Task.Factory.StartNew(() => {
                var options = WindowsServiceUtilities.GetOptionsFromFile();
                commandLineProcessor.EntryPoint(args, overrideOptions: options);
            }, TaskCreationOptions.LongRunning);
        }

        protected override void OnStop()
        {
            commandLineProcessor.QuitEvent.Set();
            try
            {
                backgroundTask.Wait();
            }
            catch (Exception)
            {

            }
        }
    }
}
