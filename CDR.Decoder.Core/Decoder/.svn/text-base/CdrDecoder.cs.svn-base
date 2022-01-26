using System.IO;
using CDR.Decoder.BER;
using CDR.Decoder.Parselets;
using CDR.Schema;

namespace CDR.Decoder
{
    public partial class CdrDecoder : BerDecoder
    {
        private CdrTlvParser _tlvParser;
        private byte[] _cdrSignatures;

        public CdrDecoder()
        {
            _tlvParser = new CdrTlvParser(CdrDefinitionProvider.Instance, ParseletProvider.Instance);
            if (_tlvParser != null)
            {
                _cdrSignatures = _tlvParser.DefinitionProvider.AviableSignatures();
            }
        }

        public CdrDefinitionProvider ElementDefinitionProvider { get { return _tlvParser.DefinitionProvider; } }

        public CdrElement DecodeRecord(Stream asnStream, bool searchSignature)
        {
            TlvObject tlv;
            long offset = asnStream.Position;

            if (searchSignature && (_cdrSignatures != null))
            {
                int b = asnStream.ReadByte();
                bool res = false;
                foreach(byte sgn in _cdrSignatures)
                {
                    if (b == sgn)
                    {
                        res = true;
                        break;
                    }
                }
                if (!res)
                {
                    return null;
                }
                asnStream.Seek(-1, SeekOrigin.Current);
            }

            BerDecoderResult pr = DecodeTlv(asnStream, out tlv, ref offset, 0, byte.MaxValue);

            CdrElement record = null;

            if ((pr == BerDecoderResult.Finished) && (_tlvParser != null))
            {

                record = _tlvParser.ParseTlvObject(tlv); ;
            }

            return record;
        }
    }
}
