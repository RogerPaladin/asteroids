using Controllers.Level;
using Controllers.Player;
using Controllers.UI.Hud.PlayerInfo;
using Controllers.UI.Hud.Score;
using Factories.Enemies;
using Views;

namespace Factories.Level
{
	public class LevelFactory
	{
		public LevelController Create(PlayerShipController playerShipController,
									  EnemiesSpawner enemiesSpawner,
									  ScoreController scoreController,
									  PlayerInfoController playerInfoController,
									  WeaponInfoController weaponInfoController,
									  ViewInstantiator viewInstantiator)

		{
			return new LevelController(playerShipController, enemiesSpawner, scoreController,
									   playerInfoController, weaponInfoController, viewInstantiator);
		}
	}
}