using Core;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class CollectionPointUnitExecutor : CommandExecutorBase<ISetCollectionPointCommand>
{
	[SerializeField] private ProduceUnitExecutor _produceExecutor;

	protected override Task ExecuteConcreteCommand(ISetCollectionPointCommand command)
	{
		_produceExecutor.OnUnitCreate += unit => AddMoveCommand(unit, command);
		return Task.CompletedTask;
	}

	private void AddMoveCommand(GameObject unit, ISetCollectionPointCommand command)
	{
		List<ICommandExecutor> executors = unit?.GetComponentsInParent<ICommandExecutor>().ToList();

		foreach (ICommandExecutor executor in executors)
			if (executor is CommandExecutorBase<IMoveCommand>)
				executor.Execute(new MoveCommand(command.Position));
	}
}