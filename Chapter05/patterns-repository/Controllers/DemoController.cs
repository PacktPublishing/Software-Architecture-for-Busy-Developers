using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace patterns_repository.Controllers
{
    [Route("[controller]")]
    [ApiController]  
    public class DemoController : ControllerBase
    {
        private readonly IDemoRepository _repo;
        public DemoController(IDemoRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DemoEntity>>> GetAllDemoEntities()
        {
            return Ok(await _repo.ListAllAsync());
        }

        [HttpGet]
        [Route("{odd}")]
        public async Task<ActionResult<IEnumerable<DemoEntity>>> GetOddEntities()
        {
            return Ok(await _repo.ListOnlyOddEntities());
        }
    }
}
