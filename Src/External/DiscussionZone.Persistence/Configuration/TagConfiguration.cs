using DiscussionZone.Domain;
using DiscussionZone.Persistence.Configuration.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscussionZone.Persistence.Configuration
{
    public sealed class TagConfiguration : BaseEntityConfiguration<Tag>
    {
        public override void Configure(EntityTypeBuilder<Tag> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name).IsRequired();
        }
    }
}
