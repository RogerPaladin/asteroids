using Controllers.Game;
using Controllers.UI.Hud.PlayerInfo;
using Controllers.UI.Hud.Score;
using Controllers.UI.Windows;
using Core.Controllers.Input;
using Core.Controllers.ViewPort;
using Core.Loader;
using Factories.Effects;
using Factories.Effects.Score;
using Factories.Enemies;
using Factories.Level;
using Factories.Player;
using Factories.Projectiles;
using Factories.Weapons;
using Model.Input;
using Model.Score;
using Model.ViewPort;
using Static;
using UnityEngine;
using Utils.Clock;
using Utils.Events;
using Views;
using Views.Hud;
using Views.Input;
using Views.ViewPort;

namespace Core
{
	public class Bootstrap : MonoBehaviour
	{
		[SerializeField] private Camera _camera;
		
		[SerializeField] private HudView _hudView;
		[SerializeField] private ViewPortView _viewPortView;
		[SerializeField] private Transform _windowsContainer;
		[SerializeField] private Transform _effectsContainer;
		[SerializeField] private BasePrefabs _basePrefabs;
		
		private GameController _gameController;
		private StaticData _staticData;
		private UpdateSystem _updateSystem;
		private WindowsSystem _windowsSystem;
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

			_inputController = new InputController(new InputModel());
			var inputView = gameObject.AddComponent<InputView>();
			inputView.SetData(_inputController);
			
			
			_staticData = new StaticData();
			var viewInstantiator = new ViewInstantiator(_basePrefabs, _hudView);

			var viewPortModel = new ViewPortModel();
			var viewPortController = new ViewPortController(viewPortModel);
			_viewPortView.SetData(viewPortController, _camera);

			var projectilesFactory = new ProjectilesFactory(_updateSystem, viewPortController);
			var enemyFactory = new EnemyFactory(_updateSystem, _camera);
			var projectilesSpawner = new ProjectilesSpawner(projectilesFactory, gameContainer, viewInstantiator);
			var weaponsFactory = new WeaponsFactory(_staticData, projectilesSpawner, timerSystem);
			var playerShipFactory = new PlayerShipFactory(_staticData, _inputController.Model, weaponsFactory, _updateSystem, viewPortController);
			var enemiesSpawner = new EnemiesSpawner(_staticData, enemyFactory, viewPortController, timerSystem, gameContainer, viewInstantiator);
			var levelFactory = new LevelFactory();
			var effectsFactory = new EffectsFactory(_updateSystem);
			var effectsSpawner = new EffectsSpawner(_staticData, effectsFactory, _effectsContainer, viewInstantiator);

			_windowsSystem = new WindowsSystem(_windowsContainer);

			var scoreController = new ScoreController(new ScoreModel());
			var view = viewInstantiator.Instantiate(scoreController.Model);
			view.BindModel(scoreController.Model);

			var playerInfoController = new PlayerInfoController();
			var weaponInfoController = new WeaponInfoController();

			_gameController = new GameController(levelFactory, playerShipFactory, _windowsSystem, enemiesSpawner, effectsSpawner, scoreController, playerInfoController,  weaponInfoController, gameContainer, _updateSystem, viewInstantiator, _camera, viewPortModel);
		}
		
		private void StartLoad()
		{
			var dataLoader = new DataLoader(_windowsSystem, _inputController.Model, _updateSystem, _staticData);
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
