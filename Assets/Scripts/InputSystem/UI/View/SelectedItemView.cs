using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedItemView : MonoBehaviour
{
	[SerializeField] private Image _icon;
	[SerializeField] private Text _text;
	[SerializeField] private Slider _slider;

	public Sprite Icon { set => _icon.sprite = value; }
	public string Text { set => _text.text = value; }
	public float HealthInPercent { set => _slider.value = value; }
}
