

using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class StopUnitExecutor : CommandExecutorBase<IStopCommand>
{
	[SerializeField] private MoveUnitExecutor _moveExecutor;

	protected override Task ExecuteConcreteCommand(IStopCommand command)
	{
		_moveExecutor.Stop();
		return Task.CompletedTask;
	}
}