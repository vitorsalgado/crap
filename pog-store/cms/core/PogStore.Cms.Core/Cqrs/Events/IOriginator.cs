namespace PogStore.Cms.Core.Cqrs.Events
{
	public interface IOriginator
	{
		Snapshot GetSnapshot();

		void SetSnapshot(Snapshot snapshot);
	}
}