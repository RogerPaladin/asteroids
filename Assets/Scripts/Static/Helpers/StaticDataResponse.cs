using Newtonsoft.Json;

namespace Static.Helpers
{
	public class StaticDataResponse
	{
		[JsonProperty("files")] public StaticDataFiles Files;
	}
}