using System.Collections.Generic;
using AP.MyTreeFarm.Application.CQRS.Zones;

namespace AP.MyTreeFarm.Application.CQRS.Sites;

public class SiteDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PostalCode { get; set; }
    public string Street { get; set; }
    public string StreetNumber { get; set; }
    public string MapPicturePath { get; set; }
    public List<ZoneWithoutTasksDTO> Zones { get; set; }
}