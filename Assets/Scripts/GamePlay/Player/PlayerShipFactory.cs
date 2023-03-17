using Assets.Scripts.Core;
using Assets.Scripts.Events;
using Assets.Scripts.GamePlay.Collisions;
using Assets.Scripts.GamePlay.Game;
using Assets.Scripts.GamePlay.Input;
using Assets.Scripts.GamePlay.Weapons;
using Assets.Scripts.Static;
using Assets.Scripts.Utils;
using Assets.Scripts.Utils.OffScreenChecker;
using UnityEngine;

namespace Assets.Scripts.GamePlay.Player
{
	public class PlayerShipFactory
	{
		private readonly DiContainer _diContainer;
		private readonly PlayerShipView _shipView;
		private readonly StaticData _staticData;
		private readonly InputController _inputController;
		private readonly WeaponsFactory _weaponsFactory;
		private readonly UpdateSystem _updateSystem;
		private readonly Camera _camera;

		public PlayerShipFactory(DiContainer diContainer)
		{
			_diContainer = diContainer;
			
			_shipView = _diContainer.Resolve<BasePrefabs>().PlayerShipView;
			_staticData = _diContainer.Resolve<StaticData>();
			_inputController = _diContainer.Resolve<InputController>();
			_weaponsFactory = _diContainer.Resolve<WeaponsFactory>();
			_updateSystem = _diContainer.Resolve<UpdateSystem>();
			_camera = _diContainer.Resolve<Camera>();
		}

		public PlayerShipController Create(GameContainer gameContainer)
		{
			var view = Object.Instantiate(_shipView, gameContainer.transform);
			
			var offScreenChecker = new OffScreenCheckerTeleport(_camera);
			var collisionChecker = new CollisionChecker(view.Collider);
			
			var model = new PlayerShipModel(_staticData.PlayerConfig, 
											_inputController,
											_updateSystem,
											collisionChecker,
											offScreenChecker,
											view.ProjectileSpawnPoint.position);
			var bulletWeapon = _weaponsFactory.Create(WeaponType.BULLET, model);
			var laserWeapon = _weaponsFactory.Create(WeaponType.LASER, model);
			
			model.SetWeaponFirst(bulletWeapon)
				 .SetWeaponSecond(laserWeapon);

			var result = new PlayerShipController(model, view);

			return result;
		}
	}
}