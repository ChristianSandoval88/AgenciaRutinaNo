using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using POS.Data;
using POS.Data.Repository.IRepository;
using POS.Models;
using System.Net.Http;
using System.Net.Http.Headers;

namespace POS.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class KueskyController : Controller
{
    private readonly IUnitOfWork dbContext;
    private readonly IConfiguration configuration;
    private readonly ILogger logger;

    public KueskyController(IUnitOfWork dbContext, IConfiguration configuration, ILogger<KueskyController> logger)
    {
        this.dbContext = dbContext;
        this.configuration = configuration;
        this.logger = logger;
    }
    [HttpPost]
    public IActionResult Post([FromBody] KueskyResponse response)
    {
        logger.LogInformation("*** LOG ***");
        logger.LogInformation(response.ToString());

        Response.Headers.Add("Authorization", $"Bearer {configuration["API_SECRET"]}");
        if (response != null)
        {
            var purchase = new Purchase() { payment_id = response.payment_id, amount = response.amount, DateTime = DateTime.UtcNow };
            dbContext.Purchase.Add(purchase);
            dbContext.Save();
            var requestStatus = "accept";
            if (response.status != "approved") requestStatus = "ok";

            logger.LogInformation(requestStatus); 
            return Json(new { status = requestStatus });
        }
        return Json(new { status = "ok" });
    }
}
