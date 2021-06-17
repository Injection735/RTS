using Abstractions;
using UnityEngine;

public class FractionMember : MonoBehaviour, IFractionMember
{
	[SerializeField] private int _id;

	public int Id => _id;
}
