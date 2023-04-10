using Controllers.Projectiles;
using Controllers.Projectiles.Bullet;
using Controllers.Projectiles.Laser;
using Core.Controllers.ViewPort;
using Model.Projectiles;
using Model.Projectiles.Laser;
using Static.Catalogs;
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

		public AbstractProjectileController Create(WeaponDataCatalog weaponDataCatalog)
		{
			switch (weaponDataCatalog.Type)
			{
				case WeaponType.Bullet:
					var bulletModel = new ProjectileModel(weaponDataCatalog);
					return new ProjectileBulletController(bulletModel, _viewPortController, _updateSystem);
				case WeaponType.Laser:
					var projectileLaserModel = new ProjectileLaserModel(weaponDataCatalog);
					return new ProjectileLaserController(projectileLaserModel, _viewPortController, _updateSystem);
			}

			return null;
		}
	}
}