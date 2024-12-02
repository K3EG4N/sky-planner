using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkyPlanner.Entities;

namespace SkyPlanner.Configurations
{
    public class UserCredentialConfiguration
    {
        public UserCredentialConfiguration(EntityTypeBuilder<UserCredential> builder)
        {
            builder.HasKey(c => c.UserCredentialId);
            builder.HasOne(c => c.User)
                   .WithMany()
                   .HasForeignKey(c => c.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
