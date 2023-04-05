using Newtonsoft.Json;

namespace Static.Helpers
{
    public class StaticCollectionItem
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
