using System;

namespace CDR.Decoder.Parselets
{
    // internal structure : YY, MM, DD
    class d900_BcdDateParselet : GenericParselet
    {
        public d900_BcdDateParselet()
            : base()
        {
            RegisterMethod("Date", ValueAsDate);
            DefaultValueType = "Date";
        }

        public static string ValueAsDate(byte[] value)
        {
            DateTime date = DateTime.ParseExact(BitConverter.ToString(value).Replace("-", String.Empty), "yyMMdd", null);
            return date.ToString("dd.MM.yyyy");
        }
    }
}
