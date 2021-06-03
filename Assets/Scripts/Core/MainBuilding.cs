using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Abstractions;

namespace Core
{
	public class MainBuilding : MonoBehaviour, ISelectableItem
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
	}
}