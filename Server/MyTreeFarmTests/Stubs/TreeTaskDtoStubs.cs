using AP.MyTreeFarm.Application.Interfaces;
using AP.MyTreeFarm.Domain;
using TaskStatus = AP.MyTreeFarm.Domain.TaskStatus;

namespace MyTreeFarmTests.Stubs;


public class UowStub : IUnitofWork
{
    public ITreeTasksRepository TreeTasksRepository { get; set; }
    public ITreeRepository TreeRepository { get; set; }
    public IEmployeeRepository EmployeesRepository { get;set;  }
    public ISiteRepository SitesRepository { get; set; }
    public IZoneRepository ZonesRepository { get; set; }
    public Task Commit()
    {
        throw new NotImplementedException();
    }
}
public class ZoneRepoStub : IZoneRepository
{

    private Dictionary<int, Zone> _Zones = new Dictionary<int, Zone>();
    private List<TreeTask> _taskList = new List<TreeTask>();
    private List<TreeTask> _taskList2 = new List<TreeTask>();
    private List<TreeTask> _taskList3 = new List<TreeTask>();
    public ZoneRepoStub()
    {
        var employee1 = new Employee
        {
            Id = 0,
            EmployeeId = null,
            FirstName = null,
            LastName = null,
            Email = null,
            UserName = null,
            //Password = null,
            IsAdmin = false,
            IsActive = false,
            Auth0Id = null,
            Tasks = null
        };
        
        var employee2 = new Employee
        {
            Id = 1,
            EmployeeId = null,
            FirstName = null,
            LastName = null,
            Email = null,
            UserName = null,
            //Password = null,
            IsAdmin = false,
            IsActive = false,
            Auth0Id = null,
            Tasks = null
        };

        
        _taskList.Add(new TreeTask
        {
            Id = 0,
            Name = null,
            Description = null,
            DateCreated = default,
            DateStart = null,
            DateEnd = null,
            Duration = 0,
            EmployeeId = 0,
            Employee = employee1,
            ZoneId = 0,
            Zone = null,
            Status = TaskStatus.ToDo,
            Priority = 0,
            DatePlanned = new DateTime(2001,9,11),
            DatePaused = null,
            TimePaused = 0
        });
        _taskList2.Add(new TreeTask
        {
            Id = 1,
            Name = null,
            Description = null,
            DateCreated = default,
            DateStart = null,
            DateEnd = null,
            Duration = 0,
            EmployeeId = 0,
            Employee = employee2,
            ZoneId = 0,
            Zone = null,
            Status = TaskStatus.ToDo,
            Priority = 0,
            DatePlanned = new DateTime(2001,9,11),
            DatePaused = null,
            TimePaused = 0
        });
        
        _taskList3.Add(new TreeTask
        {
            Id = 1,
            Name = null,
            Description = null,
            DateCreated = default,
            DateStart = null,
            DateEnd = null,
            Duration = 0,
            EmployeeId = 3,
            Employee = employee2,
            ZoneId = 3,
            Zone = null,
            Status = TaskStatus.ToDo,
            Priority = 0,
            DatePlanned = new DateTime(2001,9,11),
            DatePaused = null,
            TimePaused = 0
        });
        _Zones.Add( 1, new Zone
        {
            Id=1,
            Name = null,
            Site = null,
            TreeId = 0,
            SiteId = 3,
            SurfaceArea = 0,
            Tasks = _taskList,
            Tree = null
        });
        
        _Zones.Add( 2, new Zone
        {
            Id=1,
            Name = null,
            Site = null,
            TreeId = 0,
            SiteId = 1,
            SurfaceArea = 0,
            Tasks = _taskList2,
            Tree = null
        });
        
        _Zones.Add( 3, new Zone
        {
            Id=3,
            Name = null,
            Site = null,
            TreeId = 0,
            SiteId = 1,
            SurfaceArea = 0,
            Tasks = _taskList3,
            Tree = null
        });
    }
    public Task<IEnumerable<Zone>> GetAll(int pageNr, int pageSize) {
        throw new NotImplementedException();
    }

    public Task<Zone> GetById(int id)
    {
        return Task.Delay(1)
            .ContinueWith(t => _Zones[id]);
    }

    public Zone Create(Zone newPerson)
    {
        throw new NotImplementedException();
    }

    public Zone Update(Zone modifiedPerson)
    {
        throw new NotImplementedException();
    }

    public void Delete(Zone person)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Zone>> GetAll()
    {
        throw new NotImplementedException();
    }
}

