using Core;
using System;
using UniRx;
using UnityEngine;
using Zenject;

public class ProduceUnitExecutor : CommandExecutorBase<IProductionCommand>, ITickable, IUnitProducer
{
	public IReactiveCollection<IUnitProductionTask> Queue => _queue;
	private IReactiveCollection<IUnitProductionTask> _queue = new ReactiveCollection<IUnitProductionTask>();

	protected override void ExecuteConcreteCommand(IProductionCommand command)
	{
		if (command.UnitPrefab == null)
		{
			Debug.LogError("No prefab in MainBuildin excecute");
			return;
		}

		_queue.Add(new UnitProductionTask(command.ProductionIcon, command.ProductionTime + 100, command.UnitPrefab));
	}

	public void Tick()
	{
		if (_queue.Count == 0)
			return;

		var currentTask = _queue[0];
		currentTask.ProductionTimeLeft = Math.Max(currentTask.ProductionTimeLeft - Time.deltaTime, 0);

		if (currentTask.ProductionTimeLeft <= 0)
		{
			CreateUnit(currentTask);
			_queue.Remove(currentTask);
		}
	}

	private void CreateUnit(IUnitProductionTask task)
	{
		Instantiate(task.UnitPrefab, transform.position + Vector3.forward * 3, Quaternion.identity, transform.parent);
	}
}
