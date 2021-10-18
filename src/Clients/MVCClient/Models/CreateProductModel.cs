using Microsoft.AspNetCore.Http;

namespace MVCClient.Models
{
    public class CreateProductModel
    {
        public string ProductName { get; set; }
        
        public string ProductCategory { get; set; }

        public string Description { get; set; }

        public int Stack { get; set; }

        public decimal Price { get; set; }

        public IFormFile Image { get; set; }
        
        public string WebsiteId { get; set; }
    }
}