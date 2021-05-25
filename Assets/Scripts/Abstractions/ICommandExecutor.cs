using UnityEngine;

public interface ICommandExecutor
{
	void Execute(ICommand command);
}

public abstract class CommandExecutorBase<TCommand> : MonoBehaviour, ICommandExecutor where TCommand : ICommand
{
	public void Execute(ICommand command)
	{
		ExecuteConcreteCommand((TCommand) command);
	}

	protected abstract void ExecuteConcreteCommand(TCommand command);
}