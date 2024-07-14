using DiscussionZone.Domain;
using DiscussionZone.Domain.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Reflection.Emit;

namespace DiscussionZone.Persistence.Context
{
    public class AppDbContext : IdentityDbContext<AppUser, IdentityRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Entry> Entries { get; set; }
        public DbSet<EntryCategory> EntryCategories { get; set; }
        public DbSet<EntryComment> EntryComments { get; set; }
        public DbSet<EntryStatus> EntryStatuses { get; set; }
        public DbSet<EntryTag> EntryTags { get; set; }
        public DbSet<EntryVote> EntryVotes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<UserAvatar> UserAvatars { get; set; }

        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
        public override int SaveChanges()
        {
            OnBeforeSave();
            OnBeforeUpdate();
            return base.SaveChanges();
        }
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSave();
            OnBeforeUpdate();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            OnBeforeSave();
            OnBeforeUpdate();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            OnBeforeSave();
            OnBeforeUpdate();
            return base.SaveChangesAsync(cancellationToken);
        }
        private void OnBeforeSave()
        {
            ChangeTracker.Entries().Where(x => x.State == EntityState.Added)
                .Select(x => (BaseEntity)x.Entity)
                .ToList()
                .ForEach(x =>
                {
                    if (x.CreateDate == DateTime.MinValue)
                        x.CreateDate = DateTime.UtcNow;
                    x.IsDeleted = false;
                    x.IsActive = true;
                });
        }
        private void OnBeforeUpdate()
        {
            ChangeTracker.Entries().Where(x => x.State == EntityState.Modified)
                .Select(x => (BaseEntity)x.Entity)
                .ToList()
                .ForEach(x =>
                {
                    x.UpdateDate = DateTime.Now;
                });
        }
    }
}
