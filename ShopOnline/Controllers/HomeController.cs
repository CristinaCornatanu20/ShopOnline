using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopOnline.Data;
using ShopOnline.Models;
using ShopOnline.Models.DBObjects;
using ShopOnline.Repository;
using System.Diagnostics;

namespace ShopOnline.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly ProductRepository repository;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context=context;
            repository=new ProductRepository(context);
        }

        public IActionResult Index()
        {
            var products = repository.GetAllProducts();

            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> Search(string q)
        {
            var results = await _context.Products.Where(p => p.Name.Contains(q)).ToListAsync();
            return View("SearchResults", results);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }


            var products = repository.GetAllProducts().FirstOrDefault(p => p.IdProduct == id); ;

           
              products.IdCategoryNavigation = repository.GetCategoryById(products.IdCategory);
                ViewBag.CategoryNameForProduct = products.IdCategoryNavigation.Name;

            
            return View("Details", products);
        }

    }
}