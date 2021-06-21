using UnityEngine;
using UnityEngine.UI;

public class BalancesView : MonoBehaviour
{
	[SerializeField] private Text _balance;

	public Text Balance => _balance;
}
