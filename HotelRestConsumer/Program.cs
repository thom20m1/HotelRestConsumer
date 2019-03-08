using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelRestConsumer.Consumers;

namespace HotelRestConsumer
{
    class Program
    {
        
        static void Main(string[] args)
        {
            HotelCode hotelCode = new HotelCode();
            GuestCode guestCode = new GuestCode("Guests");
            RoomCode roomCode = new RoomCode("Rooms");
            BookingCode bookingCode = new BookingCode("Bookings");
            FacilityCode facilityCode = new FacilityCode("Facilities");

            hotelCode.DoTheStuff();
            guestCode.DoTheStuff();
            roomCode.DoTheStuff();
            bookingCode.DoTheStuff();
            facilityCode.DoTheStuff();

            KeepConsoleWindowOpen();
        }
        private static void KeepConsoleWindowOpen()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to close application");
            Console.ReadKey();
        }
    }
}
