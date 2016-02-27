using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoslynCodeConverterClientLibrary.Proxies;
using RoslynCodeConverterClientLibrary.Proxies.Models;

namespace MiniConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new RoslynCodeConverter();

            ConvertResponse result = client.Converter.Post(new ConvertRequest()
            {
                Code = "public class Test {}",
                RequestedConversion = "cs2vbnet"
            });

            if (true == result.ConversionOk)
            {
                Console.WriteLine("Conversion succeeded");
                Console.WriteLine(result.ConvertedCode);
            }
            else
            {
                Console.WriteLine("Error converting: " + result.ErrorMessage);
            }

            Console.Read();
        }
    }
}
