using AP.MyTreeFarm.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.MyTreeFarm.Infrastructure.Configuration
{
    public class TreeConfiguration : IEntityTypeConfiguration<Tree>
    {
        public void Configure(EntityTypeBuilder<Tree> builder)
        {
            builder.ToTable("tblTrees", "Tree")
                .HasKey(t => t.Id);
            
            builder.HasIndex(t => t.Id)
                .IsUnique();

            builder.Property(t => t.Id)
                .HasColumnType("int");

            builder.Property(t => t.Name)
                .IsRequired()
                .HasColumnType("nvarchar")
                .HasMaxLength(255);
            
            builder.Property(t => t.PictureUrl)
                .IsRequired()
                .HasColumnType("nvarchar")
                .HasMaxLength(255);
            
            builder.Property(t => t.InstructionsUrl)
                .IsRequired()
                .HasColumnType("nvarchar")
                .HasMaxLength(255);
            
            builder.Property(t => t.QrCodeUrl)
                .IsRequired()
                .HasColumnType("nvarchar")
                .HasMaxLength(255);
            
            builder.HasMany(t => t.Zones).WithOne(z => z.Tree);
        }
    }
}