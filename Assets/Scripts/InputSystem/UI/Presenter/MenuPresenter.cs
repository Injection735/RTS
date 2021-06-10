using Abstractions;
using UniRx;
using UnityEngine;
using Zenject;

public class MenuPresenter : MonoBehaviour
{
	[SerializeField] private MenuView _view;

	private ITimeModel _timeModel;

	[Inject]
	public void Init(ITimeModel model)
	{
		_timeModel = model;
		_view.ConinueButton.OnClickAsObservable().Subscribe(unit => HandleContinueButtonClick());
	}

	private void HandleContinueButtonClick()
	{
		gameObject.SetActive(false);
		_timeModel.Unpause();
	}
}
