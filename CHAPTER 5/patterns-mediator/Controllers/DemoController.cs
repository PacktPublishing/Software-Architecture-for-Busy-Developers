using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace patterns_mediator_api.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class DemoController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DemoController(IMediator mediator){            
            _mediator = mediator;
        }

        [HttpGet(Name ="list-all-demos")]
        public async Task<ActionResult<IEnumerable<DemoEntity>>> GetAllDemoEntities()
        {
            return Ok(await _mediator.Send(new DemoEntityListQuery()));
        }

        [HttpPost(Name = "add-a-demo")]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateDemoCommand createDemoCommand)
        {
            return Ok(await _mediator.Send(createDemoCommand));
        }
    }
    #region mediator related classes
    public class CreateDemoCommand : IRequest<Guid>{
        public string property1 { get; set; }
        public string property2 { get; set; }
    }
    public class CreateDemoCommandHandler : IRequestHandler<CreateDemoCommand, Guid>{
        private readonly IDemoRepository _repo;
        //usually you use a mapper too but making short version to focus on CQS
        public CreateDemoCommandHandler(IDemoRepository repo){
            _repo = repo;            
        }

        public async Task<Guid> Handle(CreateDemoCommand request, CancellationToken cancellationToken){
            
            var entity=await _repo.AddAsync(new DemoEntity
            {
                id=Guid.NewGuid(),
                property1=request.property1,
                property2=request.property2
            });


            return entity.id;
        }
    }
    public class DemoEntityListQuery : IRequest<IEnumerable<DemoEntity>>{}
    
    public class DemoEntityListQueryHandler : IRequestHandler<DemoEntityListQuery, IEnumerable<DemoEntity>>{
        private readonly IDemoRepository _repo;
        public DemoEntityListQueryHandler(IDemoRepository repo){
            _repo = repo;
        }
        public async Task<IEnumerable<DemoEntity>> Handle(DemoEntityListQuery request, CancellationToken cancellationToken){
            //return Task.FromResult(_db.DemoEntities.AsEnumerable());
            return await _repo.ListAllAsync();
        }
    }
    #endregion
}
