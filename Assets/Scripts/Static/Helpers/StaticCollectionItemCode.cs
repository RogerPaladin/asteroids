using Newtonsoft.Json;

namespace Static.Helpers
{
    public class StaticCollectionItemCode : StaticCollectionItem
    {
        [JsonProperty("#id")]
        public string ModelId { get; set; }
    }
}