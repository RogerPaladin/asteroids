using Controllers.Game;
using Controllers.UI.Hud.PlayerInfo;
using Controllers.UI.Hud.Score;
using Controllers.UI.WindowsSystem;
using Core.Loader;
using Factories.Effects;
using Factories.Effects.Score;
using Factories.Enemies;
using Factories.Level;
using Factories.Player;
using Factories.Projectiles;
using Factories.Weapons;
using Model.Score;
using Static;
using UnityEngine;
using Utils.Clock;
using Utils.Containers.UI.Windows;
using Utils.DiContainers.Effects;
using Utils.Events;
using Utils.Input;
using Views;
using Views.Hud;

namespace Core
{
	public class Bootstrap : MonoBehaviour
	{
		[SerializeField] private Camera _camera;
		
		[SerializeField] private HudView _hudView;
		[SerializeField] private WindowsContainer _windowsContainer;
		[SerializeField] private EffectsContainer _effectsContainer;
		[SerializeField] private BasePrefabs _basePrefabs;
		
		private GameController _gameController;
		private StaticData _staticData;
		private UpdateSystem _updateSystem;
		private WindowsController _windowsController;
		private InputController _inputController;

		private void Awake()
		{
			CreateRoot();
			StartLoad();
		}

		private void CreateRoot()
		{
			var gameContainer = new GameObject("GameContainer").transform;
			
			var clockBehaviour = gameObject.AddComponent<ClockBehaviour>();
			
			_updateSystem = new UpdateSystem();
			clockBehaviour.OnFrameUpdate += _updateSystem.Update;

			var timerSystem = new TimerSystem();
			clockBehaviour.OnSecondUpdate += timerSystem.OnTimer;

			_inputController = new InputController();
			
			_staticData = new StaticData();
			var viewBinder = new ViewBinder(_basePrefabs, _hudView);

			var projectilesFactory = new ProjectilesFactory(_updateSystem, _camera);
			var enemyFactory = new EnemyFactory(_updateSystem, _camera);
			var projectilesSpawner = new ProjectilesSpawner(projectilesFactory, gameContainer, viewBinder);
			var weaponsFactory = new WeaponsFactory(_staticData, projectilesSpawner, timerSystem);
			var playerShipFactory = new PlayerShipFactory(_staticData, _inputController, weaponsFactory, _updateSystem, _camera);
			var enemiesSpawner = new EnemiesSpawner(_staticData, enemyFactory, _camera, timerSystem, gameContainer, viewBinder);
			var levelFactory = new LevelFactory();
			var effectsFactory = new EffectsFactory(_updateSystem);
			var effectsSpawner = new EffectsSpawner(_staticData, effectsFactory, _effectsContainer, viewBinder);

			_windowsController = new WindowsController(_windowsContainer);

			var scoreController = new ScoreController(new ScoreModel());
			viewBinder.TryBindViewByModel(scoreController.Model);

			var playerInfoController = new PlayerInfoController();
			var weaponInfoController = new WeaponInfoController();

			_gameController = new GameController(levelFactory, playerShipFactory, _windowsController, enemiesSpawner, effectsSpawner, scoreController, playerInfoController,  weaponInfoController, gameContainer, _updateSystem, viewBinder, _camera);
		}
		
		private void StartLoad()
		{
			var dataLoader = new DataLoader(_windowsController, _inputController, _updateSystem, _staticData);
			dataLoader.StartLoad(OnCompleteLoad);
		}

		private void OnCompleteLoad()
		{
			StartGame();
		}

		private void StartGame()
		{
			_gameController.OnStartGame();
		}
	}
}
