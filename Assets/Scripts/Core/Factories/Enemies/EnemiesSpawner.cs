using Controllers.Enemies;
using Controllers.Enemies.Asteroids;
using Core.Controllers.ViewPort;
using Core.GamePlay.Enemies;
using Model.Player;
using Static.Catalogs;
using UnityEngine;
using Utils.Events;
using Utils.Spawner;
using Views;
using Views.Catalogs;
using EnemyType = Static.Catalogs.EnemyType;

namespace Factories.Enemies
{
	public class EnemiesSpawner : AbstractSpawner<AbstractEnemyController, EnemyDataCatalog>, ITimerListener
	{
		private readonly EnemiesDataCatalog _enemiesDataCatalog;
		private readonly EnemyFactory _enemyFactory;
		private readonly TimerSystem _timerSystem;
		private readonly Transform _gameContainer;
		private readonly EnemiesViewCatalog _enemiesViewCatalog;
		private readonly EnemySpawnTimerList _enemySpawnTimerList;
		private readonly ViewPortController _viewPortController;

		private PlayerShipModel _playerShipModel;

		public EnemiesSpawner(EnemiesDataCatalog enemiesDataCatalog, EnemyFactory enemyFactory, ViewPortController viewPortController, TimerSystem timerSystem, Transform gameContainer, EnemiesViewCatalog enemiesViewCatalog)
		{
			_enemiesDataCatalog = enemiesDataCatalog;
			_enemyFactory = enemyFactory;
			_viewPortController = viewPortController;
			_timerSystem = timerSystem;
			_gameContainer = gameContainer;
			_enemiesViewCatalog = enemiesViewCatalog;

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
			_enemySpawnTimerList.SetEnemiesData(_enemiesDataCatalog);

			foreach (var enemyDataCatalog in _enemiesDataCatalog.Enemies)
			{
				for (int i = 0; i < enemyDataCatalog.StartCount; i++)
				{
					var randomPos = _viewPortController.GetRandomPosition(_playerShipModel.Position.Value);
					Spawn(enemyDataCatalog, randomPos, Quaternion.identity);
				}
			}
		}

		public void OnTimer()
		{
			_enemySpawnTimerList.OnTimer();
			
			var enemyDataCatalogs = _enemySpawnTimerList.TryGetAnyToSpawn();

			foreach (var enemyDataCatalog in enemyDataCatalogs)
			{
				var randomPos = _viewPortController.GetRandomPosition(_playerShipModel.Position.Value);
				Spawn(enemyDataCatalog, randomPos, Quaternion.identity);
			}
		}
		
		public override AbstractEnemyController Spawn(EnemyDataCatalog enemyDataCatalog, Vector2 pos,
													  Quaternion rotation)
		{
			var pool = GetPoolByKey(enemyDataCatalog.Type.ToString());
			var activeList = GetActiveListByKey(enemyDataCatalog.Type.ToString());

			var enemy = pool.Get();

			if (enemy == null)
			{
				enemy = _enemyFactory.Create(enemyDataCatalog, _playerShipModel, _viewPortController);
				var view = _enemiesViewCatalog.Create(enemy.Model);
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
			var enemyDataCatalog = _enemiesDataCatalog.GetByType(EnemyType.SmallAsteroid);
			
			for (int i = 0; i < 4; i++)
			{
				var angle = 45 + (90 * i);
				var rotation = Quaternion.Euler(Vector3.forward * angle);
				Spawn(enemyDataCatalog, enemy.Model.Position.Value, rotation);
			}
		}
	}
}