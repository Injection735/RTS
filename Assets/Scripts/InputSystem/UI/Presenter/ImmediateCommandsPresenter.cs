using System.Linq;
using UnityEngine;
using Zenject;

public class ImmediateCommandsPresenter : MonoBehaviour
{
	[SerializeField] private SelectedItem _item;
	[Inject] private ImmediateCommands _commands;

	protected void Start()
	{
		_item.OnChanged += HandleSelectionChanged;
	}

	private void HandleSelectionChanged()
	{
		var commandExecutors = (_item.Value as Component)?.GetComponentsInParent<ICommandExecutor>().ToList();
		if (commandExecutors == null)
			return;

		_commands.TryExecuteImmediateCommand(commandExecutors);
	}
}
