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
		currentGroundPosition.OnChanged += HandleCurrentGroundPositionChanged;
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
