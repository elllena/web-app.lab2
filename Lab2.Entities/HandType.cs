using System.Text.Json.Serialization;

namespace Lab2.Entities
{
    public class HandType
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public HandMaterial Material { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public WoodType? WoodType { get; set; }
    }
}