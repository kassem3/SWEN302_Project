using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleJobPortal.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleJobPortal.Controllers
{
    public class AdminController : Controller
    {
        private readonly JobPortalContext _context;

        public AdminController(JobPortalContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            var users = await _context.Users.ToListAsync();
            var jobs = await _context.Jobs.ToListAsync();
            var model = new AdminViewModel
            {
                Users = users,
                Jobs = jobs
            };
            return View(model);
        }

        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserID == id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteJob(int id)
        {
            var job = await _context.Jobs.FirstOrDefaultAsync(j => j.JobID == id);
            if (job != null)
            {
                _context.Jobs.Remove(job);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
