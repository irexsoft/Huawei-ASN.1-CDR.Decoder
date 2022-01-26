using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using CDR.Schema;

namespace CDR.Decoder
{
    public enum JobDecodingMode
    {
        [Description("Single CDR-file decoding")]
        SingleDecoding,
        [Description("Batch decoding")]
        BatchDecoding
    }

    public interface IDecoderJob
    {
        JobDecodingMode DecodingMode { get; set; }
        string SourcePath { get; set; }
        string DestinationPath { get; set; }
        string DefinitionSchemaName { get; set; }
        bool IsFilterActive { get; set; }
        string FilterText { get; set; }
        //bool UseRegEx { get; set; }
        bool IsFormatterActive { get; set; }
        RecordFormatterSettings FormatterSettings { get; set; }
        long StartOffset { get; set; }
    }

    [Serializable]
    public class JobBase : IDecoderJob
    {
        private bool _pathPrepared;
        private string _sourcePath;
        private string _destPath;
        private JobDecodingMode _mode;
        private string _definitionSchema;

        public JobBase()
        {
            _mode = JobDecodingMode.SingleDecoding;
            _definitionSchema = CdrDefinitionProvider.Instance.DefaultSchema;
            CdrDefinitionProvider.Instance.CurrentSchema = _definitionSchema;
        }

        private void MakeFullPath()
        {
            switch (_mode)
            {
                case JobDecodingMode.SingleDecoding:
                    _sourcePath = String.IsNullOrEmpty(_sourcePath) ? String.Empty : Path.GetFullPath(_sourcePath);
                    if (String.IsNullOrEmpty(_destPath))
                        _destPath = String.Concat(DateTime.Now.ToString("yyyyMMddHHmmss"), ".dump");
                    break;
                case JobDecodingMode.BatchDecoding:
                    if (String.IsNullOrEmpty(_sourcePath))
                        _sourcePath = Directory.GetCurrentDirectory();
                    if (!_sourcePath.Contains("*") && !_sourcePath.Contains("?") && !_sourcePath.EndsWith(@"\"))
                        _sourcePath += Path.DirectorySeparatorChar;
                    string pattern = Path.GetFileName(_sourcePath);
                    string dir = Path.GetDirectoryName(_sourcePath);
                    if (String.IsNullOrEmpty(dir))
                        dir = Directory.GetCurrentDirectory();
                    if (String.IsNullOrEmpty(pattern))
                        pattern = "*";
                    _sourcePath = Path.Combine(Path.GetFullPath(dir), pattern);
                    if (String.IsNullOrEmpty(_destPath))
                        _destPath = String.Concat("batch-", DateTime.Now.ToString("yyyyMMddHHmmss"), ".dump");
                    break;
            }
            _destPath = Path.GetFullPath(_destPath);
            _pathPrepared = true;
        }

        public static IDecoderJob Load(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open);

            BinaryFormatter binFormatter = new BinaryFormatter();
            try
            {
                return (IDecoderJob)binFormatter.Deserialize(fs);
            }
            catch
            {
                return null;
            }
            finally
            {
                fs.Close();
            }
        }

        public void Save(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Create);

            BinaryFormatter srlFormatter = new BinaryFormatter();
            try
            {
                srlFormatter.Serialize(fs, this);
            }
            finally
            {
                fs.Close();
            }
        }

        #region Common Settings
        [Category("Common Settings")]
        [DisplayName("Decoding Mode")]
        [Description("Decoding mode.")]
        //[DefaultValue(JobDecodingMode.SingleDecoding)]
        [TypeConverter(typeof(EnumDescConverter))]
        public virtual JobDecodingMode DecodingMode
        {
            get { return _mode; }
            set
            {
                if (_mode != value)
                {
                    _mode = value;
                    _sourcePath = String.Empty;
                    _pathPrepared = false;
                }
            }
        }

        [Category("Common Settings")]
        [DisplayName("Source")]
        [Description("Path to source CDR file(s). In batch-decoding mode you can use \"*\" and \"?\" pattern.")]
        public virtual string SourcePath
        {
            get
            {
                if (!_pathPrepared) MakeFullPath();
                return _sourcePath;
            }
            set
            {
                _sourcePath = value;
                switch (DecodingMode)
                {
                    case JobDecodingMode.SingleDecoding:
                        _destPath = String.Concat(_sourcePath, ".dump");
                        break;
                    case JobDecodingMode.BatchDecoding:
                        _destPath = String.Concat("batch-", DateTime.Now.ToString("yyyyMMddHHmmss"), ".dump");
                        break;
                }
                _pathPrepared = false;
            }
        }

        [Category("Common Settings")]
        [DisplayName("Destination")]
        [Description("Destination output file.")]
        public virtual string DestinationPath
        {
            get
            {
                if (!_pathPrepared) MakeFullPath();
                return _destPath;
            }
            set
            {
                _destPath = value;
                _pathPrepared = false;
            }
        }

        [Category("Common Settings")]
        [DisplayName("Definition Schema")]
        [Description("All definition schemas describe in the file *.Definition.xml. By default, the last scheme with attribute <Default = 'true'> or the first scheme in this file.")]
        public virtual string DefinitionSchemaName
        {
            get { return _definitionSchema; }
            set
            {
                _definitionSchema = value;
                CdrDefinitionProvider.Instance.CurrentSchema = _definitionSchema;
            }
        }
        #endregion

        #region Formatter
        [Category("Formatter")]
        [DisplayName("Formatter Active")]
        [Description("Enable or disable formatting of output records. If Formatter disable, Dump-output will enabled.")]
        [DefaultValue(false)]
        public virtual bool IsFormatterActive { get; set; }

        [Category("Formatter")]
        [DisplayName("Formatter Settings")]
        [Description("Adjust Formatter settings.")]
        public virtual RecordFormatterSettings FormatterSettings { get; set; }
        #endregion

        #region Filter
        [Category("Filter")]
        [DisplayName("Filter Active")]
        [Description("Enable or disable Filter for output.")]
        [DefaultValue(false)]
        public virtual bool IsFilterActive { get; set; }

        //[Category("Filter")]
        //[DisplayName("Use Regular Expressions")]
        //[Description("If active, Filter-Text defines regular expression pattern.")]
        //[DefaultValue(false)]
        //public virtual bool UseRegEx { get; set; }

        [Category("Filter")]
        [DisplayName("Filter Text")]
        [Description("You can use regular expressions. The filter applies to whole record. If Formatter is active, the filter is applied to the record after formatting.")]
        [DefaultValue("")]
        public virtual String FilterText { get; set; }
        #endregion

        #region Other
        [Category("Other")]
        [DisplayName("Start Offset")]
        [Description("Use to skip N-bytes in each CDR-file before decoding.")]
        [DefaultValue(0)]
        public virtual long StartOffset { get; set; }
        #endregion
    }
}
