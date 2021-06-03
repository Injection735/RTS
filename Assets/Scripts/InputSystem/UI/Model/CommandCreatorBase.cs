using Zenject;
using Core;
using System;

public abstract class CommandCreatorBase<T> where T : ICommand
{
	public void CreateCommand(ICommandExecutor commandExecutor, Action<T> onCreate)
	{
		if (commandExecutor as CommandExecutorBase<T>)
			CreateSpecificCommand(onCreate);
	}

	protected abstract void CreateSpecificCommand(Action<T> onCreate);
}

public class ProduceUnitCommandCreator : CommandCreatorBase<IProductionCommand>
{
	[Inject] private AssetStorage _context;

	protected override void CreateSpecificCommand(Action<IProductionCommand> onCreate)
	{
		onCreate.Invoke(_context.Inject(new ProduceCommandHeir()));
	}
}

public class MoveCommandCreator : CommandCreatorBase<IMoveCommand>
{
	[Inject] private AssetStorage _context;

	private Action<IMoveCommand> _onCreate;

	private Vector3Value _currentGroundPosition;

	[Inject]
	private void Init(Vector3Value currentGroundPosition)
	{
		_currentGroundPosition = currentGroundPosition;
		_currentGroundPosition.OnChanged += HandleCurrentGroundPositionChanged;
	}

	private void HandleCurrentGroundPositionChanged()
	{
		_onCreate?.Invoke(_context.Inject(new MoveCommand(_currentGroundPosition.Value)));
	}

	protected override void CreateSpecificCommand(Action<IMoveCommand> onCreate)
	{
		_onCreate = onCreate;
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
