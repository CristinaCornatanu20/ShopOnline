using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopOnline.Data;
using ShopOnline.Models;

namespace ShopOnline.Controllers
{
    public class TvaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly Repository.TvaRepository _repository;
        private readonly Repository.CategoryRepository _cat;
        public TvaController(ApplicationDbContext context)
        {
            _context = context;
            _repository = new Repository.TvaRepository(context);
            _cat = new Repository.CategoryRepository(context);
        }
        // GET: TvaController
        public ActionResult Index()
        {
            var tvas = _repository.GetAllTVAs();
            foreach (var tva in tvas)
            {
                tva.IdCategoryNavigation = _repository.GetCategoryById(tva.IdCategory);
                ViewBag.CategoryNameForProduct = tva.IdCategoryNavigation.Name;
            }
            return View(tvas);
        }

        // GET: TvaController/Details/5
        public ActionResult Details(Guid id)
        {
            var tva = _repository.GetTvaById(id);
            tva.IdCategoryNavigation = _repository.GetCategoryById(tva.IdCategory);
            ViewBag.CategoryNameForProduct = tva.IdCategoryNavigation.Name;
            return View("Details", tva);
        }

        // GET: TvaController/Create
        public ActionResult Create()
        {
            ViewData["IdCategory"] = new SelectList(_cat.GetAllCategories(), "IdCategory", "Name");
            return View("Create");
        }

        // POST: TvaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTva,Tval,IdCategory")] TvaModel model)
        {
            try
            {
                _repository.InsertTva(model);
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View("Create");
            }

            return RedirectToAction(nameof(Index));

        }


        // GET: TvaController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var tva = _repository.GetTvaById(id);
            if (tva == null)
            {
                return NotFound();
            }

            ViewData["IdCategory"] = new SelectList(_context.Categories, "IdCategory", "Name", tva.IdCategory);
            return View("Edit", tva);
        }

        // POST: TvaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, [Bind("IdTva, Tval, IdCategory")] Models.TvaModel model)
        {
            try
            {

                model.IdTva = id;

                _repository.UpdateTva(model);
                return RedirectToAction("Index");


            }
            catch
            {
                return RedirectToAction("Index", id);
            }
            return RedirectToAction(nameof(Index));
        }


        // GET: TvaController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var tva = _repository.GetTvaById(id);
            if (tva == null)
            {
                return NotFound();
            }
            tva.IdCategoryNavigation = _repository.GetCategoryById(tva.IdCategory);
            ViewBag.CategoryNameForProduct = tva.IdCategoryNavigation.Name;
            return View("Delete", tva);
        }

        // POST: TvaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                _repository.DeleteTva(id);
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View("Delete");
            }
        }

    }
}
