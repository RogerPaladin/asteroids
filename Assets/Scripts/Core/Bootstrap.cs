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
using Utils.Clock;
using Utils.DiContainers;
using Utils.Events;
using Views.Hud;
using UnityEngine;
using Utils.Containers.Game;
using Utils.Containers.UI.Windows;
using Utils.DiContainers.Effects;
using Utils.Input;
using Views;

namespace Core
{
	public class Bootstrap : MonoBehaviour
	{
		[SerializeField] private Camera _camera;
		
		[SerializeField] private HudView _hudView;
		[SerializeField] private WindowsContainer _windowsContainer;
		[SerializeField] private EffectsContainer _effectsContainer;
		[SerializeField] private BasePrefabs _basePrefabs;

		private DiContainer _diContainer;
		private GameController _gameController;
		private StaticData _staticData;
		private WindowsController _windowsController;
		private GameContainer _gameContainer;
		private UpdateSystem _updateSystem;
		private TimerSystem _timerSystem;
		private InputController _inputController;
		private ViewBinder _viewBinder;

		private void Awake()
		{
			CreateDi();
			StartLoad();
		}

		private void CreateDi()
		{
			_diContainer = new DiContainer();
			
			_diContainer.Register<DiCameraProxy>(new DiCameraProxy(_camera));
			_diContainer.Register<BasePrefabs>(_basePrefabs);

			CreateContainers();
			CreateEventSystem();
			CreateStaticData();
			CreateViewBinder();
			CreateFactoriesSpawners();
			CreateControllers();
			CreateHud();
		}

		private void CreateContainers()
		{
			_gameContainer = new GameObject("GameContainer").AddComponent<GameContainer>();
			_diContainer.Register<GameContainer>(_gameContainer);

			_diContainer.Register<EffectsContainer>(_effectsContainer);
		}
		
		private void CreateEventSystem()
		{
			var clockBehaviour = gameObject.AddComponent<ClockBehaviour>();
			
			_updateSystem = new UpdateSystem();
			clockBehaviour.OnFrameUpdate += _updateSystem.Update;
			_diContainer.Register<UpdateSystem>(_updateSystem);
			
			_timerSystem = new TimerSystem();
			clockBehaviour.OnSecondUpdate += _timerSystem.OnTimer;
			_diContainer.Register<TimerSystem>(_timerSystem);
			
			_inputController = new InputController();
			_diContainer.Register<InputController>(_inputController);
		}
		
		private void CreateStaticData()
		{
			_staticData = new StaticData();
			_diContainer.Register<StaticData>(_staticData);
		}

		private void CreateViewBinder()
		{
			_viewBinder = new ViewBinder(_basePrefabs, _hudView);
			_diContainer.Register<ViewBinder>(_viewBinder);
		}
		
		private void CreateControllers()
		{
			_windowsController = new WindowsController(_windowsContainer);
			_diContainer.Register<WindowsController>(_windowsController);

			var scoreController = new ScoreController(new ScoreModel());
			_diContainer.Register<ScoreController>(scoreController);
			_viewBinder.TryBindViewByModel(scoreController.Model);
			
			_diContainer.Register<PlayerInfoController>(new PlayerInfoController());
			_diContainer.Register<WeaponInfoController>(new WeaponInfoController());
			
			_gameController = new GameController(_diContainer);
			_diContainer.Register<GameController>(_gameController);
		}

		private void CreateFactoriesSpawners()
		{
			var projectilesFactory = _diContainer.Register<ProjectilesFactory>(new ProjectilesFactory(_diContainer));
			var projectilesSpawner = _diContainer.Register<ProjectilesSpawner>(new ProjectilesSpawner(projectilesFactory, _gameContainer, _viewBinder));
			_diContainer.Register<WeaponsFactory>(new WeaponsFactory(_staticData, projectilesSpawner, _timerSystem));
			
			var enemyFactory = _diContainer.Register<EnemyFactory>(new EnemyFactory(_diContainer));
			_diContainer.Register<EnemiesSpawner>(new EnemiesSpawner(_staticData, enemyFactory, _camera, _timerSystem, _gameContainer, _viewBinder));
			
			_diContainer.Register<PlayerShipFactory>(new PlayerShipFactory(_diContainer));
			_diContainer.Register<LevelFactory>(new LevelFactory());
			
			var effectsFactory = _diContainer.Register<EffectsFactory>(new EffectsFactory(_diContainer));
			_diContainer.Register<EffectsSpawner>(new EffectsSpawner(_staticData, effectsFactory, _effectsContainer, _viewBinder));
		}

		private void CreateHud()
		{
			_diContainer.Register<HudView>(_hudView);
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
