
using System;
using System.Threading.Tasks;

namespace patterns_factorymethod
{
    class Program
    {
        
       
        static void Main(string[] args){            
            Console.Write("Bus (1) or Event Grid (2): ");
            var choice = Console.ReadLine().Trim();
            switch(choice){
                case "1":
                    Console.WriteLine(new BrokerMessageFactory().GetPublisher().ProviderName);
                    break;
                case "2":
                    Console.WriteLine(new EvGridMessageFactory().GetPublisher().ProviderName);
                    break;
                default:
                    Console.WriteLine("Oops, invalid choice");
                    break;                
            }            
        }
    }
}
