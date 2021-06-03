using System.Collections.Generic;
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
		public GameObject AttackedObject { get; }

		public AttackCommand(GameObject attackedObject)
		{
			AttackedObject = attackedObject;
		}
	}

	public class MoveCommand : IMoveCommand
	{
		public Vector3 Position { get; }

		public MoveCommand(Vector3 position)
		{
			Position = position;
		}
	}

	public class PatrolCommand : IPatrolCommand
	{
		public List<Vector3> PatrolPoints { get; }

		public PatrolCommand(List<Vector3> patrolPositions)
		{
			PatrolPoints = patrolPositions;
		}
	}

	public class StopCommand : IStopCommand
	{
	}
}