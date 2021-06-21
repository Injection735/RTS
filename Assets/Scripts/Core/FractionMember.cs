using Abstractions;
using UnityEngine;

public class FractionMember : MonoBehaviour, IFractionMember
{
	[SerializeField] private int _id;

	public int Id => _id;

	protected void Start()
	{
		AutoAttackGlobalBehaviour.Units.AddOrUpdate(GetComponent<Unit>(), this, (unit, fractionMemeber) => fractionMemeber);
	}

	protected void OnDestroy()
	{
		AutoAttackGlobalBehaviour.Units.TryRemove(GetComponent<Unit>(), out var value);
	}
}
