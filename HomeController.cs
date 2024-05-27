using Microsoft.AspNetCore.Mvc;
using SimpleJobPortal.Models;
using System.Linq;

namespace SimpleJobPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly JobPortalContext _context;

        public HomeController(JobPortalContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var jobs = _context.Jobs.ToList();
            return View(jobs);
        }
    }
}
