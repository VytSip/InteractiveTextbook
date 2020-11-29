using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InteractiveTextbook.Data;
using InteractiveTextbook.Data.Entities;
using InteractiveTextbook.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace InteractiveTextbook.Controllers
{
    public class PagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _env;

        public PagesController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Pagess
        public async Task<IActionResult> Index()
        {
            var interactiveTextbookContext = _context.Pages.Include(p => p.Textbook);
            return View(await interactiveTextbookContext.ToListAsync());
        }

        // GET: Pagess/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Pages = await _context.Pages
                .Include(p => p.Textbook)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Pages == null)
            {
                return NotFound();
            }

            return View(Pages);
        }

        // GET: Pagess/Create
        public IActionResult Create()
        {
            ViewData["TextbookId"] = new SelectList(_context.Textbooks, "Id", "Name");
            return View();
        }

        // POST: Pagess/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PageNumber,Text,TextbookId,FormFile")] PageViewModel page)
        {
            if (ModelState.IsValid)
            {
                if (page.FormFile != null)
                {
                    string fileName = Path.GetRandomFileName() + ".wav";
                    string filePath = $"{_env.WebRootPath}/AudioFiles/{fileName}";
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        page.FormFile.CopyTo(stream);
                    }
                    page.AudioPath = fileName;
                }

                var pageToAdd = PageViewModel.ConvertToModel(page);

                _context.Add(pageToAdd);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TextbookId"] = new SelectList(_context.Textbooks, "Id", "Name", page.TextbookId);
            return View(page);
        }

        // GET: Pagess/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Pages = await _context.Pages.FindAsync(id);
            if (Pages == null)
            {
                return NotFound();
            }
            ViewData["TextbookId"] = new SelectList(_context.Textbooks, "Id", "Name", Pages.TextbookId);
            return View(Pages);
        }

        // POST: Pagess/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PageNumber,Text,TextbookId")] Page Pages)
        {
            if (id != Pages.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Pages.AudioPath = _context.Pages.AsNoTracking().FirstOrDefault(x => x.Id == Pages.Id)?.AudioPath; //Remove when audio files edit implemented
                    _context.Update(Pages);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PagesExists(Pages.Id))
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
            ViewData["TextbookId"] = new SelectList(_context.Textbooks, "Id", "Name", Pages.TextbookId);
            return View(Pages);
        }

        // GET: Pagess/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Pages = await _context.Pages
                .Include(p => p.Textbook)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Pages == null)
            {
                return NotFound();
            }

            return View(Pages);
        }

        // POST: Pagess/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Pages = await _context.Pages.FindAsync(id);
            _context.Pages.Remove(Pages);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PagesExists(int id)
        {
            return _context.Pages.Any(e => e.Id == id);
        }
    }
}
