using Abstractions;
using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Core
{
	public class TimeModel : ITimeModel, ITickable
	{
		private readonly ReactiveProperty<float> _gameTime = new ReactiveProperty<float>();
		private bool _isPaused = false;

		public IObservable<int> GameTime => _gameTime.Select(value => (int) value);

		public void Tick()
		{
			if (!_isPaused)
				_gameTime.Value += Time.deltaTime;
		}

		public void Pause()
		{
			_isPaused = true;
			Time.timeScale = 0;
		}

		public void Unpause()
		{
			_isPaused = false;
			Time.timeScale = 1;
		}
	}
}
