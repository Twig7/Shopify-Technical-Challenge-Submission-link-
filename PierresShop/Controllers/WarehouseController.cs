using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using PierresShop.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Warehouse.Controllers
{
  [Authorize]
  public class WarehouseController : Controller
  {
    private readonly PierresShopContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public WarehouseController(UserManager<ApplicationUser> userManager, PierresShopContext db)
    {
      _userManager = userManager;
      _db = db;
    }
    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userItems = _db.Warehouse.Where(entry => entry.User.Id == currentUser.Id).ToList();
      return View(userItems);
    }
    public ActionResult Create()
    {
      ViewBag.InventoryId = new SelectList(_db.Inventory, "InventoryId", "Type");
      return View();
    }
    [HttpPost]
    public async Task<ActionResult> Create(Warehouse warehouse, int InventoryId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      Warehouse.User = currentUser;
      _db.Warehouse.Add(treat);
      _db.SaveChanges();
      if (InventoryId != 0)
      {
        _db.InventoryWarehouse.Add(new InventoryWarehouse() { InventoryId = InventoryId, WarehouseId = warehouse.WarehouseId});
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    [AllowAnonymous]
    public ActionResult Details(int id)
    {
      var thisWarehouse = _db.Warehouse
        .Include(warehouse => warehouse.JoinEntities)
        .ThenInclude(join => join.Inventory)
        .FirstOrDefault(warehouse => warehouse.WarehouseId == id);
      return View(thisWarehouse);
    }
      public ActionResult Edit(int id)
    {
      var thisWarehouse = _db.Warehouse.FirstOrDefault(warehouse => warehouse.WarehouseId == id);
      return View(thisWarehouse);
    }
    [HttpPost]
    public ActionResult Edit(Warehouse warehouse, int InventoryId)
    {
      if(InventoryId != 0)
      {
        _db.InventoryWarehouse.Add(new InventoryWarehouse() { InventoryId = InventoryId, WarehouseId = warehouse.WarehouseId});
      }
      _db.Entry(warehouse).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult Delete(int id)
    {
      var thisWarehouse = _db.Warehouse.FirstOrDefault(warehouse => warehouse.WarehouseId == id);
      return View(thisWarehouse);
    }
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisWarehouse = _db.Warehouse.FirstOrDefault(warehouse => warehouse.WarehouseId == id);
      _db.Warehouse.Remove(thisWarehouse);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    [HttpPost]
    public ActionResult DeleteCategory(int joinId)
    {
      var joinEntry = _db.InventoryWarehouse.FirstOrDefault(entry => entry.InventoryWarehouseId == joinId);
      _db.InventoryWarehouse.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}