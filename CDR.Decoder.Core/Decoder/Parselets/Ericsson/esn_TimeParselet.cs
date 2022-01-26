using System;

namespace CDR.Decoder.Parselets
{
    class esn_TimeParselet : GenericParselet
    {
        public esn_TimeParselet()
            : base()
        {
            RegisterMethod("Time", ValueAsTime);
            DefaultValueType = "Time";
        }

        public static string ValueAsTime(byte[] value)
        {
            if (value.Length > 3)
            {
                DateTime time = new DateTime(1, 1, 1, value[0], value[1], value[2], value[3]);
                return time.ToString("HH:mm:ss.f");
            }
            else
            {
                DateTime time = new DateTime(1, 1, 1, value[0], value[1], value[2]);
                return time.ToString("HH:mm:ss");
            }
        }
    }
}
