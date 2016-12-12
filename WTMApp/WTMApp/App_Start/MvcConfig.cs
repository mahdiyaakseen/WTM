using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using Common.Logging;

namespace WTMApp
{

    /// <summary>
    /// Configures ASP.NET MVC.
    /// </summary>
    public class MvcConfig
    {

        /// <summary>
        /// Logging.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger<MvcConfig>();


        /// <summary>
        /// Configures ASP.NET MVC.
        /// </summary>
        public static void Configure()
        {
            Log.Debug("Registering MVC areas.");
            AreaRegistration.RegisterAllAreas();

            Log.Debug("Registering global MVC filters.");
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            Log.Debug("Registering MVC routes.");
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Log.Debug("Registering script and CSS bundles.");
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

    }

}
