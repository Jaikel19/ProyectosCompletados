using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.Controls
{
    public class CtrlCardModel : CtrlBaseModel
    {
        //Content
        public string Title { get; set; }
        public string Amount { get; set; }
        public string SubAmount { get; set; }
        public string SubTitle { get; set; }
        public string DropDown { get; set; }
        public string Icon { get; set; }

        public CtrlCardModel()
        {
            ViewName = "";
        }
    }
}
