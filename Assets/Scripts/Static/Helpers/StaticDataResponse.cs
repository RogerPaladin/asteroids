using Newtonsoft.Json;

namespace Assets.Scripts.Static.Helpers
{
	public class StaticDataResponse
	{
		[JsonProperty("files")] public StaticDataFiles Files;
	}
}