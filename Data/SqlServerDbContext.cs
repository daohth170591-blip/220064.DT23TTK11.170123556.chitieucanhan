using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ExpenseManager.Models.Entities;

namespace ExpenseManager.Data
{
    public class SqlServerDbContext : DbContext
    {
        public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<FinancialGoal> FinancialGoals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Lương", Icon = "💰", Color = "#4CAF50" },
                new Category { Id = 2, Name = "Ăn uống", Icon = "🍔", Color = "#FF9800" },
                new Category { Id = 3, Name = "Đi lại", Icon = "🚗", Color = "#2196F3" },
                new Category { Id = 4, Name = "Mua sắm", Icon = "🛒", Color = "#E91E63" },
                new Category { Id = 5, Name = "Giải trí", Icon = "🎮", Color = "#9C27B0" },
                new Category { Id = 6, Name = "Hóa đơn", Icon = "📄", Color = "#F44336" },
                new Category { Id = 7, Name = "Sức khỏe", Icon = "🏥", Color = "#00BCD4" },
                new Category { Id = 8, Name = "Giáo dục", Icon = "📚", Color = "#3F51B5" }
            );
        }
    }

    public class SqlServerDbContextFactory : IDesignTimeDbContextFactory<SqlServerDbContext>
    {
        public SqlServerDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SqlServerDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost;Database=ExpenseManagerDB;Trusted_Connection=True;TrustServerCertificate=True;");
            return new SqlServerDbContext(optionsBuilder.Options);
        }
    }
}
