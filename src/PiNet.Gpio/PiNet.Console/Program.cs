using PiNet.Gpio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PiNet.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var manager = new PinManager();
            manager.Export(Pin.PinType.GPIO18);
            manager.Write(Pin.PinType.GPIO18, false);
            Thread.Sleep(TimeSpan.FromSeconds(5));
            manager.UnExport(Pin.PinType.GPIO18);

        }
    }
}
