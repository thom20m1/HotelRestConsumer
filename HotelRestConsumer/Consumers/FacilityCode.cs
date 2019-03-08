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
    public class FacilityCode:Consumer<Facility>
    {
        public FacilityCode(string uniqueURIBit)
            : base(uniqueURIBit)
        {
        }


        public override void DoTheStuff()
        {
            Console.WriteLine($"Opretter ny facilitet. Success? : {Post(new Facility(20, 1, "Spa"))}");
            Console.WriteLine($"Opretter ny facilitet. Success? : {Post(new Facility(21, 1, "Casino"))}");
            Console.WriteLine($"Opretter ny facilitet. Success? : {Post(new Facility(21, 2, "Bar"))}");
            Console.WriteLine($"Opretter ny facilitet. Success? : {Post(new Facility(22, 1, "Pool"))}");
            Console.WriteLine($"Opretter ny facilitet. Success? : {Post(new Facility(22, 2, "All-inclusive"))}");
            Console.WriteLine($"Opretter ny facilitet. Success? : {Post(new Facility(22, 3, "Gaming Arcade"))}");
            Console.WriteLine($"Opretter ny facilitet. Success? : {Post(new Facility(23, 1, "Jukebox"))}");
            Console.WriteLine($"Opretter ny facilitet. Success? : {Post(new Facility(23, 2, "Car-wash"))}");

            Console.WriteLine();
            Console.WriteLine("Her ses alle faciliteterne:");
            Console.WriteLine(GetAll());
            Console.WriteLine();
            Console.WriteLine($"Bytter bilvasken ved Harry's Motel ud med autoværksted. Success? : {Put(23, 2, new Facility(23, 2, "Certified Mechanic"))}");
            Console.WriteLine("Den ny Facilitet:");
            Console.WriteLine(GetOne(23, 2));
            Console.WriteLine();
            Console.WriteLine($"Fjerner en facilitet. Success? : {Delete(21, 2)}");
            Console.WriteLine();
            Console.WriteLine("Her ses effekterne af ændringerne:");
            Console.WriteLine(GetAll());
        }


        private Facility GetOne(int hotelNo, int facilityNo)
        {

            Facility facility;

            using (HttpClient client = new HttpClient())
            {
                Task<string> resTask = client.GetStringAsync($"{URI}/{hotelNo},{facilityNo}");
                String jsonStr = resTask.Result;

                facility = JsonConvert.DeserializeObject<Facility>(jsonStr);
            }
            return facility;
        }

        private bool Delete(int hotelNo, int facilityNo)
        {
            bool ok;


            using (HttpClient client = new HttpClient())
            {
                Task<HttpResponseMessage> deleteAsync = client.DeleteAsync($"{URI}/{hotelNo},{facilityNo}");

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
        protected virtual bool Put(int hotelNo, int facilityNo, Facility facility)
        {
            bool ok;


            using (HttpClient client = new HttpClient())
            {
                string jsonString = JsonConvert.SerializeObject(facility);
                StringContent content = new StringContent(jsonString, Encoding.ASCII, "application/json");


                Task<HttpResponseMessage> putAsync = client.PutAsync($"{URI}/{hotelNo},{facilityNo}", content);

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
