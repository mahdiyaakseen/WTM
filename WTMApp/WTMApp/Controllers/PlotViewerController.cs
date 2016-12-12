using Common.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using WTMApp.Models;

namespace WTMApp.Controllers
{
    [RoutePrefix("plotviewer")]
    [Authorize]
    public class PlotViewerController : Controller
    {
        /// <summary>
        /// Logging.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger<DataViewerController>();


        /// <summary>
        /// Displays the main data viewer view.
        /// </summary>
        /// <returns>
        /// The main data viewer view.
        /// </returns>
        [HttpGet]
        [Route("")]
        public ActionResult Index()
        {
            try
            {
                var model = XMLParseModel();
                return View(model);
            }
            catch (Exception e)
            {
                
                Log.Error($"An error occurred in {nameof(PlotViewerController)}.{nameof(Index)}.", e);
                throw; // 500
            }
        }


        public PlotMonitoringTypeViewModel XMLParseModel()
        {
            var file = Path.Combine(Server.MapPath("~/app_data"), "XMLFile1.xml");
            var model = new PlotMonitoringTypeViewModel
            {
                MonitoringTypes =
                    from type in XDocument.Load(file).Document.Descendants("Type")
                    select new SelectListItem
                    {
                        Value = type.Attribute("ID").Value,
                        Text = type.Attribute("Name").Value
                    }
            };
            return model;
        }

    }
}
