using Newtonsoft.Json;
using Static.Helpers;

namespace Static.Effects
{
	public class EffectConfig: StaticCollectionItemCode
	{
		[JsonProperty("time", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public float Time { get; private set; }
	}
}