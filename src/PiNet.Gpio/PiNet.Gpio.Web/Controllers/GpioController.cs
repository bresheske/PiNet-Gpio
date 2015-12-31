using PiNet.Gpio.Web.Models;
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
                var pins = new PinsModel()
                {
                    Pins = new List<PinState>()
                    {
                        new PinState() { Pin = PinType.GPIO18, Status = manager.Read(PinType.GPIO18) },
                        new PinState() { Pin = PinType.GPIO23, Status = manager.Read(PinType.GPIO23) },
                    }
                };
                
                return Json(pins);
            }
        }

        public ActionResult Toggle()
        {
            // mono sucks ass when it comes to autobinding.
            var idstr = Request.QueryString["id"];
            int id;
            if (string.IsNullOrWhiteSpace(idstr) || !int.TryParse(idstr, out id))
                return null;

            PinType pin;
            idstr = id.ToString("00");
            if (!Enum.TryParse<PinType>($"GPIO{idstr}", out pin))
                return null;

            var manager = new PinManager();
            var status = manager.Read(pin);
            var write = false;
            if (status == PinStatus.False || status == PinStatus.UnExported)
                write = true;
            if (status == PinStatus.UnExported)
                manager.Export(pin);
            manager.Write(pin, write);

            return null;
        }
    }
}