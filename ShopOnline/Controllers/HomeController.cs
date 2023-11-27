using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopOnline.Data;
using ShopOnline.Data.ViewModels;
using ShopOnline.Models;
using ShopOnline.Models.DBObjects;
using ShopOnline.Repository;
using System.Collections.Generic;
using System.Diagnostics;
using static java.util.Locale;

namespace ShopOnline.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly Repository.ProductRepository repository;
        private readonly Repository.CategoryRepository _categoryRepository;
        private readonly Repository.OrdersService _orderRepository;

        public HomeController(ILogger<HomeController> logger,
            ApplicationDbContext context
           )
        {
            _logger = logger;
            _context = context;
            repository = new ProductRepository(context);
            _categoryRepository = new CategoryRepository(context);
            _orderRepository = new OrdersService(context);
        }

        public IActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("AdminIndex");
            }

            var products = repository.GetAllProducts();
            var categories = _categoryRepository.GetAllCategories();
            ViewBag.Categories = new SelectList(categories, "IdCategory", "Name");


            return View(products);
        }
        
        public IActionResult Categories()
        {
            var category = _categoryRepository.GetAllCategories();
            return View( category);
        }
  
        public IActionResult ProductsByCategory(Guid categoryId)
        {
            var cat = categoryId;
            var products = repository.GetProductByIDCategory(cat);

            return View(products);
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult AdminIndex()
        {
            var products = repository.GetAllProducts();
            return View("AdminIndex", products);
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


        public async Task<IActionResult> UsersOrders()
        {
            // Obține datele comenzii și detalii comandă
            List < Order> order = _orderRepository.GetAllOrders(); ; // Obține comanda
            List<OrderDetail> orderDetails = _orderRepository.GetAllOrdersDetail(); // Obține detalii comandă

            // Construiește modelul
            var orderViewModel = new OrderViewModel
            {
                Order = order,
                OrderDetails = orderDetails
            };

            return View(orderViewModel);
        }

    }
}