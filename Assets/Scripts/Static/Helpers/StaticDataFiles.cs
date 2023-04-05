using System.Collections.Generic;
using Newtonsoft.Json;
using Static.Effects;
using Static.Enemies;
using Static.Player;
using Static.Weapons;

namespace Static.Helpers
{
	public class StaticDataFiles
	{
		[JsonProperty(StaticDataFileName.PLAYER)] public PlayerConfig PlayerConfig;
		[JsonProperty(StaticDataFileName.ENEMIES)] public Dictionary<int, EnemyConfig> EnemiesJson;
		[JsonProperty(StaticDataFileName.WEAPONS)] public Dictionary<int, WeaponConfig> WeaponsJson;
		[JsonProperty(StaticDataFileName.EFFECTS)] public Dictionary<int, EffectConfig> EffectsJson;
	}
}