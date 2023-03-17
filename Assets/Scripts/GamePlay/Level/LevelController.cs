using System;
using Assets.Scripts.GamePlay.Effects.Score;
using Assets.Scripts.GamePlay.Enemies;
using Assets.Scripts.GamePlay.Player;
using Assets.Scripts.UI.Hud.PlayerInfo;
using Assets.Scripts.UI.Hud.Score;

namespace Assets.Scripts.GamePlay.Level
{
	public class LevelController
	{
		private readonly PlayerShipController _playerShipController;
		private readonly EnemiesSpawner _enemiesSpawner;
		private readonly EffectsSpawner _effectsSpawner;
		private readonly ScoreController _scoreController;
		private readonly PlayerInfoController _playerInfoController;
		private readonly WeaponInfoController _weaponInfoController;

		public int Score { get; private set; }

		public event Action OnLoseEvent;

		public LevelController(PlayerShipController playerShipController, 
							   EnemiesSpawner enemiesSpawner,
							   EffectsSpawner effectsSpawner,
							   ScoreController scoreController, 
							   PlayerInfoController playerInfoController,
							   WeaponInfoController weaponInfoController)
		{
			_playerShipController = playerShipController;
			_enemiesSpawner = enemiesSpawner;
			_effectsSpawner = effectsSpawner;
			_scoreController = scoreController;
			_playerInfoController = playerInfoController;
			_weaponInfoController = weaponInfoController;

			_enemiesSpawner.SetPlayerShipModel(_playerShipController.Model);
		}

		public void Start()
		{
			SetScore(0);

			AddListeners();
			
			_playerShipController.OnLevelStart();
			_enemiesSpawner.OnLevelStart();
		}

		private void AddListeners()
		{
			_playerShipController.Model.RegisterObserver(_playerInfoController);
			_playerShipController.Model.WeaponSecond.Model.RegisterObserver(_weaponInfoController);
			_playerShipController.OnDestroyEvent += OnPlayerShipDestroy;
			_enemiesSpawner.OnObjDestroyEvent += OnEnemyDestroy;
		}

		private void RemoveListeners()
		{
			_playerShipController.Model.RemoveObserver(_playerInfoController);
			_playerShipController.Model.WeaponSecond.Model.RemoveObserver(_weaponInfoController);
			_playerShipController.OnDestroyEvent -= OnPlayerShipDestroy;
			_enemiesSpawner.OnObjDestroyEvent -= OnEnemyDestroy;
		}
		
		private void OnEnemyDestroy(AbstractEnemyController enemy)
		{
			SetScore(Score + enemy.Model.Score);
			
			_effectsSpawner.OnEnemyDestroy(enemy.Model);
		}

		private void SetScore(int score)
		{
			Score = score;
			
			_scoreController.OnChangeScore(Score);
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
			_effectsSpawner.OnLevelEnd();
			
			OnLoseEvent?.Invoke();
		}
	}
}