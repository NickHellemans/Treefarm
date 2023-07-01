using System.Collections.Generic;
using System.Threading.Tasks;
using AP.MyTreeFarm.Application.Interfaces;
using AP.MyTreeFarm.Domain;
using AP.MyTreeFarm.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AP.MyTreeFarm.Infrastructure.Repositories
{
    public class TreeRepository : ITreeRepository
    {
        private readonly MyTreeFarmContext context;

        public TreeRepository(MyTreeFarmContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Tree>> GetAll()
        {
            return await context.Trees.Include(t => t.Zones).ToListAsync();
            //return Task.FromResult<IEnumerable<Tree>>(context.Trees.Take(4).ToList());
        }

        public Task<IEnumerable<Tree>> GetAll(int pageNr, int pageSize)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Tree> GetById(int id)
        {
            return await context.Trees.Include(s => s.Zones).FirstOrDefaultAsync(p => p.Id == id);
        }
        
        public Tree Create(Tree newTree)
        { 
            context.Trees.Add(newTree);
            return newTree;
        }

        public Tree Update(Tree modifiedTree)
        {
            context.Trees.Update(modifiedTree);
            return modifiedTree;
        }

        public void Delete(Tree tree)
        {
            context.Trees.Remove(tree);
        }
    }
}