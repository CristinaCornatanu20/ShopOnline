using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShopOnline.Controllers
{
    public class HomeAdminController : Controller
    {
        // GET: HomeUserController
        public ActionResult Index()
        {
            return View();
        }

        // GET: HomeUserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeUserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeUserController/Create
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

        // GET: HomeUserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeUserController/Edit/5
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

        // GET: HomeUserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeUserController/Delete/5
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
