using System;
using System.Windows.Forms;

using CDR.Decoder;

namespace Decoder
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            AssemblyVersionInfo appInfo = new AssemblyVersionInfo(typeof(DecoderApplication));
            AssemblyVersionInfo dcInfo = new AssemblyVersionInfo(typeof(CdrDecoder));

            string rtf = Properties.Resources.CDR_Decoder;
            rtf = rtf.Replace("@@1", String.Format("{0} {1}.{2} - (build {3})", appInfo.Title, appInfo.Version.Major, appInfo.Version.Minor, appInfo.LastBuildDate.ToString("yyMMdd-HHmm")));
            rtf = rtf.Replace("@@2", appInfo.Copyright);
            rtf = rtf.Replace("@@3", String.Format("{0} {1}.{2} - (build {3})", dcInfo.Title, dcInfo.Version.Major, dcInfo.Version.Minor, dcInfo.LastBuildDate.ToString("yyMMdd-HHmm")));
            rtf = rtf.Replace("@@4", dcInfo.Copyright);
            richTextBox.Rtf = rtf;
        }

        private void richTextBox_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {

        }
    }
}
