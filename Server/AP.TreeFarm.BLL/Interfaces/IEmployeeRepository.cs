using System.Collections.Generic;
using System.Threading.Tasks;
using AP.MyTreeFarm.Domain;

namespace AP.MyTreeFarm.Application.Interfaces;

public interface IEmployeeRepository: IGenericRepository<Employee>
{
    Task<IEnumerable<Employee>> GetAll();
    Task<Employee> GetByEmail(string email);
}
