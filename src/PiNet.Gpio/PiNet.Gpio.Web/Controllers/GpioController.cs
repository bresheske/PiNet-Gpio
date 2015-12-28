using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace PiNet.Gpio.Web.Controllers
{
    public class GpioController : ApiController
    {

        public IHttpActionResult Get()
        {
            return Ok(new { something = "foreveryone" });
        }
    }
}
