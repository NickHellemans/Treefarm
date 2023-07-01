using AP.MyTreeFarm.Application.CQRS.Trees;
using AP.MyTreeFarm.Application.CQRS.Zones;
using X.PagedList;

namespace MyTreeFarmDashboard.Models;

public class TreeVM
{
    public TreeDTO Tree { get; set; }
    public IPagedList<ZoneWithoutTasksDTO> PagedZones { get; set; }
}