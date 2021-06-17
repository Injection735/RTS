using UniRx;
using UnityEngine;

public interface IUnitProductionTask
{
	Sprite Icon { get; }
	ReactiveProperty<float> ProductionTimeLeft { get; set; }
	float ProductionTime { get; }
	GameObject UnitPrefab { get; }
}
