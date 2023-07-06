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
    public class ProductDiscountsController : Controller
    {
        private readonly ShopContext _context;

        public ProductDiscountsController(ShopContext context)
        {
            _context = context;
        }

        // GET: ProductDiscounts
        public async Task<IActionResult> Index()
        {
            var shopContext = _context.ProductDiscount.Include(p => p.Discount).Include(p => p.Product);
            return View(await shopContext.ToListAsync());
        }

        // GET: ProductDiscounts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.ProductDiscount == null)
            {
                return NotFound();
            }

            var productDiscount = await _context.ProductDiscount
                .Include(p => p.Discount)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productDiscount == null)
            {
                return NotFound();
            }

            return View(productDiscount);
        }

        // GET: ProductDiscounts/Create
        public IActionResult Create()
        {
            ViewData["DiscountId"] = new SelectList(_context.Discount, "Id", "Name");
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Name");
            return View();
        }

        // POST: ProductDiscounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,DiscountId")] ProductDiscount productDiscount)
        {
            if (ModelState.IsValid)
            {
                productDiscount.Id = Guid.NewGuid();
                _context.Add(productDiscount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DiscountId"] = new SelectList(_context.Discount, "Id", "Name", productDiscount.DiscountId);
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Name", productDiscount.ProductId);
            return View(productDiscount);
        }

        // GET: ProductDiscounts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.ProductDiscount == null)
            {
                return NotFound();
            }

            var productDiscount = await _context.ProductDiscount.FindAsync(id);
            if (productDiscount == null)
            {
                return NotFound();
            }
            ViewData["DiscountId"] = new SelectList(_context.Discount, "Id", "Name", productDiscount.DiscountId);
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Name", productDiscount.ProductId);
            return View(productDiscount);
        }

        // POST: ProductDiscounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ProductId,DiscountId")] ProductDiscount productDiscount)
        {
            if (id != productDiscount.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productDiscount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductDiscountExists(productDiscount.Id))
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
            ViewData["DiscountId"] = new SelectList(_context.Discount, "Id", "Name", productDiscount.DiscountId);
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Name", productDiscount.ProductId);
            return View(productDiscount);
        }

        // GET: ProductDiscounts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.ProductDiscount == null)
            {
                return NotFound();
            }

            var productDiscount = await _context.ProductDiscount
                .Include(p => p.Discount)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productDiscount == null)
            {
                return NotFound();
            }

            return View(productDiscount);
        }

        // POST: ProductDiscounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.ProductDiscount == null)
            {
                return Problem("Entity set 'ShopContext.ProductDiscount'  is null.");
            }
            var productDiscount = await _context.ProductDiscount.FindAsync(id);
            if (productDiscount != null)
            {
                _context.ProductDiscount.Remove(productDiscount);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductDiscountExists(Guid id)
        {
          return (_context.ProductDiscount?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
