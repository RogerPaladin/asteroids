using Newtonsoft.Json;

namespace Assets.Scripts.Static.Helpers
{
    public class StaticCollectionItem
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
