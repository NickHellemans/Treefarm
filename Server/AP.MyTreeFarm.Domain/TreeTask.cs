using System;
using System.Collections.Generic;
using System.Linq;
using AP.MyTreeFarm.Domain;
using FluentValidation;

namespace AP.MyTreeFarm.Domain
{
    public class TreeTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public int Duration { get; set; }

        //Foreign keys
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int ZoneId { get; set; }
        public Zone Zone { get; set; }

        public TaskStatus Status { get; set; }

        public int Priority { get; set; }

        public DateTime DatePlanned { get; set; }
        
        //Properties for pausing
        public DateTime? DatePaused { get; set; }
        public double TimePaused { get; set; }
    }

    public enum TaskStatus
    {
        ToDo,
        InProgress,
        Paused,
        Done
    }

   
}