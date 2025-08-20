using System.Text.Json.Serialization;

namespace blazornew.Model.signup
{
    public class LocationModel
    {
        public int Id { get; set; }
        [JsonPropertyName("country_name")] 
        public string Name { get; set; }

        //[JsonPropertyName("state_name")]
        //public string StateName { get; set; }

        //[JsonPropertyName("city_name")]
        //public string CityName { get; set; }
    }

    public class StateModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("state_name")]
        public string StateName { get; set; }
    }

    public class CityModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("city_name")]
        public string CityName { get; set; }
    }
}
