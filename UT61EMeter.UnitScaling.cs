using System;

namespace UT61EDMM
{

    public partial class UT61EMeter
    {
        class UnitScaling
        {
            public readonly string units;
            public readonly decimal valueMuntiplyer = 1;

            public UnitScaling(SwitchPositions switchPosition, int range, StatusFlags status, OptionsFlags options)
            {
                //todo could add lots of data validation here to check settings match behavior but beyond the scope of intial version

                switch (switchPosition)
                {
                    case SwitchPositions.V:
                        if (options.HasFlag(OptionsFlags.VoltAmpHz))
                        {
                            if (status.HasFlag(StatusFlags.dutyCycle))
                            {
                                units = "% V";
                                //todo
                            }
                            else
                            {
                                units = "Hz V";
                                //todo
                            }
                        }
                        else
                        {
                            if (range == 4)
                            {

                                units = "mV";
                                valueMuntiplyer = 0.01M;

                            }
                            else
                            {
                                units = "V";
                                valueMuntiplyer = 1 / (decimal)Math.Pow(10, 4 - range);
                            }
                            if (status.HasFlag(StatusFlags.negertive))
                                valueMuntiplyer *= -1;
                            if (options.HasFlag(OptionsFlags.AC))
                            {
                                units += " AC";
                            }
                        }
                        break;

                    case SwitchPositions.Ohms:
                        units = "Ohms";
                        break;

                    case SwitchPositions.F:
                        units = "F";
                        //todo
                        break;


                    case SwitchPositions.uA:
                        if (options.HasFlag(OptionsFlags.VoltAmpHz))
                        {
                            //todo
                            if (status.HasFlag(StatusFlags.dutyCycle))
                            {
                                units = "% uA";
                                //todo
                            }
                            else
                            {
                                units = "Hz uA";
                                //todo
                            }
                        }
                        else
                        {
                            units = "uA";
                            valueMuntiplyer = 1 / (decimal)Math.Pow(10, 2 - range);
                            if (status.HasFlag(StatusFlags.negertive))
                                valueMuntiplyer *= -1;
                            if (options.HasFlag(OptionsFlags.AC))
                            {
                                units += " AC";
                            }
                        }
                        break;

                    case SwitchPositions.mA:
                        if (options.HasFlag(OptionsFlags.VoltAmpHz))
                        {
                            //todo
                            if (status.HasFlag(StatusFlags.dutyCycle))
                            {
                                units = "% mA";
                                //todo
                            }
                            else
                            {
                                units = "Hz mA";
                                //todo
                            }
                        }
                        else
                        {
                            units = "mA";
                            valueMuntiplyer = 1 / (decimal)Math.Pow(10, 3 - range);
                            if (status.HasFlag(StatusFlags.negertive))
                                valueMuntiplyer *= -1;
                            if (options.HasFlag(OptionsFlags.AC))
                            {
                                units += " AC";
                            }
                        }
                        break;

                    case SwitchPositions.A:
                        if (options.HasFlag(OptionsFlags.VoltAmpHz))
                        {
                            
                            if (status.HasFlag(StatusFlags.dutyCycle))
                            {
                                units = "% A";
                                //todo
                            }
                            else
                            {
                                units = "Hz A";
                                //todo
                            }
                        }
                        else
                        {
                            units = "A";
                            valueMuntiplyer = 0.001M;
                            if (status.HasFlag(StatusFlags.negertive))
                                valueMuntiplyer *= -1;
                            if (options.HasFlag(OptionsFlags.AC))
                            {
                                units += " AC";
                            }
                        }
                        break;

                    case SwitchPositions.BEEP:
                        units = "BEEP";
                        //todo - future task not important
                        break;

                    case SwitchPositions.DIODE:
                        units = "DIODE";
                        //todo - future task not important
                        break;

                    case SwitchPositions.HZdutycycle:
                        if (status.HasFlag(StatusFlags.dutyCycle))
                        {
                            units = "%";
                            //todo
                        }
                        else
                        {
                            units = "Hz";
                            //todo
                        }
                        //todo

                        break;
                    default:
                        throw (new ArgumentException("switch positon given is invaid"));
                }
            }
        }
    }
}
