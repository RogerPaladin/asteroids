using System.Collections.Generic;
using Controllers.Enemies;
using Static.Enemies;

namespace Core.GamePlay.Enemies
{
	public class EnemySpawnTimerList
	{
		private Dictionary<string, float> _timers = new Dictionary<string, float>();
		private readonly Dictionary<string, HashSet<AbstractEnemyController>> _active;
		private EnemiesData _enemiesData;

		public EnemySpawnTimerList(Dictionary<string, HashSet<AbstractEnemyController>> active)
		{
			_active = active;
		}
		
		public void OnLevelStart()
		{
			_timers.Clear();
		}
		
		public void SetEnemiesData(EnemiesData enemiesData)
		{
			_enemiesData = enemiesData;

			foreach (var enemyConfig in _enemiesData.All.Values)
			{
				if (enemyConfig.Respawn == 0)
					continue;
				
				_timers[enemyConfig.ModelId] = 0;
			}
		}

		public void OnTimer()
		{
			foreach (var enemyConfig in _enemiesData.All.Values)
			{
				if (enemyConfig.Respawn == 0)
					continue;

				if (!_active.ContainsKey(enemyConfig.ModelId))
					continue;
				
				var activeList = _active[enemyConfig.ModelId];
				
				if (activeList.Count < enemyConfig.StartCount)
				{
					if (_timers[enemyConfig.ModelId] == 0)
						_timers[enemyConfig.ModelId] = enemyConfig.Respawn;
					else
						_timers[enemyConfig.ModelId]--;
				}
			}
		}

		public HashSet<EnemyConfig> TryGetAnyToSpawn()
		{
			var result = new HashSet<EnemyConfig>();
			
			foreach (var kv in _timers)
			{
				var enemyConfig = _enemiesData.GetByType(kv.Key);
				
				if (!_active.ContainsKey(enemyConfig.ModelId))
					continue;
				
				var activeList = _active[enemyConfig.ModelId];
				
				if (activeList.Count >= enemyConfig.StartCount)
					continue;
				
				if (kv.Value == 0)
					result.Add(enemyConfig);
			}

			return result;
		}
	}
}