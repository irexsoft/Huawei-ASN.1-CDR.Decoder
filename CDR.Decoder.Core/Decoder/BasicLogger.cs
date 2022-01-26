using System;
using System.Collections.Generic;
using System.Text;

namespace CDR.Decoder
{
    public enum LogLevel
    {
        // Error conditions
        Error = 0,

        // Warning conditions
        Warning = 1,

        // Informational
        Info = 2,

        // Debug-level messages
        Debug0 = 3,
        Debug1 = 4
    }

    public class LogMessage
    {
        public DateTime TimeStamp;
        public LogLevel Level;
        public bool ShowTimeStamp;
        public string Message;
        public override string ToString()
        {
            return String.Format("[{0}]\t{1}", TimeStamp.ToString("dd MMM HH:mm:ss"), Message);
        }
    }

    public class BasicLogger
    {
        private static int _logBufferCapacity = 100;
        private List<LogMessage> _buffer;

        public BasicLogger()
            : this(LogLevel.Info)
        {
        }

        public BasicLogger(LogLevel logLevel)
        {
            LogLevel = logLevel;
            _buffer = new List<LogMessage>(_logBufferCapacity);
        }

        public static int BufferCapacity { get { return _logBufferCapacity; } }
        public int MessagesCount { get { return _buffer.Count; } }
        public LogLevel LogLevel { get; set; }

        public IList<LogMessage> LastMessages
        {
            get
            {
                lock (_buffer)
                {
                    return _buffer.AsReadOnly();
                }
            }
        }

        public event EventHandler MessagesChanged;

        public void WriteLogMessage(string message, LogLevel level)
        {
            WriteLogMessage(message, level, true);
        }

        public void WriteLogMessage(string message, LogLevel level, bool showTimeStamp)
        {
            if (level > LogLevel) return;

            if (_buffer.Count == _logBufferCapacity)
            {
                _buffer.RemoveAt(0);
            }

            _buffer.Add(new LogMessage
            {
                TimeStamp = DateTime.Now,
                Level = level,
                ShowTimeStamp = showTimeStamp,
                Message = message
            });

            if (MessagesChanged != null)
            {
                this.MessagesChanged(this, EventArgs.Empty);
            }
        }

        public void AppendLogMessage(string message)
        {
            LogMessage msg = _buffer[_buffer.Count - 1];
            msg.Message = String.Concat(msg.Message, message);

            if (MessagesChanged != null)
            {
                this.MessagesChanged(this, EventArgs.Empty);
            }
        }
    }
}
