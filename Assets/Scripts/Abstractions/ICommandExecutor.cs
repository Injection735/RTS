using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public interface ICommandExecutor
{
	void Execute(ICommand command);
}

public abstract class CommandExecutorBase<TCommand> : MonoBehaviour, ICommandExecutor where TCommand : ICommand
{
	private List<System.Func<Task>> queue = new List<System.Func<Task>>();
	private List<ICommand> commandList = new List<ICommand>();

	public void Execute(ICommand command)
	{
		commandList.Add(command);
		queue.Add(async () => await ExecuteConcreteCommand((TCommand) command));

		if (queue.Count == 1)
			ExecuteNext();
	}

	public ICommand CurrentCommand => commandList.Count > 0 ? commandList[0] : null;

	private async void ExecuteNext()
	{
		if (queue.Count == 0)
			return;

		if (queue.Count > 0)
			await queue[0].Invoke();

		queue.RemoveAt(0);
		commandList.RemoveAt(0);

		ExecuteNext();
	}

	protected abstract Task ExecuteConcreteCommand(TCommand command);
}