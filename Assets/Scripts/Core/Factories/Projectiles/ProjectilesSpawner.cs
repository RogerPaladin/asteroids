using Controllers.Projectiles;
using Static.Weapons;
using UnityEngine;
using Utils.Collisions;
using Utils.Spawner;
using Views;
using Views.GamePlay.Projectiles;

namespace Factories.Projectiles
{
	public class ProjectilesSpawner : AbstractSpawner<AbstractProjectileController, WeaponConfig>
	{
		private readonly ProjectilesFactory _projectilesFactory;
		private readonly Transform _gameContainer;
		private readonly ViewInstantiator _viewInstantiator;

		public ProjectilesSpawner(ProjectilesFactory projectilesFactory, Transform gameContainer, ViewInstantiator viewInstantiator)
		{
			_projectilesFactory = projectilesFactory;
			_gameContainer = gameContainer;
			_viewInstantiator = viewInstantiator;
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
				var view = (IProjectileView)_viewInstantiator.Instantiate(projectile.Model);
				view.BindModel(projectile.Model);
				view.SetParent(_gameContainer);
				projectile.SetCollisionChecker(new CollisionChecker(view.Collider));
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