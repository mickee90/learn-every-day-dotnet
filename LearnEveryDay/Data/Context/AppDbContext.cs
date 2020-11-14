using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LearnEveryDay.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LearnEveryDay.Data
{
  public class AppDbContext : IdentityDbContext<User, UserRole, Guid>
  {
    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
    {

    }

    public DbSet<Post> Posts { get; set; }
    public override DbSet<User> Users { get; set; }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
      OnBeforeSavning();

      return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess)
    {
      OnBeforeSavning();

      return await base.SaveChangesAsync(acceptAllChangesOnSuccess);
    }


    public override async Task<int> SaveChangesAsync(
        bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default(CancellationToken)
    )
    {
      OnBeforeSavning();

      return (await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<UserRole>().HasData(new List<UserRole>
      {
        new UserRole {
          Id = Guid.Parse("AD6FC1B5-08CB-43E1-A26F-6CB6753B70BF"),
          Name = "Admin",
          NormalizedName = "ADMIN"
        },
        new UserRole {
          Id = Guid.Parse("A9B1BC21-C51C-4FF8-B37E-DC9452EDF74D"),
          Name = "User",
          NormalizedName = "USER"
        }
      });
    }

    private void OnBeforeSavning()
    {
      var entries = ChangeTracker.Entries();
      var utcNow = DateTime.UtcNow;

      foreach (var entry in entries)
      {
        // for entries that inherit from BaseEntity
        // set CreatedAt and UpdatedAt approperly
        if (entry.Entity is BaseEntity trackable)
        {
          switch (entry.State)
          {
            case EntityState.Modified:
              // set the updated date to now
              trackable.UpdatedAt = utcNow;

              // mark property as "don't touch"
              // we don't want to update on a Modify operation
              entry.Property("CreatedAt").IsModified = false;
              break;

            case EntityState.Added:
              // set both updated and created at to "now"
              trackable.UpdatedAt = utcNow;
              trackable.CreatedAt = utcNow;
              break;
          }
        }
      }
    }
  }

}
