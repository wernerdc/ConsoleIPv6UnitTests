using System.Net;
using System;

namespace ConsoleIPv6UnitTests
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Clear();

            Console.WriteLine("ConsoleIPv6UnitTests \n");

            string address = "2001:Odb8:0f61:a1ff::80";
            Console.WriteLine(address);
            string expanded = IPv6Helper.ExpandAddress(address);
            Console.WriteLine(expanded) ; 
            string shortened = IPv6Helper.ShortenAddress(expanded);
            Console.WriteLine(shortened) ; 
            Console.WriteLine() ;

            address = "2002:O:00b3:0:0004:0abc::2"; 
            Console.WriteLine(address);
            expanded = IPv6Helper.ExpandAddress(address); 
            Console.WriteLine(expanded);
            shortened  = IPv6Helper.ShortenAddress(address); 
            Console.WriteLine(shortened); 
            Console.WriteLine(); 
            
            address = "2002:0:b3:0:d4::2"; 
            Console.WriteLine(address); 
            expanded = IPv6Helper.ExpandAddress(address);
            Console.WriteLine(expanded); 
            shortened = IPv6Helper.ShortenAddress(expanded); 
            Console.WriteLine(shortened); 
            Console.WriteLine();

            Console.WriteLine("Press [ENTER] to exit...");
            Console.ReadLine();
        }
    }
}
