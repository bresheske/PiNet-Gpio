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

        public ActionResult Toggle(int id)
        {
            var manager = new PinManager();
            var status = manager.Read(PinType.GPIO21);
            var write = false;
            if (status == PinStatus.False || status == PinStatus.UnExported)
                write = true;
            manager.Write(PinType.GPIO21, write);
            return Json(new { id = 21, status = manager.Read(PinType.GPIO23).ToString() }, JsonRequestBehavior.AllowGet);
        }
    }
}