using AppVPN.Models.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppVPN.Models
{
    public class AppCtx : IdentityDbContext<User>
    {
        public AppCtx(DbContextOptions<AppCtx> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Tarif> Tarifs { get; set; }

        public DbSet<Price> Prices { get; set; }
    }
}
