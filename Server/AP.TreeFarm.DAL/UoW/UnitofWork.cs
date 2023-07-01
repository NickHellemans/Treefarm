using System.Threading.Tasks;
using AP.MyTreeFarm.Application.Interfaces;
using AP.MyTreeFarm.Infrastructure.Contexts;

namespace AP.MyTreeFarm.Infrastructure.UoW
{
    public class UnitofWork : IUnitofWork
    {
        private readonly MyTreeFarmContext ctxt;
        private readonly ITreeTasksRepository treeTasksRepo;
        private readonly ITreeRepository treeRepo;
        private readonly IEmployeeRepository employeeRepo;
        private readonly ISiteRepository siteRepo;
        private readonly IZoneRepository zoneRepo;

        public UnitofWork(MyTreeFarmContext ctxt, ITreeTasksRepository treeTasksRepo
        ,ITreeRepository treeRepo, ISiteRepository siteRepo,IEmployeeRepository employeeRepo, IZoneRepository zoneRepo)
        {
            this.ctxt = ctxt;
            this.treeTasksRepo = treeTasksRepo;
            this.treeRepo = treeRepo;
            this.employeeRepo = employeeRepo;
            this.siteRepo = siteRepo;
            this.zoneRepo = zoneRepo;
        }
        public ITreeTasksRepository TreeTasksRepository => treeTasksRepo;
        public ITreeRepository TreeRepository => treeRepo;
        public IEmployeeRepository EmployeesRepository => employeeRepo;

        public ISiteRepository SitesRepository => siteRepo;
        public IZoneRepository ZonesRepository => zoneRepo;
        public async Task Commit()
        {
            await ctxt.SaveChangesAsync();
        }
    }
}
