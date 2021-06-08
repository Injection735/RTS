

using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class StopUnitExecutor : CommandExecutorBase<IStopCommand>
{
	[SerializeField] private MoveUnitExecutor _moveExecutor;

	protected override void ExecuteConcreteCommand(IStopCommand command)
	{
		_moveExecutor.Stop();
	}
}