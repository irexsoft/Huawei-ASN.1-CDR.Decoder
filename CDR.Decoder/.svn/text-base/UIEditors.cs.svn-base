using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using CDR.Decoder;
using CDR.Schema;

namespace Decoder
{
    class FileSaveEditor : UITypeEditor
    {
        private SaveFileDialog _dlg;

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (_dlg == null) InitializeDialog();

            string path = (value as string);
            if (!String.IsNullOrEmpty(path))
            {
                _dlg.InitialDirectory = Path.GetDirectoryName(path);
                _dlg.FileName = Path.GetFileName(path);
            }

            if (_dlg.ShowDialog() == DialogResult.OK)
            {
                return _dlg.FileName;
            }

            return value;
        }

        private void InitializeDialog()
        {
            _dlg = new SaveFileDialog();
            _dlg.Filter = "All Files (*.*)|*.*";
            _dlg.DefaultExt = "dump";
            //_dlg.RestoreDirectory = true;
        }
    }

    class FolderEditor : UITypeEditor
    {
        private FolderBrowserDialog _dlg;

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        private void InitializeDialog()
        {
            _dlg = new FolderBrowserDialog();
            _dlg.Description = "Select directory with CDR-files:";
            _dlg.RootFolder = Environment.SpecialFolder.MyComputer;
            _dlg.ShowNewFolderButton = false;
            _dlg.SelectedPath = Directory.GetCurrentDirectory();
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (_dlg == null) InitializeDialog();

            string path = (value as string);
            if (!String.IsNullOrEmpty(path))
            {
                _dlg.SelectedPath = Path.GetDirectoryName(path);
            }

            if (_dlg.ShowDialog() == DialogResult.OK)
            {
                return _dlg.SelectedPath;
            }

            return value;
        }
    }

    class FormatterSettingsEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            if (edSvc == null)
            {
                return null;
            }

            using (FormatterSettingsForm form = new FormatterSettingsForm(value as RecordFormatterSettings))
            {
                if (edSvc.ShowDialog(form) == DialogResult.OK)
                {
                    return form.FormatterSettings;
                }
            }

            return value;
        }
    }

    class DefinitionSchemaEditor : UITypeEditor
    {
        private ListBox _box;
        private IWindowsFormsEditorService _edSvc;

        public DefinitionSchemaEditor()
            : base()
        {
            _box = new ListBox();
            _box.BorderStyle = BorderStyle.None;
            _box.Items.AddRange(CdrDefinitionProvider.Instance.AvailableSchemas());
            _box.Height = _box.Items.Count * 25;
            _box.SelectedValueChanged += new EventHandler(BoxSelectedValueChanged);
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            _edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            if (_edSvc == null)
            {
                return null;
            }

            _edSvc.DropDownControl(_box);
            return _box.SelectedItem;
        }

        void BoxSelectedValueChanged(object sender, EventArgs e)
        {
            if (_edSvc != null)
                _edSvc.CloseDropDown();
        }
    }
}
