using System;

namespace PogStore.Cms.Core.Infrastructure.Log
{
	public sealed class Logger
	{
		public delegate void LogEventHandler(object sender, LogEventArgs e);

		public event LogEventHandler Log;

		private static readonly Logger instance = new Logger();

		private Logger()
		{
			Severity = LogSeverity.Error;
		}

		public static Logger Instance
		{
			get { return instance; }
		}

		private LogSeverity _severity;

		private bool _isDebug;
		private bool _isInfo;
		private bool _isWarning;
		private bool _isError;
		private bool _isFatal;

		public LogSeverity Severity
		{
			get
			{
				return _severity;
			}

			set
			{
				_severity = value;

				int severity = (int)_severity;

				_isDebug = ((int)LogSeverity.Debug) >= severity ? true : false;
				_isInfo = ((int)LogSeverity.Info) >= severity ? true : false;
				_isWarning = ((int)LogSeverity.Warning) >= severity ? true : false;
				_isError = ((int)LogSeverity.Error) >= severity ? true : false;
				_isFatal = ((int)LogSeverity.Fatal) >= severity ? true : false;
			}
		}

		public void Debug(string message)
		{
			if (_isDebug)
				Debug(message, null);
		}

		public void Debug(string message, Exception exception)
		{
			if (_isDebug)
				OnLog(new LogEventArgs(LogSeverity.Debug, message, exception, DateTime.Now));
		}

		public void Info(string message)
		{
			if (_isInfo)
				Info(message, null);
		}

		public void Info(string message, Exception exception)
		{
			if (_isInfo)
				OnLog(new LogEventArgs(LogSeverity.Info, message, exception, DateTime.Now));
		}

		public void Warning(string message)
		{
			if (_isWarning)
				Warning(message, null);
		}

		public void Warning(string message, Exception exception)
		{
			if (_isWarning)
				OnLog(new LogEventArgs(LogSeverity.Warning, message, exception, DateTime.Now));
		}

		public void Error(string message)
		{
			if (_isError)
				Error(message, null);
		}

		public void Error(string message, Exception exception)
		{
			if (_isError)
				OnLog(new LogEventArgs(LogSeverity.Error, message, exception, DateTime.Now));
		}

		public void Fatal(string message)
		{
			if (_isFatal)
				Fatal(message, null);
		}

		public void Fatal(string message, Exception exception)
		{
			if (_isFatal)
				OnLog(new LogEventArgs(LogSeverity.Fatal, message, exception, DateTime.Now));
		}

		public void OnLog(LogEventArgs e)
		{
			if (Log != null)
			{
				Log(this, e);
			}
		}

		public void Attach(ILog observer)
		{
			Log += observer.Log;
		}

		public void Detach(ILog observer)
		{
			Log -= observer.Log;
		}
	}
}