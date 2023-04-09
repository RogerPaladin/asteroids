using System;
using System.Linq;
using Model.Enemies;
using Static.Catalogs;
using UnityEngine;
using Views.GamePlay.Enemies;

namespace Views.Catalogs
{
    [CreateAssetMenu(fileName = "EnemiesViewCatalog", menuName = "Catalogs/View/EnemiesViewCatalog")]
    public class EnemiesViewCatalog : ScriptableObject
    {
        [field: SerializeField] public EnemyViewCatalog[] Enemies { get; private set; }

		public EnemyViewCatalog GetByType(EnemyType type)
		{
			return Enemies.FirstOrDefault(e => e.Type == type);
		}
		
		public EnemyView Create(EnemyModel enemyModel)
		{
			var effectViewCatalog = GetByType(enemyModel.EnemyDataCatalog.Type);

			if (effectViewCatalog == null)
				throw new Exception(enemyModel.EnemyDataCatalog.Type.ToString());
			
			return Instantiate(effectViewCatalog.View);
		}
	}

    [Serializable]
    public class EnemyViewCatalog
    {
        [field: SerializeField] public EnemyType Type { get; private set; }
        [field: SerializeField] public EnemyView View { get; private set; }
    }
}