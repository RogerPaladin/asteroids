using System;
using Assets.Scripts.Events;
using Assets.Scripts.GamePlay.Enemies.Asteroids;
using Assets.Scripts.GamePlay.Player;
using Assets.Scripts.Static;
using Assets.Scripts.Static.Enemies;
using Assets.Scripts.Utils.Spawner;
using UnityEngine;

namespace Assets.Scripts.GamePlay.Enemies
{
	public class EnemiesSpawner : AbstractSpawner<AbstractEnemyController, EnemyConfig>, IDisposable, ITimerListener
	{
		private readonly StaticData _staticData;
		private readonly EnemyFactory _enemyFactory;
		private readonly Camera _camera;
		private readonly TimerSystem _timerSystem;
		
		private PlayerShipModel _playerShipModel;

		private readonly EnemySpawnTimerList _enemySpawnTimerList;

		public EnemiesSpawner(StaticData staticData, EnemyFactory enemyFactory, Camera camera, TimerSystem timerSystem)
		{
			_staticData = staticData;
			_enemyFactory = enemyFactory;
			_camera = camera;
			_timerSystem = timerSystem;
			
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
		
		public void InitialSpawn()
		{
			_enemySpawnTimerList.SetEnemiesData(_staticData.EnemiesData);

			foreach (var enemyConfig in _staticData.EnemiesData.All.Values)
			{
				for (int i = 0; i < enemyConfig.StartCount; i++)
				{
					var randomPos = Utils.Utils.GetRandomPosition(_camera, _playerShipModel.Position);
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
				var randomPos = Utils.Utils.GetRandomPosition(_camera, _playerShipModel.Position);
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
				enemy = _enemyFactory.Create(config, _playerShipModel);
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
				Spawn(enemyConfig, enemy.Model.Position, rotation);
			}
		}
	}
}