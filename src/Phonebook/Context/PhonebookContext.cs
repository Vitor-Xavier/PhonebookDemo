using Microsoft.EntityFrameworkCore;
using Phonebook.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Phonebook.Context
{
    public class PhonebookContext : DbContext
    {
        public PhonebookContext(DbContextOptions<PhonebookContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Person> People { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<ContactType> ContactTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFProviders.InMemory;Trusted_Connection=True;ConnectRetryCount=0");
        }

        public override int SaveChanges()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreatedAt = DateTime.Now;
                    ((BaseEntity)entity.Entity).UpdatedAt = DateTime.Now;
                }
                else
                {

                    Entry(((BaseEntity)entity.Entity)).Property(x => x.CreatedAt).IsModified = false;
                    ((BaseEntity)entity.Entity).UpdatedAt = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreatedAt = DateTime.Now;
                    ((BaseEntity)entity.Entity).UpdatedAt = DateTime.Now;
                }
                else
                {

                    Entry(((BaseEntity)entity.Entity)).Property(x => x.CreatedAt).IsModified = false;
                    ((BaseEntity)entity.Entity).UpdatedAt = DateTime.Now;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(e => e.Username).IsUnique();
            modelBuilder.Seed();
        }
    }
}
