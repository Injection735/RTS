using UnityEngine;

namespace Abstractions
{
	public interface ISelectableItem : IHealthHolder
	{
		Sprite Icon { get; }

		void Select(bool isSelected);
	}
}
