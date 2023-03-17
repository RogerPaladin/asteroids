using System.Collections.Generic;
using Assets.Scripts.Static.Effects;
using Assets.Scripts.Static.Enemies;
using Assets.Scripts.Static.Player;
using Assets.Scripts.Static.Weapons;
using Newtonsoft.Json;

namespace Assets.Scripts.Static.Helpers
{
	public class StaticDataFiles
	{
		[JsonProperty(StaticDataFileName.PLAYER)] public PlayerConfig PlayerConfig;
		[JsonProperty(StaticDataFileName.ENEMIES)] public Dictionary<int, EnemyConfig> EnemiesJson;
		[JsonProperty(StaticDataFileName.WEAPONS)] public Dictionary<int, WeaponConfig> WeaponsJson;
		[JsonProperty(StaticDataFileName.EFFECTS)] public Dictionary<int, EffectConfig> EffectsJson;
	}
}