using Assets.Scripts.Static.Helpers;
using Newtonsoft.Json;
using UnityEngine;

namespace Assets.Scripts.Static.Effects
{
	public class EffectConfig: StaticCollectionItemCode
	{
		[JsonProperty("time", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public float Time { get; private set; }
	}
}