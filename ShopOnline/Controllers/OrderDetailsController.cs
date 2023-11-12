using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopOnline.Data;
using ShopOnline.Models.DBObjects;

namespace ShopOnline.Controllers
{
    public class OrderDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OrderDetails
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.OrderDetails.Include(o => o.IdOrderNavigation).Include(o => o.IdProductNavigation).Include(o => o.IdTvaNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: OrderDetails/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.OrderDetails == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetails
                .Include(o => o.IdOrderNavigation)
                .Include(o => o.IdProductNavigation)
                .Include(o => o.IdTvaNavigation)
                .FirstOrDefaultAsync(m => m.IdOrderDetails == id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }

        // GET: OrderDetails/Create
        public IActionResult Create()
        {
            ViewData["IdOrder"] = new SelectList(_context.Orders, "IdOrder", "IdOrder");
            ViewData["IdProduct"] = new SelectList(_context.Products, "IdProduct", "IdProduct");
            ViewData["IdTva"] = new SelectList(_context.Tvas, "IdTva", "IdTva");
            return View();
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdOrderDetails,IdOrder,IdProduct,IdTva,PriceTva,Quantity,PriceTotal")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                orderDetail.IdOrderDetails = Guid.NewGuid();
                _context.Add(orderDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdOrder"] = new SelectList(_context.Orders, "IdOrder", "IdOrder", orderDetail.IdOrder);
            ViewData["IdProduct"] = new SelectList(_context.Products, "IdProduct", "IdProduct", orderDetail.IdProduct);
            ViewData["IdTva"] = new SelectList(_context.Tvas, "IdTva", "IdTva", orderDetail.IdTva);
            return View(orderDetail);
        }

        // GET: OrderDetails/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.OrderDetails == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetails.FindAsync(id);
            if (orderDetail == null)
            {
                return NotFound();
            }
            ViewData["IdOrder"] = new SelectList(_context.Orders, "IdOrder", "IdOrder", orderDetail.IdOrder);
            ViewData["IdProduct"] = new SelectList(_context.Products, "IdProduct", "IdProduct", orderDetail.IdProduct);
            ViewData["IdTva"] = new SelectList(_context.Tvas, "IdTva", "IdTva", orderDetail.IdTva);
            return View(orderDetail);
        }

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("IdOrderDetails,IdOrder,IdProduct,IdTva,PriceTva,Quantity,PriceTotal")] OrderDetail orderDetail)
        {
            if (id != orderDetail.IdOrderDetails)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderDetailExists(orderDetail.IdOrderDetails))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdOrder"] = new SelectList(_context.Orders, "IdOrder", "IdOrder", orderDetail.IdOrder);
            ViewData["IdProduct"] = new SelectList(_context.Products, "IdProduct", "IdProduct", orderDetail.IdProduct);
            ViewData["IdTva"] = new SelectList(_context.Tvas, "IdTva", "IdTva", orderDetail.IdTva);
            return View(orderDetail);
        }

        // GET: OrderDetails/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.OrderDetails == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetails
                .Include(o => o.IdOrderNavigation)
                .Include(o => o.IdProductNavigation)
                .Include(o => o.IdTvaNavigation)
                .FirstOrDefaultAsync(m => m.IdOrderDetails == id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.OrderDetails == null)
            {
                return Problem("Entity set 'ApplicationDbContext.OrderDetails'  is null.");
            }
            var orderDetail = await _context.OrderDetails.FindAsync(id);
            if (orderDetail != null)
            {
                _context.OrderDetails.Remove(orderDetail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderDetailExists(Guid id)
        {
          return (_context.OrderDetails?.Any(e => e.IdOrderDetails == id)).GetValueOrDefault();
        }
    }
}
