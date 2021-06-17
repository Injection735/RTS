using Core;
using System.Threading;
using UniRx;
using UnityEngine;
using Zenject;

public class GameStateController : MonoBehaviour
{
	[SerializeField] private GameObject _winPanel;
	[SerializeField] private GameObject _loosePanel;

	[Inject] private BuildingsData _data;
	[Inject] private TimeModel _timeModel;

	private Subject<AsyncExtensions.Void> _onWin = new Subject<AsyncExtensions.Void>();
	private Subject<AsyncExtensions.Void> _onLoose = new Subject<AsyncExtensions.Void>();

	private void Start()
	{
		_winPanel.SetActive(false);
		_loosePanel.SetActive(false);

		_onWin.ObserveOn(Scheduler.MainThread).Subscribe(@void => Win()).AddTo(this);
		_onLoose.ObserveOn(Scheduler.MainThread).Subscribe(@void => Loose()).AddTo(this);

		var thread = new Thread(GameStateRoutine);
		thread.Start();
	}

	private void GameStateRoutine()
	{
		while (true)
		{
			if (_data.IsEnemyWin())
			{
				_onLoose.OnNext(new AsyncExtensions.Void());
				Debug.Log("ENEMY WIN");
				return;
			}

			if (_data.IsSelfWin())
			{
				_onWin.OnNext(new AsyncExtensions.Void());
				Debug.Log("SELF WIN");
				return;
			}

			Thread.Sleep(200);
		}
	}

	private void Win()
	{
		_winPanel.SetActive(true);
		_timeModel.Pause();
	}

	private void Loose()
	{
		_loosePanel.SetActive(true);
		_timeModel.Pause();
	}
}
