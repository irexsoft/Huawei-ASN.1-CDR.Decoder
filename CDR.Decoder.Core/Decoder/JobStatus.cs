using System;

namespace CDR.Decoder
{
    public enum JobResultCode
    {
        FatalError = -1,
        AllOK = 0,
        CanceledByUser = 1
    }

    public class JobStatus
    {
        private DateTime _endTime;
        private bool _isCompleted;

        public JobStatus()
        {
            this.Reset();
        }

        public DateTime StartTime { get; set; }
        public DateTime EndTime
        {
            get { return _endTime; }
        }

        public JobResultCode ResultCode { get; set; }
        public bool IsRunning { get; set; }
        public bool IsCompleted
        {
            get { return _isCompleted; }
            set
            {
                if (value)
                {
                    _endTime = DateTime.Now;
                    _isCompleted = value;
                    IsRunning = false;
                }
            }
        }

        public long RecordsOut { get; set; }
        public long RecordsOutTotal { get; set; }
        public int CdrFilesOut { get; set; }
        public int CdrFilesIn { get; set; }
        public int Percent { get; set; }
        public string CurrentCdrFile { get; set; }

        public void Reset()
        {
            IsCompleted = false;
            IsRunning = false;
            StartTime = DateTime.Now;
            _endTime = StartTime;
            RecordsOut = 0;
            RecordsOutTotal = 0;
            CdrFilesOut = 0;
            CdrFilesIn = 0;
            Percent = 0;
            CurrentCdrFile = String.Empty;
            ResultCode = JobResultCode.AllOK;
        }
    }
}
