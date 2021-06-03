using UnityEngine;

public class AttackUnitExecutor : CommandExecutorBase<IAttackCommand>
{
	protected override void ExecuteConcreteCommand(IAttackCommand command)
	{
		Debug.Log("Unit Attack!");
	}
}
