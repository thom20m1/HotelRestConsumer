using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ModelLib.Model;
using Newtonsoft.Json;

namespace HotelRestConsumer
{
    public abstract class Consumer<T>
    {
        protected const string baseURIBit = "http://localhost:48074/api/";
        protected string URI;

        protected Consumer(string uniqueURIBit)
        {
            URI = baseURIBit + uniqueURIBit;
            ;
        }

        public abstract void DoTheStuff();


        protected List<T> GetAll()
        {
            List<T> things = new List<T>();

            using (HttpClient client = new HttpClient())
            {
                Task<string> resTask = client.GetStringAsync(URI);
                String jsonStr = resTask.Result;

                things = JsonConvert.DeserializeObject<List<T>>(jsonStr);
            }
            return things;
        }

        protected virtual T GetOne(int id)
        {
            T thing;

            using (HttpClient client = new HttpClient())
            {
                Task<string> resTask = client.GetStringAsync($"{URI}/{id}");
                String jsonStr = resTask.Result;

                thing = JsonConvert.DeserializeObject<T>(jsonStr);
            }
            return thing;
        }
        protected virtual bool Delete(int id)
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
        protected bool Post(T thing)
        {
            bool ok;


            using (HttpClient client = new HttpClient())
            {
                string jsonString = JsonConvert.SerializeObject(thing);
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

        protected virtual bool Put(int id, T thing)
        {
            bool ok;


            using (HttpClient client = new HttpClient())
            {
                string jsonString = JsonConvert.SerializeObject(thing);
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
