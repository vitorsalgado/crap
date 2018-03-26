namespace PogStore.Cms.Core.Infrastructure.Log
{
	public interface ILog
	{
		void Log(object sender, LogEventArgs e);
	}
}