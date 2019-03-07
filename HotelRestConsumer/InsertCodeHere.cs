using ModelLib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HotelRestConsumer
{
    public class HotelCode
    {
        private const string URI = "http://localhost:48074/api/hotels";
        public void DoTheStuff()
        {
            List<Hotel> hotels = GetAll();
            foreach (Hotel hotel in hotels)
            {
                Console.WriteLine($"Hotel:: {hotel}");
            }

            Console.WriteLine($"Hotel Nr #1: {GetOne(1)}");

            Console.WriteLine("Forsøger at slætte Hotel #1337.");
            Console.WriteLine($"Det lykkedes: {Delete(1337)}.");
        }

        private List<Hotel> GetAll()
        {
            List<Hotel> hotels = new List<Hotel>();

            using (HttpClient client = new HttpClient())
            {
                Task<string> resTask = client.GetStringAsync(URI);
                String jsonStr = resTask.Result;

                hotels = JsonConvert.DeserializeObject<List<Hotel>>(jsonStr);
            }
            return hotels;
        }

        private Hotel GetOne(int id)
        {
            Hotel hotel = new Hotel();

            using (HttpClient client = new HttpClient())
            {
                Task<string> resTask = client.GetStringAsync($"{URI}/{id}");
                String jsonStr = resTask.Result;

                hotel = JsonConvert.DeserializeObject<Hotel>(jsonStr);
            }
            return hotel;
        }
        private bool Delete(int id)
        {
            bool ok;
            

            using (HttpClient client = new HttpClient())
            {
                Task<HttpResponseMessage> deleteAsync = client.DeleteAsync($"{URI}/{id}");

                HttpResponseMessage resp = deleteAsync.Result;

                if (resp.IsSuccessStatusCode)
                {
                    String jsonString = resp.Content.ReadAsStringAsync().Result;
                    ok = JsonConvert.DeserializeObject<bool>(jsonString);
                }
                else
                {
                    ok = false;
                }

            }
            return ok;
        }
    }
}
