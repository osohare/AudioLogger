using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;

using System.Web.Security;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;

using CommonController;
using CommonController.db;

namespace AudioLoggerWebSite
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class MediaServer : IHttpHandler
    {
        [DllImport("kernel32.dll")]
        public static extern bool GetProcessWorkingSetSize(IntPtr proc, out int min, out int max);
        [DllImport("kernel32.dll")]
        public static extern bool SetProcessWorkingSetSize(IntPtr proc, int min, int max);

        HttpContext currentContext = null;
        HttpRequest request = null;
        HttpResponse response = null;

        private int clv_signal = 0;
        private DateTime t0 = DateTime.Now;
        private DateTime t1 = DateTime.Now;
        private bool attachment = false;
        private bool livefeed = false;

        private void FlushMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            Trace.TraceInformation("MEMORY FLUSH: " + System.Diagnostics.Process.GetCurrentProcess().PagedMemorySize64);
            Trace.Flush();
        }

        protected void parseByCryptedCookie()
        {
            string data = ((FormsIdentity)currentContext.User.Identity).Ticket.UserData;
            clv_signal = int.Parse(data.Split('=')[0]);
            t0 = DateTime.FromBinary(long.Parse(data.Split('=')[1]));
            t1 = DateTime.FromBinary(long.Parse(data.Split('=')[2]));
            livefeed = bool.Parse(data.Split('=')[3]);
            attachment = bool.Parse(data.Split('=')[4]);
        }

        protected void parseByQueryString()
        {
            if (request["clv_signal"] == null)
                response.End();
            else
                clv_signal = int.Parse(request["clv_signal"]);
            if (request["t0"] == null)
                response.End();
            else
                t0 = DateTime.FromBinary(long.Parse(request["t0"]));
            if (request["t1"] == null)
                response.End();
            else
                t1 = DateTime.FromBinary(long.Parse(request["t1"]));
            if (request["livefeed"] == null)
                response.End();
            else
                livefeed = bool.Parse(request["livefeed"]);
            if (request["attachment"] == null)
                response.End();
            else
                attachment = bool.Parse(request["attachment"]);
        }

        protected void parseRequest()
        {
            if (request["AUDIOLOGGER"] == null)
                parseByQueryString();
            else
                parseByCryptedCookie();
        }

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                currentContext = context;
                request = context.Request;
                response = context.Response;

                FormsAuthenticationTicket ticket = ((FormsIdentity)context.User.Identity).Ticket;

                if (ticket.Expired || !context.User.Identity.IsAuthenticated)
                    throw new ApplicationException();

                parseRequest();

                KLogger k = Signal.GetFromDatabase(clv_signal);

                response.Clear();
                response.BufferOutput = false;
                response.ContentType = "audio/mpeg";
                response.AppendHeader("Connection", "close");

                if (attachment)
                    response.AppendHeader("Content-Disposition", "attachment; filename=\"mediastream.mp3\"");

                foreach (TimeLine t in TimeLine.getMediaContent(clv_signal, t0, t1))
                {
                    try
                    {
                        if (response.IsClientConnected)
                        {
                            response.TransmitFile(k.workingDirectory + t.Ticks.ToString() + ".mp3");
                            response.Flush();
                            FlushMemory();
                        }
                    }
                    catch {}
                }
                response.End();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}
