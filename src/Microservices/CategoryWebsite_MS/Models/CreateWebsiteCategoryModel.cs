using System.ComponentModel.DataAnnotations;

namespace CategoryWebsite_MS.Models
{
    public class CreateWebsiteCategoryModel
    {
        [Required]
        public string CategoryName { get; set; }
    }
}