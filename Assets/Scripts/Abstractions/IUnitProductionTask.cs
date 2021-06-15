using UnityEngine;

public interface IUnitProductionTask
{
	Sprite Icon { get; }
	float ProductionTimeLeft { get; set; }
	float ProductionTime { get; }
	GameObject UnitPrefab { get; }
}
