using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Abstractions;

namespace Core
{
	public class MainBuilding : CommandExecutorBase<IProductionCommand>, ISelectableItem
	{
		[SerializeField] private OutlineSelector _selector;
		[SerializeField] private Sprite _icon;
		[SerializeField] private float _health;
		[SerializeField] private float _maxHp;

		public Sprite Icon => _icon;
		public float Health => _health;
		public float MaxHp => _maxHp;

		public void Select(bool isSelected)
		{
			_selector.SetSelected(isSelected);
		}

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
}