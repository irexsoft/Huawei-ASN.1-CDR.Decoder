using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CDR.Decoder.BER;
using CDR.Decoder.Parselets;

namespace CDR.Decoder
{
    public class CdrElement
    {
        private long _offset;
        private string _path = String.Empty;
        private string _name = String.Empty;
        private bool _isConstructed;
        private object _value;
        private IParselet _parselet;
        private string _defValueType = String.Empty;

        public long Offset { get { return _offset; } }
        public string Path { get { return _path; } }
        public string Name { get { return _name; } set { _name = value; } }
        public bool IsConstructed { get { return _isConstructed; } }
        public object Value { get { return _value; } }

        public IParselet Parselet
        {
            get { return _parselet; }
            set
            {
                _parselet = value;
                _defValueType = String.Empty;
            }
        }

        public string DefaultValueType
        {
            get
            {
                if ((_defValueType != null) && (_defValueType.Length > 0))
                {
                    return _defValueType;
                }
                else
                {
                    return (_parselet != null) ? _parselet.DefaultValueType : String.Empty;
                }
            }
            set { _defValueType = value; }
        }

        public CdrElement()
        {
        }

        public static CdrElement CreateFromTlv(TlvObject tlv)
        {
            CdrElement element = new CdrElement();
            element._offset = tlv.Offset;
            element._path = tlv.Path;
            element._name = tlv.Path;
            element._isConstructed = tlv.IsConstructed;
            if (!tlv.IsEmpty)
            {
                if (tlv.IsConstructed)
                {
                    List<CdrElement> val = new List<CdrElement>((tlv.Value as List<TlvObject>).Count);
                    foreach (TlvObject ch in (tlv.Value as List<TlvObject>))
                    {
                        val.Add(CdrElement.CreateFromTlv(ch));
                    };
                    element._value = val;
                }
                else
                {
                    byte[] src = (tlv.Value as byte[]);
                    byte[] dst = new byte[src.Length];
                    Array.Copy(src, dst, src.Length);
                    element._value = dst;
                }
            }

            return element;
        }

        public bool IsEmpty
        {
            get
            {
                if (_value == null)
                {
                    return true;
                }
                else if (_isConstructed)
                {
                    return (_value as List<CdrElement>).Count > 0 ? false : true;
                }
                else
                {
                    return ((_value as byte[]).Length == 0);
                }
            }
        }

        public IList<CdrElement> GetAllChilds()
        {
            if (!this.IsConstructed || this.IsEmpty)
                return null;

            List<CdrElement> allChilds = new List<CdrElement>(1);
            IList<CdrElement> td;

            foreach (CdrElement child in (this.Value as List<CdrElement>))
            {
                if (child.IsConstructed)
                {
                    td = child.GetAllChilds();
                    if (td != null)
                    {
                        allChilds.AddRange(td);
                    }
                }
                else
                {
                    allChilds.Add(child);
                }
            }
            return (allChilds.Count > 0) ? allChilds.AsReadOnly() : null;
        }

        public CdrElement[] FindChild(string path)
        {
            IList<CdrElement> childs = this.GetAllChilds();
            if (childs == null)
            {
                return null;
            }
            else
            {
                List<CdrElement> result = new List<CdrElement>();
                foreach (CdrElement element in childs)
                    if (String.Equals(element.Path, path))
                        result.Add(element);
                return (result.Count > 0) ? result.ToArray() : null;
            }
        }

        public override string ToString()
        {
            StringBuilder record = new StringBuilder(String.Format("{0}=", this.Name));
            if (this.IsConstructed)
            {
                record.Append('[');
                if (!this.IsEmpty)
                {
                    for (int i = 0; i < (this.Value as List<CdrElement>).Count; i++)
                    {
                        if (i > 0) record.Append(' ');
                        record.Append((this.Value as List<CdrElement>)[i].ToString());
                    }
                }
                record.Append(']');
            }
            else
            {
                record.AppendFormat("\"{0}\"", this.Parselet.DefaultValue(this));
            }

            return record.ToString();
        }

        public void DumpToXml(TextWriter dumpWriter, int ident)
        {
            for (int i = ident; i > 0; i--) { dumpWriter.Write("\t"); }
            dumpWriter.Write("<Element=\"{0} ({1})\" Offset=\"{2}\""
                , this.Path
                , this.Name
                , this.Offset);
            if (this.IsConstructed)
            {
                dumpWriter.Write(">");
            }
            else
            {
                dumpWriter.Write(" Parselet=\"{0}\">", this.Parselet.ParseletName);
            }
            if (this.IsEmpty)
            {
                dumpWriter.WriteLine("</Element>");
            }
            else if (this.IsConstructed)
            {
                dumpWriter.WriteLine();
                foreach (CdrElement element in (this.Value as List<CdrElement>)) { element.DumpToXml(dumpWriter, ident + 1); };
                for (int i = ident; i > 0; i--) { dumpWriter.Write("\t"); }
                dumpWriter.WriteLine("</Element>");
            }
            else
            {
                dumpWriter.Write("Default=\"{0}\"", this.DefaultValueType);
                foreach (string key in this.Parselet.GetValueTypes())
                {
                    dumpWriter.Write(" {0}=\"{1}\"", key, this.Parselet.Value(key, this));
                }
                dumpWriter.WriteLine("</Element>");
            }
        }

        public void DumpToTxt(TextWriter dumpWriter, string leftHeader)
        {
            dumpWriter.Write("{0} {1}", leftHeader, this.ToString());
        }
    }
}
