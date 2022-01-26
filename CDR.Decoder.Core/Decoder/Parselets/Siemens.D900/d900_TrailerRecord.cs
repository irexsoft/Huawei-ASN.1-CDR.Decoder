using System;

namespace CDR.Decoder.Parselets
{
    /// <summary>
    /// The size of the transferred file.
    /// Filesize is calculated without the size of the trailer record
    /// Attention: coding a and b were switched in CS3.0.
    /// </summary>
    class d900_FileSizeParselet : GenericParselet
    {
        public d900_FileSizeParselet()
            : base()
        {
            RegisterMethod("Coding_A", ValueAsInteger);
            RegisterMethod("Coding_B", ValueAsString);
            DefaultValueType = "Coding_A";
        }

        protected string ValueAsInteger(byte[] value)
        {
            return IntegerParselet.ValueAsInteger(value);
        }

        protected string ValueAsString(byte[] value)
        {
            return Ia5StringParselet.ValueAsString(value);
        }
    }

    /// <summary>
    /// The number of records contained in the transferred file
    /// The number is calculated without the trailer record
    /// Attention: coding a and b were switched in CS3.0.
    /// </summary>
    class d900_NumberOfRecordsParselet : GenericParselet
    {
        public d900_NumberOfRecordsParselet()
            : base()
        {
            RegisterMethod("Coding_A", ValueAsInteger);
            RegisterMethod("Coding_B", ValueAsString);
            DefaultValueType = "Coding_A";
        }

        protected string ValueAsInteger(byte[] value)
        {
            return IntegerParselet.ValueAsInteger(value);
        }

        protected string ValueAsString(byte[] value)
        {
            return Ia5StringParselet.ValueAsString(value);
        }
    }

    /// <summary>
    /// The extensionFileNumber serves like a counter and is incremented after
    /// each successful FTP_DELETE-command.
    /// With this number it is possible to take notice of charging files which are not transfered
    /// from the MSC to the post-processing-system.
    /// It is available only for one file view MSC-recoveries have no effect on this counter
    /// 
    /// The "extension file number" is a component of the SAMAR structure,
    /// it is not visible to the outside world (e.g. with DISP SAMAR),
    /// only as part of the TRAILER record. 
    /// </summary>
    class d900_ExtensionFileNumber : GenericParselet
    {
        public d900_ExtensionFileNumber()
            : base()
        {
            RegisterMethod("Coding_A", ValueAsInteger);
            RegisterMethod("Coding_B", ValueAsString);
            DefaultValueType = "Coding_A";
        }

        protected string ValueAsInteger(byte[] value)
        {
            return IntegerParselet.ValueAsInteger(value);
        }

        protected string ValueAsString(byte[] value)
        {
            return Ia5StringParselet.ValueAsString(value);
        }
    }
}
