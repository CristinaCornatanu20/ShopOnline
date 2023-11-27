using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopOnline.Data;
using System.IO;
//using ShopOnline.Models.DBObjects;
using Microsoft.Web.Helpers;
using Newtonsoft.Json;
using javax.tools;
using java.awt.image;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using ShopOnline.Models;
using com.sun.org.apache.bcel.@internal.generic;
using ShopOnline.Models.DBObjects;
using System.Text;
using com.sun.security.ntlm;
using com.sun.corba.se.impl.orbutil.concurrent;
using Microsoft.AspNetCore.Authorization;

namespace ShopOnline.Controllers
{
    [Authorize(Roles = "Admin")]

    public class ProductController : Controller
    {
       // private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        private Repository.ProductRepository _context;
        private Repository.CategoryRepository _cat;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private ApplicationDbContext context;


        public ProductController(ApplicationDbContext context1,IHostingEnvironment hostingEnvironment, IWebHostEnvironment webHostEnvironment, ILogger<ProductController> logger)
        {
            _context = new Repository.ProductRepository(context1);
            _cat = new Repository.CategoryRepository(context1);
            _hostingEnvironment = hostingEnvironment;
            _webHostEnvironment = webHostEnvironment;
            context = context1;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }

        // GET: Product
        public ActionResult Index()
        {
            var products = _context.GetAllProducts();

            foreach (var product in products)
            {
                product.IdCategoryNavigation = _context.GetCategoryById(product.IdCategory);
                ViewBag.CategoryNameForProduct = product.IdCategoryNavigation.Name;
                
            }


            return View(products);
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null || context.Products == null)
            {
                return NotFound();
            }


            var product = _context.GetProductById(id);
            
            return View("Details",product);
        }
        
        // GET: Product/Create
        public ActionResult Create()
        {
             ViewData["IdCategory"] = new SelectList(_cat.GetAllCategories(), "IdCategory","Name");

            ViewData["Image"] = new SelectList(_cat.GetAllCategories(), "IdCategory", "Name");
            return View("Create");
            
        }

        ILogger<ProductController> _logger;
        // POST: Product/Create Cu repository
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
         public async Task<IActionResult> Create([Bind("IdProduct,Name,Description,IdCategory,Price,StockQuantity,Image")] ProductModel model, IFormFile image)
         {

                 try
                 {
                    // Models.ProductModel model = new Models.ProductModel();
                 if (image != null && image.Length > 0)
                 {
                     var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, "Products", image.FileName);

                     using (var stream = new FileStream(imagePath, FileMode.Create))
                     {
                         await image.CopyToAsync(stream);
                     }

                     model.Image = "/Products/" + image.FileName; 
                 }

                     _context.InsertProduct(model);
                    return RedirectToAction(nameof(Index));

             }
                 catch 
                 {
                     ModelState.AddModelError(string.Empty, "Error saving the product. Please try again.");
                     return View("Create"); 

                  }

             return RedirectToAction(nameof(Index));
         }
        
        
        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var product = _context.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["IdCategory"] = new SelectList(context.Categories, "IdCategory", "Name", product.IdCategory);
            return View("Edit",product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("IdProduct,Name,Description,IdCategory,Price,StockQuantity,Image")] ProductModel product, IFormFile image)
        {
            if (id != product.IdProduct)
            {
                return NotFound();
            }
            
            ViewData["IdCategory"] = new SelectList(context.Categories, "IdCategory", "IdCategory", product.IdCategory);
            return View(product);
        }
        
        
        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = _context.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View("Delete", product);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                var product = _context.GetProductById(id);
                _context.DeleteProduct(id,product.Image);
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View("Delete");
            }
        }

        
        
    }
}
