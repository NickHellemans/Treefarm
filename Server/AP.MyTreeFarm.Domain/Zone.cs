using System.Collections.Generic;

namespace AP.MyTreeFarm.Domain
{
    public class Zone
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float SurfaceArea { get; set; }
        
        //Foreign keys
        public int SiteId { get; set; }
        public Site Site { get; set; }
        public int TreeId { get; set; }
        public Tree Tree { get; set; }

        public List<TreeTask> Tasks { get; set; }
    }
}