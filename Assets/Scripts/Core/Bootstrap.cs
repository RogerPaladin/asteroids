using Assets.Scripts.Events;
using Assets.Scripts.GamePlay;
using Assets.Scripts.GamePlay.Background;
using Assets.Scripts.GamePlay.Effects;
using Assets.Scripts.GamePlay.Effects.Score;
using Assets.Scripts.GamePlay.Enemies;
using Assets.Scripts.GamePlay.Game;
using Assets.Scripts.GamePlay.Input;
using Assets.Scripts.GamePlay.Level;
using Assets.Scripts.GamePlay.Player;
using Assets.Scripts.GamePlay.Projectiles;
using Assets.Scripts.GamePlay.Weapons;
using Assets.Scripts.Static;
using Assets.Scripts.UI.Hud;
using Assets.Scripts.UI.Hud.PlayerInfo;
using Assets.Scripts.UI.Hud.Score;
using Assets.Scripts.UI.WindowsSystem;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Core
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

		private void Awake()
		{
			CreateDi();
			StartLoad();
		}

		private void CreateDi()
		{
			_diContainer = new DiContainer();
			
			_diContainer.Register(_camera);
			_diContainer.Register(_basePrefabs);

			CreateContainers();
			CreateEventSystem();
			CreateStaticData();
			CreateFactoriesSpawners();
			CreateControllers();
			CreateHud();
		}

		private void CreateContainers()
		{
			_gameContainer = new GameObject("GameContainer").AddComponent<GameContainer>();
			_diContainer.Register(_gameContainer);

			_diContainer.Register(_effectsContainer);
		}
		
		private void CreateEventSystem()
		{
			_updateSystem = gameObject.AddComponent<UpdateSystem>();
			_diContainer.Register(_updateSystem);
			
			_timerSystem = gameObject.AddComponent<TimerSystem>();
			_diContainer.Register(_timerSystem);
			
			_inputController = new InputController();
			_diContainer.Register(_inputController);
		}
		
		private void CreateStaticData()
		{
			_staticData = new StaticData();
			_diContainer.Register(_staticData);
		}
		
		private void CreateControllers()
		{
			_windowsController = new WindowsController(_windowsContainer);
			_diContainer.Register(_windowsController);
			
			_diContainer.Register(new ScoreController(_hudView.ScoreView));
			_diContainer.Register(new PlayerInfoController(_camera, _hudView.PlayerInfoView));
			_diContainer.Register(new WeaponInfoController(_hudView.WeaponInfoView));
			
			_gameController = new GameController(_diContainer);
			_diContainer.Register(_gameController);
		}

		private void CreateFactoriesSpawners()
		{
			var projectilesFactory = _diContainer.Register(new ProjectilesFactory(_diContainer));
			var projectilesSpawner = _diContainer.Register(new ProjectilesSpawner(projectilesFactory));
			_diContainer.Register(new WeaponsFactory(_staticData, projectilesSpawner, _timerSystem));
			
			var enemyFactory = _diContainer.Register(new EnemyFactory(_diContainer));
			_diContainer.Register(new EnemiesSpawner(_staticData, enemyFactory, _camera, _timerSystem));
			
			_diContainer.Register(new PlayerShipFactory(_diContainer));
			_diContainer.Register(new LevelFactory());
			
			var effectsFactory = _diContainer.Register(new EffectsFactory(_diContainer));
			_diContainer.Register(new EffectsSpawner(_staticData, effectsFactory));
		}

		private void CreateHud()
		{
			_diContainer.Register(_hudView);
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
