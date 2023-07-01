using System;
using AP.MyTreeFarm.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.MyTreeFarm.Infrastructure.Seeding
{
    public static class TreeTaskSeeding
    {
        public static void Seed(this EntityTypeBuilder<TreeTask> modelBuilder)
        {
            modelBuilder.HasData(
                // Chad
                // Zone 1, 2
                new TreeTask
                {
                    Id = 1,
                    Name = "Takken snoeien",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse lacinia nisi ut urna ullamcorper, quis gravida ex venenatis. Sed elementum maximus eros at dignissim. Pellentesque vitae mi mi. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.",
                   DateCreated = DateTime.Now.AddDays(-4),
                    DateStart = null,
                    DateEnd = null,
                    Duration = 120,
                    EmployeeId = 2,
                    ZoneId = 1,
                    Status = TaskStatus.ToDo,
                    Priority = 1,
                    DatePlanned = DateTime.Today,
                    TimePaused = 0,
                    DatePaused = null
                },
                new TreeTask
                {
                    Id = 2,
                    Name = "Gezondheidscheck",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse lacinia nisi ut urna ullamcorper, quis gravida ex venenatis. Sed elementum maximus eros at dignissim. Pellentesque vitae mi mi. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.",
                   DateCreated = DateTime.Now.AddDays(-4),
                    DateStart = null,
                    DateEnd = null,
                    Duration = 60,
                    EmployeeId = 2,
                    ZoneId = 1,
                    Status = TaskStatus.ToDo,
                    Priority = 2,
                    DatePlanned = DateTime.Today,
                    TimePaused = 0,
                    DatePaused = null
                },
                new TreeTask
                {
                    Id = 3,
                    Name = "Appelinspectie",
                    Description = "Verwijderen van rotte appels, appels met wormen/rupsen, ... ",
                   DateCreated = DateTime.Now.AddDays(-4),
                    DateStart = null,
                    DateEnd = null,
                    Duration = 60,
                    EmployeeId = 2,
                    ZoneId = 1,
                    Status = TaskStatus.ToDo,
                    Priority = 2,
                    DatePlanned = DateTime.Today,
                    TimePaused = 0,
                    DatePaused = null
                },
                new TreeTask
                {
                    Id = 4,
                    Name = "Onkruidverdelging",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse lacinia nisi ut urna ullamcorper, quis gravida ex venenatis. Sed elementum maximus eros at dignissim. Pellentesque vitae mi mi. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.",
                   DateCreated = DateTime.Now.AddDays(-4),
                    DateStart = null,
                    DateEnd = null,
                    Duration = 120,
                    EmployeeId = 2,
                    ZoneId = 1,
                    Status = TaskStatus.ToDo,
                    Priority = 0,
                    DatePlanned = DateTime.Today,
                    TimePaused = 0,
                    DatePaused = null
                },
                new TreeTask
                {
                    Id = 5,
                    Name = "Morgen 1",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse lacinia nisi ut urna ullamcorper, quis gravida ex venenatis. Sed elementum maximus eros at dignissim. Pellentesque vitae mi mi. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.",
                   DateCreated = DateTime.Now.AddDays(-4),
                    DateStart = null,
                    DateEnd = null,
                    Duration = 300,
                    EmployeeId = 2,
                    ZoneId = 2,
                    Status = TaskStatus.ToDo,
                    Priority = 1,
                    DatePlanned = DateTime.Today.AddDays(1),
                    TimePaused = 0,
                    DatePaused = null
                },
                new TreeTask
                {
                    Id = 6,
                    Name = "Morgen 2",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse lacinia nisi ut urna ullamcorper, quis gravida ex venenatis. Sed elementum maximus eros at dignissim. Pellentesque vitae mi mi. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.",
                   DateCreated = DateTime.Now.AddDays(-4),
                    DateStart = null,
                    DateEnd = null,
                    Duration = 150,
                    EmployeeId = 2,
                    ZoneId = 2,
                    Status = TaskStatus.ToDo,
                    Priority = 0,
                    DatePlanned = DateTime.Today.AddDays(1),
                    TimePaused = 0,
                    DatePaused = null
                },
                new TreeTask
                {
                    Id = 7,
                    Name = "Morgen 3",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse lacinia nisi ut urna ullamcorper, quis gravida ex venenatis. Sed elementum maximus eros at dignissim. Pellentesque vitae mi mi. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.",
                   DateCreated = DateTime.Now.AddDays(-4),
                    DateStart = null,
                    DateEnd = null,
                    Duration = 30,
                    EmployeeId = 2,
                    ZoneId = 2,
                    Status = TaskStatus.ToDo,
                    Priority = 0,
                    DatePlanned = DateTime.Today.AddDays(1),
                    TimePaused = 0,
                    DatePaused = null
                },
                new TreeTask
                {
                    Id = 8,
                    Name = "Vandaag-1 1",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse lacinia nisi ut urna ullamcorper, quis gravida ex venenatis. Sed elementum maximus eros at dignissim. Pellentesque vitae mi mi. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.",
                   DateCreated = DateTime.Now.AddDays(-4),
                    DateStart = DateTime.Now.AddDays(-1),
                    DateEnd = DateTime.Now.AddDays(-1).AddMinutes(240),
                    Duration = 300,
                    EmployeeId = 2,
                    ZoneId = 2,
                    Status = TaskStatus.Done,
                    Priority = 0,
                    DatePlanned = DateTime.Today.AddDays(-1),
                    TimePaused = 0,
                    DatePaused = null
                },
                 new TreeTask
                 {
                     Id = 9,
                     Name = "Vandaag-1 2",
                     Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse lacinia nisi ut urna ullamcorper, quis gravida ex venenatis. Sed elementum maximus eros at dignissim. Pellentesque vitae mi mi. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.",
                     DateCreated = DateTime.Now.AddDays(-4),
                     DateStart = DateTime.Now.AddDays(-1).AddMinutes(250),
                     DateEnd = DateTime.Now.AddDays(-1).AddMinutes(310),
                     Duration = 50,
                     EmployeeId = 2,
                     ZoneId = 2,
                     Status = TaskStatus.Done,
                     Priority = 0,
                     DatePlanned = DateTime.Today.AddDays(-1),
                     TimePaused = 0,
                     DatePaused = null
                 },
                new TreeTask
                {
                    Id = 10,
                    Name = "Vandaag-2 1",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse lacinia nisi ut urna ullamcorper, quis gravida ex venenatis. Sed elementum maximus eros at dignissim. Pellentesque vitae mi mi. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.",
                   DateCreated = DateTime.Now.AddDays(-4),
                    DateStart = DateTime.Now.AddDays(-2),
                    DateEnd = DateTime.Now.AddDays(-2).AddMinutes(300),
                    Duration = 300,
                    EmployeeId = 2,
                    ZoneId = 1,
                    Status = TaskStatus.Done,
                    Priority = 0,
                    DatePlanned = DateTime.Today.AddDays(-2),
                    TimePaused = 3000,
                    DatePaused = DateTime.Now.AddDays(-2).AddMinutes(150)
                },
                new TreeTask
                {
                    Id = 11,
                    Name = "Vandaag-2 2",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse lacinia nisi ut urna ullamcorper, quis gravida ex venenatis. Sed elementum maximus eros at dignissim. Pellentesque vitae mi mi. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.",
                    DateCreated = DateTime.Now.AddDays(-4),
                    DateStart = DateTime.Now.AddDays(-2).AddMinutes(310),
                    DateEnd = DateTime.Now.AddDays(-2).AddMinutes(150),
                    Duration = 120,
                    EmployeeId = 2,
                    ZoneId = 1,
                    Status = TaskStatus.Done,
                    Priority = 0,
                    DatePlanned = DateTime.Today.AddDays(-2),
                    TimePaused = 10,
                    DatePaused = null
                },
                new TreeTask
                {
                    Id = 12,
                    Name = "Vandaag-3 1",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse lacinia nisi ut urna ullamcorper, quis gravida ex venenatis. Sed elementum maximus eros at dignissim. Pellentesque vitae mi mi. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.",
                   DateCreated = DateTime.Now.AddDays(-4),
                    DateStart = DateTime.Now.AddDays(-3),
                    DateEnd = DateTime.Now.AddDays(-3).AddMinutes(120),
                    Duration = 100,
                    EmployeeId = 2,
                    ZoneId = 2,
                    Status = TaskStatus.Done,
                    Priority = 0,
                    DatePlanned = DateTime.Today.AddDays(-3),
                    TimePaused = 0,
                    DatePaused = null
                },
                // Belle
                new TreeTask
                {
                    Id = 13,
                    Name = "Noten oogsten",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse lacinia nisi ut urna ullamcorper, quis gravida ex venenatis. Sed elementum maximus eros at dignissim. Pellentesque vitae mi mi. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.",
                    DateCreated = DateTime.Now,
                    DateStart = null,
                    DateEnd = null,
                    Duration = 30,
                    EmployeeId = 3,
                    ZoneId = 7,
                    Status = TaskStatus.ToDo,
                    Priority = 0,
                    DatePlanned = DateTime.Today,
                    TimePaused = 0,
                    DatePaused = null
                },
                new TreeTask
                {
                    Id = 14,
                    Name = "Bladeren opstoken",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse lacinia nisi ut urna ullamcorper, quis gravida ex venenatis. Sed elementum maximus eros at dignissim. Pellentesque vitae mi mi. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.",
                    DateCreated = DateTime.Now,
                    DateStart = null,
                    DateEnd = null,
                    Duration = 30,
                    EmployeeId = 3,
                    ZoneId = 8,
                    Status = TaskStatus.ToDo,
                    Priority = 0,
                    DatePlanned = DateTime.Today,
                    TimePaused = 0,
                    DatePaused = null
                }
            );
        }
    }
}