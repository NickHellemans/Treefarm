using AP.MyTreeFarm.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.MyTreeFarm.Infrastructure.Seeding
{
    public static class TreeSeeding
    {
        public static void Seed(this EntityTypeBuilder<Tree> modelBuilder)
        {
            modelBuilder.HasData(
                new Tree
                {
                    Id = 1,
                    Name = "Malus sylvestris",
                    PictureUrl = "appelboom.jpg",
                    InstructionsUrl = "appelboom.pdf",
                    QrCodeUrl = "appelboomQR.png",
                },
                new Tree
                {
                    Id = 2,
                    Name = "Reine Claude d'Oullins",
                    PictureUrl = "pruimenboom.jpg",
                    InstructionsUrl = "pruimenboom.pdf",
                    QrCodeUrl = "pruimenboomQR.png",
                }
            );
        }
    }
}