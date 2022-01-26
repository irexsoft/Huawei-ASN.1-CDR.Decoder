using System;

namespace CDR.Decoder.Parselets
{
    class esn_DateParselet : GenericParselet
    {
        public esn_DateParselet()
            : base()
        {
            RegisterMethod("Date", ValueAsDate);
            DefaultValueType = "Date";
        }

        public static string ValueAsDate(byte[] value)
        {
            if (value.Length == 3)
            {
                DateTime time = new DateTime(value[0], value[1], value[2]);
                return time.ToString("yy.MM.dd");
            }
            else
            {
                DateTime time = new DateTime(value[0] * 100 + value[1], value[2], value[3]);
                return time.ToString("yyyy.MM.dd");
            }
        }
    }
}
