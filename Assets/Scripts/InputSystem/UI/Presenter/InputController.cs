using Abstractions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
	[SerializeField] private Camera _camera;
	[SerializeField] private SelectedItem _item;

	protected void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out var hitInfo))
			{
				var selectableItem  = hitInfo.collider.GetComponent<ISelectableItem>();
				_item.SetValue(selectableItem);
			}
			else
			{
				_item.SetValue(null);
			}
		}
	}
}
