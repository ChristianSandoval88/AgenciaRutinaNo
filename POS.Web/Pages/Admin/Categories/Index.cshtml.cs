using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace POS.Web.Pages.Admin.Categories;
public class IndexModel : PageModel
{
    [Authorize]
    public void OnGet()
    {
    }
}
