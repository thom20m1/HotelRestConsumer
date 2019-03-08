using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLib.Model;

namespace HotelRestConsumer.Consumers
{
    public class BookingCode:Consumer<Booking>
    {
        public BookingCode(string uniqueURIBit)
            : base(uniqueURIBit)
        {

        }


        public override void DoTheStuff()
        {

        }
    }
}
