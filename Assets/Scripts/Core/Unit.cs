using Abstractions;
using UnityEngine;

public class Unit : MonoBehaviour, ISelectableItem
{
	[SerializeField] private Sprite _icon;
	[SerializeField] private float _health;
	[SerializeField] private float _maxHp;

	public Sprite Icon => _icon;
	public float Health => _health;
	public float MaxHp => _maxHp;

	public void Select(bool isSelected)
	{

	}
}