using AP.MyTreeFarm.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.MyTreeFarm.Infrastructure.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("tblEmployees", "Employee")
                    .HasKey(e => e.Id);
            
            //Needed? -> Already PK
            builder.HasIndex(e => e.Id)
                    .IsUnique();

            builder.Property(e => e.Id)
                   .HasColumnType("int");

            builder.Property(e => e.EmployeeId)
                .IsRequired()
                .HasColumnType("nvarchar")
                .HasMaxLength(255);

            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasColumnType("nvarchar")
                .HasMaxLength(255);
            
            builder.Property(e => e.LastName)
                .IsRequired()
                .HasColumnType("nvarchar")
                .HasMaxLength(255);
            
            builder.Property(e => e.Email)
                .IsRequired()
                .HasColumnType("nvarchar")
                .HasMaxLength(255);
            
            builder.Property(e => e.UserName)
                .IsRequired()
                .HasColumnType("nvarchar")
                .HasMaxLength(255);
            
            /*
            builder.Property(e => e.Password)
                .IsRequired()
                .HasColumnType("nvarchar")
                .HasMaxLength(255); */

            builder.Property(e => e.IsAdmin)
                .IsRequired()
                .HasColumnType("bit");
            
            builder.Property(e => e.IsActive)
                .IsRequired()
                .HasColumnType("bit");
            
            builder.Property(e => e.Auth0Id)
                .IsRequired()
                .HasColumnType("nvarchar")
                .HasMaxLength(255);
            
            builder.HasMany(e => e.Tasks).WithOne(t => t.Employee);
        }
    }
}