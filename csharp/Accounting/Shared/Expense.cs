using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Shared
{
    public class Expense
    {
        public Expense()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Subject { get; set; }
        public ExpenseCategory Category { get; set; }
        public decimal Amount { get; set; }
        public string? UserName { get; set; }
    }
}
