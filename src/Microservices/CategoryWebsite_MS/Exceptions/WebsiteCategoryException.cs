using System;

namespace CategoryWebsite_MS.Exceptions
{
    public class WebsiteCategoryException: Exception
    {
        public int StatusCode { get; set; }

        public WebsiteCategoryException(string message) : base(message) { }
    }
}