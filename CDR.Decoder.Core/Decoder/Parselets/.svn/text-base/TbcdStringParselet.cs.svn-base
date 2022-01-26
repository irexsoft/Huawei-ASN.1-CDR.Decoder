using System;

namespace CDR.Decoder.Parselets
{
    /*
     * TBCD-string: Technical Binary Coded Digits.
     * The octetstring contains the digits 0 to F, two digits per octet. 
     * Bit 4 to 1 of octet n encoding digit 2(n-1)+1, Bit 5 to 8 of octet n encoding digit 2n,
     * bit 8 being most significant bit.
     * e.g. number = 12345 stored in following order : 2143F5FFFF... ( FF... only for fixed length)
     * represent several digits from 0 through 9, *, #, a, b, c, two
     * digits per octet, each digit encoded 0000 to 1001 (0 to 9),
     * 1010 (*), 1011 (#), 1100 (a), 1101 (b) or 1110 (c); 1111 used
     * as filler when there is an odd number of digits.

     * bits 8765 of octet n encoding digit 2n
     * bits 4321 of octet n encoding digit 2(n-1) +1 which require diffrernt type of encoding .
     */

    class TbcdStringParselet : GenericParselet
    {
        public TbcdStringParselet()
            : base()
        {
            RegisterMethod("TBCD", ValueAsTBCDString);
            DefaultValueType = "TBCD";
        }

        public static string ValueAsTBCDString(byte[] value)
        {
            string res = String.Empty;

            byte msb;
            byte lsb;

            foreach (byte x in value)
            {
                msb = (byte)((x & 0xF0) >> 4);
                lsb = (byte)(x & 0xF);

                if (lsb != 0xF)
                {
                    if (lsb > 9)
                    {
                        switch (lsb)
                        {
                            case 10:
                                res += '*';
                                break;
                            case 11:
                                res += '#';
                                break;
                            case 12:
                                res += 'a';
                                break;
                            case 13:
                                res += 'b';
                                break;
                            case 14:
                                res += 'c';
                                break;
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
                            switch (msb)
                            {
                                case 10:
                                    res += '*';
                                    break;
                                case 11:
                                    res += '#';
                                    break;
                                case 12:
                                    res += 'a';
                                    break;
                                case 13:
                                    res += 'b';
                                    break;
                                case 14:
                                    res += 'c';
                                    break;
                            }
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

            return res;
        }
    }
}
