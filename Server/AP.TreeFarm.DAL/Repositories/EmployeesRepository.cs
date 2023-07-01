using System.Collections.Generic;
using System.Threading.Tasks;
using AP.MyTreeFarm.Application.Interfaces;
using AP.MyTreeFarm.Domain;
using AP.MyTreeFarm.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AP.MyTreeFarm.Infrastructure.Repositories;

public class EmployeesRepository : IEmployeeRepository
{
    private readonly MyTreeFarmContext context;

    public EmployeesRepository(MyTreeFarmContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<Employee>> GetAll()
    {
        return await context.Employees.Include(e => e.Tasks)
            .ThenInclude(t => t.Zone)
            .ThenInclude(z => z.Tree)
            .ToListAsync();
    }

    public Task<IEnumerable<Employee>> GetAll(int pageNr, int pageSize)
    {
        throw new System.NotImplementedException();
    }

    public async Task<Employee> GetById(int id)
    {
        return await context.Employees
            .Include(e=>e.Tasks)
            .ThenInclude(t => t.Zone)
            .ThenInclude(z => z.Tree)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
    
    public async Task<Employee> GetByEmail(string email)
    {
        return await context.Employees
            .Include(e=>e.Tasks)
            .ThenInclude(t => t.Zone)
            .ThenInclude(z => z.Tree)
            .FirstOrDefaultAsync(p => p.Email == email);
    }
    
    public Employee Create(Employee newEmployee)
    {
        context.Employees.Add(newEmployee);
        return newEmployee;
    }

    public Employee Update(Employee modifiedEmployee)
    {
        context.Employees.Update(modifiedEmployee);
        return modifiedEmployee;
    }

    public void Delete(Employee employee)
    {
        context.Employees.Remove(employee);
    }

   
}
    
