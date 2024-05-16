
using Data.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Doorang.Controllers
{
    public class HomeController : Controller
    {
        AppDbContext _dbContext;

        public HomeController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var cards = _dbContext.Cards.ToList();
            return View(cards);
        }

      
    }
}
