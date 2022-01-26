using System;
using System.Windows.Forms;

using CDR.Decoder;
using CDR.Schema;

namespace Decoder
{
    class DecoderApplication
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static int Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DecoderJob job = null;
            bool startJob = false;
            string cmd = String.Empty;

            if (args.Length > 0)
            {
                switch (args.Length)
                {
                    case 1:
                        cmd = args[0];
                        break;
                    case 2:
                        if (String.Equals(args[0], "/s", StringComparison.CurrentCultureIgnoreCase))
                        {
                            startJob = true;
                            cmd = args[1];
                        }
                        break;
                }

                if (cmd.Length > 0)
                {
                    if (cmd[0] != '/')
                    {
                        job = new DecoderJob();
                        job.SourcePath = cmd;
                        job.DestinationPath = String.Concat(cmd, ".dump");
                    }
                    else if (cmd.Length > 3)
                    {
                        switch (Char.ToLower(cmd[1]))
                        {
                            case 'd':
                                job = new DecoderJob();
                                job.DecodingMode = JobDecodingMode.BatchDecoding;
                                job.SourcePath = cmd.Substring(3);
                                break;
                            case 'j':
                                job = DecoderJob.Load(cmd.Substring(3)) as DecoderJob;
                                break;
                        }
                    }
                }
            }

            MainForm frm = new MainForm((job != null) ? job : new DecoderJob(), startJob);

            AssemblyVersionInfo vInfo = new AssemblyVersionInfo(typeof(DecoderApplication));
            frm.Text = String.Format("{0} {1}.{2} - (build {3})", vInfo.Title, vInfo.Version.Major, vInfo.Version.Minor, vInfo.LastBuildDate.ToString("yyMMdd-HHmm"));

            Application.Run(frm);

            return (int)frm.ResultCode;
        }
    }
}
