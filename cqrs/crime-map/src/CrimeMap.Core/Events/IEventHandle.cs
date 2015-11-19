namespace CrimeMap.Core.Events {

	public interface IEventHandle<T> where T : IEvent {

		void Handle(T @event);

	}
}
