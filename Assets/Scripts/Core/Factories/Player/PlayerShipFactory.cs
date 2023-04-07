using Controllers.Player;
using Core.Controllers.ViewPort;
using Factories.Weapons;
using Model.Input;
using Model.Player;
using Static;
using Static.Weapons;
using Utils.Events;

namespace Factories.Player
{
	public class PlayerShipFactory
	{
		private readonly StaticData _staticData;
		private readonly InputModel _inputModel;
		private readonly WeaponsFactory _weaponsFactory;
		private readonly UpdateSystem _updateSystem;
		private readonly ViewPortController _viewPortController;

		public PlayerShipFactory(StaticData staticData, InputModel inputModel, WeaponsFactory weaponsFactory, UpdateSystem updateSystem, ViewPortController viewPortController)
		{
			_staticData = staticData;
			_inputModel = inputModel;
			_weaponsFactory = weaponsFactory;
			_updateSystem = updateSystem;
			_viewPortController = viewPortController;
		}

		public PlayerShipController Create()
		{
			var model = new PlayerShipModel(_staticData.PlayerConfig);
			var bulletWeapon = _weaponsFactory.Create(WeaponType.BULLET, model);
			var laserWeapon = _weaponsFactory.Create(WeaponType.LASER, model);

			var result = new PlayerShipController(model, _inputModel, _updateSystem, _viewPortController);
			
			result.SetWeaponFirst(bulletWeapon)
				  .SetWeaponSecond(laserWeapon);

			return result;
		}
	}
}