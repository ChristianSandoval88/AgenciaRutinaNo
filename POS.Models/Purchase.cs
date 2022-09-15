using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string payment_id { get; set; }=String.Empty;
        [Column(TypeName = "decimal(10,2)")]
        public decimal amount { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}