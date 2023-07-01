using System.Collections.Generic;
using AP.MyTreeFarm.Application.CQRS.TreeTasks;

namespace AP.MyTreeFarm.Application.CQRS.Employees;

public class EmployeeDTO
{
    public int Id { get; set; }
    public string EmployeeId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    //public string Password { get; set; }
    public bool IsAdmin { get; set; }
    public bool IsActive { get; set; }
        
    public List<TreeTaskWithoutEmployeeDTO> Tasks { get; set; }


}
