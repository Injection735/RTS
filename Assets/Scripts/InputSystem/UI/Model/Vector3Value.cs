using Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(Vector3Value), menuName = "startegy/Vector3Value")]
public class Vector3Value : ScriptableObject
{
	private Vector3 _currentValue;

	public Vector3 Value => _currentValue;

	public void SetValue(Vector3 value)
	{
		_currentValue = value;
		OnChanged.Invoke();
	}

	public Action OnChanged;
}
