using System.Collections.Generic;

namespace PierresShop.Models
{
  public class Inventory
  {
    public Inventory()
  {
    this.JoinEntities = new HashSet<InventoryWarehouse>();
  }
  
  public int InventoryId { get; set; }
  public string Type { get; set; }
  public int Price { get; set; }
  public virtual ICollection<InventoryWarehouse> JoinEntities { get; set; }
  }
}