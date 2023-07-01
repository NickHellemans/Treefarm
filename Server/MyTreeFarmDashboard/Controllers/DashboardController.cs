using AP.MyTreeFarm.Application.CQRS.Employees;
using AP.MyTreeFarm.Application.CQRS.TreeTasks;
using AP.MyTreeFarm.Application.CQRS.Zones;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyTreeFarmDashboard.Models;
using MyTreeFarmDashboard.Services;
using X.PagedList;
using TaskStatus = AP.MyTreeFarm.Domain.TaskStatus;

namespace MyTreeFarmDashboard.Controllers;

[Authorize(Roles = "Admin")]
public class DashboardController : Controller
{
    private readonly IRestService _restService;
    private const int PageSize = 10;
    public DashboardController(IRestService restService)
    {
        _restService = restService;
    }
    // GET All
    public async Task<IActionResult> Index(string sortBy, string? searchBox, string currentFilter,string currentStatus, int page = 1)
    {
        ViewBag.CurrentSort = sortBy;
        ViewBag.SortByDatePlanned = string.IsNullOrEmpty(sortBy) ? "date_planned_desc" : "";
        ViewBag.SortByName = sortBy == "name" ? "name_desc" : "name";
        ViewBag.SortByDuration = sortBy == "duration" ? "duration_desc" : "duration";
        ViewBag.SortByPriority = sortBy == "priority" ? "priority_desc" : "priority";
        ViewBag.SortByEmployee = sortBy == "employee" ? "employee_desc" : "employee";
        ViewBag.SortByZone = sortBy == "zone" ? "zone_desc" : "zone";
        ViewBag.SortByStatus = sortBy == "status" ? "status_desc" : "status";
        //ViewBag.SortByDatePlanned = sortBy == "date_planned" ? "date_planned_desc" : "date_planned";
         
        if (searchBox != null)
        {
            page = 1;
        }
        else
        {
            searchBox = currentFilter;
        }

        ViewBag.CurrentFilter = searchBox;
        
        var response = await _restService.GetResource<List<TreeTaskDTO>>("TreeTask/");
        if (!response.IsSuccessful || response.Data == null) return RedirectToAction("ErrorPage", "Account");
        var tasks = response.Data.AsQueryable();
        if (!string.IsNullOrEmpty(searchBox))
        {
            var searchBoxLower = searchBox.ToLower();
            tasks = tasks.Where(p =>
                p.Name.ToLower().Contains(searchBoxLower) || p.Employee.FirstName.ToLower().Contains(searchBoxLower) ||
                p.Employee.LastName.ToLower().Contains(searchBoxLower) || p.Zone.Name.ToLower().Contains(searchBoxLower));
        }
        
        tasks = sortBy switch
        {
            "name_desc" => tasks.OrderByDescending(p => p.Name),
            "name" => tasks.OrderBy(p => p.Name),
            "duration_desc" => tasks.OrderByDescending(p => p.Duration),
            "duration" => tasks.OrderBy(p => p.Duration),
            "priority_desc" => tasks.OrderByDescending(p => p.Priority),
            "priority" => tasks.OrderBy(p => p.Priority),
            "employee_desc" => tasks.OrderByDescending(p => p.Employee.LastName),
            "employee" => tasks.OrderBy(p => p.Employee.LastName),
            "zone_desc" => tasks.OrderByDescending(p => p.Zone.Name),
            "zone" => tasks.OrderBy(p => p.Zone.Name),
            "status_desc" => tasks.OrderByDescending(p => p.Status),
            "status" => tasks.OrderBy(p => p.Status),
            "date_planned_desc" => tasks.OrderByDescending(p => p.DatePlanned),
            //"date_planned" => tasks.OrderBy(p => p.DatePlanned),
            _ => tasks.OrderBy(t => t.DatePlanned)
        };
        ViewBag.CurrentStatus = currentStatus;
        ViewBag.ToDoStatus = currentStatus == "ToDo" ? "" : "ToDo";
        ViewBag.PausedStatus = currentStatus == "Paused" ? "" : "Paused";
        ViewBag.InProgressStatus = currentStatus == "In Progress" ? "" : "In Progress";
        ViewBag.DoneStatus = currentStatus == "Done" ? "" : "Done";
        ViewBag.AllStatusWithDone = currentStatus == "AllWithDone" ? "" : "AllWithDone";
        
        tasks = currentStatus switch
        {
            "ToDo" => tasks.Where(p => p.Status == TaskStatus.ToDo),
            "In Progress" => tasks.Where(p => p.Status == TaskStatus.InProgress),
            "Paused" => tasks.Where(p => p.Status == TaskStatus.Paused),
            "Done" => tasks.Where(p => p.Status == TaskStatus.Done),
            "AllWithDone" => tasks,
            _ => tasks.Where(p => p.Status != TaskStatus.Done)
        };

        var pagedList = await tasks.ToPagedListAsync(page, PageSize);

        return View(pagedList);
    }
    
    // GET Detail
    public async Task<IActionResult> Detail(int id)
    {
        var response = await _restService.GetResource<TreeTaskDTO>("TreeTask/" + id);
        if (response.IsSuccessful)
            return View(response.Data);
        return RedirectToAction("ErrorPage", "Account");
    }
    
