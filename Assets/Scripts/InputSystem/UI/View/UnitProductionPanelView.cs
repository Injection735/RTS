using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class UnitProductionPanelView : MonoBehaviour
{
	[SerializeField] private Image _productionIcon;
	[SerializeField] private Text _productionTimeLeft;
	[SerializeField] private Slider _productionProgress;

	[SerializeField] private Sprite _emptyIcon;

	[SerializeField] private List<Image> _images;

	public void DisplayQueue(IList<IUnitProductionTask> tasks)
	{
		if (tasks.Count > 0)
		{
			var currentTask = tasks[0];
			_productionIcon.sprite = currentTask.Icon;
			_productionTimeLeft.text = TimeSpan.FromSeconds((int) currentTask.ProductionTime).ToString();
			_productionProgress.value = currentTask.ProductionTimeLeft.Value / currentTask.ProductionTime;
			return;
		}

		for (int i = 1; i < tasks.Count; i++)
			_images[i - 1].sprite = tasks[i].Icon;
	}

	public void AddNewItem(CollectionAddEvent<IUnitProductionTask> newElement)
	{
		if (newElement.Index == 0)
		{
			var currentTask = newElement.Value;
			_productionIcon.sprite = currentTask.Icon;
			currentTask.ProductionTimeLeft.Subscribe((time) =>
			{
				UpdateTimeProgress(time, currentTask.ProductionTime);
			});
			return;
		}
		
		_images[newElement.Index - 1].sprite = newElement.Value.Icon;
	}

	public void ClearAll()
	{
		_productionIcon.sprite = _emptyIcon;
		_productionTimeLeft.text = TimeSpan.FromSeconds(0).ToString();
		_productionProgress.value = 0;

		foreach(var image in _images)
			image.sprite = _emptyIcon;
	}

	private void UpdateTimeProgress(float timeLeft, float productionTime)
	{
		_productionTimeLeft.text = TimeSpan.FromSeconds((int) timeLeft).ToString();
		_productionProgress.value = timeLeft / productionTime;
	}
}
