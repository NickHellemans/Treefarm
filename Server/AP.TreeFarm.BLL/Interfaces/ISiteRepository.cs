using System.Collections.Generic;
using System.Threading.Tasks;
using AP.MyTreeFarm.Domain;

namespace AP.MyTreeFarm.Application.Interfaces;

public interface ISiteRepository: IGenericRepository<Site>
{
    Task<IEnumerable<Site>> GetAll();
}