using Abstractions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour
{
	[SerializeField] private Camera _camera;
	[SerializeField] private SelectedItem _item;
	[SerializeField] private Vector3Value _currentGroundPosition;

	[SerializeField] private EventSystem _eventSystem;

	protected void Update()
	{
		if (_eventSystem.IsPointerOverGameObject())
			return;

		// выделение ЛКМ
		if (Input.GetMouseButtonDown(0))
		{
			if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out var hitInfo))
			{
				var selectableItem  = hitInfo.collider.GetComponent<ISelectableItem>();
				_item.SetValue(selectableItem);
			}
			else
				_item.SetValue(null);
		}

		// перемещение ПКМ
		if (Input.GetMouseButtonDown(1))
		{
			if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out var hitInfo))
			{
				_currentGroundPosition.SetValue(hitInfo.point);
			}
		}
	}
}
