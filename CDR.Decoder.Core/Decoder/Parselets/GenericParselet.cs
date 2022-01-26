using System;
using System.Collections.Generic;

namespace CDR.Decoder.Parselets
{
    class GenericParselet : IParselet
    {
        protected delegate string ParseletMethod(byte[] value);

        private Dictionary<string, ParseletMethod> _methods;
        private string _defaultValueType;

        public GenericParselet()
        {
            _methods = new Dictionary<string, ParseletMethod>(1);
            _methods.Add("RAW", ValueAsRAW);
            _defaultValueType = "RAW";
        }

        protected void RegisterMethod(string valueName, ParseletMethod parseletMethod)
        {
            if (!_methods.ContainsKey(valueName))
            {
                _methods.Add(valueName, parseletMethod);
            }
        }

        public static string ValueAsRAW(byte[] value)
        {
            return BitConverter.ToString(value).Replace("-", String.Empty);
        }

        #region IParselet Members

        public string[] GetValueTypes()
        {
            string[] keys = new string[_methods.Keys.Count];
            _methods.Keys.CopyTo(keys, 0);
            return keys;
        }

        public string DefaultValue(CdrElement element)
        {
            return this.Value(element.DefaultValueType, element);
        }

        public string Value(string valueType,CdrElement element)
        {
            if (element.IsConstructed || element.IsEmpty)
            {
                return String.Empty;
            }
            else
            {
                return _methods.ContainsKey(valueType) ? _methods[valueType](element.Value as byte[]) : String.Empty;
            }
        }

        public string ParseletName
        {
            get { return this.GetType().Name; }
        }

        public string DefaultValueType
        {
            get { return _defaultValueType; }
            set
            {
                if (_methods.ContainsKey(value))
                {
                    _defaultValueType = value;
                }
            }
        }

        #endregion
    }
}
