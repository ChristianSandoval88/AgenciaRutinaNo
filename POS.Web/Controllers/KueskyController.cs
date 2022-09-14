using Microsoft.AspNetCore.Mvc;
using POS.Models;
using System.Net.Http;
using System.Net.Http.Headers;

namespace POS.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class KueskyController : Controller
{
    private readonly IConfiguration configuration;

    public KueskyController(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    [HttpPost]
    public IActionResult Post([FromBody] KueskyResponse request)
    {
        Response.Headers.Add("Authorization", $"Bearer { configuration["API_SECRET"] }");
        if (request != null)
        {
            var requestStatus = "accept";
            if (request.status != "approved") requestStatus = "ok";

            return Json(new { status = requestStatus });
        }
        return Json(new { status = "ok" });
    }
}
