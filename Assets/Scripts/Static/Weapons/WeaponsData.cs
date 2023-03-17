using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Static.Helpers;
using Newtonsoft.Json.Linq;

namespace Assets.Scripts.Static.Weapons
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