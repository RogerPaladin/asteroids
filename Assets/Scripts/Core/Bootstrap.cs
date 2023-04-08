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
using Model.WeaponInfo;
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
		[SerializeField] private Transform _windowsContainer;
		[SerializeField] private Transform _effectsContainer;
		[SerializeField] private BasePrefabs _basePrefabs;//todo!!!! Давай перенесем справочные данные в справочники ScriptableObject. Пример: EnemiesViewCatalog
		
		private GameController _gameController;
		private StaticData _staticData;//todo!!!! попробуем и эти данные записать в ScriptableObject, чтобы не парсить json. Заодно посмотрим удобно ли это. Пример: StaticDataCatalog. А все, что связано с Json грохнем
		private UpdateSystem _updateSystem;
		private WindowsSystem _windowsSystem;
		private InputController _inputController;
		private ViewInstantiator _viewInstantiator;//todo!!!! Префабы, которые инстантиируются можно будет получить через справочник типа EnemiesViewCatalog, а этот класс удалить

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
			_viewInstantiator = new ViewInstantiator(_basePrefabs, _hudView);

			var viewPortModel = new ViewPortModel();
			var viewPortController = new ViewPortController(viewPortModel);
			var viewPortView = gameObject.AddComponent<ViewPortView>();
			viewPortView.SetData(viewPortController, _camera);

			var projectilesFactory = new ProjectilesFactory(_updateSystem, viewPortController);
			var enemyFactory = new EnemyFactory(_updateSystem);
			var projectilesSpawner = new ProjectilesSpawner(projectilesFactory, gameContainer, _viewInstantiator);
			var weaponsFactory = new WeaponsFactory(_staticData, projectilesSpawner, timerSystem);
			var playerShipFactory = new PlayerShipFactory(_staticData, _inputController.Model, weaponsFactory, _updateSystem, viewPortController);
			var enemiesSpawner = new EnemiesSpawner(_staticData, enemyFactory, viewPortController, timerSystem, gameContainer, _viewInstantiator);
			var levelFactory = new LevelFactory();
			var effectsFactory = new EffectsFactory(_updateSystem);
			var effectsSpawner = new EffectsSpawner(_staticData, effectsFactory, _effectsContainer, _viewInstantiator);

			_windowsSystem = new WindowsSystem(_windowsContainer);

			var scoreController = new ScoreController(new ScoreModel());
			var view = _viewInstantiator.Instantiate(scoreController.Model);//todo!!!! проще сразу написать _hudView.ScoreView, чем _viewInstantiator.Instantiate(scoreController.Model)
			view.BindModel(scoreController.Model);//todo!!!! Я бы перешел на SetData, чтобы избавиться от EventListener, SyncModel.
                                         //todo!!!! Сам класс AbstractView все-таки сильно связывает руки для дальнейшего рефактора из-за того, что у нее только одна модель, обязательная подписка/отписка и прочие приблуды, которые не всегда нужны.
                                         //todo!!!! Здесь наша цель уйти от наследования в сторону декомпозиции

			var playerInfoController = new PlayerInfoController(new PlayerInfoModel(), viewPortController);
			var weaponInfoController = new WeaponInfoController();

			_gameController = new GameController(levelFactory, playerShipFactory, _windowsSystem, enemiesSpawner, effectsSpawner, scoreController, playerInfoController,  weaponInfoController, gameContainer, _updateSystem, _viewInstantiator, viewPortModel);
		}
		
		private void StartLoad()
		{
			var dataLoader = new DataLoader(_windowsSystem, _inputController.Model, _updateSystem, _viewInstantiator, _staticData);
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
