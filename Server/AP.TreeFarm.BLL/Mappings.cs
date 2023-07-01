using AP.MyTreeFarm.Application.CQRS.Employees;
using AP.MyTreeFarm.Application.CQRS.Sites;
using AP.MyTreeFarm.Application.CQRS.Trees;
using AP.MyTreeFarm.Application.CQRS.TreeTasks;
using AP.MyTreeFarm.Application.CQRS.Zones;
using AP.MyTreeFarm.Domain;
using AutoMapper;

namespace AP.MyTreeFarm.Application;
internal class Mappings : Profile
{
    public Mappings()
    {
        CreateMap<TreeTask, TreeTaskDTO>();
        CreateMap<TreeTask, TreeTaskWithoutEmployeeDTO>();
        CreateMap<TreeTask, TreeTaskZoneDTO>();
        CreateMap<TreeTask, CreateTreeTaskDTO>();
        CreateMap<TreeTask, UpdateTreeTaskDTO>();
        CreateMap<UpdateTreeTaskDTO, TreeTaskDTO>();
        CreateMap<Tree, TreeDTO>();
        CreateMap<Tree, CreateTreeDTO>();
        CreateMap<Tree, TreeWithoutZonesDTO>();
        CreateMap<Tree, TreeInstructionsDTO>();
        CreateMap<Tree, UpdateTreeDTO>();
        CreateMap<UpdateTreeDTO, TreeDTO>();
        CreateMap<Employee, EmployeeDTO>();
        CreateMap<Employee, EmployeeWithoutTasksDTO>();
        CreateMap<Employee, CreateEmployeeDTO>();
        CreateMap<Employee, UpdateEmployeeDTO>();
        CreateMap<UpdateEmployeeDTO, EmployeeDTO>();
        CreateMap<Site, SiteDTO>();
        CreateMap<Site, CreateSiteDTO>();
        CreateMap<Site, UpdateSiteDTO>();
        CreateMap<Site, SiteWithoutZonesDTO>();
        CreateMap<UpdateSiteDTO, SiteDTO>();
        CreateMap<Zone, ZoneDTO>();
        CreateMap<Zone, ZoneWithoutTasksDTO>();
        CreateMap<Zone, ZoneTreeTaskDTO>();
        CreateMap<Zone, CreateZoneDTO>();
        CreateMap<Zone, UpdateZoneDTO>();
        CreateMap<UpdateZoneDTO, ZoneDTO>();
    }
}
