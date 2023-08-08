using Accounting.Shared;
using System.ComponentModel.DataAnnotations;

namespace Accounting.Client.Components.Expense
{
    public class ExpenseModel
    {
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public ExpenseCategory Category { get; set; }
        [Required]
        public decimal Amount { get; set; }
    }
}
