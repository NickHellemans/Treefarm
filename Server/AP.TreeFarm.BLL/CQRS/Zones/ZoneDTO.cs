using System.Collections.Generic;
using AP.MyTreeFarm.Application.CQRS.Sites;
using AP.MyTreeFarm.Application.CQRS.Trees;
using AP.MyTreeFarm.Application.CQRS.TreeTasks;

namespace AP.MyTreeFarm.Application.CQRS.Zones;

public class ZoneDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public float SurfaceArea { get; set; }
        
    //Foreign keys
    public int SiteId { get; set; }
    public SiteWithoutZonesDTO Site { get; set; }
    public int TreeId { get; set; }
    public TreeWithoutZonesDTO Tree { get; set; }

    public List<TreeTaskZoneDTO> Tasks { get; set; }
}