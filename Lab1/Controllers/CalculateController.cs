using Lab1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab1.Controllers
{
    public class CalculateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
    }
}
