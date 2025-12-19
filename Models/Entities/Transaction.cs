using System.ComponentModel.DataAnnotations;

namespace ExpenseManager.Models.Entities
{
    public enum TransactionType
    {
        Income,
        Expense
    }

    public class Transaction
    {
        public int Id { get; set; }
        
        [Required]
        public decimal Amount { get; set; }
        
        [Required]
        public TransactionType Type { get; set; }
        
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
        
        [Required]
        public DateTime Date { get; set; }
        
        [StringLength(500)]
        public string Description { get; set; }
        
        public string AttachmentPath { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
