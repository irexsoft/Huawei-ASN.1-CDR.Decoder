using System;

namespace CDR.Decoder.Parselets
{
    public interface IParselet
    {
        string[] GetValueTypes();
        string DefaultValue(CdrElement element);
        string Value(string valueType, CdrElement element);
        string ParseletName { get; }
        string DefaultValueType { get; }
    }
}
