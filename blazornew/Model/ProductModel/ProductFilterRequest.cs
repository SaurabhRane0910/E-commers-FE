namespace blazornew.Model.ProductModel
{
    public class ProductFilterRequest
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortKey { get; set; } = "id";
        public string SortValue { get; set; } = "desc"; 
        public string Search { get; set; } = "";
       
    }
}
