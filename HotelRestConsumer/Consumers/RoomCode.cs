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
    public class RoomCode:Consumer<Room>
    {
        public RoomCode(string uniqueURIBit)
            : base(uniqueURIBit)
        {
        }


        public override void DoTheStuff()
        {
            Console.WriteLine($"Opretter nyt værelse. Success? : {Post(new Room(1, 20, "2", 500))}");
            Console.WriteLine($"Opretter nyt værelse. Success? : {Post(new Room(2, 20, "2", 500))}");
            Console.WriteLine($"Opretter nyt værelse. Success? : {Post(new Room(3, 20, "3", 800))}");
            Console.WriteLine($"Opretter nyt værelse. Success? : {Post(new Room(4, 20, "2", 500))}");
            Console.WriteLine($"Opretter nyt værelse. Success? : {Post(new Room(1, 21, "4", 4000))}");
            Console.WriteLine($"Opretter nyt værelse. Success? : {Post(new Room(2, 21, "2", 2500))}");
            Console.WriteLine($"Opretter nyt værelse. Success? : {Post(new Room(3, 21, "2", 2500))}");
            Console.WriteLine($"Opretter nyt værelse. Success? : {Post(new Room(1, 22, "3", 4000))}");
            Console.WriteLine($"Opretter nyt værelse. Success? : {Post(new Room(2, 22, "3", 1000))}");
            Console.WriteLine($"Opretter nyt værelse. Success? : {Post(new Room(3, 22, "3", 1000))}");
            Console.WriteLine($"Opretter nyt værelse. Success? : {Post(new Room(4, 22, "5", 1000))}");
            Console.WriteLine($"Opretter nyt værelse. Success? : {Post(new Room(5, 22, "5", 1000))}");
            Console.WriteLine($"Opretter nyt værelse. Success? : {Post(new Room(1, 23, "1", 200))}");
            Console.WriteLine($"Opretter nyt værelse. Success? : {Post(new Room(2, 23, "1", 200))}");
            Console.WriteLine($"Opretter nyt værelse. Success? : {Post(new Room(3, 23, "1", 200))}");
            Console.WriteLine($"Opretter nyt værelse. Success? : {Post(new Room(4, 23, "2", 300))}");
            Console.WriteLine($"Opretter nyt værelse. Success? : {Post(new Room(5, 23, "2", 300))}");
            Console.WriteLine($"Opretter nyt værelse. Success? : {Post(new Room(1, 24, "1", 50))}");
            Console.WriteLine($"Opretter nyt værelse. Success? : {Post(new Room(2, 24, "1", 50))}");
            Console.WriteLine($"Opretter nyt værelse. Success? : {Post(new Room(3, 24, "1", 50))}");

        }

        private Room GetOne(int roomNo, int hotelNo)
        {

            Room room;

            using (HttpClient client = new HttpClient())
            {
                Task<string> resTask = client.GetStringAsync($"{URI}/{roomNo},{hotelNo}");
                String jsonStr = resTask.Result;

                room = JsonConvert.DeserializeObject<Room>(jsonStr);
            }
            return room;
        }

        private bool Delete(int roomNo, int hotelNo)
        {
            bool ok;


            using (HttpClient client = new HttpClient())
            {
                Task<HttpResponseMessage> deleteAsync = client.DeleteAsync($"{URI}/{roomNo},{hotelNo}");

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
        protected virtual bool Put(int roomNo, int hotelNo, Room room)
        {
            bool ok;


            using (HttpClient client = new HttpClient())
            {
                string jsonString = JsonConvert.SerializeObject(room);
                StringContent content = new StringContent(jsonString, Encoding.ASCII, "application/json");


                Task<HttpResponseMessage> putAsync = client.PutAsync($"{URI}/{roomNo},{hotelNo}", content);

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
