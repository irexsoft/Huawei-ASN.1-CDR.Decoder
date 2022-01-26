using System;
using System.Globalization;

namespace CDR.Decoder.Parselets
{
    class sgsn_TimeParselet : GenericParselet
    {
        public sgsn_TimeParselet()
            : base()
        {
            RegisterMethod("Time", ValueAsTime);
            DefaultValueType = "Time";
        }

        public static string ValueAsTime(byte[] value)
        {
            if (value.Length == 9)
            {
                string v = BitConverter.ToString(value, 0, 6).Replace("-", String.Empty);
                DateTime time = DateTime.ParseExact(v, "yyMMddHHmmss", CultureInfo.InvariantCulture);
                return time.ToString("yyyy-MM-dd HH:mm:ss") + (char)value[6] + BitConverter.ToString(value, 7).Replace('-', ':');
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
