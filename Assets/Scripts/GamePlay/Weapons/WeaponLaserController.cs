using Assets.Scripts.GamePlay.Player;
using Assets.Scripts.GamePlay.Projectiles;

namespace Assets.Scripts.GamePlay.Weapons
{
	public class WeaponLaserController: AbstractWeaponController
	{
		public WeaponLaserController(WeaponModel model, PlayerShipModel playerShipModel, ProjectilesSpawner projectilesSpawner) : base(model, playerShipModel, projectilesSpawner)
		{
		}
	}
}