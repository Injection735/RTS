using Abstractions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Core;

public class ButtonsPanelPresenter : MonoBehaviour
{
	[SerializeField] private SelectedItem _item;
	[SerializeField] private ButtonsPanelView _view;

	[SerializeField] private AssetStorage _storage;

	private ISelectableItem _currentSelected;

	protected void Start()
	{
		_item.OnSelected += SetButton;
		SetButton();
		_view.OnClick += HandleClick;
	}

	private void SetButton()
	{
		if (_currentSelected == _item.Value)
			return;
		_currentSelected = _item.Value;

		var commandExecutors = (_currentSelected as Component)?.GetComponentsInParent<ICommandExecutor>().ToList();

		_view.ClearButtons();
		_view.SetButtons(commandExecutors);
	}

	private void HandleClick(ICommandExecutor executor)
	{

		if (executor as CommandExecutorBase<IProductionCommand>)
		{
			executor.Execute(_storage.Inject(new ProduceCommand()));
		}
		else
			Debug.Log("Not handled " + executor.GetType());
	}
}
