using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin2.Data.Models;

namespace Xamarin2.WebClient.Services
{
    public class RestReservationService
    {
        static HttpClient client;

        static RestReservationService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }

        public static async Task<IEnumerable<Reservation>> GetReservations()
        {
            IEnumerable<Reservation> Items = new List<Reservation>();

            var uri = new Uri(string.Format(Constants.RestUrlReservations, string.Empty));

            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Items = JsonConvert.DeserializeObject<IEnumerable<Reservation>>(content);
            }

            return Items;
        }

        public static async Task<Reservation> GetReservation(int ID)
        {
            Reservation reservation;

            var uri = new Uri(string.Format(Constants.RestUrlReservations, ID.ToString()));

            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                reservation = JsonConvert.DeserializeObject<Reservation>(content);
                return reservation;
            }

            return null;
        }

        public static void Save(Reservation reservation)
        {
            var uri = new Uri(string.Format(Constants.RestUrlReservations, reservation.ReservationID.ToString()));

            //var foo = await client.PutAsJsonAsync(uri, order);

            //var jsonFormatter = new JsonMediaTypeFormatter();
            var value = JsonConvert.SerializeObject(reservation);
            var foo = new StringContent(value);
            foo.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            ////HttpContent content = new ObjectContent<Order>(order, jsonFormatter);
            HttpResponseMessage responseMessage = client.PutAsync(uri, foo).Result;
        }
    }
}
