using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace patterns_repository
{
    public interface IDemoRepository : IAsyncRepository<DemoEntity>{
        Task<IEnumerable<DemoEntity>> ListOnlyOddEntities();
    }
}
