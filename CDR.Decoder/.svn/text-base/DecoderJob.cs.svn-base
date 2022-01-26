using System;
using System.ComponentModel;
using System.Drawing.Design;
using CDR.Decoder;

namespace Decoder
{
    [Serializable]
    [TypeDescriptionProvider(typeof(DynamicTypeDescriptionProvider))]
    public class DecoderJob : JobBase
    {
        [RefreshProperties(RefreshProperties.All)]
        public override JobDecodingMode DecodingMode
        {
            get
            {
                return base.DecodingMode;
            }
            set
            {
                base.DecodingMode = value;
            }
        }

        [Editor(typeof(FileSaveEditor), typeof(UITypeEditor))]
        public override string DestinationPath
        {
            get
            {
                return base.DestinationPath;
            }
            set
            {
                base.DestinationPath = value;
            }
        }

        [RefreshProperties(RefreshProperties.All)]
        public override bool IsFilterActive
        {
            get
            {
                return base.IsFilterActive;
            }
            set
            {
                base.IsFilterActive = value;
            }
        }

        [RefreshProperties(RefreshProperties.All)]
        public override bool IsFormatterActive
        {
            get
            {
                return base.IsFormatterActive;
            }
            set
            {
                base.IsFormatterActive = value;
            }
        }

        [Editor(typeof(FormatterSettingsEditor), typeof(UITypeEditor))]
        public override RecordFormatterSettings FormatterSettings
        {
            get
            {
                return base.FormatterSettings;
            }
            set
            {
                base.FormatterSettings = value;
            }
        }

        [Editor(typeof(DefinitionSchemaEditor), typeof(UITypeEditor))]
        public override string DefinitionSchemaName
        {
            get
            {
                return base.DefinitionSchemaName;
            }
            set
            {
                base.DefinitionSchemaName = value;
            }
        }

        /// <summary>
        /// This is a callback function for DynamicTypeDescriptionProvider.
        /// You can modify the collection in this method.
        /// Things you can do in this method:
        ///   Hide a property
        ///   Show a property
        ///   Add/Remove attributes of a property
        ///   Create a new property on the fly
        ///   
        /// More info: http://www.codeproject.com/KB/grid/PropertyGridDynamicProp.aspx 
        /// </summary>
        /// <param name="pdc"></param>
        public void ModifyDynamicProperties(PropertyDescriptorCollection pdc)
        {
            PropertyDescriptor pd = pdc.Find("SourcePath", false);
            pdc.Remove(pd);

            switch (DecodingMode)
            {
                case JobDecodingMode.SingleDecoding:
                    pdc.Add(TypeDescriptor.CreateProperty(
                        this.GetType(),
                        pd,
                        new EditorAttribute(typeof(System.Windows.Forms.Design.FileNameEditor), typeof(UITypeEditor))
                        ));
                    break;
                case JobDecodingMode.BatchDecoding:
                    pdc.Add(TypeDescriptor.CreateProperty(
                        this.GetType(),
                        pd,
                        new EditorAttribute(typeof(FolderEditor), typeof(UITypeEditor))
                        ));
                    break;
            }

            pd = pdc.Find("FilterText", false);
            pdc.Remove(pd);
            pdc.Add(TypeDescriptor.CreateProperty(this.GetType(), pd, new ReadOnlyAttribute(!IsFilterActive)));

            pd = pdc.Find("FormatterSettings", false);
            pdc.Remove(pd);
            pdc.Add(TypeDescriptor.CreateProperty(this.GetType(), pd, new ReadOnlyAttribute(!IsFormatterActive)));
        }
    }
}
