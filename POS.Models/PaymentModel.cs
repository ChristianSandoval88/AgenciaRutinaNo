using System.ComponentModel.DataAnnotations;

namespace POS.Models;

public class PaymentModel
{
    [Required(ErrorMessage = "Campo requerido")]
    [Range(1, 5, ErrorMessage = "La cantidad debe ser un valor numerico entre {1} y {2}")]
    public int Qty { get; set; }
    public decimal Price { get; set; }
    public string ProductName { get; set; }
}