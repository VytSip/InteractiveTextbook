using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InteractiveTextbook.Data;
using InteractiveTextbook.Models;
using Microsoft.AspNetCore.Mvc;

namespace InteractiveTextbook.Controllers
{
    public class AnalysisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnalysisController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var TextBooks = _context.Textbooks.ToList();
            return View(TextBooks);
        }

        public IActionResult ViewPages(int textbookId)
        {
            var textbookPages = _context.Pages.Where(x => x.TextbookId == textbookId).OrderBy(x => x.PageNumber).ToList();

            return View(textbookPages);
        }

        public IActionResult ViewTimers(int pageId)
        {
            var timers = _context.PageTimers.Where(x => x.PageId == pageId).ToList();

            if (timers.Any())
            {
                var viewModel = new AnalysisViewModel
                {
                    Timers = timers,
                    AverageTime = Average(timers.Select(x => x.TimeElapsed).ToList())
                };

                return View(viewModel);
            }
            return View(new AnalysisViewModel());
        }

        public TimeSpan Average(IEnumerable<TimeSpan> timeSpans)
        {
            IEnumerable<long> ticksPerTimeSpan = timeSpans.Select(t => t.Ticks);
            double averageTicks = ticksPerTimeSpan.Average();
            long averageTicksLong = Convert.ToInt64(averageTicks);

            TimeSpan averageTimeSpan = TimeSpan.FromTicks(averageTicksLong);

            return averageTimeSpan;
        }
    }
}
