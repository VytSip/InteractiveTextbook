using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InteractiveTextbook.Data;
using InteractiveTextbook.Data.Entities;
using InteractiveTextbook.Singleton;
using InteractiveTextbook.Tools;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NAudio.Wave;

namespace InteractiveTextbook.Controllers
{
    public class ReadController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _env;
        private TimerTracker Timer = TimerTracker.GetInstance();
        private MusicPlayer MusicPlayer = MusicPlayer.GetInstance();

        [ThreadStatic]
        private static bool Lock = false;

        public ReadController(ApplicationDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Textbookss
        public async Task<IActionResult> Index()
        {
            return View(await _context.Textbooks.ToListAsync());
        }

        public IActionResult Textbook(int textbookId, int pageNumber)
        {
            Timer.StartedReading = DateTime.Now;
            var page = _context.Pages.Include(x => x.Textbook).FirstOrDefault(x => x.TextbookId == textbookId && x.PageNumber == pageNumber);
            if (page == null)
            {
                page = _context.Pages.Include(x => x.Textbook).FirstOrDefault(x => x.TextbookId == textbookId);
            }
            
            return View(page);
        }

        public void StopTimer(int id)
        {
            MusicPlayer.KeepPlaying = false;
            var timeElapsed = DateTime.Now.Subtract(Timer.StartedReading);

            var timerObject = new PageTimer
            {
                PageId = id,
                TimeElapsed = timeElapsed,
                RecordCreatedOn = DateTime.Now
            };
            _context.Add(timerObject);
            _context.SaveChanges();
        }

        public void PlayMusic(int id)
        {
            var apth = _env.WebRootPath;
            MusicPlayer.KeepPlaying = false;
            Lock = false;

            string fileToPlay = _context.Pages.FirstOrDefault(x => x.Id == id).AudioPath;

            if (!string.IsNullOrEmpty(fileToPlay))
            {
                using (var waveOut = new WaveOutEvent())
                using (var wavReader = new WaveFileReader($"{_env.WebRootPath}/AudioFiles/{fileToPlay}"))
                {
                    Thread.Sleep(100);
                    MusicPlayer.KeepPlaying = true;
                    waveOut.Init(wavReader);
                    waveOut.Play();
                    while (MusicPlayer.KeepPlaying)
                    {
                        Thread.Sleep(100);
                    }
                }
            }
        }
    }
}
