using UnityEngine;

public class PatrolUnitExecutor : CommandExecutorBase<IPatrolCommand>
{
	protected override void ExecuteConcreteCommand(IPatrolCommand command)
	{
		Debug.Log($"Unit Patrol points {string.Join(", ", command.PatrolPoints)}");
	}
}
