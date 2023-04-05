using Factories.Projectiles;
using Model.Player;
using Model.Weapons;

namespace Controllers.Weapons
{
	public class WeaponLaserController: AbstractWeaponController
	{
		public WeaponLaserController(WeaponModel model, PlayerShipModel playerShipModel, ProjectilesSpawner projectilesSpawner) : base(model, playerShipModel, projectilesSpawner)
		{
		}
	}
}