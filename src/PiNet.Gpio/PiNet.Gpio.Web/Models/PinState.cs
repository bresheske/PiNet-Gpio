using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PiNet.Gpio.Web.Models
{
    public class PinState
    {
        public PinType Pin { get; set; }
        public PinStatus Status{ get; set; }
    }
}