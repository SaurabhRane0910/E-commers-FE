using System.Text.Json.Serialization;

namespace blazornew.Model.CategoryModel
{
    public class Category
    {
        public int Id { get; set; }

        [JsonPropertyName("category_name")]
        public string CategoryName { get; set; }

        [JsonPropertyName("category_image")]
        public string CategoryImage { get; set; }
    }
}
