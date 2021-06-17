using Abstractions;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Core
{
	public class ProduceEllenCommand : IProductionCommandEllen
	{
		[InjectAsset("Ellen")] private GameObject _unitPrefab;
		public GameObject UnitPrefab => _unitPrefab;
		[Inject(Id = "Ellen")] public int ProductionTime { get; }
		[Inject(Id = "Ellen")] public Sprite ProductionIcon { get; }
	}

	public class ProduceChomperCommand : IProductionCommandChomper
	{
		[InjectAsset("Chomper")] private GameObject _unitPrefab;
		public GameObject UnitPrefab => _unitPrefab;
		[Inject(Id = "Chomper")] public int ProductionTime { get; }
		[Inject(Id = "Chomper")] public Sprite ProductionIcon { get; }
	}

	public class AttackCommand : IAttackCommand
	{
		public IAttackable Target { get; }

		public AttackCommand(IAttackable target)
		{
			Target = target;
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

	public class SetCollectionPointCommand : ISetCollectionPointCommand
	{
		public Vector3 Position { get; }

		public SetCollectionPointCommand(Vector3 position)
		{
			Position = position;
		}
	}

	public class StopCommand : IStopCommand
	{
	}

	public class PatrolCommand : IPatrolCommand
	{
		public List<Vector3> PatrolPoints { get; }

		public PatrolCommand(List<Vector3> patrolPositions)
		{
			PatrolPoints = patrolPositions;
		}
	}
}