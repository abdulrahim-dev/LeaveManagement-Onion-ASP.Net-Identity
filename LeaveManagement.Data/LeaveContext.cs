using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using LeaveManagement.Core.DomainModels;
using LeaveManagement.Data.ConfigurationMapping;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LeaveManagement.Data
{
    public class LeaveContext : IdentityDbContext<ApplicationIdentityUser, ApplicationIdentityRole, int, 
        ApplicationIdentityUserLogin, ApplicationIdentityUserRole, ApplicationIdentityUserClaim>, IEntitiesContext//DbContext
    {
        public LeaveContext() : base("name=LeaveContextContextConnectionString")
        {
            var a = Database.Connection.ConnectionString;
            Database.SetInitializer(new LeaveContextInitalizeDb());
        }

        //New Tables
        public DbSet<UserProfile> Employee { get; set; }

        /// <summary>
        /// Fluent API is another way to configure your domain classes. Fluent API provides more functionality for configuration than DataAnnotations....
        /// EntityTypeConfiguration is an important class in Fluent API. EntityTypeConfiguration provides you important methods to configure entities and its properties to override various Code-First conventions. It can be obtained by calling the Entity<TEntity>() method of DbModelBuilder class
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EmployeeDetailsMap());
            base.OnModelCreating(modelBuilder);
        }

        #region ---EnitityContext Interface Implementation-----------------
        private ObjectContext _objectContext;
        private DbTransaction _transaction;
        public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        public void SetAsAdded<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            UpdateEntityState(entity, EntityState.Added);
        }

        public void SetAsModified<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            UpdateEntityState(entity, EntityState.Modified);
        }

        public void SetAsDeleted<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            UpdateEntityState(entity, EntityState.Deleted);
        }

        public void BeginTransaction()
        {
            _objectContext = ((IObjectContextAdapter)this).ObjectContext;
            if (_objectContext.Connection.State == ConnectionState.Open)
            {
                return;
            }
            _objectContext.Connection.Open();
            _transaction = _objectContext.Connection.BeginTransaction();
        }

        public int Commit()
        {
            var saveChanges = SaveChanges();
            _transaction.Commit();
            return saveChanges;
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        public Task<int> CommitAsync()
        {
            var saveChangesAsync = SaveChangesAsync();
            _transaction.Commit();
            return saveChangesAsync;
        }

        private void UpdateEntityState<TEntity>(TEntity entity, EntityState entityState) where TEntity : BaseEntity
        {
            var dbEntityEntry = GetDbEntityEntrySafely(entity);
            dbEntityEntry.State = entityState;
        }

        private DbEntityEntry GetDbEntityEntrySafely<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            var dbEntityEntry = Entry<TEntity>(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                Set<TEntity>().Attach(entity);
            }
            return dbEntityEntry;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_objectContext != null && _objectContext.Connection.State == ConnectionState.Open)
                {
                    _objectContext.Connection.Close();
                }
                if (_objectContext != null)
                {
                    _objectContext.Dispose();
                }
                if (_transaction != null)
                {
                    _transaction.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}
