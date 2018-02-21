using System;
using System.IO.Ports;

namespace UT61EDMM
{
    public partial class UT61EMeter : IDisposable
    {
        SerialPort Port;

        public UT61EMeter(string portName)
        {
            Port = new SerialPort
            {
                PortName = portName,
                BaudRate = 19200,
                //this string is the end of line gnersted by the meter
                NewLine = "\r\n",
                DataBits = 7,
                Parity = Parity.Odd,
                // Set the read/write timeouts
                ReadTimeout = 2000,
                WriteTimeout = 2000,
            };

            Port.Open();

            Port.DtrEnable = false;
            Port.ReadExisting();
        }

       public string GetMeasurementString()
        {
            Port.DtrEnable=true;
            var inputPacket = "";
            while (inputPacket.Length < 12)
            {
                inputPacket = Port.ReadLine();
            }
            inputPacket = inputPacket.Substring(inputPacket.Length - 12);
            Port.DtrEnable = false;

            return inputPacket;
        }

        public Measurement GetMeasurement()
        {
            return new Measurement(GetMeasurementString());
        }

        public void Dispose()
        {
            Port.Close();
        }
    }
}
