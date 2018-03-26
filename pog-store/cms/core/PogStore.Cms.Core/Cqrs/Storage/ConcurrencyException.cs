using System;
using System.Runtime.Serialization;

namespace PogStore.Cms.Core.Cqrs.Storage
{
	[Serializable]
	public class ConcurrencyException : Exception
	{
		public Guid EventSourceId { get; private set; }

		public long EventSourceVersion { get; private set; }

		public ConcurrencyException(Guid eventSourceId, long versionToBeSaved)
			: base(string.Format("There is a newer than {0} version of the event source with id {1} you are trying to save stored in the event store.", versionToBeSaved, eventSourceId))
		{
			EventSourceId = eventSourceId;
			EventSourceVersion = versionToBeSaved;
		}

		protected ConcurrencyException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			EventSourceId = (Guid)info.GetValue("EventSourceId", typeof(Guid));
			EventSourceVersion = info.GetInt64("EventSourceVersion");
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("EventSourceId", EventSourceId);
			info.AddValue("EventSourceVersion", EventSourceVersion);
		}
	}
}