using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum CommandExecutorType
{
	Produce,
	Attack,
	Move,
	Patrol,
	Stop,
	SetCollectionPoint
}

public class ButtonsPanelView : MonoBehaviour
{
	[SerializeField] private Button _moveButton;
	[SerializeField] private Button _attackButton;
	[SerializeField] private Button _produceUnitButton;
	[SerializeField] private Button _patrolButton;
	[SerializeField] private Button _stopButton;
	[SerializeField] private Button _setCollectionPointButton;

	private Dictionary<CommandExecutorType, Button> _buttons;

	public Dictionary<CommandExecutorType, Button> Buttons => _buttons;

	public static Type GetType(CommandExecutorType type)
	{
		switch (type)
		{
			case CommandExecutorType.Produce:
				return typeof(CommandExecutorBase<IProductionCommand>);
			case CommandExecutorType.Attack:
				return typeof(CommandExecutorBase<IAttackCommand>);
			case CommandExecutorType.Move:
				return typeof(CommandExecutorBase<IMoveCommand>);
			case CommandExecutorType.Patrol:
				return typeof(CommandExecutorBase<IPatrolCommand>);
			case CommandExecutorType.Stop:
				return typeof(CommandExecutorBase<IStopCommand>);
			case CommandExecutorType.SetCollectionPoint:
				return typeof(CommandExecutorBase<ISetCollectionPointCommand>);
		}

		throw new Exception("Can't recognize CommandExecutorType");
	}

	protected void Start()
	{
		_buttons = new Dictionary<CommandExecutorType, Button>()
		{
			{ CommandExecutorType.Produce, _produceUnitButton },
			{ CommandExecutorType.Attack, _attackButton },
			{ CommandExecutorType.Move, _moveButton },
			{ CommandExecutorType.Patrol, _patrolButton },
			{ CommandExecutorType.Stop, _stopButton },
			{ CommandExecutorType.SetCollectionPoint, _setCollectionPointButton }
		};
	}
}
