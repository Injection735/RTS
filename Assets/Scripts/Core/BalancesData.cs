using UniRx;

public class BalancesData
{
	public ReactiveProperty<int> Coins { get; set; } = new ReactiveProperty<int>(100);
}
