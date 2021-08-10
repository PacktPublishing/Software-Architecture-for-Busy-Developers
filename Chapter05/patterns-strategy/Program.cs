using System;
using System.Threading.Tasks;

namespace patterns_strategy
{
    class Program
    {
        static async Task Main(string[] args){
            IEventPublisher brokerPublisher = new BrokerPublisher();
            await brokerPublisher.PublishMessage(
                new FormatMessageStrategy(new BrokerMessageFormatter()).Format("a message"));
            IEventPublisher gridPublisher = new EvGridPublisher();
            await gridPublisher.PublishMessage(
                new FormatMessageStrategy(new EvGridMessageFormatter()).Format("a message"));
            Console.Read();
        }
    }
    public class FormatMessageStrategy{
        private IMessageFormatter _strategy;
        public FormatMessageStrategy(IMessageFormatter strategy){
            _strategy = strategy;
        }
        public string Format(string message){
            return _strategy.FormatMessage(message);
        }
    }
    public interface IMessageFormatter{
        string FormatMessage(string message);
    }
    public class EvGridMessageFormatter : IMessageFormatter{
        public string FormatMessage(string message){
            Console.WriteLine("in EvGridMessageFormatter");
            //let's pretend we formatted the message for evengrid
            return message;
        }
    }
    public class BrokerMessageFormatter : IMessageFormatter{
        public string FormatMessage(string message){
            Console.WriteLine("in BrokerMessageFormatter");
            //let's pretend we formatted the message for a bus
            return message;
        }
    }
    public interface IEventPublisher{
        Task PublishMessage(string message);
    }

    public class BrokerPublisher : IEventPublisher{
        public async Task PublishMessage(string message){
            await Task.Run(() => {
                //SendMessageToBus()
            });
        }
    }
    public class EvGridPublisher : IEventPublisher{
        public async Task PublishMessage(string message){
            await Task.Run(() => {
                //SendMessageToGrid()
            });
        }
    }

}
