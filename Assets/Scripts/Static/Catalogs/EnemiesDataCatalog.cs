using System;
using UnityEngine;

namespace Static
{
    [CreateAssetMenu(fileName = "EnemiesDataCatalog", menuName = "Catalogs/EnemiesDataCatalog")]
    public class EnemiesDataCatalog : ScriptableObject
    {
        [field: SerializeField] public EnemyDataCatalog[] Enemies { get; private set; }
    }

    [Serializable]
    public class EnemyDataCatalog
    {
        [field: SerializeField] public Enemy Type { get; private set; }
        
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public int Score { get; private set; }
        [field: SerializeField] public int StartCount { get; private set; }
        [field: SerializeField] public float Respawn { get; private set; }
    }

    public enum Enemy
    {
        BigAsteroid,
        SmallAsteroid,
        Ufo,
    }
}