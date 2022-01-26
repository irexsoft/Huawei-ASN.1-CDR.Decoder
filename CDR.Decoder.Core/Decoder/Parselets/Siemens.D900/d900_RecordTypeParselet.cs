using System;
using System.Collections.Generic;

namespace CDR.Decoder.Parselets
{
    class d900_RecordTypeParselet : GenericParselet
    {
        private static Dictionary<int, string> _types = new Dictionary<int, string>(){
            {0, "singleTypeA"},
            {1, "firstTypeA"},
            {2, "intermediateTypeA"},
            {3, "lastTypeA"},
            {4, "singleTypeB"},
            {5, "firstTypeB"},
            {6, "intermediateTypeB"},
            {7, "lastTypeB"},
            {8, "undefined"}
        };

        public d900_RecordTypeParselet()
            : base()
        {
            RegisterMethod("Int", ValueAsInteger);
            RegisterMethod("Type", ValueAsTypeName);
            DefaultValueType = "Type";
        }

        public static string ValueAsInteger(byte[] value)
        {
            return IntegerParselet.ValueAsInteger(value);
        }

        public static string ValueAsTypeName(byte[] value)
        {
            if (value == null)
                return String.Empty;
            int v = IntegerParselet.ArrayToInt32(value);

            if (_types.ContainsKey(v))
            {
                return _types[v];
            }
            return _types[8];
        }
    }
}
