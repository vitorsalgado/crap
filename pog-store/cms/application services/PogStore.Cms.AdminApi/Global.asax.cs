using PogStore.Cms.Core.Infrastructure.StartupTask;
using System.Web.Http;

namespace PogStore.Cms.AdminApi
{
	public class WebApiApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			GlobalConfiguration.Configure(WebApiConfig.Register);
			RunStartupTasks();
		}

		private static void RunStartupTasks()
		{
			try
			{
				((ITaskManager)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(ITaskManager))).RunTasks();
			}
			catch { }
		}
	}
}