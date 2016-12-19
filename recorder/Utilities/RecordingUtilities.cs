using AudioLogger.Recorder.CommandLineClasses;
using NAudio.Lame;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AudioLogger.Recorder.Utilities
{
    /// <summary>
    /// Handles all NAudio related tasks
    /// </summary>
    internal class RecordingUtilities
    {
        private Options options;
        private WaveFormat waveFormat;
        private WaveInEvent waveIn;
        private WasapiLoopbackCapture loopback;

        /// <summary>
        /// Returns the current IWaveIn device either physical or loopback
        /// </summary>
        internal IWaveIn CurrentRecordingDevice
        {
            get { return waveIn == null ? (IWaveIn)loopback : (IWaveIn)waveIn; }
        }

        internal RecordingUtilities(Options options)
        {
            this.options = options;

            if (!string.IsNullOrWhiteSpace(options.WorkingDirectory) 
                && !Directory.Exists(options.WorkingDirectory))
            {
                throw new DirectoryNotFoundException(string.Format("The directory specified {0} does not exist", options.WorkingDirectory));
            }
        }

        /// <summary>
        /// Start a recording session with the options specified on audio HW
        /// </summary>
        /// <returns>Same instance for fluent syntaxis</returns>
        internal RecordingUtilities Record()
        {
            if (loopback != null)
                throw new InvalidOperationException();

            waveFormat = new WaveFormat(options.SampleRate, options.Channels);

            //TODO: Set volume before recording

            waveIn = new WaveInEvent();
            waveIn.DeviceNumber = options.DeviceId;
            waveIn.WaveFormat = waveFormat;
            waveIn.BufferMilliseconds = options.BufferSeconds * 1000;
            waveIn.DataAvailable += new EventHandler<WaveInEventArgs>(DataAvailable);
            //waveIn.RecordingStopped += RecordingStopped;
            waveIn.StartRecording();

            return this;
        }

        /// <summary>
        /// Start a recording session with th default options on the loopback device
        /// </summary>
        /// <returns>Same instance for fluent syntaxis</returns>
        internal RecordingUtilities RecordLoopback()
        {
            if (waveIn != null)
                throw new InvalidOperationException();

            waveFormat = new WaveFormat(options.SampleRate, options.Channels);

            loopback = new WasapiLoopbackCapture();
            loopback.DataAvailable += new EventHandler<WaveInEventArgs>(DataAvailable);
            //loopback.RecordingStopped += RecordingStopped;
            loopback.StartRecording();

            return this;
        }

        /// <summary>
        /// Send buffer to disk 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Data contaning the buffer values</param>
        private void DataAvailable(object sender, WaveInEventArgs e)
        {
#if DEBUG
            Console.WriteLine("Bytes recorded: {0}", e.BytesRecorded);
#endif
            string fileName = string.Format("{0}.mp3", DateTime.Now.Ticks);

            if (options.WorkingDirectory != null)
            {
                fileName = Path.Combine(options.WorkingDirectory, fileName);
            }

            var writter = new LameMP3FileWriter(fileName, waveFormat, (int)RecordingBitRate.BITRATE_128);

            //Create a new task/thread separately to save this buffer to disk, avoid thread blockage at all cost
            Task.Factory.StartNew(()=> {

                writter.WriteAsync(e.Buffer, 0, e.BytesRecorded)
                .ContinueWith(x => {
                    writter.Flush();
                    writter.Dispose();
                    writter = null;
#if DEBUG
                Console.WriteLine("File written: {0}", fileName);
#endif
                //TODO: report back to the cloud server that this service is running, index the fileChunk, upload to cloud storage

                });
            }, TaskCreationOptions.LongRunning);
        }

    }
}
