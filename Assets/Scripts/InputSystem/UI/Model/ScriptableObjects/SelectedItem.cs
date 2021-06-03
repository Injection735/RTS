using Abstractions;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(SelectedItem), menuName = "startegy/" + nameof(SelectedItem))]
public class SelectedItem : SubscribeValue<ISelectableItem>
{ }