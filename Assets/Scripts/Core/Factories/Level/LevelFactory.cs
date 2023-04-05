using Controllers.Level;
using Controllers.Player;
using Controllers.UI.Hud.PlayerInfo;
using Controllers.UI.Hud.Score;
using Factories.Effects.Score;
using Factories.Enemies;
using UnityEngine;
using Utils.DiContainers;
using Views;

namespace Factories.Level
{
	public class LevelFactory : IDiContainerChild
	{
		public LevelController Create(PlayerShipController playerShipController,
									  EnemiesSpawner enemiesSpawner,
									  ScoreController scoreController,
									  PlayerInfoController playerInfoController,
									  WeaponInfoController weaponInfoController,
									  ViewBinder viewBinder,
									  Camera camera)

		{
			return new LevelController(playerShipController, enemiesSpawner, scoreController,
									   playerInfoController, weaponInfoController, viewBinder, camera);
		}
	}
}