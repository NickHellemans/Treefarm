namespace MyTreeFarmDashboard.Models;

public class AnalyseVM
{
    public string EmployeeName { get; set; }
    public int EmployeeId { get; set; }
    public double AverageTimePaused { get; set; }
    public double AverageDuration { get; set; }
    public int TotalTasks { get; set; }
    public int AboveDurationCounter { get; set; }
}