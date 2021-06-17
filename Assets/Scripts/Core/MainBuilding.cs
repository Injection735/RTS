using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Abstractions;
using Zenject;

namespace Core
{
	public class MainBuilding : MonoBehaviour, ISelectableItem
	{
		[SerializeField] private OutlineSelector _selector;
		[SerializeField] private Sprite _icon;
		[SerializeField] private float _health;
		[SerializeField] private float _maxHp;

		[Inject] private BuildingsData _buildingsData;

		private IFractionMember _fractionComponent;

		public Sprite Icon => _icon;
		public float Health => _health;
		public float MaxHp => _maxHp;

		public void Select(bool isSelected)
		{
			_selector.SetSelected(isSelected);
		}

		private void Awake()
		{
			_fractionComponent = GetComponent<IFractionMember>();
			if (_fractionComponent == null)
				return;

			if (_fractionComponent.Id == 0)
				_buildingsData.AddSelf();
			else
				_buildingsData.AddEnemy();
		}

		private void OnDestroy()
		{
			if (_fractionComponent == null)
				return;

			if (_fractionComponent.Id == 0)
				_buildingsData.OnSelfBuildingDestroyed();
			else
				_buildingsData.OnEnemyBuildingDestroyed();
		}
	}
}