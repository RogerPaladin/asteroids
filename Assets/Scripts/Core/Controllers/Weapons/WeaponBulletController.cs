using Factories.Projectiles;
using Model.Player;
using Model.Weapons;

namespace Controllers.Weapons
{
	public class WeaponBulletController: AbstractWeaponController
	{
		public WeaponBulletController(WeaponModel model, PlayerShipModel playerShipModel, ProjectilesSpawner projectilesSpawner) : base(model, playerShipModel, projectilesSpawner)
		{
		}
	}
}