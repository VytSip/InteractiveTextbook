using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InteractiveTextbook.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InteractiveTextbook.Controllers
{
    public class ReadController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReadController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Textbookss
        public async Task<IActionResult> Index()
        {
            return View(await _context.Textbooks.ToListAsync());
        }

        public IActionResult Textbook(int textbookId, int pageNumber)
        {
            var page = _context.Pages.Include(x => x.Textbook).FirstOrDefault(x => x.TextbookId == textbookId && x.PageNumber == pageNumber);
            if (page == null)
            {
                page = _context.Pages.Include(x => x.Textbook).FirstOrDefault(x => x.TextbookId == textbookId);
            }
            return View(page);
        }
    }
}
