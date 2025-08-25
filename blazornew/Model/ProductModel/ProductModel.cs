using System.Text.Json.Serialization;

namespace blazornew.Model.ProductModel
{
    public class ProductModel
    {
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string ProductName { get; set; }

        [JsonPropertyName("product_image")]
        public string ProductImage { get; set; }
    }
}
