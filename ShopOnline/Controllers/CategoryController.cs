using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopOnline.Data;
using ShopOnline.Models;
using ShopOnline.Repository;


namespace ShopOnline.Controllers
{
    public class CategoryController : Controller
    {
        //private readonly ApplicationDbContext _context;
        private Repository.CategoryRepository _context;
        private Repository.ProductRepository _productRepository;
        public CategoryController(ApplicationDbContext context, ProductRepository productRepository)
        {
            _context = new Repository.CategoryRepository(context);
            _productRepository = productRepository;
        }

        // GET: Category
        public ActionResult Index()
        {
              var category=_context.GetAllCategories();
                return View("Index",category);
        }
       
        // GET: Category/Details/5
        public ActionResult Details(Guid id)
        {
           

            var category = _context.GetCategoryById(id);
            return View("DetailsCategory", category);
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View("CreateCategory");
        }

        // POST: Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {

              try {
                  Models.CategoryModel model = new Models.CategoryModel();
                  var task = TryUpdateModelAsync(model);
                  task.Wait();
                  if (task.Result)
                  {
                      _context.InsertCategory(model);
                  }
                  return RedirectToAction(nameof(Index));
            }
              catch
              {
                  return View("CreateCategory");
              }
              return RedirectToAction(nameof(Index));
           

        }

        // GET: Category/Edit/5
        public ActionResult Edit(Guid id)
        {
           

            var category =  _context.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View("EditCategory",category);
        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var model = new Models.CategoryModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _context.UpdateCategory(model);
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index", id);
                }

            }
            catch
            {
                return RedirectToAction("Index", id);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Category/Delete/5
        public ActionResult Delete(Guid id)
        {
            

            var category = _context.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }

            return View("DeleteCategory", category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id,IFormCollection collection)
        {
            try
            {
                _context.DeleteCategory(id);
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View("DeleteCategory");
            }
        }

        
    }
}
