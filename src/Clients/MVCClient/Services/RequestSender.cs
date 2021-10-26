using Microsoft.AspNetCore.Http;
using MVCClient.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MVCClient.Services
{
    public class RequestSender : IRequestSender
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RequestSender(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<object> SendRequestAsync(string method, string uri, string accessToken, bool asJObject = false, object data = null)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("Ocelot");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                string bodyContent = "";
                HttpResponseMessage response = null;

                switch (method)
                {
                    case "POST":

                        StringContent contentPost = new(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                        response = await client.PostAsync(uri, contentPost);
                        bodyContent = await response.Content.ReadAsStringAsync();
                        break;
                    case "PATCH":

                        StringContent contentPatch = new(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                        response = await client.PatchAsync(uri, contentPatch);
                        bodyContent = await response.Content.ReadAsStringAsync();
                        break;

                    case "DELETE":

                        response = await client.DeleteAsync(uri);
                        bodyContent = await response.Content.ReadAsStringAsync();
                        break;
                    default:

                        response = await client.GetAsync(uri);
                        bodyContent = await response.Content.ReadAsStringAsync();
                        break;
                }

                if (asJObject)
                {
                    if (!string.IsNullOrEmpty(bodyContent))
                    {
                        JObject result = JObject.Parse(bodyContent);
                        result.Add("succeeded", true);
                        return result;
                    }
                    JObject error = new();

                    error.Add("statusCode", (int)response.StatusCode);
                    error.Add("title", response.ReasonPhrase);
                    error.Add("succeeded", false);

                    return error;
                }
                else
                {
                    if (!string.IsNullOrEmpty(bodyContent)) return bodyContent;

                    var errorResponse = new
                    {
                        statusCode = (int)response.StatusCode,
                        title = response.ReasonPhrase,
                        succeeded = false
                    };

                    return JsonConvert.SerializeObject(errorResponse);
                }

            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<JObject> CreateWithImage(Dictionary<string, string> props, IFormFile image, string accessToken, string uri)
        {
            try
            {
                if (!image.IsImage())
                {
                    JObject e = new();

                    e.Add("statusCode", 400);
                    e.Add("title", "The file must be an image");
                    e.Add("succeeded", false);

                    return e;
                }

                var content = new MultipartFormDataContent();

                content.Add(new StreamContent(image.OpenReadStream()), "Image", "Image");

                content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data") { Name = "Image", FileName = image.FileName };

                foreach (var prop in props)
                {
                    content.Add(new StringContent(prop.Value), prop.Key);
                }

                var client = _httpClientFactory.CreateClient("Ocelot");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var result = await client.PostAsync(uri, content);

                string bodyContent = await result.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(bodyContent)) return JObject.Parse(bodyContent);

                JObject error = new();

                error.Add("statusCode", (int)result.StatusCode);
                error.Add("title", result.ReasonPhrase);
                error.Add("succeeded", false);

                return error;


            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
