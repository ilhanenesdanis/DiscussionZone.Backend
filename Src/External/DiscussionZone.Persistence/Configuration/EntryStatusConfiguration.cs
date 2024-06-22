using DiscussionZone.Domain;
using DiscussionZone.Persistence.Configuration.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscussionZone.Persistence.Configuration
{
    public sealed class EntryStatusConfiguration : BaseEntityConfiguration<EntryStatus>
    {
        public override void Configure(EntityTypeBuilder<EntryStatus> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name).IsRequired();
        }
    }
}
