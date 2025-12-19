using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExpenseManager.Data;
using ExpenseManager.Models.Entities;

namespace ExpenseManager.Controllers
{
    public class SeedController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SeedController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Demo()
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == "demo@demo.com");
            if (existingUser != null)
            {
                _context.Transactions.RemoveRange(_context.Transactions.Where(t => t.UserId == existingUser.Id));
                _context.Budgets.RemoveRange(_context.Budgets.Where(b => b.UserId == existingUser.Id));
                _context.FinancialGoals.RemoveRange(_context.FinancialGoals.Where(g => g.UserId == existingUser.Id));
                _context.Users.Remove(existingUser);
                await _context.SaveChangesAsync();
            }

            var user = new User
            {
                FullName = "Nguyễn Văn Demo",
                Email = "demo@demo.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                IsAdmin = false,
                CreatedAt = DateTime.Now
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var userId = user.Id;

            _context.Transactions.AddRange(
                new Transaction { Amount = 15000000, Type = TransactionType.Income, CategoryId = 1, UserId = userId, Date = DateTime.Now.AddDays(-15), Description = "Lương tháng 1", AttachmentPath = "", CreatedAt = DateTime.Now },
                new Transaction { Amount = 2000000, Type = TransactionType.Income, CategoryId = 1, UserId = userId, Date = DateTime.Now.AddDays(-5), Description = "Thưởng dự án", AttachmentPath = "", CreatedAt = DateTime.Now },
                
                new Transaction { Amount = 50000, Type = TransactionType.Expense, CategoryId = 2, UserId = userId, Date = DateTime.Now.AddDays(-14), Description = "Ăn sáng bánh mì", AttachmentPath = "", CreatedAt = DateTime.Now },
                new Transaction { Amount = 85000, Type = TransactionType.Expense, CategoryId = 2, UserId = userId, Date = DateTime.Now.AddDays(-13), Description = "Cơm trưa quán cơm", AttachmentPath = "", CreatedAt = DateTime.Now },
                new Transaction { Amount = 120000, Type = TransactionType.Expense, CategoryId = 2, UserId = userId, Date = DateTime.Now.AddDays(-12), Description = "Ăn tối lẩu", AttachmentPath = "", CreatedAt = DateTime.Now },
                new Transaction { Amount = 45000, Type = TransactionType.Expense, CategoryId = 2, UserId = userId, Date = DateTime.Now.AddDays(-11), Description = "Cafe sáng", AttachmentPath = "", CreatedAt = DateTime.Now },
                new Transaction { Amount = 200000, Type = TransactionType.Expense, CategoryId = 2, UserId = userId, Date = DateTime.Now.AddDays(-10), Description = "Ăn nhà hàng cuối tuần", AttachmentPath = "", CreatedAt = DateTime.Now },
                new Transaction { Amount = 65000, Type = TransactionType.Expense, CategoryId = 2, UserId = userId, Date = DateTime.Now.AddDays(-9), Description = "Phở bò", AttachmentPath = "", CreatedAt = DateTime.Now },
                new Transaction { Amount = 150000, Type = TransactionType.Expense, CategoryId = 2, UserId = userId, Date = DateTime.Now.AddDays(-8), Description = "Buffet trưa", AttachmentPath = "", CreatedAt = DateTime.Now },
                new Transaction { Amount = 35000, Type = TransactionType.Expense, CategoryId = 2, UserId = userId, Date = DateTime.Now.AddDays(-7), Description = "Trà sữa", AttachmentPath = "", CreatedAt = DateTime.Now },
                new Transaction { Amount = 90000, Type = TransactionType.Expense, CategoryId = 2, UserId = userId, Date = DateTime.Now.AddDays(-6), Description = "Cơm gà", AttachmentPath = "", CreatedAt = DateTime.Now },
                new Transaction { Amount = 180000, Type = TransactionType.Expense, CategoryId = 2, UserId = userId, Date = DateTime.Now.AddDays(-5), Description = "Ăn tối với bạn", AttachmentPath = "", CreatedAt = DateTime.Now },
                new Transaction { Amount = 75000, Type = TransactionType.Expense, CategoryId = 2, UserId = userId, Date = DateTime.Now.AddDays(-4), Description = "Bún bò Huế", AttachmentPath = "", CreatedAt = DateTime.Now },
                new Transaction { Amount = 55000, Type = TransactionType.Expense, CategoryId = 2, UserId = userId, Date = DateTime.Now.AddDays(-3), Description = "Bánh mì thịt", AttachmentPath = "", CreatedAt = DateTime.Now },
                
                new Transaction { Amount = 200000, Type = TransactionType.Expense, CategoryId = 3, UserId = userId, Date = DateTime.Now.AddDays(-14), Description = "Đổ xăng xe máy", AttachmentPath = "", CreatedAt = DateTime.Now },
                new Transaction { Amount = 35000, Type = TransactionType.Expense, CategoryId = 3, UserId = userId, Date = DateTime.Now.AddDays(-10), Description = "Grab đi làm", AttachmentPath = "", CreatedAt = DateTime.Now },
                new Transaction { Amount = 250000, Type = TransactionType.Expense, CategoryId = 3, UserId = userId, Date = DateTime.Now.AddDays(-7), Description = "Đổ xăng đầy bình", AttachmentPath = "", CreatedAt = DateTime.Now },
                new Transaction { Amount = 15000, Type = TransactionType.Expense, CategoryId = 3, UserId = userId, Date = DateTime.Now.AddDays(-5), Description = "Gửi xe", AttachmentPath = "", CreatedAt = DateTime.Now },
                
                new Transaction { Amount = 500000, Type = TransactionType.Expense, CategoryId = 4, UserId = userId, Date = DateTime.Now.AddDays(-12), Description = "Mua áo sơ mi", AttachmentPath = "", CreatedAt = DateTime.Now },
                new Transaction { Amount = 350000, Type = TransactionType.Expense, CategoryId = 4, UserId = userId, Date = DateTime.Now.AddDays(-8), Description = "Mua giày thể thao", AttachmentPath = "", CreatedAt = DateTime.Now },
                
                new Transaction { Amount = 150000, Type = TransactionType.Expense, CategoryId = 5, UserId = userId, Date = DateTime.Now.AddDays(-11), Description = "Xem phim rạp", AttachmentPath = "", CreatedAt = DateTime.Now },
                new Transaction { Amount = 300000, Type = TransactionType.Expense, CategoryId = 5, UserId = userId, Date = DateTime.Now.AddDays(-6), Description = "Karaoke với bạn", AttachmentPath = "", CreatedAt = DateTime.Now },
                
                new Transaction { Amount = 800000, Type = TransactionType.Expense, CategoryId = 6, UserId = userId, Date = DateTime.Now.AddDays(-15), Description = "Tiền điện nước tháng", AttachmentPath = "", CreatedAt = DateTime.Now },
                new Transaction { Amount = 1500000, Type = TransactionType.Expense, CategoryId = 6, UserId = userId, Date = DateTime.Now.AddDays(-15), Description = "Tiền thuê nhà", AttachmentPath = "", CreatedAt = DateTime.Now },
                new Transaction { Amount = 200000, Type = TransactionType.Expense, CategoryId = 6, UserId = userId, Date = DateTime.Now.AddDays(-10), Description = "Tiền internet", AttachmentPath = "", CreatedAt = DateTime.Now },
                
                new Transaction { Amount = 300000, Type = TransactionType.Expense, CategoryId = 7, UserId = userId, Date = DateTime.Now.AddDays(-9), Description = "Khám bệnh", AttachmentPath = "", CreatedAt = DateTime.Now },
                new Transaction { Amount = 150000, Type = TransactionType.Expense, CategoryId = 7, UserId = userId, Date = DateTime.Now.AddDays(-4), Description = "Mua thuốc cảm", AttachmentPath = "", CreatedAt = DateTime.Now },
                
                new Transaction { Amount = 250000, Type = TransactionType.Expense, CategoryId = 8, UserId = userId, Date = DateTime.Now.AddDays(-13), Description = "Mua sách lập trình", AttachmentPath = "", CreatedAt = DateTime.Now },
                new Transaction { Amount = 500000, Type = TransactionType.Expense, CategoryId = 8, UserId = userId, Date = DateTime.Now.AddDays(-7), Description = "Khóa học online", AttachmentPath = "", CreatedAt = DateTime.Now }
            );

            _context.Budgets.AddRange(
                new Budget { UserId = userId, CategoryId = 2, Amount = 3000000, StartDate = DateTime.Now.AddDays(-30), EndDate = DateTime.Now.AddDays(30) },
                new Budget { UserId = userId, CategoryId = 3, Amount = 1000000, StartDate = DateTime.Now.AddDays(-30), EndDate = DateTime.Now.AddDays(30) },
                new Budget { UserId = userId, CategoryId = 4, Amount = 2000000, StartDate = DateTime.Now.AddDays(-30), EndDate = DateTime.Now.AddDays(30) },
                new Budget { UserId = userId, CategoryId = 5, Amount = 1000000, StartDate = DateTime.Now.AddDays(-30), EndDate = DateTime.Now.AddDays(30) }
            );

            _context.FinancialGoals.AddRange(
                new FinancialGoal { UserId = userId, Name = "Mua xe máy", TargetAmount = 50000000, CurrentAmount = 15000000, TargetDate = DateTime.Now.AddMonths(6), ImagePath = "", CreatedAt = DateTime.Now },
                new FinancialGoal { UserId = userId, Name = "Du lịch Đà Lạt", TargetAmount = 10000000, CurrentAmount = 3000000, TargetDate = DateTime.Now.AddMonths(3), ImagePath = "", CreatedAt = DateTime.Now },
                new FinancialGoal { UserId = userId, Name = "Quỹ khẩn cấp", TargetAmount = 30000000, CurrentAmount = 8000000, TargetDate = DateTime.Now.AddMonths(12), ImagePath = "", CreatedAt = DateTime.Now }
            );

            await _context.SaveChangesAsync();

            return Content("✅ Đã tạo tài khoản demo thành công!\n\nThông tin đăng nhập:\nEmail: demo@demo.com\nMật khẩu: 123456\n\nDữ liệu đã tạo:\n- 2 giao dịch thu nhập\n- 27 giao dịch chi tiêu\n- 4 ngân sách\n- 3 mục tiêu tài chính");
        }

        public async Task<IActionResult> AddTransactionsForUser()
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == "nhunglth190382@tvu-onschool.edu.vn");
            
            if (user == null)
            {
                user = new User
                {
                    FullName = "Nguyễn Thị Hồng Nhung",
                    Email = "nhunglth190382@tvu-onschool.edu.vn",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                    IsAdmin = false,
                    CreatedAt = DateTime.Now
                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }

            var userId = user.Id;

            _context.Transactions.AddRange(
                new Transaction { Amount = 15000000, Type = TransactionType.Income, CategoryId = 1, UserId = userId, Date = DateTime.Now.AddDays(-10), Description = "Lương tháng 1", AttachmentPath = "", CreatedAt = DateTime.Now },
                new Transaction { Amount = 50000, Type = TransactionType.Expense, CategoryId = 2, UserId = userId, Date = DateTime.Now.AddDays(-9), Description = "Ăn sáng", AttachmentPath = "", CreatedAt = DateTime.Now },
                new Transaction { Amount = 200000, Type = TransactionType.Expense, CategoryId = 3, UserId = userId, Date = DateTime.Now.AddDays(-8), Description = "Xăng xe", AttachmentPath = "", CreatedAt = DateTime.Now },
                new Transaction { Amount = 500000, Type = TransactionType.Expense, CategoryId = 4, UserId = userId, Date = DateTime.Now.AddDays(-7), Description = "Mua quần áo", AttachmentPath = "", CreatedAt = DateTime.Now },
                new Transaction { Amount = 150000, Type = TransactionType.Expense, CategoryId = 5, UserId = userId, Date = DateTime.Now.AddDays(-6), Description = "Xem phim", AttachmentPath = "", CreatedAt = DateTime.Now },
                new Transaction { Amount = 800000, Type = TransactionType.Expense, CategoryId = 6, UserId = userId, Date = DateTime.Now.AddDays(-5), Description = "Tiền điện nước", AttachmentPath = "", CreatedAt = DateTime.Now },
                new Transaction { Amount = 300000, Type = TransactionType.Expense, CategoryId = 7, UserId = userId, Date = DateTime.Now.AddDays(-4), Description = "Khám bệnh", AttachmentPath = "", CreatedAt = DateTime.Now },
                new Transaction { Amount = 1000000, Type = TransactionType.Expense, CategoryId = 8, UserId = userId, Date = DateTime.Now.AddDays(-3), Description = "Học phí", AttachmentPath = "", CreatedAt = DateTime.Now },
                new Transaction { Amount = 80000, Type = TransactionType.Expense, CategoryId = 2, UserId = userId, Date = DateTime.Now.AddDays(-2), Description = "Ăn trưa", AttachmentPath = "", CreatedAt = DateTime.Now },
                new Transaction { Amount = 2000000, Type = TransactionType.Income, CategoryId = 1, UserId = userId, Date = DateTime.Now.AddDays(-1), Description = "Thưởng", AttachmentPath = "", CreatedAt = DateTime.Now }
            );

            await _context.SaveChangesAsync();

            return Content($"✅ Đã tạo tài khoản và thêm 10 giao dịch cho {user.FullName} ({user.Email}) thành công!\n\nThông tin đăng nhập:\nEmail: {user.Email}\nMật khẩu: 123456");
        }
    }
}
