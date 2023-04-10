using Controllers.Player;
using Core.Controllers.ViewPort;
using Factories.Weapons;
using Model.Input;
using Model.Player;
using Static.Catalogs;
using Utils.Events;

namespace Factories.Player
{
	public class PlayerShipFactory
	{
		private readonly PlayerDataCatalog _playerDataCatalog;
		private readonly InputModel _inputModel;
		private readonly WeaponsFactory _weaponsFactory;
		private readonly UpdateSystem _updateSystem;
		private readonly ViewPortController _viewPortController;

		public PlayerShipFactory(PlayerDataCatalog playerDataCatalog, InputModel inputModel, WeaponsFactory weaponsFactory, UpdateSystem updateSystem, ViewPortController viewPortController)
		{
			_playerDataCatalog = playerDataCatalog;
			_inputModel = inputModel;
			_weaponsFactory = weaponsFactory;
			_updateSystem = updateSystem;
			_viewPortController = viewPortController;
		}

		public PlayerShipController Create()
		{
			var model = new PlayerShipModel(_playerDataCatalog);
			var bulletWeapon = _weaponsFactory.Create(WeaponType.Bullet, model);
			var laserWeapon = _weaponsFactory.Create(WeaponType.Laser, model);

			var result = new PlayerShipController(model, _inputModel, _updateSystem, _viewPortController);
			
			result.SetWeaponFirst(bulletWeapon)
				  .SetWeaponSecond(laserWeapon);

			return result;
		}
	}
}