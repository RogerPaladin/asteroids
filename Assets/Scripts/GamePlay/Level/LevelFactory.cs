using Assets.Scripts.GamePlay.Effects.Score;
using Assets.Scripts.GamePlay.Enemies;
using Assets.Scripts.GamePlay.Player;
using Assets.Scripts.UI.Hud.PlayerInfo;
using Assets.Scripts.UI.Hud.Score;

namespace Assets.Scripts.GamePlay.Level
{
	public class LevelFactory
	{
		public LevelController Create(PlayerShipController playerShipController, 
									  EnemiesSpawner enemiesSpawner, 
									  EffectsSpawner effectsSpawner, 
									  ScoreController scoreController, 
									  PlayerInfoController playerInfoController,
									  WeaponInfoController weaponInfoController)
		{
			return new LevelController(playerShipController, enemiesSpawner, effectsSpawner, scoreController, playerInfoController, weaponInfoController);
		}
	}
}