using System.ComponentModel.DataAnnotations;

namespace ExpenseManager.Models.Entities
{
    public class Category
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        public string Icon { get; set; }
        
        public string Color { get; set; }
        
        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<Budget> Budgets { get; set; }
    }
}
