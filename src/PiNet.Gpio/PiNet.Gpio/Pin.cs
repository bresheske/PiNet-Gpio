using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiNet.Gpio
{
    public class Pin
    {
        public enum PinType
        {
            // Left pins (3.3v DC Side)
            GPIO02,
            GPIO03,
            GPIO04,
            GPIO17,
            GPIO27,
            GPIO22,
            GPIO10,
            GPIO09,
            GPIO11,
            GPIO05,
            GPIO06,
            GPIO13,
            GPIO19,
            GPIO26,

            // Right pins (5v DC Side)
            GPIO14,
            GPIO15,
            GPIO18,
            GPIO23,
            GPIO24,
            GPIO25,
            GPIO08,
            GPIO07,
            GPIO12,
            GPIO16,
            GPIO20,
            GPIO21
        };

        public PinType Type { get; set; }

        public Pin(PinType type)
        {
            Type = type;
        }
    }
}
