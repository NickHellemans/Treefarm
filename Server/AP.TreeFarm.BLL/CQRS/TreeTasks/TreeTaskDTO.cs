using System;
using AP.MyTreeFarm.Application.CQRS.Employees;
using AP.MyTreeFarm.Application.CQRS.Zones;
using AP.MyTreeFarm.Domain;

namespace AP.MyTreeFarm.Application.CQRS.TreeTasks;

public class TreeTaskDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateStart { get; set; }
    public DateTime? DateEnd { get; set; } 
    public int Duration { get; set; }
    
    public int Priority { get; set; }
    
    public DateTime DatePlanned { get; set; }
    
    public DateTime? DatePaused { get; set; }
    
    public double TimePaused { get; set; }
        
    //Foreign keys
    public int EmployeeId { get; set;  }
    public EmployeeWithoutTasksDTO Employee { get; set; }
        
    public int ZoneId { get; set;  }
    public ZoneTreeTaskDTO Zone { get; set; }
        
    public TaskStatus Status { get; set; }
}