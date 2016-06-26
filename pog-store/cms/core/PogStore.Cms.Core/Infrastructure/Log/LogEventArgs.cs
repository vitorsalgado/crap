using System;

namespace PogStore.Cms.Core.Infrastructure.Log
{
	public class LogEventArgs
	{
		public LogEventArgs(LogSeverity severity, string message, Exception exception, DateTime date)
		{
			Severity = severity;
			Message = message;
			Exception = exception;
			Date = date;
		}

		public LogSeverity Severity { get; private set; }

		public string Message { get; private set; }

		public Exception Exception { get; private set; }

		public DateTime Date { get; private set; }

		public string SeverityString
		{
			get { return Severity.ToString("G"); }
		}

		public override String ToString()
		{
			return "" + Date
				+ " - " + SeverityString
				+ " - " + Message
				+ " - " + Exception.ToString();
		}
	}
}