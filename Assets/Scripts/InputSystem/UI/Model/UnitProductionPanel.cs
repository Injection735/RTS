using Abstractions;
using System;
using UniRx;
using UnityEngine;

public class UnitProductionPanel
{
	public IReactiveCollection<IUnitProductionTask> UnitProductionQueue = new ReactiveCollection<IUnitProductionTask>();

	public void HandleSelecctionChanged(ISelectableItem currentItem)
	{
		var producer = (currentItem as Component)?.GetComponent<IUnitProducer>();
		UnitProductionQueue = producer?.Queue;
	}
}