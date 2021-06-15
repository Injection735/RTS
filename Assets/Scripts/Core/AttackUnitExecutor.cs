using System.Threading.Tasks;
using UnityEngine;

public class AttackUnitExecutor : CommandExecutorBase<IAttackCommand>
{
	protected override Task ExecuteConcreteCommand(IAttackCommand command)
	{
		Debug.Log("Unit Attack!");
		return Task.CompletedTask;
	}
}
