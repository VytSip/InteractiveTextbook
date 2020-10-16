using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InteractiveTextbook.Data;
using InteractiveTextbook.Data.Entities;

namespace InteractiveTextbooks.Controllers
{
    public class TextbooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TextbooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Textbookss
        public async Task<IActionResult> Index()
        {
            return View(await _context.Textbooks.ToListAsync());
        }

        // GET: Textbookss/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Textbooks = await _context.Textbooks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Textbooks == null)
            {
                return NotFound();
            }

            return View(Textbooks);
        }

        // GET: Textbookss/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Textbookss/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Textbook Textbooks)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Textbooks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Textbooks);
        }

        // GET: Textbookss/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Textbooks = await _context.Textbooks.FindAsync(id);
            if (Textbooks == null)
            {
                return NotFound();
            }
            return View(Textbooks);
        }

        // POST: Textbookss/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Textbook Textbooks)
        {
            if (id != Textbooks.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Textbooks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TextbooksExists(Textbooks.Id))
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
            return View(Textbooks);
        }

        // GET: Textbookss/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Textbooks = await _context.Textbooks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Textbooks == null)
            {
                return NotFound();
            }

            return View(Textbooks);
        }

        // POST: Textbookss/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Textbooks = await _context.Textbooks.FindAsync(id);
            _context.Textbooks.Remove(Textbooks);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TextbooksExists(int id)
        {
            return _context.Textbooks.Any(e => e.Id == id);
        }
    }
}
