using Abstractions;
using UniRx;
using UnityEngine;
using Zenject;

public class UnitProductionPanelPresenter : MonoBehaviour
{
	[SerializeField] private SelectedItem _item;
	[SerializeField] private UnitProductionPanelView _view;

	[Inject] private UnitProductionPanel _productionPanel;

	private ISelectableItem _currentSelected;

	protected void Start()
	{
		_item.OnChanged += HandleSelectionChanged;
	}

	private void HandleSelectionChanged()
	{
		if (_currentSelected == _item.Value)
			return;

		_currentSelected = _item.Value;

		_productionPanel.HandleSelecctionChanged(_currentSelected);
		HandleUnitProductionQueue();
	}

	private void HandleUnitProductionQueue()
	{
		_view.ClearAll();
		if (_productionPanel.UnitProductionQueue == null)
			return;

		_view.DisplayQueue(_productionPanel.UnitProductionQueue);

		_productionPanel.UnitProductionQueue.ObserveAdd().Subscribe(addEvent => 
		{
			_view.AddNewItem(addEvent);
		}).AddTo(this);
	}
}
