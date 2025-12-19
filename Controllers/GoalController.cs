using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExpenseManager.Data;
using ExpenseManager.Models.Entities;
using System.Security.Claims;

namespace ExpenseManager.Controllers
{
    [Authorize]
    public class GoalController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public GoalController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View(await _context.FinancialGoals.Where(g => g.UserId == userId).ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FinancialGoal goal, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                goal.UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                if (image != null)
                {
                    var fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
                    var path = Path.Combine(_env.WebRootPath, "images", "goals", fileName);
                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }
                    goal.ImagePath = "/images/goals/" + fileName;
                }

                _context.FinancialGoals.Add(goal);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(goal);
        }

        [HttpPost]
        public async Task<IActionResult> AddAmount(int id, decimal amount)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var goal = await _context.FinancialGoals.FirstOrDefaultAsync(g => g.Id == id && g.UserId == userId);

            if (goal != null)
            {
                goal.CurrentAmount += amount;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var goal = await _context.FinancialGoals.FirstOrDefaultAsync(g => g.Id == id && g.UserId == userId);

            if (goal != null)
            {
                _context.FinancialGoals.Remove(goal);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}
