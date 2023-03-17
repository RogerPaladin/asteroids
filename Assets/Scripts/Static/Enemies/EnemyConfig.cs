using Assets.Scripts.Static.Helpers;
using Newtonsoft.Json;

namespace Assets.Scripts.Static.Enemies
{
	public class EnemyConfig: StaticCollectionItemCode
	{
		[JsonProperty("speed", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public float Speed { get; private set; }
		
		[JsonProperty("score", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public int Score { get; private set; }
		
		[JsonProperty("start", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public int StartCount { get; private set; }
		
		[JsonProperty("respawn", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public float Respawn { get; private set; }
	}
}