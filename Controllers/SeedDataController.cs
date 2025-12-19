using Microsoft.AspNetCore.Mvc;
using ExpenseManager.Data;
using ExpenseManager.Models.Entities;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManager.Controllers
{
    public class SeedDataController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SeedDataController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult CreateData()
        {
            _context.Database.EnsureCreated();
            
            var existingUser = _context.Users.FirstOrDefault(u => u.Email == "daohth170591@sv-onuni.edu.vn");
            
            if (existingUser != null)
            {
                var existingTrans = _context.Transactions.Where(t => t.UserId == existingUser.Id).ToList();
                var existingBudgets = _context.Budgets.Where(b => b.UserId == existingUser.Id).ToList();
                var existingGoals = _context.FinancialGoals.Where(g => g.UserId == existingUser.Id).ToList();
                
                _context.Transactions.RemoveRange(existingTrans);
                _context.Budgets.RemoveRange(existingBudgets);
                _context.FinancialGoals.RemoveRange(existingGoals);
                _context.Users.Remove(existingUser);
                _context.SaveChanges();
            }

            var user = new User
            {
                FullName = "Huỳnh Thị Hồng Đào",
                Email = "daohth170591@sv-onuni.edu.vn",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("daohth170591"),
                IsAdmin = false,
                CreatedAt = DateTime.Now
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            var transactions = new List<Transaction>
            {
                new Transaction { Amount = 50000, Type = TransactionType.Expense, CategoryId = 2, UserId = user.Id, Date = new DateTime(2025, 12, 1), Description = "Ăn sáng", AttachmentPath = "", CreatedAt = DateTime.Now },
                new Transaction { Amount = 200000, Type = TransactionType.Expense, CategoryId = 3, UserId = user.Id, Date = new DateTime(2025, 12, 2), Description = "Đổ xăng xe", AttachmentPath = "", CreatedAt = DateTime.Now },
                new Transaction { Amount = 150000, Type = TransactionType.Expense, CategoryId = 2, UserId = user.Id, Date = new DateTime(2025, 12, 3), Description = "Ăn trưa với bạn", AttachmentPath = "", CreatedAt = DateTime.Now },
                new Transaction { Amount = 300000, Type = TransactionType.Expense, CategoryId = 4, UserId = user.Id, Date = new DateTime(2025, 12, 5), Description = "Mua quần áo", AttachmentPath = "", CreatedAt = DateTime.Now },
                new Transaction { Amount = 80000, Type = TransactionType.Expense, CategoryId = 2, UserId = user.Id, Date = new DateTime(2025, 12, 7), Description = "Ăn tối", AttachmentPath = "", CreatedAt = DateTime.Now },
                new Transaction { Amount = 500000, Type = TransactionType.Expense, CategoryId = 6, UserId = user.Id, Date = new DateTime(2025, 12, 10), Description = "Tiền điện tháng 12", AttachmentPath = "", CreatedAt = DateTime.Now },
                new Transaction { Amount = 120000, Type = TransactionType.Expense, CategoryId = 5, UserId = user.Id, Date = new DateTime(2025, 12, 12), Description = "Xem phim", AttachmentPath = "", CreatedAt = DateTime.Now },
                new Transaction { Amount = 250000, Type = TransactionType.Expense, CategoryId = 4, UserId = user.Id, Date = new DateTime(2025, 12, 14), Description = "Mua mỹ phẩm", AttachmentPath = "", CreatedAt = DateTime.Now },
                new Transaction { Amount = 100000, Type = TransactionType.Expense, CategoryId = 2, UserId = user.Id, Date = new DateTime(2025, 12, 16), Description = "Ăn uống cuối tuần", AttachmentPath = "", CreatedAt = DateTime.Now },
                new Transaction { Amount = 180000, Type = TransactionType.Expense, CategoryId = 3, UserId = user.Id, Date = new DateTime(2025, 12, 18), Description = "Grab đi làm", AttachmentPath = "", CreatedAt = DateTime.Now },
                new Transaction { Amount = 15000000, Type = TransactionType.Income, CategoryId = 1, UserId = user.Id, Date = new DateTime(2025, 12, 1), Description = "Lương tháng 12", AttachmentPath = "", CreatedAt = DateTime.Now }
            };

            _context.Transactions.AddRange(transactions);
            _context.SaveChanges();

            var budgets = new List<Budget>
            {
                new Budget { UserId = user.Id, CategoryId = 2, Amount = 3000000, StartDate = new DateTime(2025, 12, 1), EndDate = new DateTime(2025, 12, 31) },
                new Budget { UserId = user.Id, CategoryId = 3, Amount = 1500000, StartDate = new DateTime(2025, 12, 1), EndDate = new DateTime(2025, 12, 31) },
                new Budget { UserId = user.Id, CategoryId = 4, Amount = 2000000, StartDate = new DateTime(2025, 12, 1), EndDate = new DateTime(2025, 12, 31) },
                new Budget { UserId = user.Id, CategoryId = 5, Amount = 1000000, StartDate = new DateTime(2025, 12, 1), EndDate = new DateTime(2025, 12, 31) }
            };

            _context.Budgets.AddRange(budgets);
            _context.SaveChanges();

            var goals = new List<FinancialGoal>
            {
                new FinancialGoal { UserId = user.Id, Name = "Mua xe máy", TargetAmount = 50000000, CurrentAmount = 10000000, TargetDate = new DateTime(2026, 6, 30), ImagePath = "", CreatedAt = DateTime.Now },
                new FinancialGoal { UserId = user.Id, Name = "Du lịch Nhật Bản", TargetAmount = 30000000, CurrentAmount = 5000000, TargetDate = new DateTime(2026, 12, 31), ImagePath = "", CreatedAt = DateTime.Now },
                new FinancialGoal { UserId = user.Id, Name = "Tiết kiệm khẩn cấp", TargetAmount = 20000000, CurrentAmount = 8000000, TargetDate = new DateTime(2026, 3, 31), ImagePath = "", CreatedAt = DateTime.Now }
            };

            _context.FinancialGoals.AddRange(goals);
            _context.SaveChanges();

            return Content($"Đã tạo tài khoản và {transactions.Count} giao dịch, {budgets.Count} ngân sách, {goals.Count} mục tiêu thành công!\n\nTài khoản: daohth170591\nMật khẩu: daohth170591\nEmail: daohth170591@sv-onuni.edu.vn\n\nUser ID: {user.Id}");
        }

        public IActionResult CheckData()
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == "daohth170591@sv-onuni.edu.vn");
            if (user == null)
                return Content("Không tìm thấy tài khoản!");

            var transactions = _context.Transactions.Include(t => t.Category).Where(t => t.UserId == user.Id).ToList();
            var result = $"User ID: {user.Id}\nEmail: {user.Email}\nTên: {user.FullName}\n\nSố giao dịch: {transactions.Count}\n\n";
            
            foreach (var t in transactions)
            {
                result += $"- {t.Date:dd/MM/yyyy}: {t.Description} - {t.Amount:N0} VNĐ ({t.Type})\n";
            }

            return Content(result);
        }
    }
}
