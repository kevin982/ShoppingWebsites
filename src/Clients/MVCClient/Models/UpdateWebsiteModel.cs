using System;
using Microsoft.AspNetCore.Http;

namespace MVCClient.Models
{
    public class UpdateWebsiteModel
    {
        public Guid WebsiteId { get; set; }
        public string  NewName{ get; set; }

        public Guid WebsiteCategory { get; set; }
        public string Location { get; set; }
        public IFormFile Image { get; set; }
        
    }
}