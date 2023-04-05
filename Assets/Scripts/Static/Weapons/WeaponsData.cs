using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Static.Helpers;

namespace Static.Weapons
{
	public class WeaponsData: StaticCollection<WeaponConfig>
	{
		public WeaponsData(JToken token) : base(token)
		{
		}

		public WeaponsData(Dictionary<int, WeaponConfig> all) : base(all)
		{
		}

		public WeaponConfig GetByType(string weaponType)
		{
			return All.Where(d => d.Value.ModelId == weaponType)
					  .Select(d => d.Value)
					  .FirstOrDefault();
		}
	}
}