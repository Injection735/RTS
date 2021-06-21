using Abstractions;
using System;
using System.Threading;
using UnityEngine;
using Zenject;

public partial class AttackUnitExecutor : CommandExecutorBase<IAttackCommand>
{
	public class AttackOperation : IAwaitable<AsyncExtensions.Void>
	{
		private AttackUnitExecutor _attacker;
		private IAttackable _target;
		private bool _isCanceled = false;

		public event Action OnComplete;

		public AttackOperation(IAttackable target, AttackUnitExecutor attacker)
		{
			_target = target;
			_attacker = attacker;

			var thread = new Thread(AttackRoutine);
			thread.Start();
		}

		private void AttackRoutine(object obj)
		{
			while (true)
			{
				if (_target.Health <= 0 || _isCanceled)
				{
					OnComplete?.Invoke();
					return;
				}

				Vector3 targetPosition;
				Vector3 attackerPosition;

				lock (_attacker)
				{
					targetPosition = _attacker._targetPosition;
					attackerPosition = _attacker._attackerPosition;
				}

				var distance = (targetPosition - attackerPosition).magnitude;

				if (distance > _attacker._attackDistance)
				{
					// TODO walk
					_attacker._destination.OnNext(targetPosition);
					Thread.Sleep(200);
				}
				else
				{
					_attacker._attackTarget.OnNext(_target);
					// атаковать
					Thread.Sleep((int) (_attacker._attackSpeed * 1000));
				}
			}
		}

		public IAwaiter<AsyncExtensions.Void> GetAwaiter()
		{
			return new AttackOperationAwaiter(this);
		}

		public void Cancel()
		{
			_isCanceled = true;
		}
	}
}