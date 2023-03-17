using Assets.Scripts.Events;
using Assets.Scripts.GamePlay.Player;
using Assets.Scripts.GamePlay.Projectiles;
using Assets.Scripts.Static;
using Assets.Scripts.Static.Weapons;
using Assets.Scripts.Utils;

namespace Assets.Scripts.GamePlay.Weapons
{
	public class WeaponsFactory
	{
		private readonly DiContainer _diContainer;
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