using UnityEngine;
using Zenject;

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
		public Vector3 Position { get; }

		public MoveCommand(Vector3 position)
		{
			Position = position;
		}
	}

	public class StopCommand : IStopCommand
	{
	}

	public class PatrolCommand : IPatrolCommand
	{
	}
}