using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioLogger.Recorder.CommandLineClasses
{
    /// <summary>
    /// Options supported by the comand line
    /// </summary>
    internal class Options
    {
        /// <summary>
        /// ID for the recording device of this instance (-1 for loopback device)
        /// </summary>
        [Option('i', "id", HelpText = "ID for the recording device of this instance (-1 for loopback device)", Required = false, DefaultValue = 0)]
        public int DeviceId { get; set; }

        /// <summary>
        /// Sampling rate for recording
        /// </summary>
        [Option('r', "rate", HelpText = "Sampling rate for recording", Required = false, DefaultValue = RecordingSampleRate.SAMPLE_RATE_11025)]
        public int SampleRate { get; set; }

        /// <summary>
        /// Number of recording channels
        /// </summary>
        [Option('c', "channels", HelpText = "Number of recording channels", Required = false, DefaultValue = RecordingChannels.CHANNELS_MONO)]
        public int Channels { get; set; }

        /// <summary>
        /// Number of seconds for buffering
        /// </summary>
        [Option('b', "buffer", HelpText = "Number of seconds for buffering", Required = false, DefaultValue = 10)]
        public int BufferSeconds { get; set; }

        /// <summary>
        /// Working directory to drop compressed files
        /// </summary>
        [Option('w', "workingDir", HelpText = "Working directory to drop compressed files", Required = false, DefaultValue = "")]
        public string WorkingDirectory { get; set; }

        //TODO: Need to be able to specify bitrate

        /// <summary>
        /// "Make this recorder permanent installing as a windows service
        /// </summary>
        [Option('p', "permanent", HelpText = "Make this recorder permanent installing as a windows service", Required = false)]
        public bool MakePermanent { get; set; }

        /// <summary>
        /// Remove this recorder as windows service
        /// </summary>
        [Option("remove", HelpText = "Remove this recorder as windows service", Required = false)]
        public bool Remove { get; set; }

        /// <summary>
        /// List available devices for recording
        /// </summary>
        [Option('l', "list", HelpText = "List available devices for recording", Required = false)]
        public bool List { get; set; }

        /// <summary>
        /// The program was invoked from a windows service container
        /// </summary>
        public bool AsService { get; set; }
    }
}
