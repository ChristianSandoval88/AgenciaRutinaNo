using Microsoft.AspNetCore.Mvc;
using POS.Models;
using System.Net.Http;
using System.Net.Http.Headers;

namespace POS.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class KueskyController : Controller
{
    private static readonly HttpClient client = new HttpClient();
    [HttpPost]
    public IActionResult Post([FromBody] KueskyResponse request)
    {
        Response.Headers.Add("Authorization", "Bearer ec3c1eb4-e22c-4e86-af87-feac42a78113");
        if (request != null)
        {
            var requestStatus = "accept";
            if (request.status != "approved") requestStatus = "ok";

            return Json(new { status = requestStatus });
        }
        return Json(new { status = "ok" });
    }
}
