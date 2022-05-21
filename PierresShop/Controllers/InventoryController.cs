using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using PierresShop.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace PierresShop.Controllers
{
  [Authorize]
  public class InventoryController : Controller
  {
    private readonly PierresShopContext _db;
    public InventoryController(PierresShopContext db)
    {
      _db = db;
    }
    [AllowAnonymous]
    public ActionResult Index()
    {
      return View(_db.Inventory.ToList());
    }
    [Authorize]
    public ActionResult Create()
    {
      return View();
    }
    [Authorize]
    [HttpPost]
    public ActionResult Create(Inventory inventory)
    {
      _db.Inventory.Add(inventory);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult Details(int id)
    {
      var thisInventory = _db.Inventory
        .Include(inventory => inventory.JoinEntities)
        .ThenInclude(join => join.Warehouse)
        .FirstOrDefault(inventory => inventory.InventoryId == id);
      return View(thisInventory);
    }
    public ActionResult Edit(int id)
    {
      var thisInventory = _db.Inventory.FirstOrDefault(inventory => inventory.InventoryId == id);
      return View(thisInventory);
    }
    [HttpPost]
    public ActionResult Edit (Inventory inventory)
    {
      _db.Entry(inventory).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult Delete(int id)
    {
      var thisInventory = _db.Inventory.FirstOrDefault(inventory => inventory.InventoryId == id);
      return View(thisInventory);
    }
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisInventory = _db.Inventory.FirstOrDefault(inventory => inventory.InventoryId == id);
      _db.Inventory.Remove(thisInventory);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}