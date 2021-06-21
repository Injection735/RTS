using Abstractions;
using Core;
using UnityEngine;

public class Unit : MonoBehaviour, ISelectableItem, IAttackable, IAttacker
{
	[SerializeField] private Sprite _icon;
	[SerializeField] private float _health;
	[SerializeField] private float _maxHp;
	[SerializeField] private float _visibleRange;

	public Sprite Icon => _icon;
	public float Health => _health;
	public float MaxHp => _maxHp;
	public float VisibleRange => _visibleRange;

	public Vector3 Position { get; private set; }

	private AttackUnitExecutor _executor;

	public void AutoAttackTarget(IAttackable target)
	{
		_executor.Execute(new AutoAttackCommand(target));
	}

	public bool CanPerformAutoAttack()
	{
		if (_executor == null)
			return false;

		return _executor.CurrentCommand == null;
	}

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

	protected void Start()
	{
		_executor = GetComponent<AttackUnitExecutor>();
	}

	protected void Update()
	{
		Position = transform.position;
	}
}