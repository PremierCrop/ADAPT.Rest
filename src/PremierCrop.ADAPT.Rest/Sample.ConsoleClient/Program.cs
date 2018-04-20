using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sample.ConsoleClient
{
    class Program
    {
        
        static async Task Main(string[] args)
        {
            Console.WriteLine("Press enter when WebApi is ready");
            Console.ReadLine();

            var example = new ReferenceLinkClientExample("http://localhost:59117");
            await example.Run();

            Console.WriteLine();
            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();
        }
    }
}
