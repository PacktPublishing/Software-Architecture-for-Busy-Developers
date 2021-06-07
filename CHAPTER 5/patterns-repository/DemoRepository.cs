using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace patterns_repository
{
    public class DemoRepository : BaseRepository<DemoEntity>, IDemoRepository
    {
        public DemoRepository(DemoContext dbContext) : base(dbContext) { }        
        public async Task<IEnumerable<DemoEntity>> ListOnlyOddEntities(){
            return await Task<IEnumerable<DemoEntity>>.Run(() => {
                return _dbContext.DemoEntities.ToList().Where((c, i) => i % 2 != 0) 
                    as IEnumerable<DemoEntity>;                
            });           
        }
    }
}
