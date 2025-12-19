using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExpenseManager.Data;
using ExpenseManager.Models.Entities;
using System.Security.Claims;

namespace ExpenseManager.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public TransactionController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index(string search, int? categoryId, DateTime? fromDate, DateTime? toDate)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var query = _context.Transactions.Include(t => t.Category).Where(t => t.UserId == userId);

            if (!string.IsNullOrEmpty(search))
                query = query.Where(t => t.Description.Contains(search));

            if (categoryId.HasValue)
                query = query.Where(t => t.CategoryId == categoryId.Value);

            if (fromDate.HasValue)
                query = query.Where(t => t.Date >= fromDate.Value);

            if (toDate.HasValue)
                query = query.Where(t => t.Date <= toDate.Value);

            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View(await query.OrderByDescending(t => t.Date).ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Transaction transaction, IFormFile attachment)
        {
            if (ModelState.IsValid)
            {
                transaction.UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                if (attachment != null)
                {
                    var fileName = Guid.NewGuid() + Path.GetExtension(attachment.FileName);
                    var path = Path.Combine(_env.WebRootPath, "uploads", fileName);
                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await attachment.CopyToAsync(stream);
                    }
                    transaction.AttachmentPath = "/uploads/" + fileName;
                }

                _context.Transactions.Add(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View(transaction);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var transaction = await _context.Transactions.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
            
            if (transaction == null)
                return NotFound();

            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View(transaction);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Transaction transaction, IFormFile attachment)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var existingTransaction = await _context.Transactions.FirstOrDefaultAsync(t => t.Id == transaction.Id && t.UserId == userId);

            if (existingTransaction == null)
                return NotFound();

            existingTransaction.Amount = transaction.Amount;
            existingTransaction.Type = transaction.Type;
            existingTransaction.CategoryId = transaction.CategoryId;
            existingTransaction.Date = transaction.Date;
            existingTransaction.Description = transaction.Description;

            if (attachment != null)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(attachment.FileName);
                var path = Path.Combine(_env.WebRootPath, "uploads", fileName);
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await attachment.CopyToAsync(stream);
                }
                existingTransaction.AttachmentPath = "/uploads/" + fileName;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var transaction = await _context.Transactions.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}
