using Lifelog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lifelog.Domain.Infra.Contexts.Mapping;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");
        
        // PK
        builder.HasKey(x => x.Id);

        // Identity
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        // Properties
        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnName("Name")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);
        
        builder.Property(x => x.Email)
            .IsRequired()
            .HasColumnName("Email")
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);
        
        builder.Property(x => x.Password)
            .IsRequired()
            .HasColumnName("Password")
            .HasColumnType("VARCHAR")
            .HasMaxLength(80);
        
        builder.Property(x => x.Slug)
            .IsRequired()
            .HasColumnName("Slug")
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);

        builder.Property(x => x.Image)
            .IsRequired(false)
            .HasColumnName("Image")
            .HasColumnType("VARCHAR")
            .HasMaxLength(255);

        builder.Property(x => x.Auth2Fa)
            .HasColumnName("Auth2Fa")
            .HasColumnType("BIT")
            .HasDefaultValue(false);

        builder.Property(x => x.Language)
            .HasColumnName("Language")
            .HasColumnType("SMALLINT")
            .HasDefaultValue(1);

        builder.Property(x => x.Timezone)
            .HasColumnName("Timezone")
            .HasColumnType("SMALLINT")
            .HasDefaultValue(1);
        
        builder.Property(x => x.Joined)
            .IsRequired()
            .HasColumnType("SMALLDATETIME")
            .HasMaxLength(60)
            .HasDefaultValueSql("GETDATE()");

        // Indexes
        builder.HasIndex(x => x.Slug, "IX_User_Slug")
            .IsUnique();
        
        // Relationships
        builder.HasOne(x => x.Role)
            .WithMany(x => x.Users)
            .HasConstraintName("FK_User_Role")
            .OnDelete(DeleteBehavior.Cascade);
    }
}