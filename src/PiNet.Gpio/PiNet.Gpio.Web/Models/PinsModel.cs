using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PiNet.Gpio.Web.Models
{
    public class PinsModel
    {
        public IEnumerable<PinState> Pins { get; set; }
    }
}