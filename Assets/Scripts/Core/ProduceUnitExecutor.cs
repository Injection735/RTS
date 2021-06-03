﻿using UnityEngine;

public class ProduceUnitExecutor : CommandExecutorBase<IProductionCommand>
{
	protected override void ExecuteConcreteCommand(IProductionCommand command)
	{
		if (command.UnitPrefab == null)
		{
			Debug.LogError("No prefab in MainBuildin excecute");
			return;
		}

		Instantiate(command.UnitPrefab, transform.position, Quaternion.identity, transform.parent);

	}
}