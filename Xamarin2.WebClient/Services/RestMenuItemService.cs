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
    public class RestMenuItemService
    {
        static HttpClient client;

        static RestMenuItemService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }

        public static async Task<IEnumerable<MenuItem>> GetMenuItems()
        {
            IEnumerable<MenuItem> Items = new List<MenuItem>();

            var uri = new Uri(string.Format(Constants.RestUrlMenuItems, string.Empty));

            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Items = JsonConvert.DeserializeObject<IEnumerable<MenuItem>>(content);
            }

            return Items;
        }

        public static async Task<MenuItem> GetMenuItem(int ID)
        {
            MenuItem order;

            var uri = new Uri(string.Format(Constants.RestUrlMenuItems, ID.ToString()));

            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                order = JsonConvert.DeserializeObject<MenuItem>(content);
                return order;
            }

            return null;
        }
    }
}
