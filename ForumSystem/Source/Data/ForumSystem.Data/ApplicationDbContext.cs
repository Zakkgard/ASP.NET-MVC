namespace ForumSystem.Data
{
    using Common.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Migrations;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        private void ApplyAuditInfoRules()
        {
            var entities = this.ChangeTracker
                .Entries()
                .Where(e => e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified)));

            foreach (var entity in entities)
            {
                var currentEntity = (IAuditInfo)entity.Entity;

                if (entity.State == EntityState.Added)
                {
                    if (!currentEntity.PreserveCreatedOn)
                    {
                        currentEntity.CreatedOn = DateTime.Now;
                    }
                    else
                    {
                        currentEntity.ModifiedOn = DateTime.Now;
                    }
                }
            }
        }

        private void ApplyDeletableEntityRules()
        {
            var entities = this.ChangeTracker
                .Entries()
                .Where(e => e.Entity is IDeletableEntity && e.State == EntityState.Deleted);

            foreach (var entity in entities)
            {
                var currentEntity = (IDeletableEntity)entity.Entity;

                currentEntity.DeletedOn = DateTime.Now;
                currentEntity.IsDeleted = true;
                entity.State = EntityState.Modified;
            }
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            this.ApplyDeletableEntityRules();
            return base.SaveChanges();
        }

        public IDbSet<Tag> Tags { get; set; }
    }
}
