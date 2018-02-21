using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using UT61EDMM;

namespace UT61EExample
{
    class Program
    {
        static void Main(string[] args)
        {
            string comPortName = SetPortName();
            var Meter = new UT61EMeter(comPortName);
            var measurmentStr = Meter.GetMeasurementString();
             Console.WriteLine(measurmentStr);
            var measurment = Meter.GetMeasurement();
            Console.WriteLine("{0:###0.0###} {1}", measurment.value, measurment.units);
            Console.WriteLine(measurment.range +", " + measurment.SwitchPosition.ToString("G") + ", " + measurment.status.ToString("G") + ", "+ measurment.options.ToString("G"));
            var continueLoop = true;
            while (continueLoop)
            {
                if (Console.KeyAvailable)
                {
                    var pressedKey = Console.ReadKey().Key;
                    if (pressedKey == ConsoleKey.Q)
                    {
                        continueLoop = false;
                    }
                    else if (pressedKey == ConsoleKey.Enter)
                    {
                        measurment = Meter.GetMeasurement();
                        Console.WriteLine("{0:###0.0###} {1}", measurment.value, measurment.units);
                        Console.WriteLine(measurment.range + ", " + measurment.SwitchPosition.ToString("G") + ", " + measurment.status + "| " + measurment.options);
                    }
                }
            }
            Meter.Dispose();
        }

        public static string SetPortName()
        {

            List<String> AvalablePortNames = new List<String>();
            string portName = null;

            Console.WriteLine("Available Ports:");
            foreach (string s in SerialPort.GetPortNames())
            {
                AvalablePortNames.Add(s);
            }
            for (int i = 0; i < AvalablePortNames.Count; i++)
            {
                Console.WriteLine("{0}. {1}", i, AvalablePortNames[i]);
            }

            Console.WriteLine("Enter COM port index value from list(Default: 0. {0}): ", AvalablePortNames[0]);
            while (portName == null)
            {
                string portToSelectInput = Console.ReadLine();

                if (portToSelectInput == "")
                {
                    portName = AvalablePortNames[0];
                }
                else
                {

                    if (int.TryParse(portToSelectInput, out int portToSelect))
                    {
                        if (portToSelect < AvalablePortNames.Count && portToSelect >= 0)
                        {
                            portName = AvalablePortNames[portToSelect];
                        }
                        else
                        {
                            Console.WriteLine("{0} is not a valid index from the list", portToSelect);
                        }
                    }
                    else
                    {
                        Console.WriteLine("{0} is not a valid index from the list", portToSelectInput);
                    }
                }
            }
            return portName;
        }
    }
}
