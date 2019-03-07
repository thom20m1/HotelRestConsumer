using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ModelLib.Model;
using Newtonsoft.Json;

namespace HotelRestConsumer.Consumers
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

                Console.WriteLine("Opretter nyt hotelObjekt, ID findes.");
                Console.WriteLine($"Lykkedes? : {Post(new Hotel(1, "SjussHus", "Strædevej 3", "28941323", "kontakt@mail.dk"))}");
                Console.WriteLine("Opretter nyt hotel objekt, unikt ID.");
                Console.WriteLine($"Lykkedes? : {Post(new Hotel(2, "D'Angleterre", "Kongens Nytorv 24", "28941324", "reception@brevpost.com"))}");
                Console.WriteLine();
                Console.WriteLine($"Opdaterer nr 2. Det Lykkedes? {Put(2, new Hotel(2, "D'Angleterre", "Nordhavn nr 9", "28941324", "reception@brevpost.com"))}");
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
            private bool Post(Hotel hotel)
            {
                bool ok;


                using (HttpClient client = new HttpClient())
                {
                    string jsonString = JsonConvert.SerializeObject(hotel);
                    StringContent content = new StringContent(jsonString, Encoding.ASCII, "application/json");


                    Task<HttpResponseMessage> postAsync = client.PostAsync(URI, content);

                    HttpResponseMessage resp = postAsync.Result;

                    if (resp.IsSuccessStatusCode)
                    {
                        String jsonResString = resp.Content.ReadAsStringAsync().Result;
                        ok = JsonConvert.DeserializeObject<bool>(jsonResString);
                    }
                    else
                    {
                        ok = false;
                    }

                }
                return ok;
            }

            private bool Put(int id, Hotel hotel)
            {
                bool ok;


                using (HttpClient client = new HttpClient())
                {
                    string jsonString = JsonConvert.SerializeObject(hotel);
                    StringContent content = new StringContent(jsonString, Encoding.ASCII, "application/json");


                    Task<HttpResponseMessage> putAsync = client.PutAsync($"{URI}/{id}", content);

                    HttpResponseMessage resp = putAsync.Result;

                    if (resp.IsSuccessStatusCode)
                    {
                        String jsonResString = resp.Content.ReadAsStringAsync().Result;
                        ok = JsonConvert.DeserializeObject<bool>(jsonResString);
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
