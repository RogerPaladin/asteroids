using Assets.Scripts.GamePlay.Player;
using Assets.Scripts.GamePlay.Projectiles;

namespace Assets.Scripts.GamePlay.Weapons
{
	public class WeaponBulletController: AbstractWeaponController
	{
		public WeaponBulletController(WeaponModel model, PlayerShipModel playerShipModel, ProjectilesSpawner projectilesSpawner) : base(model, playerShipModel, projectilesSpawner)
		{
		}
	}
}