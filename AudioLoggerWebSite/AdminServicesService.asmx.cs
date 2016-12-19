using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

using System.ServiceProcess;

using CommonController;
using CommonController.db;

namespace AudioLoggerWebSite
{
    /// <summary>
    /// Summary description for AdminServicesService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class AdminServicesService : System.Web.Services.WebService
    {
        public AdminServicesService()
        {
            //if (!LoggerSecurity.checkSecurityContext(RequestSoapContext.Current, "admin"))
            //    throw new ApplicationException();
        }

        [WebMethod]
        public string[] getDevices()
        {
            return KLogger.getDevices();
        }

        [WebMethod]
        public string[] getLines(string deviceName)
        {
            return KLogger.getLines(deviceName);
        }

        [WebMethod]
        public bool newConfig(KLogger k)
        {
            return Signal.Insert(k);
        }

        [WebMethod]
        public KLogger[] getConfig()
        {
            KLoggerCollection k = new KLoggerCollection();
            k.LoadLoggersFromDataBase();
            KLogger[] loggers = k.ToArray();
            return loggers;
        }

        [WebMethod]
        public bool setConfig(KLogger k)
        {
            return Signal.Update(k);
        }

        [WebMethod]
        public bool startLogger(int stationID)
        {
            LoggerCustomCommands cmd = new LoggerCustomCommands();
            cmd.stationID = stationID;
            cmd.command = LoggerCommands.StartLogger;

            Message.Insert(cmd.Serialize(), null);

            ServiceController sc = new ServiceController("RecordingService");
            sc.ExecuteCommand(ServiceCommands.ReadPendingMessages.GetHashCode());
            return true;
        }

        [WebMethod]
        public bool stopLogger(int stationID)
        {
            LoggerCustomCommands cmd = new LoggerCustomCommands();
            cmd.stationID = stationID;
            cmd.command = LoggerCommands.StopLogger;

            Message.Insert(cmd.Serialize(), null);

            ServiceController sc = new ServiceController("RecordingService");
            sc.ExecuteCommand(ServiceCommands.ReadPendingMessages.GetHashCode());
            return true;
        }

        [WebMethod]
        public bool startService()
        {
            ServiceController sc = new ServiceController("RecordingService");
            sc.Start();
            return true;
        }

        [WebMethod]
        public bool stopService()
        {
            ServiceController sc = new ServiceController("RecordingService");
            sc.Stop();
            return true;
        }

        [WebMethod]
        public ServiceControllerStatus statusService()
        {
            ServiceController sc = new ServiceController("RecordingService");
            return sc.Status;
        }
    }
}
