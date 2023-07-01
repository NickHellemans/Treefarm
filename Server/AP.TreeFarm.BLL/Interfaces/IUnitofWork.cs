using System.Threading.Tasks;

namespace AP.MyTreeFarm.Application.Interfaces
{
    public interface IUnitofWork
    {
        public ITreeTasksRepository TreeTasksRepository { get; }
        public ITreeRepository TreeRepository { get; }
        public IEmployeeRepository EmployeesRepository { get; }
        public ISiteRepository SitesRepository { get; }
        public IZoneRepository ZonesRepository { get; }
        Task Commit();
        
    }
}
