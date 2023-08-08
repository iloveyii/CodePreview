using Accounting.Shared;
using System.ComponentModel.DataAnnotations;

namespace Accounting.Client.Components
{
    public class EarningModel
    {
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public EarningCategory Category { get; set; }
        [Required]
        public decimal Amount { get; set;}
    }
}
