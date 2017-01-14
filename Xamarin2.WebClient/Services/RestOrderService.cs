using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using Xamarin2.Data.Models;

namespace Xamarin2.WebClient.Services
{
    public class RestOrderService
    {
        static HttpClient client;

        static RestOrderService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }

        public static async Task<IEnumerable<Order>> GetOrders()
        {
            IEnumerable<Order> Items = new List<Order>();

            var uri = new Uri(Constants.RestUrlOrders);

            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Items = JsonConvert.DeserializeObject<IEnumerable<Order>>(content);
            }

            return Items;
        }

        public static async Task<Order> GetOrder(int ID)
        {
            Order order;

            var uri = new Uri(string.Format(Constants.RestUrlOrder, ID.ToString()));

            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                order = JsonConvert.DeserializeObject<Order>(content);
                return order;
            }

            return null;
        }

        public static async void Save(Order order)
        {
            var uri = new Uri(string.Format(Constants.RestUrlOrder, order.OrderID.ToString()));

            //var jsonFormatter = new JsonMediaTypeFormatter();
            var foo = await client.PutAsJsonAsync(uri, order);
            //var value = JsonConvert.SerializeObject(order);
            //var foo = new StringContent(value);

            ////HttpContent content = new ObjectContent<Order>(order, jsonFormatter);
            //HttpResponseMessage responseMessage = client.PutAsync(uri, foo).Result;
        }
    }
}
