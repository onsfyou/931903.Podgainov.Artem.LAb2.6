using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LAb6.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}