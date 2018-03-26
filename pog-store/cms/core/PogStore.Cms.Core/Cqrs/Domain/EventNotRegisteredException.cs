using PogStore.Cms.Core.Cqrs.Events;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PogStore.Cms.Core.Cqrs.Domain
{
	public class EventNotRegisteredException : Exception
	{
		private string _message;

		public EventNotRegisteredException()
		{
		}

		public EventNotRegisteredException(IEvent @event, IEnumerable<IEvent> registeredEvents)
		{
			StringBuilder str = new StringBuilder();
			str.AppendFormat("Then event \"{0}\" is not registered in the Aggregate Root.", @event.ToString());
			str.AppendLine();
			str.Append("Current Registered Events in Aggregate Root:").AppendLine();

			foreach (var e in registeredEvents)
			{
				str.Append("*  ").Append(@event.ToString()).Append("  *").AppendLine();
			}

			this._message = str.ToString();
		}

		public EventNotRegisteredException(IEvent @event)
		{
			StringBuilder str = new StringBuilder();
			str.AppendFormat("Then event \"{0}\" is not registered in the Aggregate Root.", @event.ToString());
			str.AppendLine();

			this._message = str.ToString();
		}

		protected EventNotRegisteredException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		public override string Message
		{
			get
			{
				return this._message;
			}
		}
	}
}