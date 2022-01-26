using System.IO;

namespace CDR.Decoder.BER   // Basic Encoding Rules
{

    public enum BerDecoderResult
    {
        Failed = 0,
        Finished = 1,
        EOF = 2
    }

    public class BerDecoder
    {
        // Fetch out BER-encoded data until EOF or error
        protected BerDecoderResult DecodeTlv(Stream asnStream, out TlvObject tlvObj, ref long offset, byte level, byte maxLevel)
        {
            BerDecoderResult procResult = BerDecoderResult.Finished;
            tlvObj = null;

            byte[] tlvBuffer = new byte[32];
            byte tlvBuffer_Length = 0;
            int octet;

            int tlvTag = 0;
            int tlvTag_Length = 0;

            // Decode the TLV tag
            while (tlvTag_Length == 0)
            {
                // Get the next byte from the input stream
                octet = asnStream.ReadByte();
                if (octet == -1)
                {
                    return BerDecoderResult.EOF;
                }

                // Skip the fillers between billing records (TLV with level == 0)
                if ((level == 0) && ((octet == 0xFF) || (octet == 0)))
                {
                    offset++;
                    continue;
                }

                tlvBuffer[tlvBuffer_Length++] = (byte)octet;
                tlvTag_Length = FetchTlvTag(tlvBuffer, tlvBuffer_Length, ref tlvTag);
                switch (tlvTag_Length)
                {
                    case -1:
                        return BerDecoderResult.Failed;
                    case 0:
                        continue;   // More data expected
                }
            }

            long tlvOffset = offset;
            offset += tlvTag_Length;

            bool _constructed = ((tlvBuffer[0] & 0x20) == 0x20);

            int tlvLength = 0;
            int tlvLength_Length = 0;

            // Decode the TLV length
            while (tlvLength_Length == 0)
            {
                // Get the next byte from the input stream
                octet = asnStream.ReadByte();
                if (octet == -1)
                {
                    return BerDecoderResult.EOF;
                }

                tlvBuffer[tlvBuffer_Length++] = (byte)octet;
                tlvLength_Length = FetchTlvLength(_constructed, tlvBuffer, tlvTag_Length, tlvBuffer_Length - tlvTag_Length, ref tlvLength);
                switch (tlvLength_Length)
                {
                    case -1:
                        return BerDecoderResult.Failed;
                    case 0:
                        continue;   // More data expected
                }
            }
            offset += tlvLength_Length;
            tlvObj = new TlvObject(tlvTag >> 2, (AsnTagClass)(tlvBuffer[0] & 0xC0), _constructed, tlvOffset);

            if (tlvLength > 0)
            {
                if (_constructed)
                {
                    if (level < maxLevel)
                    {
                        long _vLength = offset + tlvLength;
                        BerDecoderResult _pr;
                        while (offset < _vLength)
                        {
                            TlvObject _childObj;
                            _pr = this.DecodeTlv(asnStream, out _childObj, ref offset, (byte)(level + 1), maxLevel);
                            if (_pr != BerDecoderResult.Finished)
                            {
                                break;
                            }
                            tlvObj.AddTlv(_childObj);
                        }
                    }
                    else
                    {
                        // Stop processing for TLV with level > maxLevel, and skip content.
                        offset += tlvLength;
                        asnStream.Seek(tlvLength, SeekOrigin.Current);
                    }
                }
                else
                {
                    int _readOffset = tlvObj.ReadValue(asnStream, tlvLength);
                    offset += _readOffset;
                    if (_readOffset != tlvLength)
                    {
                        return BerDecoderResult.Failed;
                    }
                }
            }
            return procResult;
        }

        private static int FetchTlvTag(byte[] tlvBuffer, int bufLength, ref int tlvTag)
        {
            if (bufLength == 0)
                return 0;

            int val;
            int tclass;

            val = tlvBuffer[0];
            tclass = (val >> 6);

            if ((val &= 0x1F) != 0x1F)
            {
                // Simple form: everything encoded in a single octet.
                // Tag Class is encoded using two least significant bits.
                tlvTag = (val << 2) | tclass;
                return 1;
            }

            // Each octet contains 7 bits of useful information.
            // The MSB is 0 if it is the last octet of the tag.
            int skipped;
            int bpos;
            for (val = 0, bpos = 1, skipped = 2; skipped <= bufLength; bpos++, skipped++)
            {
                byte _oct = tlvBuffer[bpos];
                if ((_oct & 0x80) > 0)
                {
                    val = (val << 7) | (_oct & 0x7F);

                    // Make sure there are at least 9 bits spare at the MS side of a value.
                    if ((val >> ((8 * sizeof(int)) - 9)) > 0)
                    {
                        // We would not be able to accomodate any more tag bits.
                        return -1;
                    }
                }
                else
                {
                    val = (val << 7) | _oct;
                    tlvTag = (val << 2) | tclass;
                    return skipped;
                }
            }

            return 0;	// Want more data ...
        }

        private static int FetchTlvLength(bool isConstructed, byte[] tlvBuffer, int bufStart, int bufLength, ref int tlvLength)
        {
            if (bufLength == 0)
                return 0;   // Want more ...

            byte oct;
            int len;
            int skipped;

            oct = tlvBuffer[bufStart];
            if ((oct & 0x80) == 0)
            {
                // Short definite length.
                tlvLength = oct;
                return 1;
            }
            else
            {
                if (isConstructed && (oct == 0x80))
                {
                    tlvLength = -1;	// Indefinite length
                    return 1;
                }

                if (oct == 0xFF)
                {
                    // Reserved in standard for future use.
                    return -1;
                }

                oct &= 0x7F;	// Leave only the 7 LS bits

                int _bpos;

                for (len = 0, _bpos = bufStart + 1, skipped = 1; (oct > 0) && (++skipped <= bufLength); _bpos++, oct--)
                {

                    len = (len << 8) | tlvBuffer[_bpos];
                    if ((len < 0) || ((len >> ((8 * sizeof(int)) - 8) > 0) && (oct > 1)))
                    {
                        return -1; // Too large length value.
                    }
                }
            }

            if (oct == 0)
            {
                int _lenplusepsilon = len + 1024;
                /*
                 * Here length may be very close or equal to 2G.
                 * However, the arithmetics used in some decoders
                 * may add some (small) quantities to the length,
                 * to check the resulting value against some limits.
                 * This may result in integer wrap-around, which
                 * we try to avoid by checking it earlier here.
                 */
                if (_lenplusepsilon < 0)
                {
                    return -1; // Too large length value
                }

                tlvLength = len;
                return skipped;
            }

            return 0;   // Want more
        }
    }
}
