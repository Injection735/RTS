using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedItemPresenter : MonoBehaviour
{
	[SerializeField] SelectedItem _item;
	[SerializeField] SelectedItemView _view;

	protected void Start()
	{
		_item.OnChanged += UpdateView;
		UpdateView();
	}

	private void UpdateView()
	{
		_view.gameObject.SetActive(_item.Value != null);

		if (_item.Value == null)
			return;

		_item.Value.Select(_item.Value != null);
		_view.Icon = _item.Value.Icon;
		_view.Text = $"{_item.Value.Health}/{_item.Value.MaxHp}";
		_view.HealthInPercent = _item.Value.Health / _item.Value.MaxHp;
	}
}
