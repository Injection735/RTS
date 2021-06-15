using Zenject;
using Core;
using System;
using System.Threading;
using System.Diagnostics;
using UnityEngine;

public abstract class CommandCreatorBase<T> where T : ICommand
{
	public void CreateCommand(ICommandExecutor commandExecutor, Action<T> onCreate)
	{
		if (commandExecutor as CommandExecutorBase<T>)
			CreateSpecificCommand(onCreate);
	}

	protected abstract void CreateSpecificCommand(Action<T> onCreate);

	public virtual void CancelCommand()
	{ }
}

public abstract class CommandCreatorCancelable<T, TParam> : CommandCreatorBase<T> where T : ICommand
{
	[Inject] private AssetStorage _context;
	[Inject] private IAwaitable<TParam> _param;
	private CancellationTokenSource _tokenSource;

	public void CreateCommand(ICommandExecutor commandExecutor, Action<T> onCreate)
	{
		if (commandExecutor as CommandExecutorBase<T>)
			CreateSpecificCommand(onCreate);
	}

	protected override async void CreateSpecificCommand(Action<T> onCreate)
	{
		_tokenSource = new CancellationTokenSource();
		try
		{
			var nextClick = await _param.AsTask().WithCancelation(_tokenSource.Token);
			onCreate?.Invoke(_context.Inject(CreateSpecificCommandWithParameter(nextClick)));
		}
		catch (OperationCanceledException e)
		{
			UnityEngine.Debug.Log("Operation canceled");
		}
	}

	protected abstract T CreateSpecificCommandWithParameter(TParam param);

	public override void CancelCommand()
	{
		if (_tokenSource == null)
			return;

		_tokenSource.Cancel();
		_tokenSource.Dispose();
		_tokenSource = null;
	}
}

public class ProduceUnitCommandCreator : CommandCreatorBase<IProductionCommand>
{
	[Inject] private AssetStorage _context;

	protected override void CreateSpecificCommand(Action<IProductionCommand> onCreate)
	{
		onCreate.Invoke(_context.Inject(new ProduceCommand()));
	}
}

public class MoveCommandCreator : CommandCreatorCancelable<IMoveCommand, Vector3>
{
	protected override IMoveCommand CreateSpecificCommandWithParameter(Vector3 param) => new MoveCommand(param);
}

public class StopCommandCreator : CommandCreatorBase<IStopCommand>
{
	[Inject] private AssetStorage _context;

	protected override void CreateSpecificCommand(Action<IStopCommand> onCreate)
	{
		onCreate.Invoke(_context.Inject(new StopCommand()));
	}
}

public class AttackCommandCreator : CommandCreatorBase<IAttackCommand>
{
	[Inject] private AssetStorage _context;

	private Action<IAttackCommand> _onCreate;

	private GameObjectValue _enemy;

	[Inject]
	private void Init(GameObjectValue enemy)
	{
		_enemy = enemy;
		_enemy.OnChanged += HandleAttack;
	}

	private void HandleAttack()
	{
		_onCreate?.Invoke(_context.Inject(new AttackCommand(_enemy.Value)));
	}

	protected override void CreateSpecificCommand(Action<IAttackCommand> onCreate)
	{
		_onCreate = onCreate;
	}
}

public class PatrolCommandCreator : CommandCreatorBase<IPatrolCommand>
{
	[Inject] private AssetStorage _context;

	private Action<IPatrolCommand> _onCreate;

	private Vector3Collection _patrolPoints;

	[Inject]
	private void Init(Vector3Collection patrolPoints)
	{
		_patrolPoints = patrolPoints;
		_patrolPoints.OnChanged += HandleCurrentGroundPositionChanged;
	}

	private void HandleCurrentGroundPositionChanged()
	{
		_onCreate?.Invoke(_context.Inject(new PatrolCommand(_patrolPoints.Value)));
	}

	protected override void CreateSpecificCommand(Action<IPatrolCommand> onCreate)
	{
		_onCreate = onCreate;
	}
}
