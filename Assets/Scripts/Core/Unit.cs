using Abstractions;
using UnityEngine;

public class Unit : MonoBehaviour, ISelectableItem, IAttackable
{
	[SerializeField] private Sprite _icon;
	[SerializeField] private float _health;
	[SerializeField] private float _maxHp;

	public Sprite Icon => _icon;
	public float Health => _health;
	public float MaxHp => _maxHp;

	public Vector3 Position => transform.position;

	public void ReceiveDamage(float damage)
	{
		_health -= damage;

		if (_health <= 0)
		{
			Debug.Log("Death");
			Destroy(gameObject);
		}
	}

	public void Select(bool isSelected)
	{

	}
}