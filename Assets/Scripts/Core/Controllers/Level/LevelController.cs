using System;
using Controllers.Enemies;
using Controllers.Player;
using Controllers.UI.Hud.PlayerInfo;
using Controllers.UI.Hud.Score;
using Factories.Effects.Score;
using Factories.Enemies;
using Model.Enemies;
using Model.WeaponInfo;
using UnityEngine;
using Utils.Reactivity;
using Views;

namespace Controllers.Level
{
	public class LevelController
	{
		private readonly PlayerShipController _playerShipController;
		private readonly EnemiesSpawner _enemiesSpawner;
		private readonly ScoreController _scoreController;
		private readonly PlayerInfoController _playerInfoController;
		private readonly WeaponInfoController _weaponInfoController;
		private readonly ViewBinder _viewBinder;
		private readonly Camera _camera;

		public Observable<int> Score { get; private set; } = new Observable<int>(0);

		public event Action OnLoseEvent;
		public event Action<EnemyModel> OnEnemyDestroyEvent;

		public LevelController(PlayerShipController playerShipController, 
							   EnemiesSpawner enemiesSpawner,
							   ScoreController scoreController, 
							   PlayerInfoController playerInfoController,
							   WeaponInfoController weaponInfoController,
							   ViewBinder viewBinder,
							   Camera camera)
		{
			_playerShipController = playerShipController;
			_enemiesSpawner = enemiesSpawner;
			_scoreController = scoreController;
			_playerInfoController = playerInfoController;
			_weaponInfoController = weaponInfoController;
			_viewBinder = viewBinder;
			_camera = camera;

			_enemiesSpawner.SetPlayerShipModel(_playerShipController.Model);
		}

		public void Start()
		{
			SetScore(0);

			CreatePlayerInfo();
			CreateWeaponInfo();
			
			AddListeners();
			
			_playerShipController.OnLevelStart();
			_enemiesSpawner.OnLevelStart();
		}

		private void CreatePlayerInfo()
		{
			var playerInfoModel = new PlayerInfoModel(_playerShipController.Model, _camera);
			_playerInfoController.SetModel(playerInfoModel);
			_viewBinder.TryBindViewByModel(playerInfoModel);
		}

		private void CreateWeaponInfo()
		{
			var weaponInfoModel = new WeaponInfoModel(_playerShipController.WeaponSecond.Model);
			_weaponInfoController.SetModel(weaponInfoModel);
			_viewBinder.TryBindViewByModel(weaponInfoModel);
			_weaponInfoController.Activate();
		}

		private void AddListeners()
		{
			
			_playerShipController.OnDestroyEvent += OnPlayerShipDestroy;
			_enemiesSpawner.OnObjDestroyEvent += OnEnemyDestroy;
		}

		private void RemoveListeners()
		{
			_playerInfoController.Deactivate();
			_weaponInfoController.Deactivate();
			_playerShipController.OnDestroyEvent -= OnPlayerShipDestroy;
			_enemiesSpawner.OnObjDestroyEvent -= OnEnemyDestroy;
		}
		
		private void OnEnemyDestroy(AbstractEnemyController enemy)
		{
			SetScore(Score.Value + enemy.Model.Score);
			
			OnEnemyDestroyEvent?.Invoke(enemy.Model);
		}

		private void SetScore(int score)
		{
			Score.Value = score;
			
			_scoreController.OnChangeScore(Score.Value);
		}
		
		private void OnPlayerShipDestroy()
		{
			OnLose();
		}

		private void OnLose()
		{
			RemoveListeners();

			_playerShipController.OnLevelEnd();
			_enemiesSpawner.OnLevelEnd();

			OnLoseEvent?.Invoke();
		}
	}
}