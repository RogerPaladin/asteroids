using Controllers.Projectiles;
using Static.Catalogs;
using UnityEngine;
using Utils.Spawner;
using Views.Catalogs;

namespace Factories.Projectiles
{
	public class ProjectilesSpawner : AbstractSpawner<AbstractProjectileController, WeaponDataCatalog>
	{
		private readonly ProjectilesFactory _projectilesFactory;
		private readonly Transform _gameContainer;
		private readonly ProjectilesViewCatalog _projectilesViewCatalog;

		public ProjectilesSpawner(ProjectilesFactory projectilesFactory, Transform gameContainer, ProjectilesViewCatalog projectilesViewCatalog)
		{
			_projectilesFactory = projectilesFactory;
			_gameContainer = gameContainer;
			_projectilesViewCatalog = projectilesViewCatalog;
		}

		public void OnLevelEnd()
		{
			ReturnAllActiveToPool();
		}

		public override AbstractProjectileController Spawn(WeaponDataCatalog weaponDataCatalog, Vector2 pos, Quaternion rotation)
		{
			var pool = GetPoolByKey(weaponDataCatalog.Type.ToString());
			var activeList = GetActiveListByKey(weaponDataCatalog.Type.ToString());
			
			var projectile = pool.Get();

			if (projectile == null)
			{
				projectile = _projectilesFactory.Create(weaponDataCatalog);
				var view = _projectilesViewCatalog.Create(projectile.Model);
				view.SetData(projectile.Model, projectile);
				view.SetParent(_gameContainer);
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