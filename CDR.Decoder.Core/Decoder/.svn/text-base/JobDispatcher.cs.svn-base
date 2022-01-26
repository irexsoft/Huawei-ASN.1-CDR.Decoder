using System;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;

namespace CDR.Decoder
{
    public class JobDispatcher
    {
        private BackgroundWorker _worker;
        private IDecoderJob _job;
        private JobStatus _status;
        private BasicLogger _logger;
        private bool _ready;

        public JobDispatcher()
            : this(null)
        {
        }

        public JobDispatcher(IDecoderJob job)
        {
            _worker = new BackgroundWorker();
            _worker.DoWork += new DoWorkEventHandler(DoJob);
            _worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(JobCompleted);
            _worker.ProgressChanged += new ProgressChangedEventHandler(JobProgressChanged);
            _worker.WorkerReportsProgress = true;
            _worker.WorkerSupportsCancellation = true;
            _job = job;
            _status = new JobStatus();
            _logger = new BasicLogger();
            _ready = true;

            AssemblyVersionInfo vInfo = new AssemblyVersionInfo(typeof(JobBase));
            _logger.WriteLogMessage(String.Format("*** {0} {1}.{2} - (build {3})", vInfo.Title, vInfo.Version.Major, vInfo.Version.Minor, vInfo.LastBuildDate.ToString("yyMMdd-HHmm")), LogLevel.Info);

            string[] shemas = CDR.Schema.CdrDefinitionProvider.Instance.AvailableSchemas();
            if ((shemas != null) && (shemas.Length > 0))
            {
                _logger.WriteLogMessage(String.Format("*** Definition XML Version: {0}", CDR.Schema.CdrDefinitionProvider.Instance.XmlVersion), LogLevel.Info, false);
                _logger.WriteLogMessage("*** Available Schemas:", LogLevel.Info, false);
                foreach (string s in shemas)
                {
                    _logger.WriteLogMessage(String.Format("> {0}", s), LogLevel.Info, false);
                }

                byte[] sign = CDR.Schema.CdrDefinitionProvider.Instance.AviableSignatures();
                if ((sign != null) && (sign.Length > 0))
                {
                    _logger.WriteLogMessage(String.Format("*** Available Signatures: {0}", BitConverter.ToString(sign).Replace("-", "; ")), LogLevel.Info, false);
                }
                else
                {
                    _logger.WriteLogMessage("!!! Signatures definition not found, check definition.xml", LogLevel.Error, false);
                    _ready = false;
                }
            }
            else
            {
                _logger.WriteLogMessage("!!! Schemas definition not found, check definition.xml", LogLevel.Error, false);
                _ready = false;
            }
        }

        private void JobProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (this.OnJobProgressChanged != null)
                this.OnJobProgressChanged(sender, null);
        }

