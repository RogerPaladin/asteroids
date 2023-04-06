using Controllers.Enemies;
using Controllers.Enemies.Asteroids;
using Controllers.Enemies.Ufos;
using Core.Controllers.ViewPort;
using Model.Enemies;
using Model.Player;
using Static.Enemies;
using UnityEngine;
using Utils.Events;

namespace Factories.Enemies
{
	public class EnemyFactory
	{
		private readonly UpdateSystem _updateSystem;
		private readonly Camera _camera;

		public EnemyFactory(UpdateSystem updateSystem, Camera camera)
		{
			_updateSystem = updateSystem;
			_camera = camera;
		}

		public AbstractEnemyController Create(EnemyConfig config, PlayerShipModel playerShipModel, ViewPortController viewPortController)
		{
			var model = new EnemyModel(config, _updateSystem, playerShipModel);

			switch (config.ModelId)
			{
				case EnemyType.BIG_ASTEROID:
					return new BigAsteroidController(model, viewPortController);
				case EnemyType.SMALL_ASTEROID:
					return new SmallAsteroidController(model, viewPortController);
				case EnemyType.UFO:
					return new UfoController(model, viewPortController);
			}

			return null;
		}
	}
}