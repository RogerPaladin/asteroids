using Assets.Scripts.Static.Weapons;
using Assets.Scripts.Utils.Spawner;
using UnityEngine;

namespace Assets.Scripts.GamePlay.Projectiles
{
	public class ProjectilesSpawner : AbstractSpawner<AbstractProjectileController, WeaponConfig>
	{
		private readonly ProjectilesFactory _projectilesFactory;

		public ProjectilesSpawner(ProjectilesFactory projectilesFactory)
		{
			_projectilesFactory = projectilesFactory;
		}

		public void OnLevelEnd()
		{
			ReturnAllActiveToPool();
		}

		public override AbstractProjectileController Spawn(WeaponConfig weaponConfig, Vector2 pos, Quaternion rotation)
		{
			var pool = GetPoolByKey(weaponConfig.ModelId);
			var activeList = GetActiveListByKey(weaponConfig.ModelId);
			
			var projectile = pool.Get();

			if (projectile == null)
			{
				projectile = _projectilesFactory.Create(weaponConfig);
				projectile.OnDestroyEvent += OnObjDestroy;
			}
			
			projectile.SetPosition(pos);
			projectile.SetRotation(rotation);
			projectile.Activate();

			activeList.Add(projectile);
			
			return projectile;
		}
		
		protected override string GetKey(AbstractProjectileController obj) => obj.Model.ModelId;
	}
}