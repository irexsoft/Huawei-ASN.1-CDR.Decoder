using System;

namespace CDR.Decoder.Parselets
{
    class sgsn_iPBinAddress : IntegerParselet
    {
        public sgsn_iPBinAddress()
            : base()
        {
            RegisterMethod("IP", ValueAsIP);
            DefaultValueType = "IP";
        }

        public static string ValueAsIP(byte[] value)
        {
            if (value.Length == 4)
            {
                return string.Format("{0}.{1}.{2}.{3}", value[0], value[1], value[2], value[3]);
            }
            else if (value.Length == 6)
            {
                return string.Format("{0}.{1}.{2}.{3}.{4}.{5}", value[0], value[1], value[2], value[3], value[4], value[5]);
            }
            return string.Empty;
        }
    }
}
