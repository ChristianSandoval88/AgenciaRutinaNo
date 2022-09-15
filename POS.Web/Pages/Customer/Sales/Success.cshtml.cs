using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace POS.Web.Pages.Customer.Sales
{
    public class SuccessModel : PageModel
    {
        public IActionResult OnGet()
        {
            TempData["iconoMensaje"] = "success";
            TempData["tituloMensaje"] = "Gracias por su compra";
            TempData["mensaje"] = "La compra se realizo correctamente, gracias!";
            return RedirectToPage("Index");
        }
    }
}
