using UnityEngine;

namespace Core
{
	public class ProduceCommand : IProductionCommand
	{
		[InjectAsset("Ellen")] private GameObject _unitPrefab; // TODO private
		public GameObject UnitPrefab => _unitPrefab;
	}

	public class ProduceCommandHeir : ProduceCommand
	{
	}

	public class AttackCommand : IAttackCommand
	{
	}

	public class MoveCommand : IMoveCommand
	{
		public Vector3 Position => new Vector3();
	}

	public class StopCommand : IStopCommand
	{
	}

	public class PatrolCommand : IPatrolCommand
	{
	}
}