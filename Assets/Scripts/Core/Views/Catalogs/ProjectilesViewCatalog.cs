using System;
using System.Linq;
using Model.Projectiles;
using Static.Catalogs;
using UnityEngine;
using Views.GamePlay.Projectiles;
using Object = UnityEngine.Object;

namespace Views.Catalogs
{
    [CreateAssetMenu(fileName = "ProjectilesViewCatalog", menuName = "Catalogs/View/ProjectilesViewCatalog")]
    public class ProjectilesViewCatalog : ScriptableObject
    {
        [field: SerializeField] public ProjectileViewCatalog[] Projectiles { get; private set; }

		public ProjectileViewCatalog GetByType(WeaponType type)
		{
			return Projectiles.FirstOrDefault(p => p.Type == type);
		}
		
		public ProjectileView Create(ProjectileModel projectileModel)
		{
			var projectileViewCatalog = GetByType(projectileModel.WeaponDataCatalog.Type);

			if (projectileViewCatalog == null)
				throw new Exception(projectileModel.WeaponDataCatalog.Type.ToString());
			
			return Instantiate(projectileViewCatalog.View);
		}
	}

    [Serializable]
    public class ProjectileViewCatalog
    {
        [field: SerializeField] public WeaponType Type { get; private set; }
        [field: SerializeField] public ProjectileView View { get; private set; }
    }
}