        private void JobCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _status.IsCompleted = true;
            if (this.AfterJobCompleted != null)
                this.AfterJobCompleted(sender, null);
        }

        private void DoJob(object sender, DoWorkEventArgs e)
        {
            if ((!_ready) || String.IsNullOrEmpty(_job.SourcePath))
            {
                _logger.WriteLogMessage("*** Job Dispatcher not ready.", LogLevel.Info, true);
                return;
            }

            _logger.WriteLogMessage("+++ Start new job:", LogLevel.Info);

            _status.ResultCode = JobResultCode.FatalError;

            CdrDecoder decoder = new CdrDecoder();
            decoder.ElementDefinitionProvider.CurrentSchema = _job.DefinitionSchemaName;
            CdrElement record;

            RecordFormatter formatter = (_job.IsFormatterActive && (_job.FormatterSettings != null)) ? new RecordFormatter(_job.FormatterSettings) : null;
            Regex filterRegex = null;
            if (_job.IsFilterActive && !String.IsNullOrEmpty(_job.FilterText))
            {
                try
                {
                    filterRegex = new Regex(_job.FilterText, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
                }
                catch (Exception error)
                {
                    _logger.WriteLogMessage(String.Format("!!! A filter regular expression parsing error in FilterText occurred: {0}", error.Message), LogLevel.Error, true);
                    _status.ResultCode = JobResultCode.FatalError;
                    _logger.WriteLogMessage("+++ The job finished with an error.", LogLevel.Info);
                    return;
                }
            }

            StreamWriter dstFile = new StreamWriter(_job.DestinationPath);
            if ((formatter != null) && _job.FormatterSettings.PrintColumnsHeader)
            {
                dstFile.WriteLine(_job.FormatterSettings.ColumnsHeader);
            }

            FileInfo[] cdrFiles;

            cdrFiles = new DirectoryInfo(Path.GetDirectoryName(_job.SourcePath)).GetFiles(Path.GetFileName(_job.SourcePath), SearchOption.TopDirectoryOnly);
            FileStream cdr;
            long cdrLength;
            long rem;
            string recText;

            // SGSN patch ///////////////////////////////////////////////////////////
            
            bool sgsn = !String.IsNullOrEmpty(decoder.ElementDefinitionProvider.Type) && (String.Compare(decoder.ElementDefinitionProvider.Type, "SGSN", true) == 0);
            List<CdrElement> sgsnRecord = new List<CdrElement>();

            /////////////////////////////////////////////////////////////////////////

            _status.CdrFilesIn = cdrFiles.Length;
            _logger.WriteLogMessage(String.Format("Files to decode: {0}", _status.CdrFilesIn), LogLevel.Info, false);
            _logger.WriteLogMessage(String.Format("Schema: {0}", _job.DefinitionSchemaName), LogLevel.Info, false);
            _worker.ReportProgress(_status.Percent);
            foreach (FileInfo fi in cdrFiles)
            {
                cdr = new FileStream(fi.FullName, FileMode.Open);
                cdrLength = cdr.Length;

                if (_job.StartOffset > 0) cdr.Seek(_job.StartOffset, SeekOrigin.Begin);

                _status.RecordsOut = 0;
                _status.CurrentCdrFile = fi.Name;
                rem = 0;

                if (sgsn)
                {
                    sgsnRecord.Clear();
                }

                _logger.WriteLogMessage(String.Format("{0} ... ", fi.Name), LogLevel.Info);
                _worker.ReportProgress(_status.Percent);
                for (; ; )
                {
                    if (_status.RecordsOut == 0)
                    {
                        if (sgsn && (sgsnRecord.Count > 0))
                        {
                            record = decoder.DecodeRecord(cdr, false);
                        }
                        else
                        {
                            record = decoder.DecodeRecord(cdr, true);
                        }
                    }
                    else
                    {
                        record = decoder.DecodeRecord(cdr, false);
                    }
                    if (record == null)
                    {
                        if (sgsn && (sgsnRecord.Count > 0))
                        {
                            _status.RecordsOut++;
                            _status.RecordsOutTotal++;
                            _status.Percent = (int)Math.Ceiling((double)cdr.Position / cdrLength * 100);

                            if (formatter == null)
                            {
                                StringBuilder sgsnText = new StringBuilder(String.Format("{0,8} > {1} {2}=[", sgsnRecord[0].Offset, _status.RecordsOut, sgsnRecord[0].Name), sgsnRecord.Count + 1);
                                for (int s = 1; s < sgsnRecord.Count; s++)
                                {
                                    if (s > 1)
                                        sgsnText.Append(' ');
                                    sgsnText.Append(sgsnRecord[s].ToString());
                                }

                                sgsnText.Append("]");
                                recText = sgsnText.ToString();
                            }
                            else
                            {
                                recText = formatter.FormatSGSNRecord(sgsnRecord);
                            }

                            if ((filterRegex == null) || (filterRegex.Match(recText).Success))
                                dstFile.WriteLine(recText);

                            Math.DivRem(_status.RecordsOut, 1000, out rem);
                            if (rem == 0) _worker.ReportProgress(_status.Percent);

                        }
                        break;
                    }

                    if (sgsn)
                    {
                        if (_worker.CancellationPending)
                        {
                            break;
                        }
                        if (record.IsConstructed && (record.Path.Equals("20") || record.Path.Equals("23") || record.Path.Equals("24")))
                        {
                            if (sgsnRecord.Count == 0)
                            {
                                sgsnRecord.Add(record);
                                continue;
                            }
                        }
                        else
                        {
                            sgsnRecord.Add(record);
                            continue;
                        }
                    }

                    _status.RecordsOut++;
                    _status.RecordsOutTotal++;
                    _status.Percent = (int)Math.Ceiling((double)cdr.Position / cdrLength * 100);

                    if (sgsn)
                    {
                        if (formatter == null)
                        {
                            StringBuilder sgsnText = new StringBuilder(String.Format("{0,8} > {1} {2}=[", sgsnRecord[0].Offset, _status.RecordsOut, sgsnRecord[0].Name), sgsnRecord.Count + 1);
                            for (int s = 1; s < sgsnRecord.Count; s++)
                            {
                                if (s > 1)
                                    sgsnText.Append(' ');
                                sgsnText.Append(sgsnRecord[s].ToString());
                            }

                            sgsnText.Append("]");
                            recText = sgsnText.ToString();
                        }
                        else
                        {
                            recText = formatter.FormatSGSNRecord(sgsnRecord);
                        }
                        sgsnRecord.Clear();
                        sgsnRecord.Add(record);
                    }
                    else
                    {
                        recText = (formatter == null) ? String.Format("{0,8} > {1} {2}", record.Offset, _status.RecordsOut, record.ToString()) : formatter.FormatRecord(record);
                    }

                    if ((filterRegex == null) || (filterRegex.Match(recText).Success))
                        dstFile.WriteLine(recText);

                    Math.DivRem(_status.RecordsOut, 1000, out rem);
                    if (rem == 0) _worker.ReportProgress(_status.Percent);
                    if (_worker.CancellationPending)
                    {
                        break;
                    }
                }

                cdr.Close();

                _logger.AppendLogMessage(_status.RecordsOut.ToString());

                if (_worker.CancellationPending)
                {
                    break;
                }
                else
                {
                    _status.CdrFilesIn--;
                    _status.CdrFilesOut++;
                    _status.Percent = 100;
                    _worker.ReportProgress(_status.Percent);
                }
            }

            dstFile.Close();
            if (_worker.CancellationPending)
            {
                _status.ResultCode = JobResultCode.CanceledByUser;
                _logger.WriteLogMessage("+++ Process aborted by user.", LogLevel.Info);
            }
            else
            {
                _status.ResultCode = JobResultCode.AllOK;
                _logger.WriteLogMessage("+++ Decoding is successful done.", LogLevel.Info);
            }
        }

        public event EventHandler AfterJobCompleted;
        public event EventHandler OnJobProgressChanged;

        public IDecoderJob Job
        {
            get { return _job; }
            set { _job = value; }
        }

        public BasicLogger Logger
        {
            get { return _logger; }
        }

        public JobStatus Status
        {
            get { return _status; }
        }

        public void ExecuteJob()
        {
            _status.IsRunning = true;
            _status.StartTime = DateTime.Now;
            _worker.RunWorkerAsync();
        }

        public void CancelJob()
        {
            _worker.CancelAsync();
        }
    }
}
