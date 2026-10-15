using Cookbook_1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cookbook_1.Database.Configurations
{
    // sealed Запрещает наследоваться от этого класса
    public sealed class JwtTokenEntityConfiguration : IEntityTypeConfiguration<JwtToken>
    {
        public void Configure(EntityTypeBuilder<JwtToken> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserId)
                .ValueGeneratedNever()
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
