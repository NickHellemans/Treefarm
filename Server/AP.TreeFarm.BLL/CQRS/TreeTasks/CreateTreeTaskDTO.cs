using System;
using System.Collections.Generic;
using System.Linq;
using AP.MyTreeFarm.Application.Interfaces;
using FluentValidation;
using AP.MyTreeFarm.Application.Errors;

namespace AP.MyTreeFarm.Application.CQRS.TreeTasks
{
    public class CreateTreeTaskDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }

        public int EmployeeId { get; set; }

        public int ZoneId { get; set; }
        public int Priority { get; set; }

        public DateTime DatePlanned { get; set; }
    }

    public class CreateTreeTaskDTOValidator : AbstractValidator<CreateTreeTaskDTO>
    {
        public bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
        
        public CreateTreeTaskDTOValidator()
        {
            //RuleFor(x => x.Name).NotEmpty().WithMessage(TreeTaskErrors.Name);
            RuleFor(x => x.Name).Must(name => !string.IsNullOrEmpty(name) && name.Length <= 255).WithMessage(TreeTaskErrors.Name);
            RuleFor(x => x.Description).Must(desc => !string.IsNullOrEmpty(desc) && desc.Length <= 4000).WithMessage(TreeTaskErrors.Description);
            RuleFor(x => x.Priority).Must(prio => prio >= 0).WithMessage(TreeTaskErrors.Priority);
            RuleFor(x => x.Duration).Must(duration => duration > 0).WithMessage(TreeTaskErrors.Duration);
            RuleFor(x => x.DatePlanned).Must(BeAValidDate).WithMessage(TreeTaskErrors.DatePlanned);
        }
    }

    public class CreateTreeTaskDTOAdvancedValidator : CreateTreeTaskDTOValidator
        {
            private readonly IUnitofWork _uow;

            public CreateTreeTaskDTOAdvancedValidator(IUnitofWork uow)
            {
                _uow = uow;
                //Business rule: Max 2 sites works - Tested
                RuleFor(x => x).MustAsync(async (dto, i) =>
                    {
                        var employee = await _uow.EmployeesRepository.GetById(dto.EmployeeId);
                        var zone = await _uow.ZonesRepository.GetById(dto.ZoneId);
                        var siteIdList = new List<int>
                        {
                            zone.SiteId
                        };

                        siteIdList.AddRange(employee.Tasks.Select(t => t.Zone.SiteId));

                        return siteIdList.Distinct().Count() <= 2;
                    }
                ).WithMessage(TreeTaskErrors.MaxTwoSites);

                //Business rule 4: Max 4 tasks per day - tested
                RuleFor(x => x).MustAsync(async (dto, i) =>
                    {
                        //Dict<DatePlanned, task count>
                        var tasksPerDay = new Dictionary<DateTime, int> { { dto.DatePlanned, 1 } };
                        var employee = await _uow.EmployeesRepository.GetById(dto.EmployeeId);
                        foreach (var t in employee.Tasks)
                        {
                            if (!tasksPerDay.ContainsKey(t.DatePlanned))
                            {
                                tasksPerDay.Add(t.DatePlanned, 1);
                            }
                            else
                            {
                                tasksPerDay.TryGetValue(t.DatePlanned, out var count);
                                tasksPerDay[t.DatePlanned] = count + 1;
                                if (tasksPerDay[t.DatePlanned] > 4)
                                {
                                    return false;
                                }
                            }
                        }

                        return true;
                    }
                ).WithMessage(TreeTaskErrors.MaxFourTasksPerDay);
                
                //Business rule 4: Max 4 tasks per day - tested - refactor
                /*
                RuleFor(x => x).MustAsync(async (dto, i) =>
                    {
                        //Dict<DatePlanned, task count>
                        var counter = 1;
                        var employee = await _uow.EmployeesRepository.GetById(dto.EmployeeId);
                        foreach (var t in employee.Tasks)
                        {
                            if (dto.DatePlanned == t.DatePlanned) counter++;
                            if (counter > 4) return false;
                        }

                        return true;
                    }
                ).WithMessage(TreeTaskErrors.MaxFourTasksPerDay);*/

                //Business rule 5: Max 1 employee per day on a zone - tested
                RuleFor(x => x).MustAsync(async (dto, i) =>
                    {

                        //Dict<DatePlanned, EmployeeId>
                        var zonePerDayEmployees = new Dictionary<DateTime, int> { { dto.DatePlanned, dto.EmployeeId } };
                        var zone = await _uow.ZonesRepository.GetById(dto.ZoneId);
                        foreach (var t in zone.Tasks)
                        {
                            if (!zonePerDayEmployees.ContainsKey(t.DatePlanned))
                            {
                                zonePerDayEmployees.Add(t.DatePlanned, t.Employee.Id);
                            }
                            else
                            {
                                zonePerDayEmployees.TryGetValue(t.DatePlanned, out var employeeId);
                                if (employeeId != t.EmployeeId)
                                    return false;
                            }
                        }

                        return true;
                    }
                ).WithMessage(TreeTaskErrors.MaxOneEmployeePerZonePerDay);

                //Business rule 3: 1 week, max 5 days of working - tested
                RuleFor(x => x).MustAsync(async (dto, i) =>
                    {
                        //Get current week based on task u want to add
                        var baseDate = dto.DatePlanned;
                        var today = baseDate;
                                
                        var thisWeekStart = baseDate.AddDays(-(int)baseDate.DayOfWeek);
                        var thisWeekEnd = thisWeekStart.AddDays(7).AddSeconds(-1);
                        if (baseDate.DayOfWeek == DayOfWeek.Sunday)
                        {
                            thisWeekStart = thisWeekStart.AddDays(-6);
                            thisWeekEnd = thisWeekEnd.AddDays(-6);
                        }
                        

                        var employee = await _uow.EmployeesRepository.GetById(dto.EmployeeId);
                        var workingDaysForSpecifiedWeek = new List<DateTime>
                        {
                            dto.DatePlanned
                        };

                        foreach (var t in employee.Tasks)
                        {
                            var inCurrentWeek = t.DatePlanned >= thisWeekStart && t.DatePlanned <= thisWeekEnd;
                            if (!workingDaysForSpecifiedWeek.Contains(t.DatePlanned) && inCurrentWeek)
                            {
                                workingDaysForSpecifiedWeek.Add(t.DatePlanned);
                            }
                        }

                        return workingDaysForSpecifiedWeek.Count <= 5;
                    }
                ).WithMessage(TreeTaskErrors.MaxFiveDays);

                //Hard Business rule 3: Max 8hours in total amount of time for tasks - tested
                RuleFor(x => x).MustAsync(async (dto, i) =>
                    {
                        var tasksPerDayEmployees = new Dictionary<DateTime, int> { { dto.DatePlanned, dto.Duration } };
                        
                        var employee = await _uow.EmployeesRepository.GetById(dto.EmployeeId);
                        foreach (var t in employee.Tasks)
                        {
                            if (!tasksPerDayEmployees.ContainsKey(t.DatePlanned))
                            {
                                tasksPerDayEmployees.Add(t.DatePlanned, t.Duration);
                            }
                            else
                            {
                                tasksPerDayEmployees.TryGetValue(t.DatePlanned, out var duration);
                                tasksPerDayEmployees[t.DatePlanned] = duration + t.Duration;
                                if (tasksPerDayEmployees[t.DatePlanned] > 480)
                                    return false;
                            }
                        }
                        return true;
                    }
                ).WithMessage(TreeTaskErrors.MaxEightHoursPerDay);
                
                //Hard Business rule 3: Max 8hours in total amount of time for tasks - tested - refactor
                /*
                RuleFor(x => x).MustAsync(async (dto, i) =>
                    {
                        var totalDuration = dto.Duration;
                        var employee = await _uow.EmployeesRepository.GetById(dto.EmployeeId);
                        foreach (var t in employee.Tasks)
                        {
                            if (t.DatePlanned == dto.DatePlanned) totalDuration += t.Duration;
                            if (totalDuration > 480) return false;
                        }
                        return true;
                    }
                ).WithMessage(TreeTaskErrors.MaxEightHoursPerDay);
                */
            }

        }
    

}

