using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiNet.Gpio
{
    public class PinManager : IDisposable
    {
        private string GPIO_FOLDER = "/sys/class/gpio/";

        private readonly IList<Pin.PinType> _activepins;

        public PinManager()
        {
            _activepins = new List<Pin.PinType>();
        }

        public void Export(Pin.PinType pin)
        {
            if (_activepins.Contains(pin))
                return;
            var command = string.Format("{0} > {1}export", pin.ToString().Substring(4), GPIO_FOLDER);
            Execute(command);
            _activepins.Add(pin);
        }

        public void UnExport(Pin.PinType pin)
        {
            if (!_activepins.Contains(pin))
                return;
            var command = string.Format("{0} > {1}unexport", pin.ToString().Substring(4), GPIO_FOLDER);
            Execute(command);
            _activepins.Remove(pin);
        }

        public void Write(Pin.PinType pin, bool on)
        {
            if (!_activepins.Contains(pin))
                return;
            var val = on ? 1 : 0;
            var command = string.Format("\"out\" > {0}gpio{1}/direction", GPIO_FOLDER, pin.ToString().Substring(4));
            Execute(command);
            command = string.Format("{0} > {1}gpio{2}/value", val, GPIO_FOLDER, pin.ToString().Substring(4));
            Execute(command);
        }

        public bool Read(Pin.PinType pin)
        {
            return false;
        }

        private void Execute(string command)
        {
            var proc = new System.Diagnostics.Process();
            proc.EnableRaisingEvents = false;
            proc.StartInfo.FileName = "echo";
            proc.StartInfo.Arguments = command;

            var full = proc.StartInfo.FileName + " " + proc.StartInfo.Arguments;
            Console.WriteLine("Executing: " + full);
            
            proc.Start();
            proc.WaitForExit();

            Console.WriteLine("Current LS:");
            var ls = new System.Diagnostics.Process();
            ls.EnableRaisingEvents = true;
            ls.StartInfo.FileName = "ls";
            ls.StartInfo.Arguments = "/sys/class/gpio";
            ls.Start();
            ls.WaitForExit();

        }





        public void Dispose()
        {
            foreach(var pin in _activepins)
            {
                UnExport(pin);
            }
        }
    }
}
