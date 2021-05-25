
using UnityEngine;

namespace Core
{
	public class UnitStop : CommandExecutorBase<IStopCommand>
	{
		protected override void ExecuteConcreteCommand(IStopCommand command)
		{
			Debug.Log("Unit stop!");
		}
	}
}
