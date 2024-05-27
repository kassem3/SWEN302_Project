using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleJobPortal.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleJobPortal.Controllers
{
    public class JobsController : Controller
    {
        private readonly JobPortalContext _context;

        public JobsController(JobPortalContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var jobs = await _context.Jobs.ToListAsync();
            return View(jobs);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Job job)
        {
            if (ModelState.IsValid)
            {
                job.PostedDate = DateTime.Now;
                _context.Jobs.Add(job);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(job);
        }

        public async Task<IActionResult> Apply(int id)
        {
            var job = await _context.Jobs.FirstOrDefaultAsync(j => j.JobID == id);
            if (job == null)
            {
                return NotFound();
            }
            return View(new Application { JobID = job.JobID });
        }

        [HttpPost]
        public async Task<IActionResult> Apply(Application application)
        {
            if (ModelState.IsValid)
            {
                application.ApplicationDate = DateTime.Now;
                _context.Applications.Add(application);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(application);
        }
    }
}
