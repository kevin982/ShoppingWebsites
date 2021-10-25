using System.Collections.Generic;
using CategoryWebsite_MS.Models;

namespace CategoryWebsite_MS
{
    public class GenericLinks
    {
        private static string AppUri = "https://localhost:9004";

        public static IEnumerable<Link> GetCategoryLinks()
        {
            return new List<Link>()
            {
                new Link(){Name = "Create a new website category", Href = $"{AppUri}/api/v1/WebsiteCategory", Method = "POST"},
                new Link(){Name = "Get all website categories", Href = $"{AppUri}/api/v1/WebsiteCategory", Method = "GET"}
            };
        }
    }
}