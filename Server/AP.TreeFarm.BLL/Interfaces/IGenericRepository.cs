using System.Collections.Generic;
using System.Threading.Tasks;

namespace AP.MyTreeFarm.Application.Interfaces
{
    public interface IGenericRepository<T>
    {
        Task<IEnumerable<T>> GetAll(int pageNr, int pageSize);

        Task<T> GetById(int id);
        T Create(T newPerson);

        T Update(T modifiedPerson);
        void Delete(T person);
    }
}
