using System.Text.Json.Serialization;

namespace Lab2.Entities
{
    public class Knife
    {
        public string Name { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public KnifeType Type { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Handy Handy { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Value Value { get; set; }

        public string Origin { get; set; }

        public Visual Visual { get; set; }
    }
}
