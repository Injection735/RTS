
using Abstractions;
using Core;
using UnityEngine;
using UniRx;
using Zenject;

public class TopPanelPresenter : MonoBehaviour
{
	[SerializeField] private TopPanelView _view;
	[SerializeField] private GameObject _menu;

	private ITimeModel _timeModel;

	[Inject]
	public void Init(ITimeModel model)
	{
		_timeModel = model;
		model.GameTime.Subscribe(observer => _view.Time = observer);
		_view.MenuButton.OnClickAsObservable().Subscribe(unit => HandleMenuButtonClick());
	}

	private void HandleMenuButtonClick()
	{
		_menu.SetActive(true);
		_timeModel.Pause();
	}
}

