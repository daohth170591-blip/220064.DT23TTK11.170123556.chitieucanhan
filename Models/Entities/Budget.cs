using System.ComponentModel.DataAnnotations;

namespace ExpenseManager.Models.Entities
{
    public class Budget
    {
        public int Id { get; set; }
        
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
        
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        
        [Required]
        public decimal Amount { get; set; }
        
        [Required]
        public DateTime StartDate { get; set; }
        
        [Required]
        public DateTime EndDate { get; set; }
    }
}
