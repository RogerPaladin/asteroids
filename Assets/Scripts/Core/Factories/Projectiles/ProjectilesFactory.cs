using Controllers.Projectiles;
using Controllers.Projectiles.Bullet;
using Controllers.Projectiles.Laser;
using Model.Projectiles;
using Model.Projectiles.Laser;
using Static.Weapons;
using Utils.DiContainers;
using Utils.Events;
using Utils.OffScreenChecker;
using UnityEngine;

namespace Factories.Projectiles
{
	public class ProjectilesFactory : IDiContainerChild
	{
		private readonly DiContainer _diContainer;
		private readonly UpdateSystem _updateSystem;
		private readonly Camera _camera;

		public ProjectilesFactory(DiContainer diContainer)
		{
			_diContainer = diContainer;

			_updateSystem = _diContainer.Resolve<UpdateSystem>();
			_camera = _diContainer.Resolve<DiCameraProxy>().Camera;
		}

		public AbstractProjectileController Create(WeaponConfig config)
		{
			var offScreenChecker = new OffScreenCheckerTeleport(_camera);
			
			ProjectileModel model = null;

			switch (config.ModelId)
			{
				case WeaponType.BULLET:
					model = new ProjectileModel(config, _updateSystem, offScreenChecker);
					return new ProjectileBulletController(model);
				case WeaponType.LASER:
					var projectileLaserModel = new ProjectileLaserModel(config, _updateSystem, offScreenChecker);
					return new ProjectileLaserController(projectileLaserModel);
			}

			return null;
		}
	}
}