using Cookbook_1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cookbook_1.Database.Configurations
{
    public class RatingEntityConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.HasKey(r => new { r.RatedUserId, r.RatedRecipeId });

            builder.Property(r => r.Value).IsRequired();

            builder.HasOne(r => r.User)
                .WithMany(u => u.UserRates)
                .HasForeignKey(r => r.RatedUserId);

            builder.HasOne(r => r.Recipe)
                .WithMany(recipe => recipe.ListOfRatings)
                .HasForeignKey(r => r.RatedRecipeId);
        }
    }
}
