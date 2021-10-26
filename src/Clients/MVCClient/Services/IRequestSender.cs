using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCClient.Services
{
    public interface IRequestSender
    {
        Task<JObject> CreateWithImage(Dictionary<string, string> props, IFormFile image, string accessToken, string uri);
        Task<object> SendRequestAsync(string method, string uri, string accessToken, bool asJObject = false, object data = null);
    }
}