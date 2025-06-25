using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DAL.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.FullName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(u => u.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(u => u.Role)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.RegisteredAt)
                .IsRequired();

            builder.HasMany(u => u.Reservations)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Reviews)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(u => u.PhoneNumber)
                .IsUnique();
        }
    }
}
