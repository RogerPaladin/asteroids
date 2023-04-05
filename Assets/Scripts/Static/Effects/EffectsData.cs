using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Static.Helpers;

namespace Static.Effects
{
	public class EffectsData: StaticCollection<EffectConfig>
	{
		public EffectsData(JToken token) : base(token)
		{
		}

		public EffectsData(Dictionary<int, EffectConfig> all) : base(all)
		{
		}

		public EffectConfig GetByType(string type)
		{
			return All.Where(d => d.Value.ModelId == type)
					  .Select(d => d.Value)
					  .FirstOrDefault();
		}
	}
}