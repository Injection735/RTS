using Abstractions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand
{
}

public interface IProductionCommand : ICommand
{
	GameObject UnitPrefab { get; }
	float ProductionTime { get; }
	Sprite ProductionIcon { get; }
	int Cost { get; }
}

public interface IProductionCommandEllen : IProductionCommand
{
}

public interface IProductionCommandChomper : IProductionCommand
{
}

public interface IMoveCommand : ICommand
{
	Vector3 Position { get; }
}

public interface ISetCollectionPointCommand : ICommand
{
	Vector3 Position { get; }
}


public interface IAttackCommand : ICommand
{
	IAttackable Target { get; }
}

public interface IStopCommand : ICommand
{
}

public interface IPatrolCommand : ICommand
{
	List<Vector3> PatrolPoints { get; }
}