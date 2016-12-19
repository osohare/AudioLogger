using AudioLogger.Recorder.Utilities;
using AudioLogger.Recorder.WindowsService;
using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AudioLogger.Recorder.CommandLineClasses
{
    /// <summary>
    /// All related tasks to handling this as invoked from command line
    /// </summary>
    internal class CommandLineProcessor
    {
        /// <summary>
        /// EventWaitHandle to keep program running as property
        /// </summary>
        internal ManualResetEvent QuitEvent { get { return quitEvent; } }
        /// <summary>
        /// Based on the token passed to program determine if hosting environment is a service or console
        /// </summary>
        internal bool IsWindowsService { get { return options.AsService; } }

        private ManualResetEvent quitEvent = new ManualResetEvent(false);
        private Options options = null;  //-i 20 -r 44100 -c 2
        private RecordingUtilities recordingUtilities = null;

        /// <summary>
        /// Main entry point for processing routines
        /// </summary>
        /// <param name="args"></param>
        /// <param name="overrideOptions"></param>
        internal void EntryPoint(string[] args, Options overrideOptions = null)
        {
            //If options are not provided from out of environment parse from command line, otherwise means this is coming from config file
            if (overrideOptions == null)
            {
                var parserResult = Parser.Default.ParseArguments<Options>(args);
                if (parserResult.Errors.Any() || !CommandLineUtilities.ValidateParameters(parserResult.Value))
                {
                    //If error on commandline args or invalid boundaries exit gracefully
                    Environment.Exit(1);
                }
                options = parserResult.Value;
            }

            //All operations below work only if this is not a windows service
            if (!IsWindowsService)
            {
                if (options.List)
                {
                    //List the availabl sources according to the WinAudio API from NAudio
                    CommandLineUtilities.PrintAudioSources();
                    Environment.Exit(0);
                }

                if (options.MakePermanent)
                {
                    if (!WindowsServiceUtilities.InstallService(options))
                        Environment.Exit(-1);
                    else
                        Environment.Exit(0);
                }

                if (options.Remove)
                {
                    if (!WindowsServiceUtilities.UninstallService())
                        Environment.Exit(-1);
                    else
                        Environment.Exit(0);
                }
            }

            //What to do in case of termination of this process
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);

            //Termination of process from console Ctrl+C
            Console.CancelKeyPress += (sender, eArgs) => {
                quitEvent.Set();
                eArgs.Cancel = true;
            };

            //Start the recording session
            recordingUtilities = new RecordingUtilities(options);
            if (options.DeviceId.Equals(-1))
            {
                //For loopback recording
                recordingUtilities.RecordLoopback();
            }
            else
            {
                //Regular recording
                recordingUtilities.Record();
            }

            //Need to keep the process alive recording until a Ctrl+C, abortion or NamedPipe message terminates the process
            quitEvent.WaitOne();
        }

        /// <summary>
        /// Stop recording in case of process termination
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            try
            {
                recordingUtilities.CurrentRecordingDevice.StopRecording();
            }
            catch (Exception)
            {

            }
        }
    }
}
