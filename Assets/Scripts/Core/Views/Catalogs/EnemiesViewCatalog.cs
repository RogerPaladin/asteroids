using System;
using UnityEngine;
using Views.GamePlay.Enemies;

namespace Static
{
    [CreateAssetMenu(fileName = "EnemiesViewCatalog", menuName = "Catalogs/EnemiesViewCatalog")]
    public class EnemiesViewCatalog : ScriptableObject
    {
        [field: SerializeField] public EnemyViewCatalog[] Enemies { get; private set; }
    }

    [Serializable]
    public class EnemyViewCatalog
    {
        [field: SerializeField] public Enemy Type { get; private set; }
        [field: SerializeField] public EnemyView View { get; private set; }
    }
}