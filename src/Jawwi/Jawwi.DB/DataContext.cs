using Jawwi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;

namespace Jawwi.DB
{
    public class DataContext : DbContext
    {
        public DbSet<Location> Countries { get; set; }
        public DbSet<User> Users { get; set; }
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
            //AppDomain.CurrentDomain.SetData("DataDirectory", Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData));
        }
    
        public override int SaveChanges()
        {
            TryUpdateModifiedUtc();
                return base.SaveChanges();
        }
        //public override Task<int> SaveChangesAsync()
        //{
        //    TryUpdateModifiedUtc();
        //        return base.SaveChangesAsync();
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
        public void SetModified(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }
        private void TryUpdateModifiedUtc()
        {
            var modifiedEntities = ChangeTracker.Entries().Where(i => i.State == EntityState.Modified).ToList();
            var modelBaseEntities = modifiedEntities.Where(i => i.Entity is ModelBase);
            foreach (var entity in modelBaseEntities)
            {
                ((ModelBase)entity.Entity).ModifiedUtc = DateTime.UtcNow;
            }
            var addedEntities = ChangeTracker.Entries().Where(i => i.State == EntityState.Added).ToList();
            modelBaseEntities = addedEntities.Where(i => i.Entity is ModelBase);
            foreach (var entity in modelBaseEntities)
            {
                ((ModelBase)entity.Entity).CreatedUtc = DateTime.UtcNow;
            }
        }

        //private static void TraceValidationErrors(DbEntityValidationException ex)
        //{
        //    foreach (var validationErrors in ex.EntityValidationErrors)
        //    {
        //        foreach (var validationError in validationErrors.ValidationErrors)
        //        {
        //            Trace.TraceError("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
        //        }
        //    }
        //}
    }
}
