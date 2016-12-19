using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.ServiceProcess;
using AudioLogger.Recorder.WindowsService;
using AudioLogger.Recorder.CommandLineClasses;
using System.Diagnostics;

namespace AudioLogger.Recorder
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.ToList().Contains(WindowsServiceUtilities.RUN_AS_SERVICE_TOKEN))
            {
#if DEBUG
                Debugger.Launch();
#endif
                WindowsServiceUtilities.RunService();
            }
            else
            {
                var commandLineProcessor = new CommandLineProcessor();
                commandLineProcessor.EntryPoint(args);
            }
        }
    }
}
