using System;
using System.Linq;
using UnityEngine;

namespace Static.Catalogs
{
    [CreateAssetMenu(fileName = "EnemiesDataCatalog", menuName = "Catalogs/Data/EnemiesDataCatalog")]
    public class EnemiesDataCatalog : ScriptableObject
    {
        [field: SerializeField] public EnemyDataCatalog[] Enemies { get; private set; }

		public EnemyDataCatalog GetByType(EnemyType type)
		{
			return Enemies.FirstOrDefault(e => e.Type == type);
		}
	}

    [Serializable]
    public class EnemyDataCatalog
    {
        [field: SerializeField] public EnemyType Type { get; private set; }
        
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public int Score { get; private set; }
        [field: SerializeField] public int StartCount { get; private set; }
        [field: SerializeField] public float Respawn { get; private set; }
    }

    public enum EnemyType
    {
        BigAsteroid,
        SmallAsteroid,
        Ufo,
    }
}