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
	public class EnemyFactory : IDiContainerChild
	{
		private readonly DiContainer _diContainer;
		
		private readonly UpdateSystem _updateSystem;
		private readonly Camera _camera;

		public EnemyFactory(DiContainer diContainer)
		{
			_diContainer = diContainer;

			_updateSystem = _diContainer.Resolve<UpdateSystem>();
			_camera = _diContainer.Resolve<DiCameraProxy>().Camera;
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