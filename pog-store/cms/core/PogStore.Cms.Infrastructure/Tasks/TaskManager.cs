using PogStore.Cms.Core.Infrastructure.StartupTask;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PogStore.Cms.Infrastructure.StartupTask
{
	public class TaskManager : ITaskManager
	{
		private static object _locker = new object();
		private static bool _startupTasksExecuted = false;

		public void RunTasks()
		{
			if (_startupTasksExecuted)
				return;

			lock (_locker)
			{
				var startupTasks = new List<ITask>();

				var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
				List<Type> startupTasksTypeList = new List<Type>();

				assemblies.ForEach((assembly) =>
				{
					startupTasksTypeList.AddRange(assembly.GetTypes()
						.Where(type => typeof(ITask).IsAssignableFrom(type) && type.IsClass));
				});

				foreach (var startupTaskDefinition in startupTasksTypeList)
					startupTasks.Add((ITask)Activator.CreateInstance(startupTaskDefinition));

				startupTasks = startupTasks
					.OrderBy(x => x.Order)
					.ToList();

				startupTasks.ForEach(x =>
				{
					Trace.WriteLine(string.Format("Running task: {0}\nDescription: {1}\nOrder: {2}", x.Identifier, x.Description, x.Order));

					x.Run();

					Trace.WriteLine(string.Format("Task \"{0}\" execution ended.", x.Identifier));
					Trace.WriteLine(Environment.NewLine);
				});

				_startupTasksExecuted = true;
			}
		}
	}
}