namespace CrimeMap.Core.Events {

	public interface IOriginator {

		Snapshot GetSnapshot();

		void SetSnapshot(Snapshot snapshot);

	}

}
