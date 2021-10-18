using System;

namespace MVCClient.Models
{
    public class SaveWebsiteModel
    {
        public Guid WebsiteId { get; set; }

        public string Description { get; set; }

        public string WebsiteName { get; set; }

        public string ImageUrl { get; set; }
    }
}