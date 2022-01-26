using System.Collections.Generic;
using CDR.Decoder.BER;
using CDR.Decoder.Parselets;
using CDR.Schema;

namespace CDR.Decoder
{
    class CdrTlvParser
    {
        private CdrDefinitionProvider _definitionProvider;
        private ParseletProvider _parseletProv;

        public CdrTlvParser(CdrDefinitionProvider definitionProvider, ParseletProvider parseletProv)
        {
            _definitionProvider = definitionProvider;
            _parseletProv = parseletProv;
        }

        public CdrDefinitionProvider DefinitionProvider { get { return _definitionProvider; } }
        public ParseletProvider ParseletProvider { get { return _parseletProv; } }

        private void ParseElement(CdrElement element)
        {
            ICdrElementDefinition elementDef = DefinitionProvider.FindDefinition(element.Path);
            if (elementDef != null)
            {
                if (elementDef.Name.Length != 0)
                {
                    element.Name = elementDef.Name;
                }

                if (elementDef.Parselet.Length > 0)
                {
                    IParselet parselet = ParseletProvider.FindParselet(elementDef.Parselet);
                    if (parselet != null)
                    {
                        element.Parselet = parselet;
                        element.DefaultValueType = elementDef.ValueType;
                    }
                }
            }

            if (element.Parselet == null && !element.IsConstructed)
            {
                element.Parselet = ParseletProvider.Instance.FindParselet("GenericParselet");
            }

            if ((element.IsConstructed) && (!element.IsEmpty))
            {
                foreach (CdrElement e in (element.Value as List<CdrElement>))
                {
                    this.ParseElement(e);
                }
            }
        }

        public CdrElement ParseTlvObject(TlvObject tlv)
        {
            CdrElement element = CdrElement.CreateFromTlv(tlv);

            this.ParseElement(element);

            return element;
        }
    }
}
