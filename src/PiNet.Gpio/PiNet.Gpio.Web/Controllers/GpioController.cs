using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PiNet.Gpio.Web.Controllers
{
    public class GpioController : Controller
    {

        public ActionResult Index()
        {
            using (var manager = new PinManager())
            {
                var pins = new
                {
                    pins = new []
                    {
                        new { id = 18, status = manager.Read(PinType.GPIO18).ToString() },
                        new { id = 23, status = manager.Read(PinType.GPIO23).ToString() },
                    }
                };

                return Json(pins, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Toggle()
        {
            // mono sucks ass when it comes to autobinding.
            var idstr = Request.QueryString["id"];
            int id;
            if (string.IsNullOrWhiteSpace(idstr) || !int.TryParse(idstr, out id))
                return null;
            
            var manager = new PinManager();
            var status = manager.Read(PinType.GPIO23);
            var write = false;
            if (status == PinStatus.False || status == PinStatus.UnExported)
                write = true;
            if (status == PinStatus.UnExported)
                manager.Export(PinType.GPIO23);
            manager.Write(PinType.GPIO23, write);
            return Json(new { id = 23, previousstatus = status.ToString(), status = manager.Read(PinType.GPIO23).ToString() }, JsonRequestBehavior.AllowGet);
        }
    }
}