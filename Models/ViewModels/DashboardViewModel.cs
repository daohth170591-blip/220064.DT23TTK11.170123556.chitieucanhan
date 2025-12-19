using ExpenseManager.Models.Entities;

namespace ExpenseManager.Models.ViewModels
{
    public class DashboardViewModel
    {
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal Balance { get; set; }
        public List<Transaction> RecentTransactions { get; set; }
        public List<FinancialGoal> FinancialGoals { get; set; }
        public Dictionary<string, decimal> ExpenseByCategory { get; set; }
        public List<Budget> Budgets { get; set; }
    }
}
