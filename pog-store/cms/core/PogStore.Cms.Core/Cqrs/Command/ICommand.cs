namespace PogStore.Cms.Core.Cqrs.Command
{
	public interface ICommand
	{
		string Identifier { get; }
	}
}