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
                try
                {
                    System.Console.WriteLine("Press 'h' for help.");
                    while (true)
                    {
                        var command = System.Console.ReadLine();
                        var commandsplit = command.Split();
                        if (!commandsplit.Any())
                            continue;

                        var commandtype = commandsplit[0];

                        if (commandtype == "h")
                            WriteHelp();
                        else if (commandtype == "e")
                            ExportPin(manager, command);
                        else if (commandtype == "u")
                            UnExportPin(manager, command);
                        else if (commandtype == "w")
                            WritePin(manager, command);
                        else if (commandtype == "c")
                            CleanAll(manager);

                    }
                }
                catch (Exception)
                {

                }
            }
        }

        public static void WriteHelp()
        {
            System.Console.WriteLine("Available Commands: ");
            System.Console.WriteLine("  h: Shows this help.");
            System.Console.WriteLine("  e <PinID>: Exports a Pin.");
            System.Console.WriteLine("  u <PinID>: UnExports a Pin.");
            System.Console.WriteLine("  w <PinID> <1/0>: Writes a value to a Pin. 1 or 0 values allowed.");
            System.Console.WriteLine("  c: Cleans all pins for this session.");
        }

        public static void ExportPin(PinManager manager, string command)
        {
            var split = command.Split();
            if (split.Count() != 2)
                return;
            var pinnum = split[1];
            PinType pintype;
            if (!Enum.TryParse<PinType>("GPIO" + pinnum, out pintype))
                return;
            manager.Export(pintype);
        }

        public static void UnExportPin(PinManager manager, string command)
        {
            var split = command.Split();
            if (split.Count() != 2)
                return;
            var pinnum = split[1];
            PinType pintype;
            if (!Enum.TryParse<PinType>("GPIO" + pinnum, out pintype))
                return;
            manager.UnExport(pintype);
        }

        public static void WritePin(PinManager manager, string command)
        {
            var split = command.Split();
            if (split.Count() != 3)
                return;
            var pinnum = split[1];
            PinType pintype;
            if (!Enum.TryParse<PinType>("GPIO" + pinnum, out pintype))
                return;
            int val;
            if (!int.TryParse(split[2], out val))
                return;
            var bval = val == 1 ? true : false;
            manager.Write(pintype, bval);
        }

        public static void CleanAll(PinManager manager)
        {
            manager.CleanAll();
        }
    }
}
