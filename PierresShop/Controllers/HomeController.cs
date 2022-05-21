using Microsoft.AspNetCore.Mvc;

namespace PierresShop.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }
  }
}