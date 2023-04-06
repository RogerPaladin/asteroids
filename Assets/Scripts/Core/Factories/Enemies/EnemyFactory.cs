using Controllers.Enemies;
using Controllers.Enemies.Asteroids;
using Controllers.Enemies.Ufos;
using Core;
using Model.Enemies;
using Model.Player;
using Static.Enemies;
using Utils.DiContainers;
using Utils.Events;
using Utils.OffScreenChecker;
using UnityEngine;

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

		public AbstractEnemyController Create(EnemyConfig config, PlayerShipModel playerShipModel)
		{
			var offScreenChecker = new OffScreenCheckerTeleport(_camera);
			
			var model = new EnemyModel(config, _updateSystem, offScreenChecker, playerShipModel);

			switch (config.ModelId)
			{
				case EnemyType.BIG_ASTEROID:
					return new BigAsteroidController(model);
				case EnemyType.SMALL_ASTEROID:
					return new SmallAsteroidController(model);
				case EnemyType.UFO:
					return new UfoController(model);
			}

			return null;
		}
	}
}