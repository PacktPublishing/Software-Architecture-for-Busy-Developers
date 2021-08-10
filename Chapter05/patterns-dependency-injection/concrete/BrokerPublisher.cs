using patterns_dependency_injection.contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace patterns_dependency_injection.concrete
{
    public class BrokerPublisher : IEventPublisher{
        public async Task PublishMessage(string message)
        {
            await Task.Run(() => {
                
                //SendMessageToBus()
            });
        }        
    }    
}
