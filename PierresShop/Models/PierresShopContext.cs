using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PierresShop.Models
{
  public class PierresShopContext : IdentityDbContext<ApplicationUser>
  {
    public DbSet<Inventory> Inventory { get; set; }
    public DbSet<Warehouse> Warehouse { get; set; }
    public DbSet<InventoryWarehouse> InventoryWarehouse { get; set; }

    public PierresShopContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}