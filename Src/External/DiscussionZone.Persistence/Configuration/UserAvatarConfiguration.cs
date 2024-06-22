using DiscussionZone.Domain;
using DiscussionZone.Persistence.Configuration.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscussionZone.Persistence.Configuration
{
    public sealed class UserAvatarConfiguration:BaseEntityConfiguration<UserAvatar>
    {
        public override void Configure(EntityTypeBuilder<UserAvatar> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.FileName).IsRequired();
            builder.Property(x=>x.FileUrl).IsRequired();
            builder.HasOne(x => x.AppUser).WithOne(x => x.UserAvatar).HasForeignKey<UserAvatar>(x => x.CreatedBy);
        }
    }
}
