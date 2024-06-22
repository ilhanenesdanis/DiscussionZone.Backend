using DiscussionZone.Domain;
using DiscussionZone.Persistence.Configuration.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscussionZone.Persistence.Configuration
{
    public sealed class EntryVoteConfiguration:BaseEntityConfiguration<EntryVote>
    {
        public override void Configure(EntityTypeBuilder<EntryVote> builder)
        {
            base.Configure(builder);


            builder.HasOne(x => x.Entry).WithMany(x => x.EntryVotes).HasForeignKey(x => x.EntryId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
        }
    }
}
