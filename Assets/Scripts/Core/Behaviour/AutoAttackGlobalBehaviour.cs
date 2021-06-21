
using Abstractions;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

public class AutoAttackGlobalBehaviour : MonoBehaviour
{
	public struct AutoAttackCommandInfo
	{
		public Unit Attacker;
		public IAttackable Target;

		public AutoAttackCommandInfo(Unit attacker, IAttackable target)
		{
			Attacker = attacker;
			Target = target;
		}
	}

	public static ConcurrentDictionary<Unit, FractionMember> Units = new ConcurrentDictionary<Unit, FractionMember>();

	public static Subject<AutoAttackCommandInfo> _attackTarget = new Subject<AutoAttackCommandInfo>();

	protected void Start()
	{
		_attackTarget.ObserveOnMainThread().Subscribe(PerformAutoAttack).AddTo(this);
	}

	private void PerformAutoAttack(AutoAttackCommandInfo commandInfo)
	{
		if (commandInfo.Attacker == null)
			return;

		commandInfo.Attacker.AutoAttackTarget(commandInfo.Target);
	}

	protected void Update()
	{
		Parallel.ForEach(Units, AttackNearTarget);
	}

	private void AttackNearTarget(KeyValuePair<Unit, FractionMember> unitWithInfo)
	{
		var unit = unitWithInfo.Key;
		var fraction = unitWithInfo.Value;

		// if enabled
		if (!unit.CanPerformAutoAttack())
			return;

		// Найти врагов
		foreach (var kvp in Units)
		{
			if (kvp.Value.Id == fraction.Id)
				continue;

			var otherUnit = kvp.Key;
			var distance = (unit.Position - kvp.Key.Position).magnitude;
			if (distance < unit.VisibleRange)
			{
				_attackTarget.OnNext(new AutoAttackCommandInfo(unit, otherUnit));
				break;
			}
		}


		// Напасть
	}
}
