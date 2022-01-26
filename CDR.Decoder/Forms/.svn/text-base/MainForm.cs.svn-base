using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using CDR.Decoder;

namespace Decoder
{
    public partial class MainForm : Form
    {
        private Timer _timer;
        private TimeSpan _executionTime;
        private JobDispatcher _dispatcher;
        private bool _autorun;
        private List<LogMessage> _logBuffer;

        public MainForm(DecoderJob job, bool startJob)
        {
            InitializeComponent();

            _logBuffer = new List<LogMessage>(100);

            _dispatcher = new JobDispatcher(job);
            _dispatcher.Logger.MessagesChanged += new EventHandler(Log_MessagesChanged);

#if DEBUG
            _dispatcher.Logger.LogLevel = LogLevel.Debug0;
#endif

            _dispatcher.OnJobProgressChanged += new EventHandler(Dispatcher_OnJobProgressChanged);
            _dispatcher.AfterJobCompleted += new EventHandler(Dispatcher_AfterJobCompleted);

            _autorun = startJob;

            statusBindingSource.DataSource = _dispatcher.Status;

            jobPropertyGrid.SelectedObject = job;
            jobPropertyGrid.ExpandAllGridItems();

            _executionTime = new TimeSpan();
            _timer = new Timer();
            _timer.Tick += new EventHandler(TimerTick);
            _timer.Interval = 1000;

            this.Log_MessagesChanged(this, EventArgs.Empty);
        }

        public JobResultCode ResultCode
        {
            get { return _dispatcher.Status.ResultCode; }
        }

        private delegate void UpdateLogMessages(object sender, EventArgs e);

        void Log_MessagesChanged(object sender, EventArgs e)
        {
            if (this.logListView.InvokeRequired)
            {
                UpdateLogMessages d = new UpdateLogMessages(Log_MessagesChanged);
                this.Invoke(d, new object[] {sender, e});
            }
            else
            {
                logListView.BeginUpdate();
                logListView.Items.Clear();
                _logBuffer.Clear();
                _logBuffer.AddRange(_dispatcher.Logger.LastMessages);
                logListView.VirtualListSize = _logBuffer.Count;
                logListView.EnsureVisible(_logBuffer.Count - 1);
                logListView.EndUpdate();
            }
        }

        void Dispatcher_AfterJobCompleted(object sender, EventArgs e)
        {
            _timer.Enabled = false;
            statusBindingSource.ResetCurrentItem();

            jobPropertyGrid.Enabled = true;
            jobToolStrip.Enabled = true;
            exitButton.Enabled = true;
            decodeButton.Text = "Decode";

            if (_dispatcher.Status.ResultCode == JobResultCode.CanceledByUser)
            {
                MessageBox.Show("The Job was cancelled by User.", "Cancel The Job", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (_autorun) this.Close();
        }

        void Dispatcher_OnJobProgressChanged(object sender, EventArgs e)
        {
            statusBindingSource.ResetCurrentItem();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void decodeButton_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab(logTabPage);
            if (_dispatcher.Status.IsRunning)
            {
                _dispatcher.CancelJob();
            }
            else
            {
                jobPropertyGrid.Enabled = false;
                jobToolStrip.Enabled = false;
                exitButton.Enabled = false;
                decodeButton.Text = "Cancel";

                _timer.Enabled = true;
                _dispatcher.Status.Reset();
                _dispatcher.ExecuteJob();
            }
        }

        private void TimerTick(object sender, EventArgs e)
        {
            _executionTime = DateTime.Now - _dispatcher.Status.StartTime;
            timerText.Text = String.Format("{0:D2}:{1:D2}:{2:D2}",
                _executionTime.Days * 24 + _executionTime.Hours,
                _executionTime.Minutes,
                _executionTime.Seconds);
        }

        private void saveToolButton_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                (_dispatcher.Job as DecoderJob).Save(saveFileDialog.FileName);
            }
        }

        private void openToolButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _dispatcher.Job = DecoderJob.Load(openFileDialog.FileName) as DecoderJob;
                jobPropertyGrid.SelectedObject = _dispatcher.Job;
            }
        }

        private void newToolButton_Click(object sender, EventArgs e)
        {
            _dispatcher.Job = new DecoderJob();
            jobPropertyGrid.SelectedObject = _dispatcher.Job;
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (_autorun)
            {
                decodeButton_Click(this, EventArgs.Empty);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = _dispatcher.Status.IsRunning;
        }

        private void helpButton_Click(object sender, EventArgs e)
        {
            AboutForm frm = new AboutForm();
            frm.ShowDialog();
        }

        private void logListView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (_logBuffer[e.ItemIndex].ShowTimeStamp)
            {
                e.Item = new ListViewItem(_logBuffer[e.ItemIndex].TimeStamp.ToString("dd MMM HH:mm:ss"));
            }
            else
            {
                e.Item = new ListViewItem();
            }
            e.Item.SubItems.Add(_logBuffer[e.ItemIndex].Message);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
