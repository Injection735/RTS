
using UnityEngine;

namespace Core
{
	public class UnitAttack : CommandExecutorBase<IAttackCommand>
	{
		protected override void ExecuteConcreteCommand(IAttackCommand command)
		{
			Debug.Log("Unit attacking!");
		}
	}
}
