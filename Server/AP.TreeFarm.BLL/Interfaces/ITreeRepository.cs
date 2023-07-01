using System.Collections.Generic;
using System.Threading.Tasks;
using AP.MyTreeFarm.Domain;

namespace AP.MyTreeFarm.Application.Interfaces;

public interface ITreeRepository: IGenericRepository<Tree>
{
    Task<IEnumerable<Tree>> GetAll();
}