using System.Collections.Generic;
using AP.MyTreeFarm.Application.CQRS.Zones;

namespace AP.MyTreeFarm.Application.CQRS.Trees;

public class TreeDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PictureUrl { get; set; }
    public string InstructionsUrl { get; set; }
    public string QrCodeUrl { get; set; }
        
    //Foreign keys
    public List<ZoneWithoutTasksDTO> Zones { get; set; }
}