using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using CDR.Schema;

namespace CDR.Decoder
{
    [Serializable]
    public class FormatterColumn
    {
        public string Name;
        public ICdrElementDefinition Definition;
    }

    [Serializable]
    [TypeConverter(typeof(FormatterSettingsTypeConverter))]
    public class RecordFormatterSettings
    {
        public const string DefaultFormatString = "{0};";

        private List<FormatterColumn> _columns;

        public RecordFormatterSettings()
            : this(DefaultFormatString)
        {
        }

        public RecordFormatterSettings(string formatString)
        {
            this.Format = formatString;
            this.PrintColumnsHeader = true;
            _columns = new List<FormatterColumn>();
        }

        public RecordFormatterSettings Clone()
        {
            RecordFormatterSettings clone = new RecordFormatterSettings(this.Format);
            clone.PrintColumnsHeader = this.PrintColumnsHeader;
            clone._columns.AddRange(this._columns);
            return clone;
        }

        public int AddColumn(string name, ICdrElementDefinition field)
        {
            if (!_columns.Exists(col => String.Equals(col.Name, name, StringComparison.CurrentCultureIgnoreCase)))
            {
                _columns.Add(new FormatterColumn { Name = name, Definition = field });
                return _columns.Count - 1;
            }

            return -1;
        }

        public int InsertColumn(int index, string name, ICdrElementDefinition field)
        {
            if (!_columns.Exists(col => String.Equals(col.Name, name, StringComparison.CurrentCultureIgnoreCase)))
            {
                _columns.Insert(index, new FormatterColumn { Name = name, Definition = field });
                return index;
            }
            else
            {
                return -1;
            }
        }

        public void RemoveColumn(string name)
        {
            _columns.RemoveAll(col => String.Equals(col.Name, name, StringComparison.CurrentCultureIgnoreCase));
        }

        public void RemoveColumnAt(int index)
        {
            _columns.RemoveAt(index);
        }

        public FormatterColumn GetColumnAt(int index)
        {
            return _columns[index];
        }

        [DisplayName("Format String")]
        [Description("Format-pattern for column value. Pattern must include a {0}")]
        [DefaultValue(DefaultFormatString)]
        public string Format { get; set; }

        [DisplayName("Print Columns Header")]
        [Description("Print columns header to destination file.")]
        [DefaultValue(true)]
        public bool PrintColumnsHeader { get; set; }

        [Browsable(false)]
        public string ColumnsHeader
        {
            get
            {
                StringBuilder hdr = new StringBuilder(_columns.Count);
                foreach (FormatterColumn col in _columns)
                {
                    hdr.AppendFormat(Format, col.Name);
                }
                return hdr.ToString();
            }
        }

        [DisplayName("Columns Count")]
        [Description("Specifies the number of Columns in Formatter.")]
        public int ColumnCount
        {
            get { return _columns.Count; }
        }
    }

    public class FormatterSettingsTypeConverter : TypeConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            return (destinationType == typeof(string)) ? "(Columns Collection)" : base.ConvertTo(context, culture, value, destinationType);
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            if (value is RecordFormatterSettings)
                return TypeDescriptor.GetProperties(value, attributes);

            return base.GetProperties(context, value, attributes);
        }
    }

    public class RecordFormatter
    {
        private RecordFormatterSettings _settings;

        public RecordFormatter(RecordFormatterSettings settings)
        {
            _settings = settings;
        }

        public string FormatRecord(CdrElement record)
        {
            IList<CdrElement> childs = record.GetAllChilds();
            if ((childs == null) || (childs.Count == 0)) return String.Empty;

            StringBuilder builder = new StringBuilder();
            FormatterColumn column;
            string fieldValue;

            for (int i = 0; i < _settings.ColumnCount; i++)
            {
                column = _settings.GetColumnAt(i);
                fieldValue = String.Empty;
                foreach (CdrElement element in childs)
                {
                    if (String.Equals(element.Path, column.Definition.Path))
                    {
                        if (!String.IsNullOrEmpty(fieldValue))
                        {
                            fieldValue = String.Format("{0} | {1}", fieldValue, element.Parselet.Value(column.Definition.ValueType, element));
                        }
                        else
                        {
                            fieldValue = element.Parselet.Value(column.Definition.ValueType, element);
                        }
                    }
                }

                builder.AppendFormat(_settings.Format, fieldValue);
            }
            return builder.ToString();
        }

        public string FormatSGSNRecord(IList<CdrElement> sgsnRecord)
        {
            List<CdrElement> childs = new List<CdrElement>();
            foreach (CdrElement elm in sgsnRecord)
            {
                if (elm.IsConstructed)
                {
                    IList<CdrElement> t_childs = elm.GetAllChilds();
                    if (t_childs != null)
                        childs.AddRange(t_childs);
                }
                else
                {
                    childs.Add(elm);
                }
            }

            if (childs.Count == 0) return String.Empty;

            StringBuilder builder = new StringBuilder();
            FormatterColumn column;
            string fieldValue;

            for (int i = 0; i < _settings.ColumnCount; i++)
            {
                column = _settings.GetColumnAt(i);
                fieldValue = String.Empty;
                foreach (CdrElement element in childs)
                {
                    if (String.Equals(element.Path, column.Definition.Path))
                    {
                        if (!String.IsNullOrEmpty(fieldValue))
                        {
                            fieldValue = String.Format("{0} | {1}", fieldValue, element.Parselet.Value(column.Definition.ValueType, element));
                        }
                        else
                        {
                            fieldValue = element.Parselet.Value(column.Definition.ValueType, element);
                        }
                    }
                }

                builder.AppendFormat(_settings.Format, fieldValue);
            }
            return builder.ToString();
        }
    }
}
