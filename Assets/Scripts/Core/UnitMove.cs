
using UnityEngine;

namespace Core
{
	public class UnitMove : CommandExecutorBase<IMoveCommand>
	{
		protected override void ExecuteConcreteCommand(IMoveCommand command)
		{
			Debug.Log("Unit moving!");
		}
	}
}
