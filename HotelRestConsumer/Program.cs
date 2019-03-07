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
            HotelCode HotelCode = new HotelCode();
            HotelCode.DoTheStuff();



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
