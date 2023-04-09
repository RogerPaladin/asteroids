using System.Collections.Generic;
using Controllers.Enemies;
using Static.Catalogs;

namespace Core.GamePlay.Enemies
{
	public class EnemySpawnTimerList
	{
		private Dictionary<EnemyType, float> _timers = new Dictionary<EnemyType, float>();
		private readonly Dictionary<string, HashSet<AbstractEnemyController>> _active;
		private EnemiesDataCatalog _enemiesDataCatalog;

		public EnemySpawnTimerList(Dictionary<string, HashSet<AbstractEnemyController>> active)
		{
			_active = active;
		}
		
		public void OnLevelStart()
		{
			_timers.Clear();
		}
		
		public void SetEnemiesData(EnemiesDataCatalog enemiesDataCatalog)
		{
			_enemiesDataCatalog = enemiesDataCatalog;

			foreach (var enemyDataCatalog in _enemiesDataCatalog.Enemies)
			{
				if (enemyDataCatalog.Respawn == 0)
					continue;
				
				_timers[enemyDataCatalog.Type] = 0;
			}
		}

		public void OnTimer()
		{
			foreach (var enemyDataCatalog in _enemiesDataCatalog.Enemies)
			{
				if (enemyDataCatalog.Respawn == 0)
					continue;

				if (!_active.ContainsKey(enemyDataCatalog.Type.ToString()))
					continue;
				
				var activeList = _active[enemyDataCatalog.Type.ToString()];
				
				if (activeList.Count < enemyDataCatalog.StartCount)
				{
					if (_timers[enemyDataCatalog.Type] == 0)
						_timers[enemyDataCatalog.Type] = enemyDataCatalog.Respawn;
					else
						_timers[enemyDataCatalog.Type]--;
				}
			}
		}

		public HashSet<EnemyDataCatalog> TryGetAnyToSpawn()
		{
			var result = new HashSet<EnemyDataCatalog>();
			
			foreach (var kv in _timers)
			{
				var enemyDataCatalog = _enemiesDataCatalog.GetByType(kv.Key);
				
				if (!_active.ContainsKey(enemyDataCatalog.Type.ToString()))
					continue;
				
				var activeList = _active[enemyDataCatalog.Type.ToString()];
				
				if (activeList.Count >= enemyDataCatalog.StartCount)
					continue;
				
				if (kv.Value == 0)
					result.Add(enemyDataCatalog);
			}

			return result;
		}
	}
}