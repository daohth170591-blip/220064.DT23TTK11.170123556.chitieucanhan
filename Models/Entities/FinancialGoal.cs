using System.ComponentModel.DataAnnotations;

namespace ExpenseManager.Models.Entities
{
    public class FinancialGoal
    {
        public int Id { get; set; }
        
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        
        [Required]
        public decimal TargetAmount { get; set; }
        
        public decimal CurrentAmount { get; set; }
        
        [Required]
        public DateTime TargetDate { get; set; }
        
        public string ImagePath { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
