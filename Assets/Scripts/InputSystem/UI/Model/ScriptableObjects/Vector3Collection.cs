using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = nameof(Vector3Collection), menuName = "startegy/" + nameof(Vector3Collection))]
public class Vector3Collection : SubscribeValue<List<Vector3>>
{ }