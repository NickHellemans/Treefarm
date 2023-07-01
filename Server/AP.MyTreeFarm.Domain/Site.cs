using System.Collections.Generic;

namespace AP.MyTreeFarm.Domain
{
    public class Site
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string MapPicturePath { get; set; }
        public List<Zone> Zones { get; set; }
    }
    
}