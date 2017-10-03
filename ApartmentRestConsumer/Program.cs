using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ApartmentRestConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var i in GetCustomersAsync().Result)
            {
                Console.WriteLine(i.ToString());
            }

            
            Console.ReadLine();


        }



        private static async Task<IList<Apartment>> GetCustomersAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync("http://localhost:50260/Service1.svc/apartment/");
                IList<Apartment> cList = JsonConvert.DeserializeObject<IList<Apartment>>(content);
                return cList;
            }
        }

    }
}
