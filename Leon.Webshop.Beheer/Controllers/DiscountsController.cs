using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Leon.Webshop.Contracts.Models;
using Leon.Webshop.Data;

namespace Leon.Webshop.Beheer.Controllers
{
    public class DiscountsController : Controller
    {
        private readonly ShopContext _context;

        public DiscountsController(ShopContext context)
        {
            _context = context;
        }

        // GET: Discounts
        public async Task<IActionResult> Index()
        {
              return _context.Discount != null ? 
                          View(await _context.Discount.ToListAsync()) :
                          Problem("Entity set 'ShopContext.Discount'  is null.");
        }

        // GET: Discounts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Discount == null)
            {
                return NotFound();
            }

            var discount = await _context.Discount
                .FirstOrDefaultAsync(m => m.Id == id);
            if (discount == null)
            {
                return NotFound();
            }

            return View(discount);
        }

        // GET: Discounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Discounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Percentage,Amount")] Discount discount)
        {
            if (ModelState.IsValid)
            {
                discount.Id = Guid.NewGuid();
                _context.Add(discount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(discount);
        }

        // GET: Discounts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Discount == null)
            {
                return NotFound();
            }

            var discount = await _context.Discount.FindAsync(id);
            if (discount == null)
            {
                return NotFound();
            }
            return View(discount);
        }

        // POST: Discounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Percentage,Amount")] Discount discount)
        {
            if (id != discount.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(discount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscountExists(discount.Id))
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
            return View(discount);
        }

        // GET: Discounts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Discount == null)
            {
                return NotFound();
            }

            var discount = await _context.Discount
                .FirstOrDefaultAsync(m => m.Id == id);
            if (discount == null)
            {
                return NotFound();
            }

            return View(discount);
        }

        // POST: Discounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Discount == null)
            {
                return Problem("Entity set 'ShopContext.Discount'  is null.");
            }
            var discount = await _context.Discount.FindAsync(id);
            if (discount != null)
            {
                _context.Discount.Remove(discount);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiscountExists(Guid id)
        {
          return (_context.Discount?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
