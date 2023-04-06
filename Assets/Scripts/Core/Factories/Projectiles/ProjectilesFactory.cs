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
	public class ProjectilesFactory
	{
		private readonly UpdateSystem _updateSystem;
		private readonly Camera _camera;

		public ProjectilesFactory(UpdateSystem updateSystem, Camera camera)
		{
			_updateSystem = updateSystem;
			_camera = camera;
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