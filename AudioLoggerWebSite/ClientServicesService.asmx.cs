using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

using System.Collections.Generic;
using System.Web.Security;

using CommonController;
using CommonController.db;

namespace AudioLoggerWebSite
{
    /// <summary>
    /// Summary description for ClientServicesService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class ClientServicesService : System.Web.Services.WebService
    {
        public ClientServicesService()
        {
            //if (!LoggerSecurity.checkSecurityContext(RequestSoapContext.Current, "user"))
            //    throw new ApplicationException();
        }

        [WebMethod]
        public KLogger[] getSignals()
        {
            KLoggerCollection k = new KLoggerCollection();
            k.LoadLoggersFromDataBase();
            KLogger[] loggers = k.ToArray();
            return loggers;
        }

        [WebMethod]
        public TimeLine[] getTimeline(int clv_signal, DateTime t0, DateTime t1)
        {
            List<TimeLine> timelinesList = TimeLine.getMediaContent(clv_signal, t0, t1);
            TimeLine[] timelines = timelinesList.ToArray();
            return timelines;
        }

        [WebMethod]
        public string getCryptedCookie(int clv_signal, long t0, long t1, bool livefeed, bool attachment)
        {
            FormsAuthenticationTicket ticket = 
                new FormsAuthenticationTicket(1, User.Identity.Name, 
                    DateTime.Now, DateTime.Now.AddMinutes (5), false,
                    clv_signal.ToString() + "=" + t0.ToString() + "=" + t1.ToString() + "=" + livefeed.ToString() + "=" + attachment.ToString());
            return FormsAuthentication.Encrypt(ticket);
        }
    }
}