    // GET Create
    public async Task<IActionResult> Create(int? employeeId)
    {
        //Get employees to add to create task viewmodel
        var responseEmployees = await _restService.GetResource<List<EmployeeDTO>>("employee/");
        if (!responseEmployees.IsSuccessful)
        {
            RedirectToAction("ErrorPage", "Account");
        }
        var vm = new CreateTaskVM();
        vm.employees = new List<SelectListItem>();
        if (employeeId != null)
        {
            foreach (var employee in responseEmployees.Data.Where(employee => employee.Id == employeeId))
            {
                vm.employees.Add(new SelectListItem
                    { Text = employee.FirstName + " " + employee.LastName, Value = employee.Id.ToString() });
            }
        }
        else
        {
            foreach (var employee in responseEmployees.Data)
            {
                vm.employees.Add(new SelectListItem
                    { Text = employee.FirstName + " " + employee.LastName, Value = employee.Id.ToString() });
            }
        }

        //Get zones to add to create task viewmodel
        var responseZones = await _restService.GetResource<List<ZoneDTO>>("zone/");
        if (!responseZones.IsSuccessful)
        {
            RedirectToAction("ErrorPage", "Account");
        }
        vm.zones = new List<SelectListItem>();

        foreach (var zone in responseZones.Data)
        {
            vm.zones.Add(new SelectListItem { Text = zone.Name, Value = zone.Id.ToString() });
        }
        return View(vm);
    }
    
    //Post create
    [HttpPost]
    public async Task<IActionResult> Create(CreateTaskVM taskVM)
    {
        var responseEmployees = await _restService.GetResource<List<EmployeeDTO>>("employee/");
        var vm = new CreateTaskVM();
        vm.employees = new List<SelectListItem>();
        
        foreach (var employee in responseEmployees.Data)
        {
            vm.employees.Add(new SelectListItem { Text = employee.FirstName + " " + employee.LastName, Value = employee.Id.ToString() });
        }
        
        var responseZones = await _restService.GetResource<List<ZoneDTO>>("zone/");
        vm.zones = new List<SelectListItem>();

        foreach (var zone in responseZones.Data)
        {
            vm.zones.Add(new SelectListItem { Text = zone.Name, Value = zone.Id.ToString() });
        }
        
        var response = await _restService.PostResource<Tuple<CreateTreeTaskDTO,List<ValidationFailure>>, CreateTreeTaskDTO>("TreeTask/", taskVM.treeTask);
        
       
        if (!response.IsSuccessful)
        {
            ViewData["Errors"] = response.Data.Item2;
            vm.treeTask = response.Data.Item1;
            return View(vm);
        }
        TempData["AlertSuccess"] = "Taak succesvol aangemaakt";
        return RedirectToAction("Index", "Dashboard");
    }
    
    // GET Update
    public async Task<IActionResult> Update(int id)
    {
        var responseTask = await _restService.GetResource<UpdateTreeTaskDTO>($"TreeTask/{id}");
        if (!responseTask.IsSuccessful)
        {
            RedirectToAction("ErrorPage", "Account");
        }
        
        var responseEmployees = await _restService.GetResource<List<EmployeeDTO>>("employee/");
        if (!responseEmployees.IsSuccessful)
        {
            RedirectToAction("ErrorPage", "Account");
        }
        var vm = new UpdateTaskVM
        {
            Employees = new List<SelectListItem>(),
            Zones = new List<SelectListItem>(),
            TreeTask = responseTask.Data,
        };

        foreach (var employee in responseEmployees.Data)
        {
            vm.Employees.Add(new SelectListItem { Text = employee.FirstName + " " + employee.LastName, Value = employee.Id.ToString() });
        }
        
        var responseZones = await _restService.GetResource<List<ZoneDTO>>("zone/");
        if (!responseZones.IsSuccessful)
        {
            RedirectToAction("ErrorPage", "Account");
        }

        foreach (var zone in responseZones.Data)
        {
            vm.Zones.Add(new SelectListItem { Text = zone.Name, Value = zone.Id.ToString() });
        }
        return View(vm);
    }
    
    //Post update
    [HttpPost]
    public async Task<IActionResult> Update(int id,UpdateTaskVM taskVM)
    {
        
        var response = await _restService.PostResource<Tuple<UpdateTreeTaskDTO, List<ValidationFailure>>, UpdateTreeTaskDTO>($"TreeTask/{id}", taskVM.TreeTask);
        if (!response.IsSuccessful) 
        {
            ViewData["Errors"] = response.Data.Item2;
            taskVM.TreeTask = response.Data.Item1;
            return View(taskVM);
        }
        TempData["AlertSuccess"] = "Taak succesvol aangepast";
        return RedirectToAction("Index", "Dashboard");
    }
    
    // GET Delete
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _restService.DeleteResource<int>($"TreeTask/{id}");
        if (response.IsSuccessful)
        {
            TempData["AlertSuccess"] = "Taak succesvol verwijderd";
        }
        else
        {
            TempData["AlertError"] = "Er liep iets fout met het verwijderen van de taak";
        }
        return RedirectToAction("Index", "Dashboard");
    }
}