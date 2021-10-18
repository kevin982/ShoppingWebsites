using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCClient.Models
{
    public class CreateWebsiteModel
    {
        [Required(ErrorMessage = "You must enter the website name")]
        public string WebsiteName { get; set; }

        [Required(ErrorMessage = "You must provide the website category")]
        public Guid WebsiteCategory { get; set; }

        [Required(ErrorMessage = "You must provide the image")]
        public IFormFile Image {  get; set; }

        public string Location { get; set; }
    }
}
