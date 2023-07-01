using AP.MyTreeFarm.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.MyTreeFarm.Infrastructure.Configuration
{
    public class TreeTaskConfiguration : IEntityTypeConfiguration<TreeTask>
    {
        public void Configure(EntityTypeBuilder<TreeTask> builder)
        {
            builder.ToTable("tblTreeTasks", "TreeTask")
                .HasKey(t => t.Id);
            
            builder.HasIndex(t => t.Id)
                .IsUnique();

            builder.Property(t => t.Id)
                .HasColumnType("int");

            builder.Property(t => t.Name)
                .IsRequired()
                .HasColumnType("nvarchar")
                .HasMaxLength(255);
            
            builder.Property(t => t.Description)
                .IsRequired()
                .HasColumnType("nvarchar")
                .HasMaxLength(4000);

            builder.Property(t => t.DateCreated)
                .IsRequired()
                .HasColumnType("datetime");
            
            builder.Property(t => t.DateStart)
                .HasColumnType("datetime");
            
            builder.Property(t => t.DateEnd)
                .HasColumnType("datetime");
            
            builder.Property(t => t.Duration)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(t => t.Status)
                .IsRequired()
                .HasColumnType("int");
            
            builder.Property(t => t.Priority)
                .IsRequired()
                .HasColumnType("int");
            
            builder.Property(t => t.DatePlanned)
                .IsRequired()
                .HasColumnType("datetime");
            
            builder.Property(t => t.DatePaused)
                .HasColumnType("datetime");
            
            builder.Property(t => t.TimePaused)
                .IsRequired()
                .HasColumnType("int");
        }
    }
}