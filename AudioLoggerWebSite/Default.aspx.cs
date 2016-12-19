using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace AudioLoggerWebSite
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string applicationPhysicalPath = @"C:\Documents and Settings\Hector Armando\My Documents\Visual Studio 2005\Projects\audio\AudioLoggerWebSite\";
            string applicationUrl = @"/AudioLoggerWebSite";
            goToAdminPage(applicationPhysicalPath, applicationUrl);
        }

        protected void goToAdminPage(string applicationPhysicalPath, string applicationUrl)
        {
            Response.Redirect(@"http://localhost/asp.netwebadminfiles/default.aspx?applicationPhysicalPath=" + applicationPhysicalPath + "&applicationUrl=" + applicationUrl);
        }
    }
}
