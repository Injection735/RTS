using UnityEngine;

namespace Core
{
	public class ProduceCommand : IProductionCommand
	{
		[InjectAsset("Ellen")] private GameObject _unitPrefab;
		public GameObject UnitPrefab => _unitPrefab;
	}
}