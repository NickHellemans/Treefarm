using AP.MyTreeFarm.Application.CQRS.TreeTasks;
using AP.MyTreeFarm.Application.CQRS.Zones;
using X.PagedList;

namespace MyTreeFarmDashboard.Models;

public class ZoneVM
{
    public ZoneDTO Zone { get; set; }
    public IPagedList<TreeTaskZoneDTO> PagedTasks { get; set; }
}