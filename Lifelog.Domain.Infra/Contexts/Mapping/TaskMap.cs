using Lifelog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lifelog.Domain.Infra.Contexts.Mapping;

public class TaskMap : IEntityTypeConfiguration<TaskItem>
{
    public void Configure(EntityTypeBuilder<TaskItem> builder)
    {
        builder.ToTable("Task");
        
        // PK
        builder.HasKey(x => x.Id);

        // Identity
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(160)
            .HasColumnType("NVARCHAR");

        builder.Property(x => x.DateInit)
            .IsRequired()
            .HasColumnType("SMALLDATETIME")
            .HasMaxLength(60)
            .HasDefaultValueSql("GETDATE()");
        
        builder.Property(x => x.DateEnd)
            .IsRequired()
            .HasColumnType("SMALLDATETIME")
            .HasMaxLength(60)
            .HasDefaultValueSql("GETDATE()");
        
        builder.Property(x => x.Desc)
            .IsRequired(false)
            .HasMaxLength(1600)
            .HasColumnType("NVARCHAR");
        
        builder.Property(x => x.Done)
            .IsRequired()
            .HasColumnType("BIT");

        builder.Property(x => x.isPublic)
            .IsRequired()
            .HasColumnType("BIT");
        
        builder.Property(x => x.UserSlug)
            .IsRequired()
            .HasMaxLength(160)
            .HasColumnType("VARCHAR");

        // Relationships
        builder.HasOne(x => x.User)
            .WithMany(x => x.Tasks)
            .HasConstraintName("FK_Task_User")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Project)
            .WithMany(x => x.Tasks)
            .HasConstraintName("FK_Task_Project")
            .OnDelete(DeleteBehavior.Cascade);
    }
}