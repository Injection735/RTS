using Zenject;

public class ButtonPanel
{
	[Inject] private CommandCreatorBase<IProductionCommand> _produceCommandCreator;
	[Inject] private CommandCreatorBase<IMoveCommand> _moveCommandCreator;
	[Inject] private CommandCreatorBase<IAttackCommand> _attackCommandCreator;
	[Inject] private CommandCreatorBase<IPatrolCommand> _patrolCommandCreator;

	private bool _isPending;

	public void HandleClick(ICommandExecutor executor)
	{
		_isPending = true;

		_produceCommandCreator.CreateCommand(executor, command => ExecuteSpecificCommand(executor, command));
		_moveCommandCreator.CreateCommand(executor, command => ExecuteSpecificCommand(executor, command));
		_attackCommandCreator.CreateCommand(executor, command => ExecuteSpecificCommand(executor, command));
		_patrolCommandCreator.CreateCommand(executor, command => ExecuteSpecificCommand(executor, command));
	}

	private void ExecuteSpecificCommand(ICommandExecutor executor, ICommand command)
	{
		executor.Execute(command);
		_isPending = false;
	}
}
