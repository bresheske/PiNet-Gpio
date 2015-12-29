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
                        new { id = 18, status = manager.Read(PinType.GPIO18) },
                        new { id = 23, status = manager.Read(PinType.GPIO23) },
                    }
                };

                return Json(pins, JsonRequestBehavior.AllowGet);
            }
        }
    }
}