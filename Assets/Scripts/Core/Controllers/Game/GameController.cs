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
using Utils.DiContainers;
using Utils.Events;
using UnityEngine;
using Utils.Collisions;
using Utils.Containers.Game;
using Views;
using Views.GamePlay.Player;

namespace Controllers.Game
{
	public class GameController : IDiContainerChild
	{
		private readonly DiContainer _diContainer;
		private readonly LevelFactory _levelFactory;
		private readonly PlayerShipFactory _playerShipFactory;
		private readonly WindowsController _windowsController;
		private readonly EnemiesSpawner _enemiesSpawner;
		private readonly EffectsSpawner _effectsSpawner;
		private readonly ScoreController _scoreController;
		private readonly PlayerInfoController _playerInfoController;
		private readonly WeaponInfoController _weaponInfoController;
		private readonly BackgroundController _backgroundController;
		private readonly GameContainer _gameContainer;
		private readonly UpdateSystem _updateSystem;
		private readonly Camera _camera;

		private LevelController _currentLevel = null;
		
		private PlayerShipController _playerShipController;
		private readonly ViewBinder _viewBinder;


		public GameController(DiContainer diContainer)
		{
			_diContainer = diContainer;
			
			_levelFactory = _diContainer.Resolve<LevelFactory>();
			_playerShipFactory = _diContainer.Resolve<PlayerShipFactory>();
			_windowsController = _diContainer.Resolve<WindowsController>();
			_enemiesSpawner = _diContainer.Resolve<EnemiesSpawner>();
			_effectsSpawner = _diContainer.Resolve<EffectsSpawner>();
			_scoreController = _diContainer.Resolve<ScoreController>();
			_playerInfoController = _diContainer.Resolve<PlayerInfoController>();
			_weaponInfoController = _diContainer.Resolve<WeaponInfoController>();
			_gameContainer = _diContainer.Resolve<GameContainer>();
			_updateSystem = _diContainer.Resolve<UpdateSystem>();
			_viewBinder = _diContainer.Resolve<ViewBinder>();
			_camera = _diContainer.Resolve<DiCameraProxy>().Camera;

			SetBackground();
		}

		public void OnStartGame()
		{
			CreatePlayer();
			StartLevel();
		}
		
		private void StartLevel()
		{
			_currentLevel = _levelFactory.Create(_playerShipController, _enemiesSpawner, _scoreController, _playerInfoController, _weaponInfoController, _viewBinder, _camera);

			AddLevelEventListeners();
			
			_currentLevel.Start();
		}

		private void SetBackground()
		{
			var backgroundModel = new BackgroundModel(_camera, _updateSystem);
			var backgroundController = new BackgroundController(backgroundModel);
			_viewBinder.TryBindViewByModel(backgroundModel);
			backgroundController.SetParent(_gameContainer.transform);
			backgroundController.Activate();
		}

		private void CreatePlayer()
		{
			_playerShipController = _playerShipFactory.Create();
			var view = (IPlayerShipView)_viewBinder.TryBindViewByModel(_playerShipController.Model);
			_playerShipController.SetParent(_gameContainer.transform);
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