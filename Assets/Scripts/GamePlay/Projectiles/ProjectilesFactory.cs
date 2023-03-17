using Assets.Scripts.Core;
using Assets.Scripts.Events;
using Assets.Scripts.GamePlay.Collisions;
using Assets.Scripts.GamePlay.Game;
using Assets.Scripts.GamePlay.Projectiles.Bullet;
using Assets.Scripts.GamePlay.Projectiles.Laser;
using Assets.Scripts.GamePlay.Weapons;
using Assets.Scripts.Static.Weapons;
using Assets.Scripts.Utils;
using Assets.Scripts.Utils.OffScreenChecker;
using UnityEngine;

namespace Assets.Scripts.GamePlay.Projectiles
{
	public class ProjectilesFactory
	{
		private readonly DiContainer _diContainer;
		private readonly UpdateSystem _updateSystem;
		private readonly Camera _camera;
		private readonly ProjectileView _bulletView;
		private readonly ProjectileLaserView _projectileLaserView;
		private readonly GameContainer _gameContainer;

		public ProjectilesFactory(DiContainer diContainer)
		{
			_diContainer = diContainer;
			
			_bulletView = _diContainer.Resolve<BasePrefabs>().BulletView;
			_projectileLaserView = _diContainer.Resolve<BasePrefabs>().LaserView;

			_updateSystem = _diContainer.Resolve<UpdateSystem>();
			_camera = _diContainer.Resolve<Camera>();

			_gameContainer = _diContainer.Resolve<GameContainer>();
		}

		public AbstractProjectileController Create(WeaponConfig config)
		{
			var offScreenChecker = new OffScreenCheckerTeleport(_camera);
			
			CollisionChecker collisionChecker = null;
			ProjectileModel model = null;
			ProjectileView view = null;
			
			switch (config.ModelId)
			{
				case WeaponType.BULLET:
					view = Object.Instantiate(_bulletView, _gameContainer.transform);
					collisionChecker = new CollisionChecker(view.Collider);
					model = new ProjectileModel(config, _updateSystem, offScreenChecker, collisionChecker);
					return new ProjectileBulletController(model, view);
				case WeaponType.LASER:
					var projectileLaserView = Object.Instantiate(_projectileLaserView, _gameContainer.transform);
					collisionChecker = new CollisionChecker(projectileLaserView.Collider);
					var projectileLaserModel = new ProjectileLaserModel(config, _updateSystem, offScreenChecker, collisionChecker);
					return new ProjectileLaserController(projectileLaserModel, projectileLaserView);
			}

			return null;
		}
	}
}