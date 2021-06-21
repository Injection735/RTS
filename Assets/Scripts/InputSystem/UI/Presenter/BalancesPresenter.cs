using Abstractions;
using UniRx;
using UnityEngine;
using Zenject;

public class BalancesPresenter : MonoBehaviour
{
	[SerializeField] private BalancesView _view;
	private BalancesData _model;

	[Inject] private void Init(BalancesData model)
	{
		_model = model;
		_model.Coins.Subscribe(UpdateView).AddTo(this);
	}

	private void UpdateView(int balance)
	{
		_view.Balance.text = balance.ToString();
	}
}
