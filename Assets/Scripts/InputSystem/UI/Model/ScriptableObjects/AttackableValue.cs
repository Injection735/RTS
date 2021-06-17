using Abstractions;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(AttackableValue), menuName = "startegy/" + nameof(AttackableValue))]
public class AttackableValue : SubscribeValue<IAttackable>
{ }