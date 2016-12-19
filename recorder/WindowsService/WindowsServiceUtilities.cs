using AudioLogger.Recorder.CommandLineClasses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration.Install;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Permissions;
using System.Security.Principal;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace AudioLogger.Recorder.WindowsService
{
    /// <summary>
    /// Handle all related tasks if the service is invoked via windows service
    /// </summary>
    internal static class WindowsServiceUtilities
    {
        public const string RUN_AS_SERVICE_TOKEN = "-asservice";
        public const string CONFIG_FILE = "configuration.json";

        /// <summary>
        /// Self installer class to put as service
        /// </summary>
        /// <param name="args"></param>
        internal static bool InstallService(Options options)
        {
            if (!IsAdmin())
            {
                Console.WriteLine("The requested action requires Administrator permissions");
                return false;
            }

            List<string> argsList = new List<string>();
            options.AsService = true;
            var serializedOptions = JsonConvert.SerializeObject(options);
            File.WriteAllText(CONFIG_FILE, serializedOptions);
            //TODO: pass parameters into installer
            ManagedInstallerClass.InstallHelper(new string[] { string.Format("{0} {1}", Assembly.GetExecutingAssembly().Location, RUN_AS_SERVICE_TOKEN) });

            return true;
        }

        /// <summary>
        /// Read config file and return options, if no file exists proceed with defaults
        /// </summary>
        /// <returns></returns>
        internal static Options GetOptionsFromFile()
        {
            try
            {
                return JsonConvert.DeserializeObject<Options>(File.ReadAllText(CONFIG_FILE));
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Remove this assembly as service
        /// </summary>
        internal static bool UninstallService()
        {
            if (!IsAdmin())
            {
                Console.WriteLine("The requested action requires Administrator permissions");
                return false;
            }

            ManagedInstallerClass.InstallHelper(new string[] { "/u", Assembly.GetExecutingAssembly().Location });

            return true;
        }

        /// <summary>
        /// Determine if the current user is elevated to admin permissions from UAC
        /// </summary>
        /// <returns></returns>
        internal static bool IsAdmin()
        {
            try
            {
                AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
                PrincipalPermission principalPerm = new PrincipalPermission(null, "Administrators");
                principalPerm.Demand();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Run the service
        /// </summary>
        internal static void RunService()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] { new RecorderService() };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
