using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoslynCodeConverter.Client;
using RoslynCodeConverterClientLibrary.Proxies;
using RoslynCodeConverterClientLibrary.Proxies.Models;

namespace MiniConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new RoslynCodeConverterClientLibrary.Proxies.RoslynCodeConverter();

            //ConvertResponse result = client.Converter.Post(new ConvertRequest()
            //{
            //    Code = "public class Test {}",
            //    RequestedConversion = SupportedConversions.CSharp2Vb
            //});

            ConvertResponse result = client.Converter.Post(new ConvertRequest()
            {
                Code = "Public Class Test\r\nEnd Class",
                RequestedConversion = SupportedConversions.Vb2CSharp
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
