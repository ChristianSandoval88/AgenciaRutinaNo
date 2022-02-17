using System.ComponentModel.DataAnnotations;

namespace POS.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre de la categoría es obligatorio")]
        [Display(Name ="Nombre de la categoría")]
        public string Name { get; set; }
        [Display(Name = "Orden de listado")]
        [Range(1,100,ErrorMessage = "El orden del listado debe ser un número entero entre 1 y 100")]
        public int DisplayOrder { get; set; }
        [Display(Name = "Imágen")]
        public string Image { get; set; }
    }
}