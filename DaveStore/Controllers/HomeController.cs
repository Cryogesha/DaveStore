using DaveStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DaveStore.Data;
using DaveStore.Models.ViewModels;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace DaveStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Products(SearchViewModel searchViewModel)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'Products' is null.");
            }

            var products = from p in _context.Products select p;

            if (!string.IsNullOrEmpty(searchViewModel.SearchQuery))
            {
                products = from p in products where p.Name.Contains(searchViewModel.SearchQuery) select p;
            }

            if (searchViewModel.CategoryId != 0)
            {
                products = from p in products where p.CategoryId == searchViewModel.CategoryId select p;
            }

            return View("Products", await products.ToListAsync());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}