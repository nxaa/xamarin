using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
//using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using Xamarin2.Data.Models;

namespace Xamarin2.WebClient.Services
{
    public static class RestOrderService
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

            var uri = new Uri(string.Format(Constants.RestUrlOrders, string.Empty));

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

            var uri = new Uri(string.Format(Constants.RestUrlOrders, ID.ToString()));

            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                order = JsonConvert.DeserializeObject<Order>(content);
                return order;
            }

            return null;
        }

        public static void Save(Order order)
        {
            var uri = new Uri(string.Format(Constants.RestUrlOrders, order.OrderID.ToString()));

            //var foo = await client.PutAsJsonAsync(uri, order);

            //var jsonFormatter = new JsonMediaTypeFormatter();
            var value = JsonConvert.SerializeObject(order);
            var foo = new StringContent(value);
            foo.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            ////HttpContent content = new ObjectContent<Order>(order, jsonFormatter);
            HttpResponseMessage responseMessage = client.PutAsync(uri, foo).Result;
        }

        public static async Task<int> Add(Order order)
        {
            var uri = new Uri(string.Format(Constants.RestUrlOrders, string.Empty));

            //var foo = await client.PostAsJsonAsync(uri, order);

            //var jsonFormatter = new JsonMediaTypeFormatter();
            var value = JsonConvert.SerializeObject(order);
            var foo = new StringContent(value);
            foo.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            ////HttpContent content = new ObjectContent<Order>(order, jsonFormatter);
            HttpResponseMessage responseMessage = client.PostAsync(uri, foo).Result;
            
            var content = await responseMessage.Content.ReadAsStringAsync();
            var newOrder = JsonConvert.DeserializeObject<Order>(content);

            return newOrder.OrderID;
        }
    }
}
