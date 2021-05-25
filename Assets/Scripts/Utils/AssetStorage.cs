
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(AssetStorage), menuName = "startegy/AssetStorage")]
public class AssetStorage : ScriptableObject
{
	[SerializeField] private GameObject[] _assets;

	public GameObject GetAsset(string assetName)
	{
		return _assets.FirstOrDefault((asset) => asset.name == assetName);
	}
}
