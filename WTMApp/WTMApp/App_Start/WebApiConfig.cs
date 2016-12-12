﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

using Common.Logging;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using Owin;

namespace WTMApp
{
    /// <summary>
    /// Configures Web API.
    /// </summary>
    public class WebApiConfig
    {

        /// <summary>
        /// Logging.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger<WebApiConfig>();


        /// <summary>
        /// Registers Web API with the specified OWIN application.
        /// </summary>
        /// <param name="app">The OWIN application.</param>
        public static void Configure(IAppBuilder app)
        {
            var httpConfig = new HttpConfiguration();

            Log.Debug("Enabling response compression.");
            // Add GZip/Deflate encoding support to Web API.
            httpConfig.MessageHandlers.Add(new WTMApp.Http.ContentEncodingHandler());

            Log.Debug("Configuring media type formatters.");

            // Remove the XML formatter - we'll use JSON only.
            httpConfig.Formatters.Remove(httpConfig.Formatters.XmlFormatter);

            // JSON formatting options.
            httpConfig.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            httpConfig.Formatters.JsonFormatter.SerializerSettings.Formatting = Formatting.Indented;
            httpConfig.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter());

            Log.Debug("Registering Web API routes.");

            // Use attribute routing for Web API controllers.
            httpConfig.MapHttpAttributeRoutes();

            // Add Web API to the OWIN application.
            app.UseWebApi(httpConfig);
        }

    }
}
