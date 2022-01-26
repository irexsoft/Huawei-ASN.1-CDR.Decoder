using System;

namespace CDR.Decoder.Parselets
{
    /// <summary>
    /// The cic is an identifier for the circuit used in #7 signalling,
    /// cicOg stands for the outgoing  circuit and  cicIc for the incoming circuit.  
    /// </summary>
    class d900_CircuitParselet : GenericParselet
    {
        public d900_CircuitParselet()
            : base()
        {
            RegisterMethod("Hex", ValueAsHEX);
            RegisterMethod("pcmUnitOfCic", ValueAsPcmUnitOfCic);
            RegisterMethod("channelOfCic", ValueAsChannelOfCic);
            DefaultValueType = "Hex";
        }

        public static string ValueAsHEX(byte[] value)
        {
            return IntegerParselet.ValueAsHex(value);
        }

        public static string ValueAsPcmUnitOfCic(byte[] value)
        {
            if ((value == null) || (value.Length != 3))
            {
                return String.Empty;
            }
            else
            {
                return String.Format("{0:X2}-{1:X2}", value[0], value[1] );
            }
        }

        public static string ValueAsChannelOfCic(byte[] value)
        {
            if ((value == null) || (value.Length != 3))
            {
                return String.Empty;
            }
            else
            {
                return String.Format("{0:X2}", value[2]);
            }
        }
    }
}
