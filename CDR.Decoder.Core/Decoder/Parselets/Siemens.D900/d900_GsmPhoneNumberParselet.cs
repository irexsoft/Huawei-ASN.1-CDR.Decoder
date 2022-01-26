using System;

namespace CDR.Decoder.Parselets
{
    class d900_GsmPhoneNumberParselet : GenericParselet
    {
        /// <summary>
        /// According to 3G TS 04.08 
        /// OCTET 1 
        ///    BIT 8   = extension bit (always 0) 
        ///    BIT 7-5 = type of number 
        ///    BIT 4-1 = numbering plan 
        /// OCTET 2 
        ///    BIT 8   = extension bit (always 1) 
        ///    BIT x-y = screening indicator 
        ///    BIT u-v = presentation restriction 
        /// OCTET 3-18 
        ///    maximal 32 digits coded as TBCD-String 
        ///    and filled up with fillers 
        /// </summary>

        private static bool _check_voip = true;
        private static char _non_digit = '*';
        private static bool _single_nai = false;
        private static bool _in_2nd_byte = false;
        private static bool _octet_3a = true;
        private static char _national_code = '8';
        private static string _international_prefix = "+";

        public d900_GsmPhoneNumberParselet()
            : base()
        {
            RegisterMethod("GSMPhoneNumber", ValueAsGsmPhoneNumber);
            DefaultValueType = "GSMPhoneNumber";
        }

        public static string ValueAsGsmPhoneNumber(byte[] value)
        {
            string res = String.Empty;

            if ((value == null) || (value.Length < 2))
                return res;

            int skip = 1;
            int type = value[0] & 0xFF;
            bool ext = _in_2nd_byte || (((type & 0x80) == 0) && (!_single_nai));
            bool odd = (!(_octet_3a)) && ((type & 0x80) != 0);
            //int ext3a = (ext) ? value[1] & 0x70 : 0;

            bool international = false;
            bool national = false;

            if (!(_octet_3a))
            {
                international = (type & 0x7F) == 0x4;
                national = (type & 0x7F) == 0x3;
            }
            else
            {
                international = (type & 0x70) == 0xF;
                national = (type & 0x70) == 0x20;
            }

            if (ext) skip = 2;

            if (international)
            {
                res += _international_prefix;
            }
            else if ((national) && (_national_code != (char)0))
            {
                res += _national_code.ToString();
            }

            byte msb = 0;
            byte lsb = 0;

            for (int n = skip; n < value.Length; n++)
            {
                msb = (byte)((value[n] & 0xF0) >> 4);
                lsb = (byte)(value[n] & 0xF);

                if (lsb != 0xF)
                {
                    if (lsb > 9)
                    {
                        if ((_check_voip) && (n == skip) && (lsb == 0xB) && (msb <= 9))
                        {
                            res += "B";
                        }
                        else
                        {
                            res += ((_non_digit == (char)0) ? String.Format("{0:X}", lsb) : (lsb == 0xB) ? "*" : (lsb == 0xA) ? "#" : _non_digit.ToString());
                        }
                    }
                    else
                    {
                        res += lsb.ToString();
                    }

                    if (msb != 0xF)
                    {
                        if (msb > 9)
                        {
                            res += ((_non_digit == (char)0) ? String.Format("{0:X}", msb) : (msb == 0xB) ? "*" : (msb == 0xA) ? "#" : _non_digit.ToString());
                        }
                        else
                        {
                            res += msb.ToString();
                        }
                    }
                }
                if ((lsb == 0xF) || (msb == 0xF))
                    break;
            }

            if ((odd) && (res.Length >= 2) && (lsb == 0) && (_octet_3a))
            {
                int new_length = res.Length - 1;
                char[] dst = new char[new_length];
                res.CopyTo(0, dst, 0, new_length - 1);
                dst[new_length - 1] = res[new_length];
                res = String.Concat(dst);
            }

            return res;
        }
    }
}
