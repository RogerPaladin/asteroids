using Controllers.Projectiles;
using Static.Weapons;
using Utils.DiContainers;
using Utils.Spawner;
using UnityEngine;
using Utils.Collisions;
using Utils.Containers.Game;
using Views;
using Views.GamePlay.Projectiles;

namespace Factories.Projectiles
{
	public class ProjectilesSpawner : AbstractSpawner<AbstractProjectileController, WeaponConfig>, IDiContainerChild
	{
		private readonly ProjectilesFactory _projectilesFactory;
		private readonly GameContainer _gameContainer;
		private readonly ViewBinder _viewBinder;

		public ProjectilesSpawner(ProjectilesFactory projectilesFactory, GameContainer gameContainer, ViewBinder viewBinder)
		{
			_projectilesFactory = projectilesFactory;
			_gameContainer = gameContainer;
			_viewBinder = viewBinder;
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
				var view = (IProjectileView)_viewBinder.TryBindViewByModel(projectile.Model);
				projectile.SetParent(_gameContainer.transform);
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