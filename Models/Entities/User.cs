using System.ComponentModel.DataAnnotations;

namespace ExpenseManager.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string FullName { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        public string PasswordHash { get; set; }
        
        public bool IsAdmin { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<Budget> Budgets { get; set; }
        public ICollection<FinancialGoal> FinancialGoals { get; set; }
    }
}
