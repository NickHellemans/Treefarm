using AP.MyTreeFarm.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.MyTreeFarm.Infrastructure.Seeding
{
    public static class ZoneSeeding
    {
        public static void Seed(this EntityTypeBuilder<Zone> modelBuilder)
        {
            modelBuilder.HasData(
                new Zone
                {
                    Id = 1,
                    Name = "Eller_Zone1",
                    SurfaceArea = 0.5f,
                    SiteId = 1,
                    TreeId = 1
                },
                new Zone
                {
                    Id = 2,
                    Name = "Eller_Zone2",
                    SurfaceArea = 0.5f,
                    SiteId = 1,
                    TreeId = 2
                },
                new Zone
                {
                    Id = 3,
                    Name = "Eller_Zone3",
                    SurfaceArea = 0.5f,
                    SiteId = 1,
                    TreeId = 2
                },
                new Zone
                {
                    Id = 4,
                    Name = "Eller_Zone4",
                    SurfaceArea = 0.5f,
                    SiteId = 1,
                    TreeId = 2
                },
                new Zone
                {
                    Id = 5,
                    Name = "Meir_Zone1",
                    SurfaceArea = 0.25f,
                    SiteId = 2,
                    TreeId = 1
                },
                new Zone
                {
                    Id = 6,
                    Name = "Meir_Zone2",
                    SurfaceArea = 0.25f,
                    SiteId = 2,
                    TreeId = 2
                },
                new Zone
                {
                    Id = 7,
                    Name = "Schipper_Zone1",
                    SurfaceArea = 0.25f,
                    SiteId = 3,
                    TreeId = 2
                },
                new Zone
                {
                    Id = 8,
                    Name = "DeHoed_Zone1",
                    SurfaceArea = 0.25f,
                    SiteId = 4,
                    TreeId = 1
                }
            );
        }
    }
}