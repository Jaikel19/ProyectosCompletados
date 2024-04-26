using System.Collections.Generic;

namespace WebApp.Models.Controls
{
    public class CtrlChartModel : CtrlBaseModel
    {
        public string Title { get; set; }
        public string XAxis { get; set; }
        public string YAxis { get; set; }
        public string Series { get; set; }

        public CtrlChartModel()
        {
            ViewName = "";
        }
    }

}
