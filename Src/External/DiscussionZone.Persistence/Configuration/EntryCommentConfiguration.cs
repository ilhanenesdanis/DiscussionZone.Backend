using DiscussionZone.Domain;
using DiscussionZone.Persistence.Configuration.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscussionZone.Persistence.Configuration
{
    public sealed class EntryCommentConfiguration : BaseEntityConfiguration<EntryComment>
    {
        public override void Configure(EntityTypeBuilder<EntryComment> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Title).HasMaxLength(150).IsRequired();
            builder.Property(x => x.Content).IsRequired();
            builder.Property(x => x.ParentCommentId).IsRequired(false);
            builder.HasOne(x => x.Entry).WithMany(x => x.EntryComments).HasForeignKey(x => x.EntryId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
        }
    }
}
