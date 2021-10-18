using System.ComponentModel.DataAnnotations;

namespace MVCClient.Models
{
    public class CreateProductCategoryModel
    {
        [Required]
        public string CategoryName { get; set; }
    }
}