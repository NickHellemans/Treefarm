using AP.MyTreeFarm.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.MyTreeFarm.Infrastructure.Seeding
{
    public static class SiteSeeding
    {
        public static void Seed(this EntityTypeBuilder<Site> modelBuilder)
        {
            modelBuilder.HasData(
                new Site
                {
                    Id = 1,
                    Name = "Site_Ellerman",
                    PostalCode = "2000",
                    Street = "Ellermanstraat",
                    StreetNumber = "61",
                    MapPicturePath = "farm_map.png"
                },
                new Site
                {
                    Id = 2,
                    Name = "Site_Meir",
                    PostalCode = "2000",
                    Street = "Lange Nieuwstraat",
                    StreetNumber = "35",
                    MapPicturePath = "farm_map.png"
                },
                new Site
                {
                    Id = 3,
                    Name = "Site_Schipperskwartier",
                    PostalCode = "2000",
                    Street = "Schippersstraat",
                    StreetNumber = "20",
                    MapPicturePath = "farm_map.png"
                },
                new Site
                {
                    Id = 4,
                    Name = "Site_DeHoed",
                    PostalCode = "2000",
                    Street = "Hoedstraat",
                    StreetNumber = "420",
                    MapPicturePath = "farm_map.png"
                }
            );
        }
    }
}