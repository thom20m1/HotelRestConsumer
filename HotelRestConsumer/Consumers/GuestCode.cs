using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLib.Model;

namespace HotelRestConsumer.Consumers
{
    public class GuestCode: Consumer<Guest>
    {
        public GuestCode(string uniqueURIBit)
            : base(uniqueURIBit)
        {
        }


        public override void DoTheStuff()
        {
            Console.WriteLine($"Opretter ny gæst. Success? : {Post(new Guest(2,"Harry","26257455","kontakt@mail.com"))}");
            Console.WriteLine($"Opretter ny gæst. Success? : {Post(new Guest(3, "Henry", "09489569", "kontakt@mail.com"))}");
            Console.WriteLine($"Opretter ny gæst. Success? : {Post(new Guest(4, "Haley", "31534574", "kontakt@mail.com"))}");
            Console.WriteLine($"Opretter ny gæst. Success? : {Post(new Guest(5, "Hillary", "54622323", "kontakt@mail.com"))}");
        
        }
    }
}
