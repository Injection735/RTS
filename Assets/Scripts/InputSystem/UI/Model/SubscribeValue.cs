using Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubscribeValue<TValue> : ScriptableObject
{
	private TValue _currentValue;

	public TValue Value => _currentValue;

	public void SetValue(TValue value)
	{
		_currentValue = value;
		OnChanged?.Invoke();
	}

	public Action OnChanged;
}
