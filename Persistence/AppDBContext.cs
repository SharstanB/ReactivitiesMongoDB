using Domain.Absractions;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.IdentityEnitities;
using System.Linq.Expressions;
using System.Reflection.Emit;

namespace Persistence;

public class AppDBContext(DbContextOptions<AppDBContext> options)  : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>(options)
{
    public DbSet<Activity> Activities { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<City> Cities { get; set; }

    public DbSet<AppUser> Users { get; set; }
    public DbSet<UserDevice> UserDevices { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<AppUser>()
            .Property(user => user.Name).HasMaxLength(256);

        builder.Entity<UserDevice>()
           .HasOne(ut => ut.User)
           .WithMany(u => u.UserDevices)
           .HasForeignKey(ut => ut.UserId)
           .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<UserDevice>()
            .HasIndex(ut => ut.DeviceId);
        builder.Entity<UserDevice>()
            .HasIndex(ut => ut.RefreshToken);

        //foreach (var entityType in builder.Model.GetEntityTypes())
        //{
        //    if (typeof(ISoftDeletable).IsAssignableFrom(entityType.ClrType))
        //    {
        //        var parameter = Expression.Parameter(entityType.ClrType, "e");
        //        var deletedAtProperty = Expression.Property(parameter, nameof(ISoftDeletable.DeletedAt));
        //        var nullConstant = Expression.Constant(null, typeof(DateTime?));
        //        var filter = Expression.Lambda(Expression.Equal(deletedAtProperty, nullConstant), parameter);

        //        builder.Entity(entityType.ClrType).HasQueryFilter(filter);
        //    }
        //}
   
    }
}