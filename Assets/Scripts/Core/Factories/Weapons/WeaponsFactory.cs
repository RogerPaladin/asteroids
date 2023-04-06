using Controllers.Weapons;
using Factories.Projectiles;
using Model.Player;
using Model.Weapons;
using Static;
using Static.Weapons;
using Utils.DiContainers;
using Utils.Events;

namespace Factories.Weapons
{
	public class WeaponsFactory
	{
		private readonly ProjectilesSpawner _projectilesSpawner;
		private readonly TimerSystem _timerSystem;
		
		private readonly StaticData _staticData;

		public WeaponsFactory(StaticData staticData, ProjectilesSpawner projectilesSpawner, TimerSystem timerSystem)
		{
			_staticData = staticData;
			_projectilesSpawner = projectilesSpawner;
			_timerSystem = timerSystem;
		}

		public AbstractWeaponController Create(string weaponType, PlayerShipModel playerShipModel)
		{
			WeaponConfig config = _staticData.WeaponsData.GetByType(weaponType);
			WeaponModel model = new WeaponModel(config, _timerSystem);
			
			switch (weaponType)
			{
				case WeaponType.BULLET:
					return new WeaponBulletController(model, playerShipModel, _projectilesSpawner);
				case WeaponType.LASER:
					return new WeaponLaserController(model, playerShipModel, _projectilesSpawner);
			}

			return null;
		}
	}
}