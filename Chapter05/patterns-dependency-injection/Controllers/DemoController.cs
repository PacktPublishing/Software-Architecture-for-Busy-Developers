using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using patterns_dependency_injection.contracts;

namespace patterns_dependency_injection.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DemoController : ControllerBase{
        
        private readonly IEventPublisher _evPublisher;
        
        public DemoController(IEventPublisher evPublisher){            
            _evPublisher = evPublisher;            
        }        
        
        [HttpPost]
        public async Task<IActionResult> PublishMessage(string message){
            await _evPublisher.PublishMessage(message);
            return new AcceptedResult();
        }        
    }

    

        
}
