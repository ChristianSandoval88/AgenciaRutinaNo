using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Data.Repository.IRepository;

namespace POS.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var products = _unitOfWork.Product.GetAll(includeProperties: "Category");
            return Json(new { data = products });
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == id);
            _unitOfWork.Product.Remove(product);
            _unitOfWork.Save();
            return Json(new { mensaje = "Se borró el producto correctamente." });
        }
    }
}
