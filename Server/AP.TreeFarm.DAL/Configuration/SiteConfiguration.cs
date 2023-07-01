using AP.MyTreeFarm.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.MyTreeFarm.Infrastructure.Configuration
{
    public class SiteConfiguration : IEntityTypeConfiguration<Site>
    {
        public void Configure(EntityTypeBuilder<Site> builder)
        {
            builder.ToTable("tblSites", "Site")
                .HasKey(s => s.Id);
            
            builder.HasIndex(s => s.Id)
                .IsUnique();

            builder.Property(s => s.Id)
                .HasColumnType("int");

            builder.Property(s => s.Name)
                .IsRequired()
                .HasColumnType("nvarchar")
                .HasMaxLength(255);
            
            builder.Property(s => s.PostalCode)
                .IsRequired()
                .HasColumnType("nvarchar")
                .HasMaxLength(255);
            
            builder.Property(s => s.Street)
                .IsRequired()
                .HasColumnType("nvarchar")
                .HasMaxLength(255);
            
            builder.Property(s => s.StreetNumber)
                .IsRequired()
                .HasColumnType("nvarchar")
                .HasMaxLength(255);

            builder.Property(s => s.MapPicturePath)
                .IsRequired()
                .HasColumnType("nvarchar")
                .HasMaxLength(255);

            builder.HasMany(s => s.Zones).WithOne(z => z.Site);
        }
    }
}