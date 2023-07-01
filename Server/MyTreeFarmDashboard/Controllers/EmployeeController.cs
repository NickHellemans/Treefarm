using AP.MyTreeFarm.Application.CQRS.Employees;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MyTreeFarmDashboard.Models;
using MyTreeFarmDashboard.Services;
using X.PagedList;
using TaskStatus = AP.MyTreeFarm.Domain.TaskStatus;

namespace MyTreeFarmDashboard.Controllers;

[Authorize(Roles = "Admin")]
public class EmployeeController : Controller
{
    private readonly IRestService _restService;
    private const int PageSize = 10;
    private const int PageSizeDetail = 4;
    public EmployeeController(IRestService restService)
    {
        _restService = restService;
    }
    // GET All
    public async Task<IActionResult> Index(string sortBy, string? searchBox, string currentFilter, int page = 1)
    {
        ViewBag.CurrentSort = sortBy;
        ViewBag.SortByEmployeeId = string.IsNullOrEmpty(sortBy) ? "employee_id_desc" : "";
        ViewBag.SortByEmployeeName = sortBy == "employee" ? "employee_desc" : "employee";
        ViewBag.SortByEmail = sortBy == "email" ? "email_desc" : "email";
        ViewBag.SortByAdmin = sortBy == "admin" ? "admin_desc" : "admin";
        ViewBag.SortByActive = sortBy == "active" ? "active_desc" : "active";
        
        if (searchBox != null)
        {
            page = 1;
        }
        else
        {
            searchBox = currentFilter;
        }

        ViewBag.CurrentFilter = searchBox;
        
        var response = await _restService.GetResource<List<EmployeeDTO>>("employee/");
        if (!response.IsSuccessful || response.Data == null) return RedirectToAction("ErrorPage", "Account");

        var employees = response.Data.AsQueryable();
        
        if (!string.IsNullOrEmpty(searchBox))
        {
            var searchBoxLower = searchBox.ToLower();
            employees = employees.Where(p =>
                p.FirstName.ToLower().Contains(searchBoxLower) || p.LastName.ToLower().Contains(searchBoxLower));
        }
        
        employees = sortBy switch
        {
            "employee_id_desc" => employees.OrderByDescending(p => p.EmployeeId),
            "employee_desc" => employees.OrderByDescending(p => p.LastName),
            "employee" => employees.OrderBy(p => p.LastName),
            "email_desc" => employees.OrderByDescending(p => p.Email),
            "email" => employees.OrderBy(p => p.Email),
            "admin_desc" => employees.OrderByDescending(p => p.IsAdmin),
            "admin" => employees.OrderBy(p => p.IsAdmin),
            "active_desc" => employees.OrderByDescending(p => p.IsActive),
            "active" => employees.OrderBy(p => p.IsActive),
            _ => employees.OrderBy(t => t.EmployeeId)
        };
        var pagedList = await employees.ToPagedListAsync(page, PageSize);
        return View(pagedList);
        
    }
    
    // GET detail
    public async Task<IActionResult> Detail(int id, string sortBy,string currentStatus, int page = 1)
    {
        ViewBag.CurrentSort = sortBy;
        ViewBag.SortByDatePlanned = string.IsNullOrEmpty(sortBy) ? "date_planned_desc" : "";
        ViewBag.SortByName = sortBy == "name" ? "name_desc" : "name";
        ViewBag.SortByDuration = sortBy == "duration" ? "duration_desc" : "duration";
        ViewBag.SortByPriority = sortBy == "priority" ? "priority_desc" : "priority";
        ViewBag.SortByZone = sortBy == "zone" ? "zone_desc" : "zone";
        ViewBag.SortByStatus = sortBy == "status" ? "status_desc" : "status";
        ViewBag.SortByDatePlanned = sortBy == "date_planned" ? "date_planned_desc" : "date_planned";
        
        var response = await _restService.GetResource<EmployeeDTO>("employee/" + id);
        if (!response.IsSuccessful || response.Data == null) return RedirectToAction("ErrorPage", "Account");
        
        var employee = response.Data;
        var tasks = employee.Tasks.AsQueryable();
        tasks = sortBy switch
        {
            "name_desc" => tasks.OrderByDescending(p => p.Name),
            "name" => tasks.OrderBy(p => p.Name),
            "duration_desc" => tasks.OrderByDescending(p => p.Duration),
            "duration" => tasks.OrderBy(p => p.Duration),
            "priority_desc" => tasks.OrderByDescending(p => p.Priority),
            "priority" => tasks.OrderBy(p => p.Priority),
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
        //ViewBag.AllStatus = currentStatus == "";
        ViewBag.AllStatusWithDone = currentStatus == "AllWithDone" ? "" : "AllWithDone";
        
        tasks = currentStatus switch
        {
            "ToDo" => tasks.Where(p => p.Status == TaskStatus.ToDo),
            "In Progress" => tasks.Where(p => p.Status == TaskStatus.InProgress),
            "Paused" => tasks.Where(p => p.Status == TaskStatus.Paused),
            "Done" => tasks.Where(p => p.Status == TaskStatus.Done),
            "AllWithDone" => tasks,
            _ => tasks.Where(p => p.Status != TaskStatus.Done)
            //"All" => tasks.OrderBy(t => t.Duration),
            //_ => tasks
        };
        
        var pagedList = await tasks.ToPagedListAsync(page, PageSizeDetail);

        var employeeVm = new EmployeeVM
        {
            Employee = employee,
            PagedTasks = pagedList
        };
        return View(employeeVm);
        
    }
    
    //Get create
    public IActionResult Create()
    {
        return View();
    }
    
    //Post create
    [HttpPost]
    public async Task<IActionResult> Create(CreateEmployeeDTO employee)
    {
        var response = await _restService.PostResource<Tuple<CreateEmployeeDTO,List<ValidationFailure>>, CreateEmployeeDTO>("Employee/", employee);
        
        
        if (!response.IsSuccessful)
        {
            ViewData["Errors"] = response.Data.Item2;
            return View(response.Data.Item1);
        }
        TempData["AlertSuccess"] = "Werknemer succesvol aangemaakt";
        return RedirectToAction("Index", "Employee");
    }
    
    //Get Update
    public async Task<IActionResult> Update(int id)
    {
        var response = await _restService.GetResource<EmployeeDTO>($"employee/{id}");
        if (response.IsSuccessful)
            return View(response.Data);
        return RedirectToAction("ErrorPage", "Account");
    }
    
    //Post Update
    [HttpPost]
    public async Task<IActionResult> Update(UpdateEmployeeDTO employee)
    {
        var response = await _restService.PostResource<Tuple<EmployeeDTO, List<ValidationFailure>>, UpdateEmployeeDTO>($"Employee/{employee.Id}", employee);
        
        if (!response.IsSuccessful)
        {
            ViewData["Errors"] = response.Data.Item2;
            return View(response.Data.Item1);
        }
        TempData["AlertSuccess"] = "Werknemer succesvol aangepast";
        return RedirectToAction("Index", "Employee");
    }
    
    //Delete
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _restService.DeleteResource<int>($"Employee/{id}");
        if (response.IsSuccessful)
        {
            TempData["AlertSuccess"] = "Werknemer succesvol verwijderd";
        }
        else
        {
            TempData["AlertError"] = "Er liep iets fout met het verwijderen van de werknemer";
        }
        return RedirectToAction("Index", "Employee");
    }

}