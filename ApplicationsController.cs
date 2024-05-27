using Microsoft.AspNetCore.Mvc;
using SimpleJobPortal.Models;
using System.Linq;

namespace SimpleJobPortal.Controllers
{
    public class ApplicationsController : Controller
    {
        private readonly JobPortalContext _context;

        public ApplicationsController(JobPortalContext context)
        {
            _context = context;
        }

        public IActionResult Apply(int jobId)
        {
            var application = new Application { JobID = jobId };
            return View(application);
        }

        [HttpPost]
        public IActionResult Apply(Application application)
        {
            if (ModelState.IsValid)
            {
                application.JobSeeker = HttpContext.Session.GetString("Username");
                application.ApplicationDate = DateTime.Now;
                _context.Applications.Add(application);
                _context.SaveChanges();
                return RedirectToAction("Index", "Jobs");
            }
            return View(application);
        }

        public IActionResult List()
        {
            var username = HttpContext.Session.GetString("Username");
            var role = HttpContext.Session.GetString("Role");

            if (role == "Employer")
            {
                var applications = _context.Applications
                    .Where(a => _context.Jobs.Any(j => j.JobID == a.JobID && j.Employer == username))
                    .ToList();
                return View(applications);
            }
            else if (role == "JobSeeker")
            {
                var applications = _context.Applications
                    .Where(a => a.JobSeeker == username)
                    .ToList();
                return View(applications);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
