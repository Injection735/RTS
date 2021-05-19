using UnityEngine;

namespace Abstractions
{
	public interface ISelectableItem
	{
		Sprite Icon { get; }
		float Health { get; }
		float MaxHp { get; }

		void Select(bool isSelected);
	}
}
