using DiscussionZone.Domain;
using DiscussionZone.Persistence.Configuration.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscussionZone.Persistence.Configuration
{
    public sealed class EntryCategoryConfiguration : BaseEntityConfiguration<EntryCategory>
    {
        public override void Configure(EntityTypeBuilder<EntryCategory> builder)
        {
            base.Configure(builder);

            builder.HasOne(x => x.Category).WithMany(x => x.EntryCategories).HasForeignKey(x => x.CategoryId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
            builder.HasOne(x => x.Entry).WithMany(x => x.EntryCategories).HasForeignKey(x => x.EntryId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
        }
    }
}
