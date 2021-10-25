using System;
using System.Collections.Generic;
using CategoryWebsite_MS.Exceptions;
using CategoryWebsite_MS.Models;

namespace CategoryWebsite_MS.Services
{
    public class SenderService: ISenderService
    {
        public HateoasResponse SendResponse(object data, IEnumerable<Link> links, string message)
        {
            return new HateoasResponse()
            {
                Links = links,
                Succeeded = true,
                StatusCode = 200,
                Title = message,
                Content = data
            };
        }

        public HateoasResponse SendError(Exception ex, IEnumerable<Link> links)
        {
            if (ex is not WebsiteCategoryException)

                return new HateoasResponse
                {
                    Links = links,
                    Succeeded = false,
                    StatusCode = 500,
                    Title = "Server error",
                    Content = null,
                };

            WebsiteCategoryException e = ex as WebsiteCategoryException;

            return new HateoasResponse
            {
                Links = links,
                Succeeded = false,
                StatusCode = e.StatusCode,
                Title = (e.StatusCode == 500) ? "Server error" : e.Message,
                Content = null,
            };

        }
 
    }
}