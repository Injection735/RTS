using Abstractions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour
{
	[SerializeField] private Camera _camera;
	[SerializeField] private SelectedItem _item;

	[SerializeField] private EventSystem _eventSystem;


	protected void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			if (_eventSystem.IsPointerOverGameObject())
				return;

			if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out var hitInfo))
			{
				var selectableItem  = hitInfo.collider.GetComponent<ISelectableItem>();
				_item.SetValue(selectableItem);
			}
			else
				_item.SetValue(null);
		}
	}
}
