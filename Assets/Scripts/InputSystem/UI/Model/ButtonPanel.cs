using Zenject;

public class ButtonPanel
{
	[Inject] private CommandCreatorBase<IProductionCommandEllen> _produceEllenCommandCreator;
	[Inject] private CommandCreatorBase<IProductionCommandChomper> _produceChomperCommandCreator;
	[Inject] private CommandCreatorBase<IMoveCommand> _moveCommandCreator;
	[Inject] private CommandCreatorBase<IAttackCommand> _attackCommandCreator;
	[Inject] private CommandCreatorBase<IPatrolCommand> _patrolCommandCreator;
	[Inject] private CommandCreatorBase<IStopCommand> _stopCommandCreator;
	[Inject] private CommandCreatorBase<ISetCollectionPointCommand> _setCollectionPointCommandCreator;

	private bool _isPending;

	public void HandleClick(ICommandExecutor executor)
	{
		_isPending = true;

		_produceEllenCommandCreator.CreateCommand(executor, command => ExecuteSpecificCommand(executor, command));
		_produceChomperCommandCreator.CreateCommand(executor, command => ExecuteSpecificCommand(executor, command));
		_moveCommandCreator.CreateCommand(executor, command => ExecuteSpecificCommand(executor, command));
		_attackCommandCreator.CreateCommand(executor, command => ExecuteSpecificCommand(executor, command));
		_patrolCommandCreator.CreateCommand(executor, command => ExecuteSpecificCommand(executor, command));
		_stopCommandCreator.CreateCommand(executor, command => ExecuteSpecificCommand(executor, command));
		_setCollectionPointCommandCreator.CreateCommand(executor, command => ExecuteSpecificCommand(executor, command));
	}

	public void HandleSelecctionChanged()
	{
		CancelPendingCommand();
	}

	private void CancelPendingCommand()
	{
		if (!_isPending)
			return;

		_produceEllenCommandCreator.CancelCommand();
		_produceChomperCommandCreator.CancelCommand();
		_moveCommandCreator.CancelCommand();
	}

	private void ExecuteSpecificCommand(ICommandExecutor executor, ICommand command)
	{
		executor.Execute(command);
		_isPending = false;
	}
}
