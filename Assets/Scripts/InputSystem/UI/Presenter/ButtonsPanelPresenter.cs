using Abstractions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Core;
using Zenject;
using UniRx;
using System;

public class ButtonsPanelPresenter : MonoBehaviour
{
	[SerializeField] private SelectedItem _item;
	[SerializeField] private ButtonsPanelView _view;

	[Inject] private ButtonPanel _buttonPanel;

	[SerializeField] private AssetStorage _storage;

	private ISelectableItem _currentSelected;
	private Dictionary<CommandExecutorType, IDisposable> disposables = new Dictionary<CommandExecutorType, IDisposable>();


	protected void Start()
	{
		_item.OnChanged += HandleSelectionChanged;
		SetButton();
		InitDisposables();
		//_view.OnClick += HandleClick;
	}

	private void InitDisposables()
	{
		foreach (CommandExecutorType enumValue in System.Enum.GetValues(typeof(CommandExecutorType)))
			disposables.Add(enumValue, null);
	}

	private void HandleSelectionChanged()
	{
		_buttonPanel.HandleSelecctionChanged();
		SetButton();
	}

	private void SetButton()
	{
		if (_currentSelected == _item.Value)
			return;
		_currentSelected = _item.Value;

		var commandExecutors = (_currentSelected as Component)?.GetComponentsInParent<ICommandExecutor>().ToList();

		ClearButtons();
		SetButtons(commandExecutors);
	}

	private void ClearButtons()
	{
		foreach(var kvp in _view.Buttons)
		{
			kvp.Value.gameObject.SetActive(false);
			disposables[kvp.Key]?.Dispose();
		}
	}

	private void SetButtons(List<ICommandExecutor> commandExecutors)
	{
		if (commandExecutors == null)
			return;

		foreach (var executor in commandExecutors)
		{
			var kvpButton = _view.Buttons.FirstOrDefault(kvp => ButtonsPanelView.GetType(kvp.Key).IsInstanceOfType(executor));
			var button = kvpButton.Value;
			button?.gameObject.SetActive(button != null);

			if (button != null)
				disposables[kvpButton.Key] = button.OnClickAsObservable().Subscribe(unit => HandleClick(executor));

			if (button == null)
				Debug.Log("No button for type " + executor.GetType());
		}
	}

	private void HandleClick(ICommandExecutor executor)
	{
		_buttonPanel.HandleClick(executor);
	}
}
