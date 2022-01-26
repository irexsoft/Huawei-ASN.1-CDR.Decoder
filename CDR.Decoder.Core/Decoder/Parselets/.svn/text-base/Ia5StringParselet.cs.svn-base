using System;

namespace CDR.Decoder.Parselets
{
    class Ia5StringParselet : GenericParselet
    {
        private static char _non_ascii = (char)0;

        public Ia5StringParselet()
            : base()
        {
            RegisterMethod("ASCII", ValueAsString);
            DefaultValueType = "ASCII";
        }

        public static string ValueAsString(byte[] value)
        {
            string res = String.Empty;
            foreach (byte x in value)
            {
                res += ((x > 0x19) && (x < 0x7F)) ? (char)x : (_non_ascii != (char)0) ? _non_ascii : (char)0x2E;
            }
            return res.Trim();
        }
    }
}
