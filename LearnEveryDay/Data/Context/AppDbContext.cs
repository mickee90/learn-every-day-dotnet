using System;
using System.Threading;
using System.Threading.Tasks;
using LearnEveryDay.Models;
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
    public DbSet<User> Users { get; set; }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
      OnBeforeSavning();

      return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override async Task<int> SaveChangesAsync(
        bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default(CancellationToken)
    )
    {
      OnBeforeSavning();

      return (await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken));
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
