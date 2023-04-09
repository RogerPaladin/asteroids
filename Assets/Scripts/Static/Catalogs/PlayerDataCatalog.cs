using System;
using UnityEngine;

namespace Static.Catalogs
{
	[Serializable]
	[CreateAssetMenu(fileName = "PlayerDataCatalog", menuName = "Catalogs/Data/PlayerDataCatalog")]
	public class PlayerDataCatalog: ScriptableObject
	{
		[field: SerializeField] public float MaxSpeed { get; private set; }
		[field: SerializeField] public float MaxRotationSpeed { get; private set; }
		[field: SerializeField] public float Acceleration { get; private set; }
		[field: SerializeField] public float DeAcceleration { get; private set; }
	}
}