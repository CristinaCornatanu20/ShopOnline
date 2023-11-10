using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShopOnline.Controllers
{
    public class TVAController : Controller
    {
        // GET: TVAController
        public ActionResult Index()
        {
            return View();
        }

        // GET: TVAController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TVAController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TVAController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TVAController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TVAController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TVAController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TVAController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
