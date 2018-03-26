using System;
using CrimeMap.Core.Events;
using MassTransit;
using CrimeMap.Log;

namespace CrimeMap.EventHandlers.Infrastructure {

	public abstract class EventHandler<T> : Consumes<T>.All, IEventHandle<T> where T : class, IEvent {

		public abstract void Handle(T @event);

		protected EventHandler() {
			Logger.Instance.Attach(new ConsoleLog());
		}

		public void Consume(T message) {
			try {
				Handle(message);
			} catch (Exception ex) {
				Logger.Instance.Error(string.Format("error handling the event \"{0}\".", message.EventName, ex));
				throw;
			}
		}

	}

}
