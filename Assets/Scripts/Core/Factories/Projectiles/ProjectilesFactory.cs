using Controllers.Projectiles;
using Controllers.Projectiles.Bullet;
using Controllers.Projectiles.Laser;
using Core.Controllers.ViewPort;
using Model.Projectiles;
using Model.Projectiles.Laser;
using Static.Weapons;
using Utils.Events;

namespace Factories.Projectiles
{
	public class ProjectilesFactory
	{
		private readonly UpdateSystem _updateSystem;
		private readonly ViewPortController _viewPortController;

		public ProjectilesFactory(UpdateSystem updateSystem, ViewPortController viewPortController)
		{
			_updateSystem = updateSystem;
			_viewPortController = viewPortController;
		}

		public AbstractProjectileController Create(WeaponConfig config)
		{
			ProjectileModel model;

			switch (config.ModelId)
			{
				case WeaponType.BULLET:
					model = new ProjectileModel(config);
					return new ProjectileBulletController(model, _viewPortController, _updateSystem);
				case WeaponType.LASER:
					var projectileLaserModel = new ProjectileLaserModel(config);
					return new ProjectileLaserController(projectileLaserModel, _viewPortController, _updateSystem);
			}

			return null;
		}
	}
}