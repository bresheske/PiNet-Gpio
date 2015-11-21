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

            using (var manager = new PinManager())
            {
                manager.Export(PinType.GPIO18);
                manager.Write(PinType.GPIO18, false);
                Thread.Sleep(TimeSpan.FromSeconds(5));
                //manager.UnExport(PinType.GPIO18);
            }
        }
    }
}
