
using System;
using UnityEngine;
using UnityEngine.UI;

public class TopPanelView : MonoBehaviour
{
	[SerializeField] private Text _time;

	[SerializeField] private Button _button;

	public int Time
	{
		set => _time.text = TimeSpan.FromSeconds(value).ToString();
	}

	public Button MenuButton => _button;
}

