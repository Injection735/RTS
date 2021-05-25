using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand
{
}

public interface IProductionCommand : ICommand
{
	GameObject UnitPrefab { get; }
}

public interface IMoveCommand : ICommand
{
	Vector3 Position { get; }
}

public interface IAttackCommand : ICommand
{
}