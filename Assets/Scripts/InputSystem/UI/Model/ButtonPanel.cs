using Zenject;

public class ButtonPanel
{
	[Inject] private CommandCreatorBase<IProductionCommand> _produceCommandCreator;
	[Inject] private CommandCreatorBase<IMoveCommand> _moveCommandCreator;

	private bool _isPending;

	public void HandleClick(ICommandExecutor executor)
	{
		_isPending = true;

		_produceCommandCreator.CreateCommand(executor, command => ExecuteSpecificCommand(executor, command));
		_moveCommandCreator.CreateCommand(executor, command => ExecuteSpecificCommand(executor, command));
	}

	private void ExecuteSpecificCommand(ICommandExecutor executor, ICommand command)
	{
		executor.Execute(command);
		_isPending = false;
	}
}
