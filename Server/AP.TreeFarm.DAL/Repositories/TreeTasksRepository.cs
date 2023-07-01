using System.Collections.Generic;
using System.Threading.Tasks;
using AP.MyTreeFarm.Application.Interfaces;
using AP.MyTreeFarm.Domain;
using AP.MyTreeFarm.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AP.MyTreeFarm.Infrastructure.Repositories
{
    public class TreeTasksRepository : ITreeTasksRepository
    {
        private readonly MyTreeFarmContext context;

        public TreeTasksRepository(MyTreeFarmContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<TreeTask>> GetAll()
        {
            return await context.TreeTasks
                .Include(t => t.Employee)
                .Include(t => t.Zone)
                .ToListAsync();
        }
        
        public async Task<TreeTask> GetById(int id)
        {
            return await context.TreeTasks
                .Include(t => t.Employee)
                .Include(t => t.Zone)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        
        public Task<IEnumerable<TreeTask>> GetAll(int pageNr, int pageSize)
        {
            throw new System.NotImplementedException();
        }
        public TreeTask Create(TreeTask newTask)
        {
            context.TreeTasks.Add(newTask);
            return newTask;
        }

        public TreeTask Update(TreeTask modifiedTreeTask)
        {
            context.TreeTasks.Update(modifiedTreeTask);
            return modifiedTreeTask;
        }

        public void Delete(TreeTask treeTask)
        {
            context.TreeTasks.Remove(treeTask);
        }
    }
}