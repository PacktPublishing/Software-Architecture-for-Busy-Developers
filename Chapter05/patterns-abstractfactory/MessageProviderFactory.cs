using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace patterns_factorymethod
{
    public class BrokerMessageFactory : MessageFactory{
        public override MessagePublisher GetPublisher(){
            return new BrokerPubisher();
        }
    }
    public class EvGridMessageFactory : MessageFactory{
        public override MessagePublisher GetPublisher(){
            return new EvGridPublisher();
        }
    }
    public abstract class MessageFactory{
        public abstract MessagePublisher GetPublisher();
    }
    public class BrokerPubisher : MessagePublisher
    {
        public override string ProviderName => "BusMessage";
        public override async Task PublishMessage(string message){
            await Task.Run(() => {
                //SendMessageToBus()
            });
        }
    }
    public class EvGridPublisher : MessagePublisher{
        public override string ProviderName => "EvGridMessage";
        public override async Task PublishMessage(string message)
        {
            await Task.Run(() => {
                //SendToGrid()
            });
        }
    }
    public abstract class MessagePublisher{
        public abstract string ProviderName { get; }
        public abstract Task PublishMessage(string message);
    }
}
