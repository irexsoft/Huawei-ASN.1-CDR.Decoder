using System;
using System.Collections.Generic;
using System.Reflection;

namespace CDR.Decoder.Parselets
{
    public class ParseletProvider
    {
        private static ParseletProvider _provInstance;
        private Dictionary<string, IParselet> _parseletTable;

        private ParseletProvider()
        {
            _parseletTable = new Dictionary<string, IParselet>();
            this.RegisterAssemblyParselets(Assembly.GetExecutingAssembly());
        }

        public static ParseletProvider Instance
        {
            get
            {
                if (_provInstance == null)
                {
                    _provInstance = new ParseletProvider();
                }
                return _provInstance;
            }
        }

        protected void RegisterAssemblyParselets(Assembly asm)
        {
            IParselet parselet;

            foreach (Type t in asm.GetTypes())
            {
                if (t.GetInterface(typeof(IParselet).Name) != null)
                {
                    parselet = (asm.CreateInstance(t.FullName, true) as IParselet);
                    this.RegisterParselet(parselet);
                }
            }
        }

        public void RegisterParselet(IParselet parselet)
        {
            if (!_parseletTable.ContainsKey(parselet.ParseletName))
            {
                _parseletTable.Add(parselet.ParseletName, parselet);
            }
        }

        public IParselet FindParselet(string parseletName)
        {
            return _parseletTable.ContainsKey(parseletName) ? _parseletTable[parseletName] : null;
        }
    }
}
