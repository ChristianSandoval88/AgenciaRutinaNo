using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Data.Repository.IRepository;

namespace POS.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CategoriesController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoriesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    [HttpGet]
    public IActionResult Get()
    {
        var categories = _unitOfWork.Category.GetAll();
        return Json(new { data = categories });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var category = _unitOfWork.Category.GetFirstOrDefault(x => x.Id == id);
        _unitOfWork.Category.Remove(category);
        _unitOfWork.Save();
        return Json(new { mensaje = "Se ha eliminado la categoría." });
    }
}
