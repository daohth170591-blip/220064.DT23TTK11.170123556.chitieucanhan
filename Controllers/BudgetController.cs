using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExpenseManager.Data;
using ExpenseManager.Models.Entities;
using System.Security.Claims;

namespace ExpenseManager.Controllers
{
    [Authorize]
    public class BudgetController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BudgetController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var budgets = await _context.Budgets
                .Include(b => b.Category)
                .Where(b => b.UserId == userId)
                .ToListAsync();

            foreach (var budget in budgets)
            {
                var spent = await _context.Transactions
                    .Where(t => t.UserId == userId && 
                                t.CategoryId == budget.CategoryId && 
                                t.Type == TransactionType.Expense &&
                                t.Date >= budget.StartDate && 
                                t.Date <= budget.EndDate)
                    .SumAsync(t => t.Amount);

                ViewData[$"Spent_{budget.Id}"] = spent;
            }

            return View(budgets);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Budget budget)
        {
            if (ModelState.IsValid)
            {
                budget.UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                _context.Budgets.Add(budget);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View(budget);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var budget = await _context.Budgets.FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId);

            if (budget != null)
            {
                _context.Budgets.Remove(budget);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}
