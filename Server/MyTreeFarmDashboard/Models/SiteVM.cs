using AP.MyTreeFarm.Application.CQRS.Sites;
using AP.MyTreeFarm.Application.CQRS.Zones;
using X.PagedList;

namespace MyTreeFarmDashboard.Models;

public class SiteVM
{
    public SiteDTO Site { get; set; }
    public IPagedList<ZoneWithoutTasksDTO> PagedZones { get; set; }
}