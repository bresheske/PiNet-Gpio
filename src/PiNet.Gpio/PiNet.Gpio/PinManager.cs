using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiNet.Gpio
{
    public class PinManager : IDisposable
    {
        private string GPIO_FOLDER = "/sys/class/gpio/";

        private readonly IList<PinType> _activepins;

        public PinManager()
        {
            _activepins = new List<PinType>();
        }

        public void Export(PinType pin)
        {
            var folder = string.Format("{0}export", GPIO_FOLDER);
            Execute(folder, pin.ToString().Substring(4));
            _activepins.Add(pin);
        }

        public void UnExport(PinType pin)
        {
            var folder = string.Format("{0}unexport", GPIO_FOLDER);
            Execute(folder, pin.ToString().Substring(4));
            _activepins.Remove(pin);
        }

        public void Write(PinType pin, bool on)
        {
            var val = on ? 1 : 0;
            var folder = string.Format("{0}gpio{1}/direction", GPIO_FOLDER, pin.ToString().Substring(4));
            Execute(folder, "out");
            folder = string.Format("{0}gpio{1}/value", GPIO_FOLDER, pin.ToString().Substring(4));
            Execute(folder, val.ToString());
        }

        public PinStatus Read(PinType pin)
        {
            var folder = string.Format("{0}gpio{1}/value", GPIO_FOLDER, pin.ToString().Substring(4));
            PinStatus response;
            try
            {
                var data = File.ReadAllText(folder).Trim();
                response = data == "0"
                    ? PinStatus.False
                    : PinStatus.True;
            }
            catch
            {
                response = PinStatus.UnExported;
            }

            return response;
        }

        public void CleanAll()
        {
            var allpins = Enum.GetValues(typeof(PinType));
            foreach (var pin in allpins)
                UnExport(((PinType)pin));
        }

        private void Execute(string folder, string text)
        {
            var proc = new Process();
            proc.EnableRaisingEvents = false;
            proc.StartInfo.FileName = "/bin/bash";
            proc.StartInfo.Arguments = $"-c \"echo {text} > {folder}\"";
            proc.StartInfo.UseShellExecute = true;
            Console.WriteLine($"Executing: {proc.StartInfo.FileName} {proc.StartInfo.Arguments}");
            proc.Start();
            proc.WaitForExit();
        }

        public void Dispose()
        {
            // Copy the pins to another collection.
            var pins = new PinType[_activepins.Count];
            _activepins.CopyTo(pins, 0);

            // Unexport all of the current used pins.
            foreach (var pin in pins)
                UnExport(pin);
            
            // At this point, _activepins is empty.
        }
    }
}
