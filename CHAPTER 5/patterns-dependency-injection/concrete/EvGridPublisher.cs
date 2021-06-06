using patterns_dependency_injection.contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace patterns_dependency_injection.concrete
{
    public class EvGridPublisher: IEventPublisher{
        public async Task PublishMessage(string message){
            await Task.Run(() => {
                //SendMessageToGrid()
            });
        }
    }
}
