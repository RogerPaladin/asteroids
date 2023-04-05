using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Static.Helpers;

namespace Static.Enemies
{
	public class EnemiesData: StaticCollection<EnemyConfig>
	{
		public EnemiesData(JToken token) : base(token)
		{
		}

		public EnemiesData(Dictionary<int, EnemyConfig> all) : base(all)
		{
		}
		
		public EnemyConfig GetByType(string enemyType)
		{
			return All.Where(d => d.Value.ModelId == enemyType)
					  .Select(d => d.Value)
					  .FirstOrDefault();
		}
	}
}