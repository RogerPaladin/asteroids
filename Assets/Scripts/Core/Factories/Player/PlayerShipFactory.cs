using Controllers.Player;
using Factories.Weapons;
using Model.Player;
using Static;
using Static.Weapons;
using Utils.DiContainers;
using Utils.Events;
using Utils.OffScreenChecker;
using UnityEngine;
using Utils.Input;

namespace Factories.Player
{
	public class PlayerShipFactory
	{
		private readonly StaticData _staticData;
		private readonly InputController _inputController;
		private readonly WeaponsFactory _weaponsFactory;
		private readonly UpdateSystem _updateSystem;
		private readonly Camera _camera;

		public PlayerShipFactory(StaticData staticData, InputController inputController, WeaponsFactory weaponsFactory, UpdateSystem updateSystem, Camera camera)
		{
			_staticData = staticData;
			_inputController = inputController;
			_weaponsFactory = weaponsFactory;
			_updateSystem = updateSystem;
			_camera = camera;
		}

		public PlayerShipController Create()
		{
			var offScreenChecker = new OffScreenCheckerTeleport(_camera);

			var model = new PlayerShipModel(_staticData.PlayerConfig, 
											_inputController,
											_updateSystem,
											offScreenChecker);
			var bulletWeapon = _weaponsFactory.Create(WeaponType.BULLET, model);
			var laserWeapon = _weaponsFactory.Create(WeaponType.LASER, model);

			var result = new PlayerShipController(model);
			
			result.SetWeaponFirst(bulletWeapon)
				  .SetWeaponSecond(laserWeapon);

			return result;
		}
	}
}