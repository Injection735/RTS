

using UnityEngine;
using UnityEngine.AI;

public class MoveUnitExecutor : CommandExecutorBase<IMoveCommand>
{
	[SerializeField] private NavMeshAgent _agent;

	public void Stop()
	{
		_agent.isStopped = true;
		_agent.ResetPath();
	}

	protected async override void ExecuteConcreteCommand(IMoveCommand command)
	{
		_agent.SetDestination(command.Position);
	}
}