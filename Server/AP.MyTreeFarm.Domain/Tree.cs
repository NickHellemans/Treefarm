using System.Collections.Generic;

namespace AP.MyTreeFarm.Domain
{
    public class Tree
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public string InstructionsUrl { get; set; }
        public string QrCodeUrl { get; set; }
        
        //Foreign keys
        public List<Zone> Zones { get; set; }
    }
}