

using UnityEngine;
using UnityEngine.AI;

public class MoveUnitExecutor : CommandExecutorBase<IMoveCommand>
{
	[SerializeField] private NavMeshAgent _agent;

	protected override void ExecuteConcreteCommand(IMoveCommand command)
	{
		_agent.SetDestination(command.Position);
	}
}