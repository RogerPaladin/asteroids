using Controllers.Enemies;
using Controllers.Enemies.Asteroids;
using Core.Controllers.ViewPort;
using Core.GamePlay.Enemies;
using Model.Player;
using Static;
using Static.Enemies;
using UnityEngine;
using Utils.Events;
using Utils.Spawner;
using Views;

namespace Factories.Enemies
{
	public class EnemiesSpawner : AbstractSpawner<AbstractEnemyController, EnemyConfig>, ITimerListener
	{
		private readonly StaticData _staticData;
		private readonly EnemyFactory _enemyFactory;
		private readonly Camera _camera;
		private readonly TimerSystem _timerSystem;
		private readonly Transform _gameContainer;
		private readonly ViewInstantiator _viewInstantiator;
		private readonly EnemySpawnTimerList _enemySpawnTimerList;
		private readonly ViewPortController _viewPortController;

		private PlayerShipModel _playerShipModel;

		public EnemiesSpawner(StaticData staticData, EnemyFactory enemyFactory, ViewPortController viewPortController, TimerSystem timerSystem, Transform gameContainer, ViewInstantiator viewInstantiator)
		{
			_staticData = staticData;
			_enemyFactory = enemyFactory;
			_viewPortController = viewPortController;
			_timerSystem = timerSystem;
			_gameContainer = gameContainer;
			_viewInstantiator = viewInstantiator;

			_enemySpawnTimerList = new EnemySpawnTimerList(_active);
		}

		public void SetPlayerShipModel(PlayerShipModel playerShipModel)
		{
			_playerShipModel = playerShipModel;
		}

		public void OnLevelStart()
		{
			_enemySpawnTimerList.OnLevelStart();
			
			InitialSpawn();
			
			StartTimer();
		}
		
		public void OnLevelEnd()
		{
			StopTimer();
			ReturnAllActiveToPool();
		}
		
		private void StartTimer()
		{
			_timerSystem.AddListener(this);
		}
		
		private void StopTimer()
		{
			_timerSystem.RemoveListener(this);
		}

		private void InitialSpawn()
		{
			_enemySpawnTimerList.SetEnemiesData(_staticData.EnemiesData);

			foreach (var enemyConfig in _staticData.EnemiesData.All.Values)
			{
				for (int i = 0; i < enemyConfig.StartCount; i++)
				{
					var randomPos = _viewPortController.GetRandomPosition(_playerShipModel.Position.Value);
					Spawn(enemyConfig, randomPos, Quaternion.identity);
				}
			}
		}

		public void OnTimer()
		{
			_enemySpawnTimerList.OnTimer();
			
			var enemyConfigs = _enemySpawnTimerList.TryGetAnyToSpawn();

			foreach (var enemyConfig in enemyConfigs)
			{
				var randomPos = _viewPortController.GetRandomPosition(_playerShipModel.Position.Value);
				Spawn(enemyConfig, randomPos, Quaternion.identity);
			}
		}
		
		public override AbstractEnemyController Spawn(EnemyConfig config, Vector2 pos, Quaternion rotation)
		{
			var pool = GetPoolByKey(config.ModelId);
			var activeList = GetActiveListByKey(config.ModelId);

			var enemy = pool.Get();

			if (enemy == null)
			{
				enemy = _enemyFactory.Create(config, _playerShipModel, _viewPortController);
				var view = _viewInstantiator.Instantiate(enemy.Model);
				view.BindModel(enemy.Model);
				view.SetParent(_gameContainer);
				view.OnCollisionEvent += enemy.OnCollision;
				enemy.OnDestroyEvent += OnObjDestroy;
			}

			enemy.SetPosition(pos);
			enemy.SetRotation(rotation);
			enemy.Activate();
			
			activeList.Add(enemy);
			
			return enemy;
		}

		protected override string GetKey(AbstractEnemyController obj) => obj.Model.ModelId;
		
		protected override void OnObjDestroy(AbstractEnemyController enemy)
		{
			base.OnObjDestroy(enemy);

			if (enemy is BigAsteroidController)
				OnBigAsteroidDestroy(enemy);
		}

		private void OnBigAsteroidDestroy(AbstractEnemyController enemy)
		{
			var enemyConfig = _staticData.EnemiesData.GetByType(EnemyType.SMALL_ASTEROID);
			
			for (int i = 0; i < 4; i++)
			{
				var angle = 45 + (90 * i);
				var rotation = Quaternion.Euler(Vector3.forward * angle);
				Spawn(enemyConfig, enemy.Model.Position.Value, rotation);
			}
		}
	}
}