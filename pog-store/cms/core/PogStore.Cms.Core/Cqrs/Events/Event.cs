using System;
using System.Reflection;

namespace PogStore.Cms.Core.Cqrs.Events
{
	public abstract class Event : IEvent
	{
		public Event()
		{
			Identifier = Guid.NewGuid();
			Timestamp = DateTime.Now;
		}

		public Guid Identifier { get; private set; }

		public virtual string Name
		{
			get { return this.GetType().Name; }
		}

		public string Type
		{
			get { return this.GetType().FullName; }
		}

		public Guid EventSourceId { get; set; }

		public DateTime Timestamp { get; set; }

		public long Version { get; set; }

		public string SourceVersion
		{
			get
			{
				var assemblyName = Assembly.GetExecutingAssembly().GetName();
				return string.Format("{0}.{1}", assemblyName.Version.Major, assemblyName.Version.Minor);
			}
		}
	}
}