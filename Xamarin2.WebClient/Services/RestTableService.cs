using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin2.Data.Models;

namespace Xamarin2.WebClient.Services
{
    public static class RestTableService
    {
        static HttpClient client;

        static RestTableService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }

        public static async Task<IEnumerable<Table>> GetFreeTables()
        {
            IEnumerable<Table> Items = new List<Table>();

            var uri = new Uri(Constants.RestUrlFreeTables);

            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Items = JsonConvert.DeserializeObject<IEnumerable<Table>>(content);
            }

            return Items;
        }

        public static async Task<IEnumerable<Table>> GetTables()
        {
            IEnumerable<Table> Items = new List<Table>();

            var uri = new Uri(string.Format(Constants.RestUrlTables, string.Empty));

            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Items = JsonConvert.DeserializeObject<IEnumerable<Table>>(content);
            }

            return Items;
        }
    }
}