public class EmployeeRepoStub : IEmployeeRepository
{
    private Dictionary<int, Employee> _Employees = new Dictionary<int, Employee>();
    private List<TreeTask> _taskList = new List<TreeTask>();
    private List<TreeTask> _taskList2 = new List<TreeTask>();
    private List<TreeTask> _taskList3 = new List<TreeTask>();
    private List<TreeTask> _taskList4 = new List<TreeTask>();
    public EmployeeRepoStub()
    {
        var zone1 = new Zone
        {
            Id = 0,
            Name = null,
            SurfaceArea = 0,
            SiteId = 0,
            Site = null,
            TreeId = 0,
            Tree = null,
            Tasks = null
        };
        var zone2 = new Zone
        {
            Id = 2,
            Name = null,
            SurfaceArea = 0,
            SiteId = 1,
            Site = null,
            TreeId = 0,
            Tree = null,
            Tasks = null
        };
        var zone3 = new Zone
        {
            Id = 2,
            Name = null,
            SurfaceArea = 0,
            SiteId = 3,
            Site = null,
            TreeId = 0,
            Tree = null,
            Tasks = null
        };
        _taskList.Add(new TreeTask
        {
            Id = 0,
            Name = null,
            Description = null,
            DateCreated = default,
            DateStart = null,
            DateEnd = null,
            Duration = 0,
            EmployeeId = 0,
            Employee = null,
            ZoneId = 0,
            Zone = zone1,
            Status = TaskStatus.ToDo,
            Priority = 0,
            DatePlanned = new DateTime(2001,9,11),
            DatePaused = null,
            TimePaused = 0
        });
        _taskList.Add(new TreeTask
        {
            Id = 1,
            Name = null,
            Description = null,
            DateCreated = default,
            DateStart = null,
            DateEnd = null,
            Duration = 0,
            EmployeeId = 0,
            Employee = null,
            ZoneId = 0,
            Zone = zone2,
            Status = TaskStatus.ToDo,
            Priority = 0,
            DatePlanned = new DateTime(2001,9,11),
            DatePaused = null,
            TimePaused = 0
        });
        _taskList.Add(new TreeTask
        {
            Id = 2,
            Name = null,
            Description = null,
            DateCreated = default,
            DateStart = null,
            DateEnd = null,
            Duration = 0,
            EmployeeId = 0,
            Employee = null,
            ZoneId = 0,
            Zone = zone2,
            Status = TaskStatus.ToDo,
            Priority = 0,
            DatePlanned = new DateTime(2001,9,11),
            DatePaused = null,
            TimePaused = 0
        });
        _taskList.Add(new TreeTask
        {
            Id = 3,
            Name = null,
            Description = null,
            DateCreated = default,
            DateStart = null,
            DateEnd = null,
            Duration = 0,
            EmployeeId = 0,
            Employee = null,
            ZoneId = 0,
            Zone = zone2,
            Status = TaskStatus.ToDo,
            Priority = 0,
            DatePlanned = new DateTime(2001,9,11),
            DatePaused = null,
            TimePaused = 0
        });
        _taskList2.Add(new TreeTask
        {
            Id = 0,
            Name = null,
            Description = null,
            DateCreated = default,
            DateStart = null,
            DateEnd = null,
            Duration = 0,
            EmployeeId = 0,
            Employee = null,
            ZoneId = 0,
            Zone = zone2,
            Status = TaskStatus.ToDo,
            Priority = 0,
            DatePlanned = new DateTime(2001,9,11),
            DatePaused = null,
            TimePaused = 0
        });
        _taskList3.Add(new TreeTask
        {
            Id = 0,
            Name = null,
            Description = null,
            DateCreated = default,
            DateStart = null,
            DateEnd = null,
            Duration = 1,
            EmployeeId = 0,
            Employee = null,
            ZoneId = 0,
            Zone = zone3,
            Status = TaskStatus.ToDo,
            Priority = 0,
            DatePlanned = new DateTime(2001,9,11),
            DatePaused = null,
            TimePaused = 0
        });
        
        _Employees.Add(1, new Employee
        {
            Auth0Id = "",
            FirstName = null,
            Email = null,
            UserName = null,
            EmployeeId = null,
            Id = 1,
            IsActive = false,
            IsAdmin = false,
            LastName = null,
            //Password = null,
            Tasks = _taskList

        });
        
        _Employees.Add(2, new Employee
        {
            Auth0Id = "",
            FirstName = null,
            Email = null,
            UserName = null,
            EmployeeId = null,
            Id = 2,
            IsActive = false,
            IsAdmin = false,
            LastName = null,
            //Password = null,
            Tasks = _taskList2

        });
        
        _Employees.Add(3, new Employee
        {
            Auth0Id = "",
            FirstName = null,
            Email = null,
            UserName = null,
            EmployeeId = null,
            Id = 2,
            IsActive = false,
            IsAdmin = false,
            LastName = null,
            //Password = null,
            Tasks = _taskList3

        });
        _taskList4.Add(new TreeTask
        {
            Id = 0,
            Name = null,
            Description = null,
            DateCreated = default,
            DateStart = null,
            DateEnd = null,
            Duration = 0,
            EmployeeId = 0,
            Employee = null,
            ZoneId = 0,
            Zone = zone3,
            Status = TaskStatus.ToDo,
            Priority = 0,
            DatePlanned = new DateTime(2022,12,5),
            DatePaused = null,
            TimePaused = 0
        });
        _taskList4.Add(new TreeTask
        {
            Id = 0,
            Name = null,
            Description = null,
            DateCreated = default,
            DateStart = null,
            DateEnd = null,
            Duration = 0,
            EmployeeId = 0,
            Employee = null,
            ZoneId = 0,
            Zone = zone3,
            Status = TaskStatus.ToDo,
            Priority = 0,
            DatePlanned = new DateTime(2022,12,6),
            DatePaused = null,
            TimePaused = 0
        });
        _taskList4.Add(new TreeTask
        {
            Id = 0,
            Name = null,
            Description = null,
            DateCreated = default,
            DateStart = null,
            DateEnd = null,
            Duration = 0,
            EmployeeId = 0,
            Employee = null,
            ZoneId = 0,
            Zone = zone3,
            Status = TaskStatus.ToDo,
            Priority = 0,
            DatePlanned = new DateTime(2022,12,7),
            DatePaused = null,
            TimePaused = 0
        });
        _taskList4.Add(new TreeTask
        {
            Id = 0,
            Name = null,
            Description = null,
            DateCreated = default,
            DateStart = null,
            DateEnd = null,
            Duration = 0,
            EmployeeId = 0,
            Employee = null,
            ZoneId = 0,
            Zone = zone3,
            Status = TaskStatus.ToDo,
            Priority = 0,
            DatePlanned = new DateTime(2022,12,8),
            DatePaused = null,
            TimePaused = 0
        });
        _taskList4.Add(new TreeTask
        {
            Id = 0,
            Name = null,
            Description = null,
            DateCreated = default,
            DateStart = null,
            DateEnd = null,
            Duration = 0,
            EmployeeId = 0,
            Employee = null,
            ZoneId = 0,
            Zone = zone3,
            Status = TaskStatus.ToDo,
            Priority = 0,
            DatePlanned = new DateTime(2022,12,9),
            DatePaused = null,
            TimePaused = 0
        });
        _Employees.Add(4, new Employee
        {
            Auth0Id = "",
            FirstName = null,
            Email = null,
            UserName = null,
            EmployeeId = null,
            Id = 4,
            IsActive = false,
            IsAdmin = false,
            LastName = null,
            //Password = null,
            Tasks = _taskList4

        });
    }
    public Task<IEnumerable<Employee>> GetAll(int pageNr, int pageSize)
    {
        throw new NotImplementedException();
    }

