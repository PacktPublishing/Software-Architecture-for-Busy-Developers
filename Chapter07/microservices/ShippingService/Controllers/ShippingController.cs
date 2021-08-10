using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Dapr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Dapr.Client;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;

using Newtonsoft.Json;
using Dapr.Client.Autogen.Grpc.v1;

namespace TrackingService.Controllers
{
    /// <summary>
    /// this code is for illustration purpose only!
    /// </summary>
    [ApiController]   
    public class ShippingController : ControllerBase{
        #region DI and constructor
        private readonly DaprClient _dapr;
        private readonly ILogger<ShippingController> _logger;
        public ShippingController(ILogger<ShippingController> logger, DaprClient dapr){
            _logger = logger;
            _dapr = dapr;
        }
        #endregion
        
        [Topic("bus", "order")]
        [HttpPost]
        [Route("dapr")]        
        public async Task<IActionResult> ProcessOrderEvent([FromBody] OrderEvent ev){
            _logger.LogInformation($"Received new event");
            _logger.LogInformation("{0} {1} {2}", ev.id, ev.name, ev.type);
            switch (ev.type){
                case OrderEvent.EventType.Created:
                    var order = await GetOrder(ev.id);
                    if (order!=null){
                        _logger.LogInformation($"Starting shipping process for order {ev.id} with " +
                            $"{order.Products.Count} " +$"products!");
                    }
                    else{
                        _logger.LogInformation($"order {ev.id} could not be retrieved, suspending shipping process!");
                    }
                    break;
                #region other cases
                case OrderEvent.EventType.Updated:
                    if (await GetOrder(ev.id) != null)
                    {
                        _logger.LogInformation($"Checking shipping process impact for order {ev.id}!");
                    }
                    else
                    {
                        _logger.LogInformation($"order {ev.id} not found, cancelling shipping process if any!");
                    }
                    break;
                case OrderEvent.EventType.Deleted:
                    _logger.LogInformation($"Cancelling shipping process for order {ev.id}!");
                    break;
                    #endregion
            }
            return Accepted();
        }      
        async Task<Order> GetOrder(Guid id){
            try{
                return await _dapr.InvokeMethodAsync<object, Order>(
                    HttpMethod.Get,
                    "orderquery",
                    id.ToString(),
                    null);       
            }            
            catch (Exception ex){//should be more specific
                _logger.LogError(ex.Message);
                return null;                
            }              
        }
    }
}
