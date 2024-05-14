using CursVN.Core.Abstractions.Other;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace CursVN.Application.Other
{
    public class ImgBBService : IImageService
    {
        private string _apiKey;
        public ImgBBService(string apiKey) 
        {
            _apiKey = apiKey;
        }
        public async Task<string> Upload(MemoryStream ms)
        {
            var bytes = ms.ToArray();
            var imageBase64 = Convert.ToBase64String(bytes);

            using(HttpClient httpClient = new HttpClient())
            {
                var form = new MultipartFormDataContent();
                form.Add(new StringContent(imageBase64), "image");
                form.Add(new StringContent(_apiKey), "key");

                var response = await httpClient.PostAsync("https://api.imgbb.com/1/upload", form);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();

                return JObject.Parse(responseContent)["data"]["image"]["url"].ToString();
            }
        }
    }
}
