using Controllers.Weapons;
using Factories.Projectiles;
using Model.Player;
using Model.Weapons;
using Static;
using Static.Catalogs;
using Utils.Events;
using WeaponType = Static.Catalogs.WeaponType;

namespace Factories.Weapons
{
	public class WeaponsFactory
	{
		private readonly ProjectilesSpawner _projectilesSpawner;
		private readonly TimerSystem _timerSystem;
		
		private readonly WeaponsDataCatalog _weaponsDataCatalog;

		public WeaponsFactory(WeaponsDataCatalog weaponsDataCatalog, ProjectilesSpawner projectilesSpawner, TimerSystem timerSystem)
		{
			_weaponsDataCatalog = weaponsDataCatalog;
			_projectilesSpawner = projectilesSpawner;
			_timerSystem = timerSystem;
		}

		public AbstractWeaponController Create(WeaponType weaponType, PlayerShipModel playerShipModel)
		{
			WeaponDataCatalog weaponDataCatalog = _weaponsDataCatalog.GetByType(weaponType);
			WeaponModel model = new WeaponModel(weaponDataCatalog, _timerSystem);
			
			switch (weaponType)
			{
				case WeaponType.Bullet:
					return new WeaponBulletController(model, playerShipModel, _projectilesSpawner);
				case WeaponType.Laser:
					return new WeaponLaserController(model, playerShipModel, _projectilesSpawner);
			}

			return null;
		}
	}
}