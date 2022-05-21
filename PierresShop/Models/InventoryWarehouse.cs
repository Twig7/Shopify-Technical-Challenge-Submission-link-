namespace PierresShop.Models
{
  public class InventoryWarehouse
  {
    public int InventoryWarehouseId { get; set; }
    public int WarehouseId { get; set; }
    public virtual Warehouse Warehouse { get; set; }
    public int InventoryId { get; set; }
    public virtual Inventory Inventory { get; set; }
  }
}