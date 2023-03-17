using Assets.Scripts.Static.Helpers;
using Newtonsoft.Json;
using UnityEngine;

namespace Assets.Scripts.Static.Weapons
{
	public class WeaponConfig: StaticCollectionItemCode
	{
		[JsonProperty("projectile_speed", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public float ProjectileSpeed { get; private set; }

		[JsonProperty("ammo_start_count", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public int AmmoStartCount { get; private set; }

		[JsonProperty("ammo_max_count", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public int AmmoMaxCount { get; private set; }
		
		[JsonProperty("ammo_refresh", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public float AmmoRefresh { get; private set; }
		
		[JsonProperty("projectile_life_time", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public float ProjectileLifeTime { get; private set; }
		
		[JsonProperty("projectile_grow_time", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public float ProjectileGrowTime { get; private set; }

		[JsonProperty("projectile_grow_speed", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public float ProjectileGrowSpeed { get; private set; }
		
		[JsonProperty("projectile_destroy_on_collision", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public bool ProjectileDestroyOnCollision { get; private set; }
	}
}