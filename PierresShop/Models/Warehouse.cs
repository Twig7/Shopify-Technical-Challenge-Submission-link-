using System.Collections.Generic;

namespace PierresShop.Models
{
  public class Warehouse 
  {
    public Warehouse()
    {
      this.JoinEntities = new HashSet<InventoryWarehouse>();
    }

    public int WarehouseId { get; set; }
    public string Name { get; set; }
    public virtual ApplicationUser User { get; set; }
    public virtual ICollection<InventoryWarehouse> JoinEntities { get; }
  }
}