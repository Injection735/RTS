
using UnityEngine;

namespace Core
{
	public class UnitPatrol : CommandExecutorBase<IPatrolCommand>
	{
		protected override void ExecuteConcreteCommand(IPatrolCommand command)
		{
			Debug.Log("Unit patrol!");
		}
	}
}
