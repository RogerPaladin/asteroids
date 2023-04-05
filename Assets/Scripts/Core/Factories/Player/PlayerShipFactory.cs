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
	public class PlayerShipFactory : IDiContainerChild
	{
		private readonly DiContainer _diContainer;
		private readonly StaticData _staticData;
		private readonly InputController _inputController;
		private readonly WeaponsFactory _weaponsFactory;
		private readonly UpdateSystem _updateSystem;
		private readonly Camera _camera;

		public PlayerShipFactory(DiContainer diContainer)
		{
			_diContainer = diContainer;
			
			_staticData = _diContainer.Resolve<StaticData>();
			_inputController = _diContainer.Resolve<InputController>();
			_weaponsFactory = _diContainer.Resolve<WeaponsFactory>();
			_updateSystem = _diContainer.Resolve<UpdateSystem>();
			_camera = _diContainer.Resolve<DiCameraProxy>().Camera;
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