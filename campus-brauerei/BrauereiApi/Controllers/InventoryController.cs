using Microsoft.AspNetCore.Mvc;

namespace BrauereiApi.Controllers;

public class InventoryController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}