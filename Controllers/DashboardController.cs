using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExpenseManager.Data;
using ExpenseManager.Models.ViewModels;
using System.Security.Claims;

namespace ExpenseManager.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year;

            var transactions = await _context.Transactions
                .Include(t => t.Category)
                .Where(t => t.UserId == userId && t.Date.Month == currentMonth && t.Date.Year == currentYear)
                .ToListAsync();

            var model = new DashboardViewModel
            {
                TotalIncome = transactions.Where(t => t.Type == Models.Entities.TransactionType.Income).Sum(t => t.Amount),
                TotalExpense = transactions.Where(t => t.Type == Models.Entities.TransactionType.Expense).Sum(t => t.Amount),
                RecentTransactions = await _context.Transactions
                    .Include(t => t.Category)
                    .Where(t => t.UserId == userId)
                    .OrderByDescending(t => t.Date)
                    .Take(10)
                    .ToListAsync(),
                FinancialGoals = await _context.FinancialGoals
                    .Where(g => g.UserId == userId)
                    .ToListAsync(),
                ExpenseByCategory = transactions
                    .Where(t => t.Type == Models.Entities.TransactionType.Expense)
                    .GroupBy(t => t.Category.Name)
                    .ToDictionary(g => g.Key, g => g.Sum(t => t.Amount)),
                Budgets = await _context.Budgets
                    .Include(b => b.Category)
                    .Where(b => b.UserId == userId && b.StartDate <= DateTime.Now && b.EndDate >= DateTime.Now)
                    .ToListAsync()
            };

            model.Balance = model.TotalIncome - model.TotalExpense;

            return View(model);
        }
    }
}
