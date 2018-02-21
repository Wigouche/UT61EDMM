using System;

namespace UT61EDMM
{
    
    public enum SwitchPositions
    {
        V = ';',
        Ohms = '3',
        F = '6',
        uA = '=',
        mA = '?',
        A = '0',
        BEEP = '5',
        DIODE = '1',
        HZdutycycle = '2',
    };

    [Flags]
    public enum StatusFlags
    {
        dutyCycle = 0x08,
        negertive = 0x04,
        batteryLow = 0x02,
        Overflow = 0x01,
    };

    [Flags]
    public enum OptionsFlags
    {
        MAX = 0x08,
        MIN = 0x04,
        relitive = 0x02,
        RMR = 0x01,

        underFlow = 0x80,
        Pmax = 0x40,
        Pmin = 0x20,

        DC = 0x0800,
        AC = 0x0400,
        AutoRange = 0x0200,
        VoltAmpHz = 0x0100,

        VBAR = 0x400,
        hold = 0x2000,
        LPF = 0x1000,
    };


    public partial class UT61EMeter
    {
        public class Measurement
        {
            public readonly decimal value;
            public readonly string nonNumvalue;
            public readonly string units;
            public readonly int range;
            public readonly SwitchPositions SwitchPosition;

            public readonly StatusFlags status;
            public readonly OptionsFlags options;


            public readonly string packet;

            public Measurement(String meterPacket)
            {
                packet = meterPacket;
                if (meterPacket.Length != 12)
                    throw (new ArgumentException("incorrect format input string should be 12 char long"));

                //convert range char to number
                range = (int)char.GetNumericValue(meterPacket[0]);

                var displayDigits = meterPacket.Substring(1, 5);

                SwitchPosition = (SwitchPositions)meterPacket[6];

                status = (StatusFlags)(0x0f & meterPacket[7]);

                //colate all option flags into one enum
                options = (OptionsFlags)(0x0f & meterPacket[8]);
                options |= (OptionsFlags)((0x0f & meterPacket[9]) << 4);
                options |= (OptionsFlags)((0x0f & meterPacket[11]) << 12);
                options |= (OptionsFlags)((0x0f & meterPacket[10]) << 8);

                var measInfo = new UT61EMeter.UnitScaling(SwitchPosition, range, status, options);

                units = measInfo.units;

                if (!decimal.TryParse(displayDigits, out value))
                {
                    nonNumvalue = displayDigits;
                    value = 99999;
                }
                else
                {
                    nonNumvalue = null;
                    value *= measInfo.valueMuntiplyer;
                }
            }


        }
    }
}

