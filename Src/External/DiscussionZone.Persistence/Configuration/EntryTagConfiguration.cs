using DiscussionZone.Domain;
using DiscussionZone.Persistence.Configuration.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscussionZone.Persistence.Configuration
{
    public sealed class EntryTagConfiguration : BaseEntityConfiguration<EntryTag>
    {
        public override void Configure(EntityTypeBuilder<EntryTag> builder)
        {
            base.Configure(builder);


            builder.HasOne(x => x.Entry).WithMany(x => x.EntryTags).HasForeignKey(x => x.EntryId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
            builder.HasOne(x => x.Tag).WithMany(x => x.EntryTags).HasForeignKey(x => x.TagId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);

        }
    }
}
