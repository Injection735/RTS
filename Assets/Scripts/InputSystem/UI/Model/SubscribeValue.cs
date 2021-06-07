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
		return new ValueChangedNotifier<TValue>(this);
	}

	public class ValueChangedNotifier<TAwaited> : IAwaiter<TAwaited>
	{
		private SubscribeValue<TAwaited> _value;
		private TAwaited _result;
		private bool _isCompleted;
		private Action _continuation;

		public ValueChangedNotifier(SubscribeValue<TAwaited> value)
		{
			_value = value;
			_value.OnChanged += HandleValueChanged;
		}

		private void HandleValueChanged()
		{
			_value.OnChanged -= HandleValueChanged;
			_isCompleted = true;
			_result = _value.Value;
			_continuation.Invoke();
		}

		public bool IsCompleted => _isCompleted;

		public TAwaited GetResult() => _result;

		public void OnCompleted(Action continuation)
		{
			_continuation = continuation;

			if (_isCompleted)
				_continuation?.Invoke();
		}
	}
}
