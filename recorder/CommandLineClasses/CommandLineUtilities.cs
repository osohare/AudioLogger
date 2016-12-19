using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioLogger.Recorder.CommandLineClasses
{
    /// <summary>
    /// List audio sources, print out the list to console, validate boundaries of the options via command line, etc.
    /// </summary>
    internal static class CommandLineUtilities
    {
        /// <summary>
        /// Get the IList<WaveInCapabilities> of available recording sources on this system
        /// </summary>
        /// <returns>Available WinAudio sources for recording</returns>
        internal static IList<WaveInCapabilities> GetAudioSources()
        {
            IList<WaveInCapabilities> devicesInfo = new List<WaveInCapabilities>();
            int waveInDevices = WaveIn.DeviceCount;
            for (int waveInDevice = 0; waveInDevice < waveInDevices; waveInDevice++)
            {
                WaveInCapabilities deviceInfo = WaveIn.GetCapabilities(waveInDevice);
                devicesInfo.Add(deviceInfo);
            }
            return devicesInfo;
        }

        /// <summary>
        /// PrintAudioSources from GetAudioSources()
        /// </summary>
        internal static void PrintAudioSources()
        {
            int i = 0;
            foreach (var device in GetAudioSources())
            {
                Console.WriteLine("Device {0}: {1}", i++, device.ProductName);
            }
        }

        /// <summary>
        /// Validate boundaries from options passed via commandline
        /// </summary>
        /// <param name="options">Command line options</param>
        /// <returns>If the options are valid or not, also prints out to console warnings about out of boundaries parameters</returns>
        internal static bool ValidateParameters(Options options)
        {
            bool validParameters = true;

            if (options.DeviceId > WaveIn.DeviceCount-1 || options.DeviceId < -1)
            {
                validParameters = false;
                Console.WriteLine("The device ID {0} is not valid on this system", options.DeviceId);
            }

            if (options.BufferSeconds < 1)
            {
                validParameters = false;
                Console.WriteLine("The buffer cannot be less than 1 second");
            }

            if (options.BufferSeconds > 60)
            {
                validParameters = false;
                Console.WriteLine("The buffer cannot be more than 60 seconds");
            }

            if (!Enum.IsDefined(typeof(RecordingChannels), options.Channels))
            {
                validParameters = false;
                Console.WriteLine("The number of channels {0} is not valid on this system", options.Channels);
            }

            if (!Enum.IsDefined(typeof(RecordingSampleRate), options.SampleRate))
            {
                validParameters = false;
                Console.WriteLine("The sampling rate {0} is not valid on this system", options.SampleRate);
            }

            return validParameters;
        }
    }
}
