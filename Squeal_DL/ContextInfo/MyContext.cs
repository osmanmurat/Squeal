using Squeal_EL.IdentityModels;
using DocumentFormat.OpenXml.Bibliography;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class MyContext : IdentityDbContext<AppUser, AppRole, string>
{
    public MyContext(DbContextOptions<MyContext> opt)
        : base(opt)
    {

    }

    public virtual DbSet<TivitMedia> TivitMediaTable { get; set; }
    public virtual DbSet<TivitTag> TivitTagTable { get; set; }
    public virtual DbSet<UserTivit> UserTivitTable { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<AppRole>(x =>
        {
            x.ToTable("ROLES");
        });
    }
}
