using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MartaHackathon.Services
{
    class BeaconDataDTO
    {
        public string Proximity { get; set; }

        public int Major { get; set; }

        public int Minor { get; set; }

        public string Uuid { get; set; }
    }
    class BeaconDataService
    {
        public async Task Help(bool needHelp, string userId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://martahackathon.azurewebsites.net/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                await PostAsJsonAsync(client, "http://martahackathon.azurewebsites.net",
                    $"api/help?userId={userId}", needHelp, CancellationToken.None);
            }
        }

        public async Task<string> SendBeaconData(IEnumerable<BeaconDataDTO> beaconData,string userId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://martahackathon.azurewebsites.net/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await PostAsJsonAsync(client, "http://martahackathon.azurewebsites.net",
                    $"api/beaconData?userId={userId}", beaconData, CancellationToken.None);

                var result = await ReadAsJsonAsync<string>(response.Content);

                return result;
            }
        }

        public static async Task<T> ReadAsJsonAsync<T>(HttpContent httpContent)
        {
            var searchResultStream = await httpContent.ReadAsStreamAsync();

            var serializer = new JsonSerializer();

            using (StreamReader reader = new StreamReader(searchResultStream))
            {
                using (var jsonTextReader = new JsonTextReader(reader))
                {
                    return serializer.Deserialize<T>(jsonTextReader);
                }
            }
        }

        public Task<HttpResponseMessage> PostAsJsonAsync<T>(HttpClient client,string baseAddress, string requestUri, T item, CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(item), Encoding.Unicode, "application/json"),
                Method = HttpMethod.Post,
                RequestUri = new Uri(baseAddress + "/" + requestUri)
            };
            return SendAsync(client,request, cancellationToken);
        }

        public async Task<HttpResponseMessage> SendAsync(HttpClient client,HttpRequestMessage request, CancellationToken cancellationToken)
        {

            var response = await client.SendAsync(request, cancellationToken);

            return response;
        }
    }
}
