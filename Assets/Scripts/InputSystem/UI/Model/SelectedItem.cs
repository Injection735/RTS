using Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(SelectedItem), menuName = "startegy/SelectedItem")]
public class SelectedItem : ScriptableObject
{
	private ISelectableItem _currentValue;

	public ISelectableItem Value => _currentValue;

	public void SetValue(ISelectableItem item)
	{
		_currentValue = item;
		OnSelected.Invoke();
	}

	public Action OnSelected;
}
