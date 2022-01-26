using System;

namespace CDR.Decoder.Parselets
{
    class d900_UssdStringParselet : GenericParselet
    {
        private static char[] _chars_to_trim = new char[] { '\0', '\r'};

        public d900_UssdStringParselet()
            : base()
        {
            RegisterMethod("ASCII", ValueAsString);
            DefaultValueType = "ASCII";
        }

        public static string ValueAsString(byte[] value)
        {
            string s = String.Empty;

            foreach (byte b in value)
            {
                s = String.Concat(Convert.ToString(b, 2).PadLeft(8, '0'), s);
            }

            int rem;
            int charsCount = Math.DivRem(s.Length, 7, out rem);
            char[] chars = new char[charsCount];
            int p = s.Length;
            for (int i = 0; i < charsCount; i++)
            {
                p = p - 7;
                chars[i] = (Char)Convert.ToSByte(s.Substring(p, 7), 2);
            }

            return new String(chars).Trim(_chars_to_trim);
        }
    }
}
