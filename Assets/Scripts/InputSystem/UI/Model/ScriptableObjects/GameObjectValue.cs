using UnityEngine;

[CreateAssetMenu(fileName = nameof(GameObjectValue), menuName = "startegy/" + nameof(GameObjectValue))]
public class GameObjectValue : SubscribeValue<GameObject>
{ }