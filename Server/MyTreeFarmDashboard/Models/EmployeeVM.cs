using AP.MyTreeFarm.Application.CQRS.Employees;
using AP.MyTreeFarm.Application.CQRS.TreeTasks;
using X.PagedList;

namespace MyTreeFarmDashboard.Models;

public class EmployeeVM
{
    public EmployeeDTO Employee { get; set; }
    public IPagedList<TreeTaskWithoutEmployeeDTO> PagedTasks { get; set; }
}