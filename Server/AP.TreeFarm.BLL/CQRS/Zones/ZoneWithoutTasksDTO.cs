using AP.MyTreeFarm.Application.CQRS.Trees;

namespace AP.MyTreeFarm.Application.CQRS.Zones;

public class ZoneWithoutTasksDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public float SurfaceArea { get; set; }
    
    public TreeInstructionsDTO Tree { get; set; }
    
}