using Controllers.Background;
using Controllers.Level;
using Controllers.Player;
using Controllers.UI.Hud.PlayerInfo;
using Controllers.UI.Hud.Score;
using Controllers.UI.Windows;
using Factories.Effects.Score;
using Factories.Enemies;
using Factories.Level;
using Factories.Player;
using Model.Background;
using Model.Enemies;
using Model.ViewPort;
using Model.Windows;
using UnityEngine;
using Utils.Events;
using Views.Catalogs;
using Views.Hud;

namespace Controllers.Game
{
	public class GameController
	{
		private readonly LevelFactory _levelFactory;
		private readonly PlayerShipFactory _playerShipFactory;
		private readonly WindowsSystem _windowsSystem;
		private readonly EnemiesSpawner _enemiesSpawner;
		private readonly EffectsSpawner _effectsSpawner;
		private readonly ScoreController _scoreController;
		private readonly PlayerInfoController _playerInfoController;
		private readonly WeaponInfoController _weaponInfoController;
		private readonly BackgroundController _backgroundController;
		private readonly Transform _gameContainer;
		private readonly UpdateSystem _updateSystem;
		private readonly ViewPortModel _viewPortModel;

		private LevelController _currentLevel = null;
		
		private PlayerShipController _playerShipController;
		private readonly ViewsCatalog _viewsCatalog;
		private readonly HudView _hudView;


		public GameController(LevelFactory levelFactory,
							  PlayerShipFactory playerShipFactory,
							  WindowsSystem windowsSystem,
							  EnemiesSpawner enemiesSpawner,
							  EffectsSpawner effectsSpawner,
							  ScoreController scoreController,
							  PlayerInfoController playerInfoController,
							  WeaponInfoController weaponInfoController,
							  Transform gameContainer,
							  UpdateSystem updateSystem,
							  ViewPortModel viewPortModel,
							  ViewsCatalog viewsCatalog,
							  HudView hudView)
		{
			_levelFactory = levelFactory;
			_playerShipFactory = playerShipFactory;
			_windowsSystem = windowsSystem;
			_enemiesSpawner = enemiesSpawner;
			_effectsSpawner = effectsSpawner;
			_scoreController = scoreController;
			_playerInfoController = playerInfoController;
			_weaponInfoController = weaponInfoController;
			_gameContainer = gameContainer;
			_updateSystem = updateSystem;
			_viewsCatalog = viewsCatalog;
			_hudView = hudView;
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
			_currentLevel = _levelFactory.Create(_playerShipController, _enemiesSpawner, _scoreController, _playerInfoController, _weaponInfoController, _hudView.PlayerInfoView, _hudView.WeaponInfoView);

			AddLevelEventListeners();
			
			_currentLevel.Start();
		}

		private void SetBackground()
		{
			var backgroundModel = new BackgroundModel();
			var backgroundController = new BackgroundController(backgroundModel, _updateSystem, _viewPortModel);
			var view = _viewsCatalog.BackgroundViewCatalog.Create(backgroundModel);
			view.BindModel(backgroundModel);
			view.SetParent(_gameContainer);
			backgroundController.Activate();
		}

		private void CreatePlayer()
		{
			_playerShipController = _playerShipFactory.Create();
			var view = _viewsCatalog.PlayerViewCatalog.Create(_playerShipController.Model);
			view.SetData(_playerShipController.Model, _playerShipController);
			view.SetParent(_gameContainer);
			_playerShipController.SetProjectileSpawnPoint(view.ProjectileSpawnPoint.position);
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

			var model = new RestartWindowModel(_currentLevel.Score.Value);
			var view = _viewsCatalog.WindowsViewCatalog.Create(model);
			view.BindModel(model);
			var window = _windowsSystem.ShowWindow<RestartWindow>(model, view);
			window.SetData(() =>
			{
				window.Close();
				StartLevel();
			});
		}
	}
}