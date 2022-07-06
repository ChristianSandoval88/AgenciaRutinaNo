using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Models;
public class Product
{
    [Key]
    public int Id { get; set; } 

    [Display(Name = "Orden")]
    [Range(0, 20, ErrorMessage = "El orden debe ser un valor númerico entre 0 y 20")]
    public int DisplayOrder { get; set; } = 0;

    [Required(ErrorMessage = "El título es obligatorio")]
    [Display(Name = "Título")]
    public string Name { get; set; }

    [Required(ErrorMessage = "La reseña es obligatoria")]
    [Display(Name = "Reseña")]
    public string ShortDescription { get; set; }

    [Required(ErrorMessage = "La descripción es obligatoria")]
    [Display(Name = "Descripción")]
    public string LongDescription { get; set; }

    [Required(ErrorMessage = "El precio es obligatorio")]
    [Display(Name = "Precio")]
    [Range(0.1,50000, ErrorMessage = "El precio debe estar entre 0 y 50,000")]
    public decimal PriceSell { get; set; }
    [Display(Name = "Descripción")]
    public string? PriceSellDesc { get; set; }
    [Display(Name = "Precio 2")]
    public decimal? PriceSell2 { get; set; }
    [Display(Name = "Descripción")]
    public string? PriceSell2Desc { get; set; }
    [Display(Name = "Precio 3")]
    public decimal? PriceSell3 { get; set; }
    [Display(Name = "Descripción")]
    public string? PriceSell3Desc { get; set; }
    [Display(Name = "Precio 4")]
    public decimal? PriceSell4 { get; set; }
    [Display(Name = "Descripción")]
    public string? PriceSell4Desc { get; set; }

    [Display(Name = "¿Desde?")]
    public bool IsCustomizable { get; set; }

    [Display(Name = "¿Activo?")]
    public bool IsActive { get; set; }

    [Display(Name = "Imagen")]
    public string Image { get; set; }

    [Required(ErrorMessage = "La categoría es obligatoria")]
    [Display(Name = "Categoría")]
    public int CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    public Category Category { get; set; }
}
