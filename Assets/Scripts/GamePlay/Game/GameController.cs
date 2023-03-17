using Assets.Scripts.Core;
using Assets.Scripts.Events;
using Assets.Scripts.GamePlay.Background;
using Assets.Scripts.GamePlay.Effects.Score;
using Assets.Scripts.GamePlay.Enemies;
using Assets.Scripts.GamePlay.Level;
using Assets.Scripts.GamePlay.Player;
using Assets.Scripts.UI.Hud.PlayerInfo;
using Assets.Scripts.UI.Hud.Score;
using Assets.Scripts.UI.Windows;
using Assets.Scripts.UI.WindowsSystem;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.GamePlay.Game
{
	public class GameController
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
		private readonly BackgroundView _backgroundView;
		private readonly UpdateSystem _updateSystem;
		private readonly Camera _camera;

		private LevelController _currentLevel = null;
		
		private PlayerShipController _playerShipController;


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
			_backgroundView = _diContainer.Resolve<BasePrefabs>().BackgroundView;
			_updateSystem = _diContainer.Resolve<UpdateSystem>();
			_camera = _diContainer.Resolve<Camera>();

			SetBackground();
		}

		public void OnStartGame()
		{
			CreatePlayer();
			StartLevel();
		}
		
		private void StartLevel()
		{
			_currentLevel = _levelFactory.Create(_playerShipController, _enemiesSpawner, _effectsSpawner, _scoreController, _playerInfoController, _weaponInfoController);

			AddLevelEventListeners();
			
			_currentLevel.Start();
		}

		private void SetBackground()
		{
			var backgroundController = new BackgroundController(new BackgroundModel(_camera, _updateSystem), Object.Instantiate(_backgroundView));
			backgroundController.AddBackgroundToTransform(_gameContainer.transform);
		}

		private void CreatePlayer()
		{
			_playerShipController = _playerShipFactory.Create(_gameContainer);
		}

		private void AddLevelEventListeners()
		{
			_currentLevel.OnLoseEvent += OnLoseLevel;
		}
		
		private void RemoveLevelEventListeners()
		{
			_currentLevel.OnLoseEvent -= OnLoseLevel;
		}
		
		private void OnLoseLevel()
		{
			RemoveLevelEventListeners();

			_windowsController.ShowWindow<RestartWindow>(false, w => w.Init(_currentLevel.Score, StartLevel));
		}
	}
}