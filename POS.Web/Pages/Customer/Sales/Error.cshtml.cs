using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace POS.Web.Pages.Customer.Sales
{
    public class ErrorModel : PageModel
    {
        public IActionResult OnGet()
        {
            TempData["iconoMensaje"] = "error";
            TempData["tituloMensaje"] = "NO se realizo la compra";
            TempData["mensaje"] = "La compra NO se pudo realizar! Intente mas tarde.";
            return RedirectToPage("Index");
        }
    }
}