    public Task<Employee> GetById(int id)
    {
        return Task.Delay(1)
            .ContinueWith(t => _Employees[id]);
    }

    public Employee Create(Employee newPerson)
    {
        throw new NotImplementedException();
    }

    public Employee Update(Employee modifiedPerson)
    {
        throw new NotImplementedException();
    }

    public void Delete(Employee person)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Employee>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Employee> GetByEmail(string email)
    {
        throw new NotImplementedException();
    }
}

public class SiteRepoStub : ISiteRepository
{
    private Dictionary<int, Site> _Sites = new Dictionary<int, Site>();
    private List<Zone> _zoneList = new List<Zone>();
    public SiteRepoStub()
    {
        _zoneList.Add(new Zone
        {
            Id = 0,
            Name = null,
            SurfaceArea = 0,
            SiteId = 0,
            Site = null,
            TreeId = 3,
            Tree = null,
            Tasks = null
        });
        _zoneList.Add(new Zone
        {
            Id = 1,
            Name = null,
            SurfaceArea = 0,
            SiteId = 0,
            Site = null,
            TreeId = 1,
            Tree = null,
            Tasks = null
        });
        _zoneList.Add(new Zone
        {
            Id = 2,
            Name = null,
            SurfaceArea = 0,
            SiteId = 0,
            Site = null,
            TreeId = 1,
            Tree = null,
            Tasks = null
        });
        _zoneList.Add(new Zone
        {
            Id = 3,
            Name = null,
            SurfaceArea = 0,
            SiteId = 0,
            Site = null,
            TreeId = 1,
            Tree = null,
            Tasks = null
        });
        _Sites.Add(1, new Site
        {
            Id = 0,
            Name = null,
            PostalCode = null,
            Street = null,
            StreetNumber = null,
            MapPicturePath = null,
            Zones = _zoneList
        });
    }
    public Task<IEnumerable<Site>> GetAll(int pageNr, int pageSize)
    {
        throw new NotImplementedException();
    }

    public Task<Site> GetById(int id)
    {
        return Task.Delay(1)
            .ContinueWith(t => _Sites[id]);
    }

    public Site Create(Site newPerson)
    {
        throw new NotImplementedException();
    }

    public Site Update(Site modifiedPerson)
    {
        throw new NotImplementedException();
    }

    public void Delete(Site person)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Site>> GetAll()
    {
        throw new NotImplementedException();
    }
}