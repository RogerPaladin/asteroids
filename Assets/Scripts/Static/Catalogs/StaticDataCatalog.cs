using System;
using UnityEngine;

namespace Static
{
    [CreateAssetMenu(fileName = "StaticDataCatalog", menuName = "Catalogs/StaticDataCatalog")]
    public class StaticDataCatalog : ScriptableObject
    {
        [field: SerializeField] public PlayerCatalog PlayerCatalog { get; private set; }
    }
    
    [Serializable]
    public class PlayerCatalog
    {
        [field: SerializeField] public float MaxSpeed { get; private set; }
        [field: SerializeField] public float MaxRotationSpeed { get; private set; }
        [field: SerializeField] public float Acceleration { get; private set; }
        [field: SerializeField] public float DeAcceleration { get; private set; }
    }
}