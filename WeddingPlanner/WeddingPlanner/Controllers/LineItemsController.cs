using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WeddingPlanner.Data;
using WeddingPlanner.Models;

namespace WeddingPlanner.Controllers
{
    public class LineItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LineItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LineItems
        public IActionResult Index()
        {
            var allLineItems = _context.LineItem.Include(p => p.Category).ToList().Select(li => new LineItem
            {
                LineItemId = li.LineItemId,
                CategoryId = li.CategoryId,
                Category = li.Category,
                Date = li.Date,
                Notes = li.Notes,
                Cost = li.Cost,
            });

            return View(allLineItems);
        }

        // GET: LineItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lineItem = await _context.LineItem
                .Include(l => l.Category)
                .FirstOrDefaultAsync(m => m.LineItemId == id);
            if (lineItem == null)
            {
                return NotFound();
            }

            return View(lineItem);
        }

        // GET: LineItems/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryName");
            return View();
        }

        // POST: LineItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LineItemId,CategoryId,Date,Notes,Cost")] LineItem lineItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lineItem);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Categories");
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryName", lineItem.CategoryId);
            return View(lineItem);
        }

        // GET: LineItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lineItem = await _context.LineItem.FindAsync(id);
            if (lineItem == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryName", lineItem.CategoryId);
            return View(lineItem);
        }

        // POST: LineItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LineItemId,CategoryId,Date,Notes,Cost")] LineItem lineItem)
        {
            if (id != lineItem.LineItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lineItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LineItemExists(lineItem.LineItemId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Categories");
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryName", lineItem.CategoryId);
            return View(lineItem);
        }

        // GET: LineItems/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lineItem = _context.LineItem.Include(p => p.Category).ToList().Select(li => new LineItem
            {
                LineItemId = li.LineItemId,
                CategoryId = li.CategoryId,
                Category = li.Category,
                Date = li.Date,
                Notes = li.Notes,
                Cost = li.Cost,
            })
                .FirstOrDefault(m => m.LineItemId == id);

            if (lineItem == null)
            {
                return NotFound();
            }

            return View(lineItem);
        }

        // POST: LineItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lineItem = await _context.LineItem.FindAsync(id);
            _context.LineItem.Remove(lineItem);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Categories");
        }

        private bool LineItemExists(int id)
        {
            return _context.LineItem.Any(e => e.LineItemId == id);
        }
    }
}
