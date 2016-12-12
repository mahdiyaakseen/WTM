using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WTMApp.Models
{
    public class PlotMonitoringTypeViewModel
    {
        public string SelectedID { get; set; }
        public IEnumerable<SelectListItem> MonitoringTypes { get; set; }
    }
}