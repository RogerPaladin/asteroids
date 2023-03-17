using Newtonsoft.Json;

namespace Assets.Scripts.Static.Helpers
{
    public class StaticCollectionItemCode : StaticCollectionItem
    {
        [JsonProperty("#id")]
        public string ModelId { get; set; }
    }
}