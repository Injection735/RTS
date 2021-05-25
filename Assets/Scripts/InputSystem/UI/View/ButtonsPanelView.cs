using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsPanelView : MonoBehaviour
{
	[SerializeField] private Button _moveButton;
	[SerializeField] private Button _attackButton;
	[SerializeField] private Button _produceUnitButton;
	[SerializeField] private Button _patrolButton;
	[SerializeField] private Button _stopButton;

	private Dictionary<Type, Button> _buttons;

	public Action<ICommandExecutor> OnClick;

	protected void Start()
	{
		_buttons = new Dictionary<Type, Button>()
		{
			{ typeof(CommandExecutorBase<IProductionCommand>), _produceUnitButton },
			{ typeof(CommandExecutorBase<IAttackCommand>), _attackButton },
			{ typeof(CommandExecutorBase<IMoveCommand>), _moveButton },
			{ typeof(CommandExecutorBase<IPatrolCommand>), _patrolButton },
			{ typeof(CommandExecutorBase<IStopCommand>), _stopButton }
		};
	}

	public void ClearButtons()
	{
		foreach(var kvp in _buttons)
		{
			kvp.Value.gameObject.SetActive(false);
			kvp.Value.onClick.RemoveAllListeners();
		}
	}

	public void SetButtons(List<ICommandExecutor> commandExecutors)
	{
		if (commandExecutors == null)
			return;

		foreach (var executor in commandExecutors)
		{
			var button = _buttons
				.FirstOrDefault(kvp => kvp.Key.IsInstanceOfType(executor))
				.Value;
			button?.gameObject.SetActive(button != null);
			button?.onClick.AddListener(() => OnClick?.Invoke(executor));

			if (button == null)
				Debug.Log("No button for type " + executor.GetType());
		}
	}
}
