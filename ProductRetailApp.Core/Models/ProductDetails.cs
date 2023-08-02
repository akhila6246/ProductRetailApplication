

namespace ProductRetailApp.Core.Models
{
    public class ProductDetailsInfo
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public DateTime PostedDate { get; set; }
        public string ProductStatus { get; set; } 
    }
}
