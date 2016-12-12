using System;
using System.Configuration;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;

using Common.Logging;

using Microsoft.Owin;
using Microsoft.Owin.Logging;

using Owin;

[assembly: OwinStartup(typeof(WTMApp.Startup))]

namespace WTMApp
{
    /// <summary>
    /// OWIN startup class.
    /// </summary>
    public partial class Startup
    {

        /// <summary>
        /// Logging.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger<Startup>();


        /// <summary>
        /// Gets a flag that specifies if the application is running in App Store mode.
        /// </summary>
        internal static bool AppStoreMode
        {
            get { return Convert.ToBoolean(ConfigurationManager.AppSettings["application:useAppStore"]); }
        }


        /// <summary>
        /// Configures the OWIN application.
        /// </summary>
        /// <param name="app">The OWIN application.</param>
        public void Configuration(IAppBuilder app)
        {
            var asm = Assembly.GetExecutingAssembly();
            Log.Info($"{Environment.NewLine}{Environment.NewLine}[{asm.GetCustomAttribute<AssemblyProductAttribute>().Product} {asm.GetName().Version}]{Environment.NewLine}{asm.GetCustomAttribute<AssemblyCopyrightAttribute>().Copyright}{Environment.NewLine}");
            Log.Info("Starting application.");

            // Configure OWIN to use Common.Logging.
            app.SetLoggerFactory(new WTMApp.Logging.CommonOwinLoggerFactory());

            Log.Info("Configuring Data Core connection settings.");
            // Middleware that will add Data Core connection settings to the OWIN environment for each call.
            app.UseDataCoreConnectionSettings();

            if (AppStoreMode)
            {
                Log.Info("Configuring authentication.");
                ConfigureAppStoreAuthentication(app);
            }

            Log.Info("Configuring ASP.NET MVC.");
            MvcConfig.Configure();

            Log.Info("Configuring Web API.");
            WebApiConfig.Configure(app);

            Log.Info("Configuring SignalR");
            SignalRConfig.Configure(app);
        }

    }
}
