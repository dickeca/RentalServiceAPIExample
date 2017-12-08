using System;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using Microsoft.AspNet.Identity.EntityFramework;

namespace RentalServiceAPI.Model.Context
{
    public class RentalServiceDbContext : IdentityDbContext<User>
    {
        public virtual DbSet<Title> Titles { get; set; }
        public virtual DbSet<RentalHistory> RentalHistories { get; set; }
        public virtual DbSet<SettingsValue> SettingsValues { get; set; }
        public virtual DbSet<SettingsValueType> SettingsValueTypes { get; set; }

        public RentalServiceDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            this.Configuration.ProxyCreationEnabled = false;
        }

        public static RentalServiceDbContext Create()
        {
            return new RentalServiceDbContext();
        }

        public override int SaveChanges()
        {
            var modifiedEntries = ChangeTracker.Entries()
              .Where(x => x.Entity is IAuditableEntity
                  && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entry in modifiedEntries)
            {
                var entity = entry.Entity as IAuditableEntity;
                if (entity == null) continue;
                var identityName = Thread.CurrentPrincipal.Identity.Name;
                var now = DateTime.UtcNow;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedBy = identityName;
                    entity.CreatedDate = now;
                }
                else {
                    base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                    base.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                }

                entity.UpdatedBy = identityName;
                entity.UpdatedDate = now;
            }

            return base.SaveChanges();
        }
    }
}
