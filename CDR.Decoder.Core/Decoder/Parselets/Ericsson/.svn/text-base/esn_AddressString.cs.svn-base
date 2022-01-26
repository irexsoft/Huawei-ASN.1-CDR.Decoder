using System;

namespace CDR.Decoder.Parselets
{
    /*
>        bit 8: 1  (no extension)
> 
>        bits 765: nature of address indicator(TON)
>               000  unknown
>               001  international number
>               010  national significant number
>               011  network specific number
>               100  subscriber number
>               101  reserved
>               110  abbreviated number
>               111  reserved for extension
> 
>       bits 4321: numbering plan indicator (NPI)
>               0000  unknown
>               0001  ISDN/Telephony Numbering Plan (Rec CCITT E.164)
>               0010  spare
>               0011  data numbering plan (CCITT Rec X.121)
>               0100  telex numbering plan (CCITT Rec F.69)
>               0101  spare
>               0110  land mobile numbering plan (CCITT Rec E.212)
>               0111  spare
>               1000  national numbering plan
>               1001  private numbering plan
>               1111  reserved for extension
> 
>               all other values are reserved.
    */
    class esn_AddressString : GenericParselet
    {
        private static string _international_prefix = "+";
        private static char _national_code = '8';

        public esn_AddressString()
            : base()
        {
            RegisterMethod("Address", ValueAsAddress);
            DefaultValueType = "Address";
        }

        public static string ValueAsAddress(byte[] value)
        {
            byte[] digits = new byte[value.Length - 1];
            Array.Copy(value, 1, digits, 0, digits.Length);

            string addr = String.Empty;
            if ((value[0] & 0x10) == 0x10)
            {
                addr = _international_prefix;
            }
            addr += TbcdStringParselet.ValueAsTBCDString(digits);
            return addr;
        }
    }
}
