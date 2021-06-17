using System;
using UniRx;
using UnityEngine;

namespace Core
{
	public class UnitProductionTask : IUnitProductionTask
	{
		public Sprite Icon { get; }
		public ReactiveProperty<float> ProductionTimeLeft { get; set; }
		public float ProductionTime { get; }
		public GameObject UnitPrefab { get; }

		public UnitProductionTask(Sprite icon, float productionTime, GameObject unitPrefab)
		{
			Icon = icon;
			ProductionTimeLeft = new ReactiveProperty<float>(productionTime);
			ProductionTime = productionTime;
			UnitPrefab = unitPrefab;
		}
	}
}
