using AP.MyTreeFarm.Application.CQRS.Employees;
using AP.MyTreeFarm.Application.CQRS.TreeTasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTreeFarmDashboard.Models;
using MyTreeFarmDashboard.Services;
using X.PagedList;

namespace MyTreeFarmDashboard.Controllers;

[Authorize(Roles = "Admin")]
public class AnalyseController : Controller
{
    private readonly IRestService _restService;
    private const int PageSize = 10;
    public AnalyseController(IRestService restService)
    {
        _restService = restService;
    }
    // GET All
    public async Task<IActionResult> Index(string sortBy, string? searchBox, string currentFilter, int page = 1)
    {
        ViewBag.CurrentSort = sortBy;
        ViewBag.SortByName = string.IsNullOrEmpty(sortBy) ? "name_desc" : "";
        ViewBag.SortByAveragePause = sortBy == "average_pause" ? "average_pause_desc" : "average_pause";
        ViewBag.SortByAverageDuration = sortBy == "average_duration" ? "average_duration_desc" : "average_duration";
        ViewBag.SortByTotalTasks = sortBy == "total_tasks" ? "total_tasks_desc" : "total_tasks";
        ViewBag.SortByTotalTasksAboveDuration = sortBy == "total_tasks_above" ? "total_tasks_above_desc" : "total_tasks_above";
        
        if (searchBox != null)
        {
            page = 1;
        }
        else
        {
            searchBox = currentFilter;
        }

        ViewBag.CurrentFilter = searchBox;
        
        var response = await _restService.GetResource<List<EmployeeDTO>>("Employee/");
        if (!response.IsSuccessful || response.Data == null) return RedirectToAction("ErrorPage", "Account");

        var employees = response.Data.AsQueryable();
        var analyseData = new List<AnalyseVM>();

        foreach (var employee in employees)
        {
            var employeeAnalyse = new AnalyseVM
            {
                EmployeeName = employee.LastName + " " + employee.FirstName,
                EmployeeId = employee.Id,
                TotalTasks = employee.Tasks.Count
            };

            double totalTimePaused = 0;
            double totalDuration = 0;
            foreach (var task in employee.Tasks)
            {
                totalTimePaused += task.TimePaused;
                var actualDuration = task.DateEnd - task.DateStart;
                if (!actualDuration.HasValue) continue;
                totalDuration += actualDuration.Value.TotalMinutes;
                if (actualDuration.Value.TotalMinutes > task.Duration)
                    employeeAnalyse.AboveDurationCounter += 1;
            }

            if (employee.Tasks.Count > 0)
            {
                employeeAnalyse.AverageTimePaused = Math.Round(totalTimePaused / employee.Tasks.Count, 1);
                employeeAnalyse.AverageDuration = Math.Round(totalDuration / employee.Tasks.Count, 1);
            }
            analyseData.Add(employeeAnalyse);
        }

        var analyseDataQueryable = analyseData.AsQueryable();
        if (!string.IsNullOrEmpty(searchBox))
        {
            var searchBoxLower = searchBox.ToLower();
            analyseDataQueryable = analyseDataQueryable.Where(p => p.EmployeeName.ToLower().Contains(searchBoxLower));
        }
        
        analyseDataQueryable = sortBy switch
        {
            "name_desc" => analyseDataQueryable.OrderByDescending(p => p.EmployeeName),
            "average_pause_desc" => analyseDataQueryable.OrderByDescending(p => p.AverageTimePaused),
            "average_pause" => analyseDataQueryable.OrderBy(p => p.AverageTimePaused),
            "average_duration_desc" => analyseDataQueryable.OrderByDescending(p => p.AverageDuration),
            "average_duration" => analyseDataQueryable.OrderBy(p => p.AverageDuration),
            "total_tasks_desc" => analyseDataQueryable.OrderByDescending(p => p.TotalTasks),
            "total_tasks" => analyseDataQueryable.OrderBy(p => p.TotalTasks),
            "total_tasks_above_desc" => analyseDataQueryable.OrderByDescending(p => p.AboveDurationCounter),
            "total_tasks_above" => analyseDataQueryable.OrderBy(p => p.AboveDurationCounter),
            _ => analyseDataQueryable.OrderBy(t => t.EmployeeName)
        };

        var pagedList = await analyseDataQueryable.ToPagedListAsync(page, PageSize);
        

        return View(pagedList);
    }
    
    
}