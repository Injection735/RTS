using Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SubscribeValue<TValue> : ScriptableObject, IAwaitable<TValue>
{
	private TValue _currentValue;

	public TValue Value => _currentValue;

	public void SetValue(TValue value)
	{
		_currentValue = value;
		OnChanged?.Invoke();
	}

	public Action OnChanged;

	public IAwaiter<TValue> GetAwaiter()
	{
		return new ValueChangeNotifier<TValue>(this);
	}
}
