using System;
using UnityEngine.Analytics;

public abstract class AwaiterBase<TValue, TAwaited> : IAwaiter<TAwaited> where TValue : IAwaitable<TAwaited>
{
	private bool _isCompleted;
	private Action _continuation;

	protected TValue value;
	protected TAwaited result;

	public AwaiterBase(TValue value)
	{
		this.value = value;
	}

	protected void HandleValueChanged()
	{
		Complete();
		_isCompleted = true;
		_continuation.Invoke();
	}

	protected abstract void Complete();

	public bool IsCompleted => _isCompleted;

	public TAwaited GetResult() => result;

	public void OnCompleted(Action continuation)
	{
		_continuation = continuation;

		if (_isCompleted)
			_continuation?.Invoke();
	}
}
