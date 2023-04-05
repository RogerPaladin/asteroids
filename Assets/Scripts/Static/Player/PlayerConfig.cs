using Newtonsoft.Json;

namespace Static.Player
{
	public class PlayerConfig
	{
		[JsonProperty("speed", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public float MaxSpeed { get; private set; }
		
		[JsonProperty("rotation", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public float MaxRotationSpeed { get; private set; }
		
		[JsonProperty("accel", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public float Acceleration { get; private set; }
		
		[JsonProperty("deaccel", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public float DeAcceleration { get; private set; }
	}
}