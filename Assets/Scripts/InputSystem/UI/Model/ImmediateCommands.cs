using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ImmediateCommands
{
	[Inject] private CommandCreatorBase<IMoveCommand> _moveCommandCreator;
	[Inject] private CommandCreatorBase<IAttackCommand> _attackCommandCreator;

	public void TryExecuteImmediateCommand(List<ICommandExecutor> executors)
	{
		foreach (var executor in executors)
			ExecuteImmediateCommands(executor);
	}

	public void ExecuteImmediateCommands(ICommandExecutor executor)
	{
		_moveCommandCreator.CreateCommand(executor, command => ExecuteSpecificCommand(executor, command));
		_attackCommandCreator.CreateCommand(executor, command => ExecuteSpecificCommand(executor, command));
	}

	private void ExecuteSpecificCommand(ICommandExecutor executor, ICommand command)
	{
		executor.Execute(command);
	}
}
