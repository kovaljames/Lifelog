using Lifelog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lifelog.Domain.Infra.Contexts.Mapping;

public class ProjectMap : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("Project");
        
        // PK
        builder.HasKey(x => x.Id);

        // Identity
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnName("Name")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(160);

        builder.Property(x => x.Slug)
            .IsRequired()
            .HasColumnName("Slug")
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);

        builder.Property(x => x.Color)
            .IsRequired(false)
            .HasColumnName("Color")
            .HasColumnType("VARCHAR")
            .HasMaxLength(16);
        
        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasColumnType("SMALLDATETIME")
            .HasMaxLength(60);
        
        builder.Property(x => x.DueDate)
            .IsRequired(false)
            .HasColumnType("SMALLDATETIME")
            .HasMaxLength(60);
        
        builder.Property(x => x.Status)
            .IsRequired(false)
            .HasColumnName("Status")
            .HasColumnType("INTEGER");
        
        builder.Property(x => x.IsPublic)
            .IsRequired()
            .HasColumnName("IsPublic")
            .HasColumnType("BIT");
        
        builder.Property(x => x.Active)
            .IsRequired()
            .HasColumnName("Active")
            .HasColumnType("BIT");
        
        builder.Property(x => x.User)
            .IsRequired()
            .HasColumnName("User")
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);
        
        // Indexes
        builder.HasIndex(x => x.Slug, "IX_Project_Slug")
            .IsUnique();

        // Relationships
        builder.HasMany(x => x.Users)
            .WithMany(x => x.Projects)
            .UsingEntity<Dictionary<string, object>>(
                "ProjectUser",
                project => project.HasOne<User>()
                    .WithMany()
                    .HasForeignKey("ProjectId")
                    .HasConstraintName("FK_ProjectUser_ProjectId")
                    .OnDelete(DeleteBehavior.Cascade),
                user => user.HasOne<Project>()
                    .WithMany()
                    .HasForeignKey("UserId")
                    .HasConstraintName("FK_ProjectUser_UserId")
                    .OnDelete(DeleteBehavior.Cascade));
    }
}