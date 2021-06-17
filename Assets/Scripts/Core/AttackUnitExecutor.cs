using Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class AttackUnitExecutor : CommandExecutorBase<IAttackCommand>, ITickable
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
					// атаковать
					_attacker._target.ReceiveDamage(_attacker._attackDamage);
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
	[SerializeField] private NavMeshAgent _agent;
	[Inject(Id = "Distance")] private float _attackDistance = 1.0f;
	[Inject(Id = "AttackSpeed")] private float _attackSpeed = 0.3f;
	[Inject(Id = "AttackDamage")] private float _attackDamage = 0.3f;

	private IAttackable _target;

	private AttackOperation _currentAttackOperation;

	private Vector3 _targetPosition;
	private Vector3 _attackerPosition;

	private Subject<Vector3> _destination = new Subject<Vector3>();
	private Subject<IAttackable> _attackTarget = new Subject<IAttackable>();

	public void Tick()
	{
		Debug.Log("INVOKED " + (_target == null || _currentAttackOperation == null).ToString());
		if (_target == null || _currentAttackOperation == null)
			return;

		lock (this)
		{
			_attackerPosition = gameObject.transform.position;
			_targetPosition = _target.Position;
		}
	}

	protected async override Task ExecuteConcreteCommand(IAttackCommand command)
	{
		_target = command.Target;
		_currentAttackOperation = new AttackOperation(_target, this);

		try
		{
			await _currentAttackOperation;
		}
		catch (OperationCanceledException e)
		{
			Debug.Log("Operation canceled");
		}
	}

	[Inject]
	protected void Init()
	{
		_destination.ObserveOn(Scheduler.MainThread).Subscribe(MoveTo).AddTo(this);
		_attackTarget.ObserveOn(Scheduler.MainThread).Subscribe(AttackTarget).AddTo(this);
	}

	protected void MoveTo(Vector3 to)
	{
		_agent.SetDestination(to);
	}

	protected void AttackTarget(IAttackable target)
	{
		target.ReceiveDamage(_attackDamage);

		if (target.Health <= 0)
		{
			_currentAttackOperation.Cancel();
			_currentAttackOperation = null;
		}
	}
}
