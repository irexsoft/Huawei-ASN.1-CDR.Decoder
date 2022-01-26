using System;

namespace CDR.Decoder.Parselets
{
    class d900_BcdTimeParselet : GenericParselet
    {
        public d900_BcdTimeParselet()
            : base()
        {
            RegisterMethod("Time", ValueAsTime);
            DefaultValueType = "Time";
        }

        public static string ValueAsTime(byte[] value)
        {
            DateTime time = DateTime.ParseExact(BitConverter.ToString(value).Replace("-", String.Empty), "HHmmss", null);
            return time.ToString("HH:mm:ss");
        }
    }
}
