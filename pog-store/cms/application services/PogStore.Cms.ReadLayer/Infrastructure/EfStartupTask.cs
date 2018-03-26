using PogStore.Cms.Infrastructure.StartupTask;
using System.Data.Entity;

namespace PogStore.Cms.ReadLayer.Infrastructure {
	public class EfStartupTask : ITask {

		public string Identifier {
			get { return "pogstore.tasks.ef"; }
		}

		public string Description {
			get { return "Task to start sql server database."; }
		}

		public int Order {
			get { return 1; }
		}

		public void Run() {
			Database.SetInitializer<CmsEfDbContext>(new DbInit<CmsEfDbContext>());

			using (var startContext = new CmsEfDbContext())
				startContext.Database.Initialize(true);
		}
	}
}
