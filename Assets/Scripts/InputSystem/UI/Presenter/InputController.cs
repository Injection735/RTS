using Abstractions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UniRx;
using System;

public class InputController : MonoBehaviour
{
	[SerializeField] private Camera _camera;
	[SerializeField] private SelectedItem _item;
	[SerializeField] private Vector3Value _currentGroundPosition;
	[SerializeField] private Vector3Collection _patrolPoints;
	[SerializeField] private GameObjectValue _selectedEnemy;
	[SerializeField] private HoldValue _holdValue;

	[SerializeField] private EventSystem _eventSystem;

	private void Start()
	{
		Observable.EveryUpdate()
			.Where(_ => Input.GetMouseButtonDown(0) && !_eventSystem.IsPointerOverGameObject())
			.Subscribe(value => OnLeftClick());

		Observable.EveryUpdate()
			.Where(_ => Input.GetMouseButtonDown(1) && !_eventSystem.IsPointerOverGameObject())
			.Subscribe(value => OnRightClick());
	}

	private void OnLeftClick()
	{
		if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out var hitInfo))
		{
			var selectableItem  = hitInfo.collider.GetComponent<ISelectableItem>();
			_item.SetValue(selectableItem);
		}
		else
			_item.SetValue(null);
	}

	private void OnRightClick()
	{
		bool raycastResult = Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out var hitInfo);

		if (!raycastResult)
			return;

		if (Input.GetKey(KeyCode.LeftShift))
		{
			_patrolPoints.Value.Add(hitInfo.point);
			_patrolPoints.SetValue(_patrolPoints.Value);
			return;
		}

		if (hitInfo.transform.gameObject.tag == "Enemy") // TODO const
		{
			_selectedEnemy.SetValue(hitInfo.transform.gameObject);
			return;
		}

		_patrolPoints.Value.Clear();
		_patrolPoints.SetValue(_patrolPoints.Value);

		_currentGroundPosition.SetValue(hitInfo.point);
	}
}
