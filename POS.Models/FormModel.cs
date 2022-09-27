using System.ComponentModel.DataAnnotations;

namespace POS.Models;
public class FormModel
{
    [Display(Name="Nombre:")]
    [Required(ErrorMessage = "Campo requerido")]
    [StringLength(50,ErrorMessage ="El nombre no puede ser mayor a 50 caracteres")]
    public string Name { get; set; }
    [Display(Name = "Apellido:")]
    [Required(ErrorMessage = "Campo requerido")]
    [StringLength(50, ErrorMessage = "El apellido no puede ser mayor a 50 caracteres")]
    public string LastName { get; set; }
    [Display(Name = "Ciudad:")]
    [Required(ErrorMessage = "Campo requerido")]
    [StringLength(50, ErrorMessage = "La ciudad no puede ser mayor a 50 caracteres")]
    public string City { get; set; }
    [Display(Name = "Colonia:")]
    [Required(ErrorMessage = "Campo requerido")]
    [StringLength(50, ErrorMessage = "La colonia no puede ser mayor a 50 caracteres")]
    public string Col { get; set; }
    [Display(Name = "Calle y número:")]
    [Required(ErrorMessage = "Campo requerido")]
    [StringLength(100, ErrorMessage = "La dirección no puede ser mayor a 100 caracteres")]
    public string Address { get; set; }
    [Display(Name = "Estado:")]
    [Required(ErrorMessage = "Campo requerido")]
    [StringLength(50, ErrorMessage = "El nombre del estado no puede ser mayor a 50 caracteres")]
    public string State { get; set; }
    [Display(Name = "C.P:")]
    [Required(ErrorMessage = "Campo requerido")]
    [RegularExpression("^([0-9]{5})$", ErrorMessage = "Valide el código postal.")]
    public string ZipCode { get; set; }
    [Display(Name = "Celular:")]
    [Required(ErrorMessage = "Campo requerido")]
    [RegularExpression("^([0-9]{10})$", ErrorMessage = "Valide el número de celular.")]
    public string Phone { get; set; }
    [Display(Name = "Email:")]
    [Required(ErrorMessage = "Campo requerido")]
    [EmailAddress(ErrorMessage= "Valide el email.")]
    public string Email { get; set; }
}