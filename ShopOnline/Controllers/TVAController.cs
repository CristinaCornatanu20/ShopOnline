using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopOnline.Data;
using ShopOnline.Models;
using ShopOnline.Models.DBObjects;

namespace ShopOnline.Controllers
{
    public class TVAController1 : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly Repository.TvaRepository _repository;
        private readonly Repository.CategoryRepository _cat;
        public TVAController1(ApplicationDbContext context)
        {
            _context = context;
            _repository=new Repository.TvaRepository(context);
            _cat=new Repository.CategoryRepository(context);
        }

        // GET: TVA
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

        // GET: TVA/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var tva = _repository.GetTvaById(id);
            tva.IdCategoryNavigation = _repository.GetCategoryById(tva.IdCategory);
            ViewBag.CategoryNameForProduct = tva.IdCategoryNavigation.Name;
            return View("Details", tva);
        }

        // GET: TVA/Create
        public ActionResult Create()
        {
            ViewData["IdCategory"] = new SelectList(_cat.GetAllCategories(), "IdCategory", "Name");
            return View("Create");
        }

        // POST: TVA/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: TVA/Edit/5
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

        // POST: TVA/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: TVA/Delete/5
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

        // POST: TVA/Delete/5
        [HttpPost, ActionName("Delete")]
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
