using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace patterns_dependency_injection.contracts
{
    public interface IEventPublisher{
        Task PublishMessage(string message);
    }
   
}
