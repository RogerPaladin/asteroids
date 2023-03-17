using Assets.Scripts.Core;
using Assets.Scripts.Events;
using Assets.Scripts.GamePlay.Enemies.Asteroids;
using Assets.Scripts.GamePlay.Game;
using Assets.Scripts.GamePlay.Player;
using Assets.Scripts.Static.Enemies;
using Assets.Scripts.Utils;
using Assets.Scripts.Utils.OffScreenChecker;
using UnityEngine;

namespace Assets.Scripts.GamePlay.Enemies
{
	public class EnemyFactory
	{
		private readonly DiContainer _diContainer;
		
		private readonly UpdateSystem _updateSystem;
		private readonly Camera _camera;
		private readonly EnemyView _bigAsteroidView;
		private readonly EnemyView _smallAsteroidView;
		private readonly EnemyView _ufoView;
		private readonly GameContainer _gameContainer;

		public EnemyFactory(DiContainer diContainer)
		{
			_diContainer = diContainer;
			
			_bigAsteroidView = _diContainer.Resolve<BasePrefabs>().BigAsteroidView;
			_smallAsteroidView = _diContainer.Resolve<BasePrefabs>().SmallAsteroidView;
			_ufoView = _diContainer.Resolve<BasePrefabs>().UfoView;
			
			_updateSystem = _diContainer.Resolve<UpdateSystem>();
			_camera = _diContainer.Resolve<Camera>();

			_gameContainer = _diContainer.Resolve<GameContainer>();
		}

		public AbstractEnemyController Create(EnemyConfig config, PlayerShipModel playerShipModel)
		{
			var offScreenChecker = new OffScreenCheckerTeleport(_camera);
			
			var model = new EnemyModel(config, _updateSystem, offScreenChecker, playerShipModel);
			EnemyView view = null;
			
			switch (config.ModelId)
			{
				case EnemyType.BIG_ASTEROID:
					view = Object.Instantiate(_bigAsteroidView, _gameContainer.transform);
					return new BigAsteroidController(model, view);
				case EnemyType.SMALL_ASTEROID:
					view = Object.Instantiate(_smallAsteroidView, _gameContainer.transform);
					return new SmallAsteroidController(model, view);
				case EnemyType.UFO:
					view = Object.Instantiate(_ufoView, _gameContainer.transform);
					return new UfoController(model, view);
			}

			return null;
		}
	}
}