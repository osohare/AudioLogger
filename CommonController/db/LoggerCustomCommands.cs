using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace CommonController.db
{
    public enum ServiceCommands : int
    {
        ReadPendingMessages = 128
    }

    public enum LoggerCommands
    {
        StartLogger,
        StopLogger
    }

    public class LoggerCustomCommands
    {
        public long stationID;
        public LoggerCommands command;

        public string Serialize()
        {
            StringBuilder bld = new StringBuilder();
            using (XmlWriter xmlWr = XmlWriter.Create(bld))
            {
                XmlSerializer sr = new XmlSerializer(typeof(LoggerCustomCommands));
                sr.Serialize(xmlWr, this);
                return bld.ToString();
            } 
        }

        public static LoggerCustomCommands Deserialize(string encodedForm)
        {
            using (TextReader rd = new StringReader(encodedForm))
            {
                XmlSerializer sr = new XmlSerializer(typeof(LoggerCustomCommands));
                return (LoggerCustomCommands)sr.Deserialize(rd);
            }  
        }

        public void Execute(KLogger k)
        {
            Signal.RefreshFromDatabase(k);
            switch (command)
            {
                case LoggerCommands.StartLogger:
                    k.Start();
                    k.enabled = true;
                    Signal.Update(k);
                    break;
                case LoggerCommands.StopLogger:
                    k.Stop();
                    k.enabled = false;
                    Signal.Update(k);
                    break;
            }
        }
    }
}
