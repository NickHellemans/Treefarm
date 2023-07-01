using AP.MyTreeFarm.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.MyTreeFarm.Infrastructure.Configuration
{
    public class ZoneConfiguration : IEntityTypeConfiguration<Zone>
    {
        public void Configure(EntityTypeBuilder<Zone> builder)
        {
            builder.ToTable("tblZones", "Zone")
                .HasKey(z => z.Id);
            
            builder.HasIndex(z => z.Id)
                .IsUnique();

            builder.Property(z => z.Id)
                .HasColumnType("int");

            builder.Property(z => z.Name)
                .IsRequired()
                .HasColumnType("nvarchar")
                .HasMaxLength(255);
            
            builder.Property(z => z.SurfaceArea)
                .IsRequired()
                .HasColumnType("float")
                .HasMaxLength(255);
            
            builder.HasMany(z => z.Tasks).WithOne(t => t.Zone);
        }
    }
}