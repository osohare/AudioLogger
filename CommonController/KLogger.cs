using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Xml;
using System.Xml.Serialization;

using mp3_stream;

namespace CommonController
{
    public class KLogger
    {
        public long stationID;
        public string station;
        public string deviceName;
        public string lineName;
        public BitRate bitRate;
        public SampleRate sampleRate;
        public uint volume;
        public int recycleInterval;
        public string workingDirectory;
        public bool enabled;

        private mp3Helper helper;

        public delegate void ChunkWritterHandler(KLogger source, String fileName);
        public event ChunkWritterHandler OnChunkWritten;

        public KLogger()
        {
            helper = new mp3Helper();
        }

        public void Serialize(string fileName)
        {
            XmlSerializer xs = new XmlSerializer(typeof(KLogger));
            TextWriter tw = new StreamWriter(fileName);
            xs.Serialize(tw, this);
            tw.Close();
        }

        public static KLogger Deserialize(string fileName)
        {
            KLogger obj = new KLogger();
            XmlSerializer xs = new XmlSerializer(typeof(KLogger));
            FileStream fs = new FileStream(fileName, FileMode.Open);
            XmlReader xr = new XmlTextReader(fs);
            obj = (KLogger)xs.Deserialize(xr);
            xr.Close();
            fs.Close();
            return obj;
        }

        public void Start()
        {
            helper.OnRecycle += new mp3Helper.RecycleHandler(OnRecycle);
            helper.OnBufferWritten += new mp3Helper.BufferWrittenHandler(OnBufferWritten);
            helper.RecycleInterval = recycleInterval;
            helper.WorkingDirectory = workingDirectory;
            helper.StartRecording(deviceName, lineName, volume, bitRate, sampleRate);
        }

        public void Stop()
        {
            helper.StopRecording();
            helper.OnRecycle -= new mp3Helper.RecycleHandler(OnRecycle);
            helper.OnBufferWritten -= new mp3Helper.BufferWrittenHandler(OnBufferWritten);
        }

        private void OnRecycle(String filename)
        {
            if (OnChunkWritten != null)
                OnChunkWritten(this, filename);
        }

        private void OnBufferWritten(uint size)
        {
            //Dummy event no function
        }

        public bool isRecording
        {
            get { return helper.IsRecording; }
            set
            {
                if (value != helper.IsRecording)
                 throw new NotSupportedException();
            }
        }

        public override string ToString()
        {
            return stationID + " " + station;
        }

        public static string[] getDevices()
        {
            return mp3Helper.getWaveINDevices().ToArray();
        }

        public static string[] getLines(string deviceName)
        {
            return mp3Helper.getLines(deviceName).ToArray();
        }

    }
}
