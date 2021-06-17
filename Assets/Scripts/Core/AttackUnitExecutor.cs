using Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public partial class AttackUnitExecutor : CommandExecutorBase<IAttackCommand>, ITickable
{
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
