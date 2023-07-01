using System.Collections.Generic;
using System.Threading.Tasks;
using AP.MyTreeFarm.Domain;

namespace AP.MyTreeFarm.Application.Interfaces
{
    public interface ITreeTasksRepository: IGenericRepository<TreeTask>
    {
        Task<IEnumerable<TreeTask>> GetAll();
    }
}