using DiscussionZone.Domain;
using DiscussionZone.Persistence.Configuration.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscussionZone.Persistence.Configuration
{
    public sealed class CategoryConfiguration : BaseEntityConfiguration<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.ParentId).IsRequired(false);

        }
    }
}
