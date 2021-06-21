using Core;
using System;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using Zenject;

public abstract class ProduceUnitExecutorBase<T> : CommandExecutorBase<T>, ITickable, IUnitProducer where T : IProductionCommand
{
	[Inject] private BalancesData balancesData;

	public System.Action<GameObject> OnUnitCreate = delegate { };

	public IReactiveCollection<IUnitProductionTask> Queue => _queue;
	private IReactiveCollection<IUnitProductionTask> _queue = new ReactiveCollection<IUnitProductionTask>();

	protected override Task ExecuteConcreteCommand(T command)
	{
		if (command.UnitPrefab == null)
		{
			Debug.LogError("No prefab in MainBuildin excecute");
			return Task.CompletedTask;
		}

		if (balancesData.Coins.Value < command.Cost)
			return Task.CompletedTask;


		_queue.Add(new UnitProductionTask(command.ProductionIcon, command.ProductionTime, command.UnitPrefab, command.Cost));
		balancesData.Coins.Value -= command.Cost;
		return Task.CompletedTask;
	}

	public void Tick()
	{
		if (_queue.Count == 0)
			return;

		var currentTask = _queue[0];
		currentTask.ProductionTimeLeft.Value = Math.Max(currentTask.ProductionTimeLeft.Value - Time.deltaTime, 0);

		if (currentTask.ProductionTimeLeft.Value <= 0)
		{
			OnUnitCreate.Invoke(CreateUnit(currentTask));
			_queue.Remove(currentTask);
		}
	}

	private GameObject CreateUnit(IUnitProductionTask task)
	{
		return Instantiate(task.UnitPrefab, transform.position + Vector3.forward * 3, Quaternion.identity, transform.parent);
	}
}
