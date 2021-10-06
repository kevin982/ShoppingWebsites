using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCClient.Models
{
    public class CreateWebsiteCategoryModel
    {
        [Required]
        public string CategoryName { get; set; }
    }
}
