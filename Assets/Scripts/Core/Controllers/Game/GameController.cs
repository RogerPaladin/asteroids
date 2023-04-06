using Controllers.Background;
using Controllers.Level;
using Controllers.Player;
using Controllers.UI.Hud.PlayerInfo;
using Controllers.UI.Hud.Score;
using Controllers.UI.Windows;
using Controllers.UI.WindowsSystem;
using Factories.Effects.Score;
using Factories.Enemies;
using Factories.Level;
using Factories.Player;
using Model.Background;
using Model.Enemies;
using Model.ViewPort;
using UnityEngine;
using Utils.Collisions;
using Utils.Events;
using Views;
using Views.GamePlay.Player;

namespace Controllers.Game
{
	public class GameController
	{
		private readonly LevelFactory _levelFactory;
		private readonly PlayerShipFactory _playerShipFactory;
		private readonly WindowsController _windowsController;
		private readonly EnemiesSpawner _enemiesSpawner;
		private readonly EffectsSpawner _effectsSpawner;
		private readonly ScoreController _scoreController;
		private readonly PlayerInfoController _playerInfoController;
		private readonly WeaponInfoController _weaponInfoController;
		private readonly BackgroundController _backgroundController;
		private readonly Transform _gameContainer;
		private readonly UpdateSystem _updateSystem;
		private readonly Camera _camera;
		private readonly ViewPortModel _viewPortModel;

		private LevelController _currentLevel = null;
		
		private PlayerShipController _playerShipController;
		private readonly ViewInstantiator _viewInstantiator;


		public GameController(LevelFactory levelFactory,
							  PlayerShipFactory playerShipFactory,
							  WindowsController windowsController,
							  EnemiesSpawner enemiesSpawner,
							  EffectsSpawner effectsSpawner,
							  ScoreController scoreController,
							  PlayerInfoController playerInfoController,
							  WeaponInfoController weaponInfoController,
							  Transform gameContainer,
							  UpdateSystem updateSystem,
							  ViewInstantiator viewInstantiator, Camera camera,
							  ViewPortModel viewPortModel)
		{
			_levelFactory = levelFactory;
			_playerShipFactory = playerShipFactory;
			_windowsController = windowsController;
			_enemiesSpawner = enemiesSpawner;
			_effectsSpawner = effectsSpawner;
			_scoreController = scoreController;
			_playerInfoController = playerInfoController;
			_weaponInfoController = weaponInfoController;
			_gameContainer = gameContainer;
			_updateSystem = updateSystem;
			_viewInstantiator = viewInstantiator;
			_camera = camera;
			_viewPortModel = viewPortModel;

			SetBackground();
		}

		public void OnStartGame()
		{
			CreatePlayer();
			StartLevel();
		}
		
		private void StartLevel()
		{
			_currentLevel = _levelFactory.Create(_playerShipController, _enemiesSpawner, _scoreController, _playerInfoController, _weaponInfoController, _viewInstantiator, _camera);

			AddLevelEventListeners();
			
			_currentLevel.Start();
		}

		private void SetBackground()
		{
			var backgroundModel = new BackgroundModel();
			var backgroundController = new BackgroundController(backgroundModel, _updateSystem, _viewPortModel);
			var view = _viewInstantiator.Instantiate(backgroundModel);
			view.BindModel(backgroundModel);
			view.SetParent(_gameContainer);
			backgroundController.Activate();
		}

		private void CreatePlayer()
		{
			_playerShipController = _playerShipFactory.Create();
			var view = (IPlayerShipView)_viewInstantiator.Instantiate(_playerShipController.Model);
			view.BindModel(_playerShipController.Model);
			view.SetParent(_gameContainer);
			_playerShipController.SetProjectileSpawnPoint(view.ProjectileSpawnPoint.position);
			_playerShipController.SetCollisionChecker(new CollisionChecker(view.Collider));
			_playerShipController.Activate();
		}

		private void OnEnemyDestroy(EnemyModel model)
		{
			_effectsSpawner.OnEnemyDestroy(model);
		}
        		
		private void AddLevelEventListeners()
		{
			_currentLevel.OnLoseEvent += OnLoseLevel;
			_currentLevel.OnEnemyDestroyEvent += OnEnemyDestroy;
		}
		
		private void RemoveLevelEventListeners()
		{
			_currentLevel.OnLoseEvent -= OnLoseLevel;
			_currentLevel.OnEnemyDestroyEvent -= OnEnemyDestroy;
		}
		
		private void OnLoseLevel()
		{
			_effectsSpawner.OnLevelEnd();
			
			RemoveLevelEventListeners();

			_windowsController.ShowWindow<RestartWindow>(false, w => w.Init(_currentLevel.Score.Value, StartLevel));
		}
	}
}