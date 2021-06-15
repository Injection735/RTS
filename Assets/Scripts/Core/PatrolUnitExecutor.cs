using System.Threading.Tasks;
using UnityEngine;

public class PatrolUnitExecutor : CommandExecutorBase<IPatrolCommand>
{
	protected override Task ExecuteConcreteCommand(IPatrolCommand command)
	{
		Debug.Log($"Unit Patrol points {string.Join(", ", command.PatrolPoints)}");
		return Task.CompletedTask;
	}
}
