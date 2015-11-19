using System;
using Topshelf;

namespace CrimeMap.EventListenerService {

	class Program {

		static void Main(string[] args) {
			HostFactory.Run(x => {
				x.Service<EventListenerService>(s => {
					s.ConstructUsing(name => new EventListenerService());
					
					s.WhenStarted(tc => tc.Start());
					s.WhenStopped(tc => tc.Stop());
				});

				x.RunAsLocalSystem();
				x.StartAutomatically();
				x.EnableShutdown();

				x.SetDescription("Crime Map Event Handler");
				x.SetDisplayName("Crime Map Event Handler");
				x.SetServiceName("CrimeMapEventHandler");
			});
		}

	}

}
