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
            return Json(new { something = "foreveryone" }, JsonRequestBehavior.AllowGet);
        }
    }
}