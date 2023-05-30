using System.Text.Json.Serialization;

namespace Lab2.Entities
{
    public class Visual
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Material Material { get; set; }

        public HandType HandType { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public bool HasBloodStream { get; set; }
    }
    
}
