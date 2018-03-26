namespace PogStore.Cms.Core.Infrastructure.StartupTask
{
	public interface ITask
	{
		string Identifier { get; }

		string Description { get; }

		int Order { get; }

		void Run();
	}
}