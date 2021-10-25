using System;
using System.Collections.Generic;
using CategoryWebsite_MS.Models;

namespace CategoryWebsite_MS.Services
{
    public interface ISenderService
    {
        HateoasResponse SendResponse(object data, IEnumerable<Link> links, string message);

        HateoasResponse SendError(Exception ex, IEnumerable<Link> links);
    }
}