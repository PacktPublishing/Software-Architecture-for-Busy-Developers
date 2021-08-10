using Microsoft.AspNetCore.Mvc;

using Moq;
using patterns_dependency_injection.contracts;
using patterns_dependency_injection.Controllers;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Xunit;

namespace patterns_dependency_injection_ut
{
    public class PublishMessage{   

        [Fact]
        public async Task PublishMessageTest()
        {
            var _mockEventPub = new Mock<IEventPublisher>();
            var _demoController = new DemoController(_mockEventPub.Object);
            var actionResult = await _demoController.PublishMessage("test") ;
            Assert.IsType<AcceptedResult>(actionResult);
        }

        [Fact]
        public async Task PublishMessageTestWithBasicDirectInjection(){            
            var _demoController = new DemoController(new TestPublisher());
            var actionResult = await _demoController.PublishMessage("test");
            Assert.IsType<AcceptedResult>(actionResult);
        }
    }
    public class TestPublisher : IEventPublisher{
        public async Task PublishMessage(string message)
        {
            await Task.Run(() => {
                //test use case()
            });
        }
    }
}
