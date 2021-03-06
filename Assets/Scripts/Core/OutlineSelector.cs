using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OutlineSelector : MonoBehaviour
{
	[SerializeField] private Renderer[] _renderers;
	[SerializeField] private Material _outlineMaterial;

	private bool _isSelected;

	public void SetSelected(bool isSelected)
	{
		if (isSelected == _isSelected)
			return;

		foreach(Renderer renderer in _renderers)
		{
			var materialsList = renderer.materials.ToList();
			if (isSelected)
				materialsList.Add(_outlineMaterial);
			else
				materialsList.RemoveAt(materialsList.Count - 1);

			renderer.materials = materialsList.ToArray();
		}

		_isSelected = isSelected;
	}

}
