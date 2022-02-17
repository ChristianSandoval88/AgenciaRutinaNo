using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using POS.Data.Repository.IRepository;
using POS.Models;
namespace POS.Web.Pages.Customer.Sales
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public IEnumerable<Product> ProductList { get; set; }
        public IEnumerable<Category> CategoryList { get; set; }
        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet()
        {
            ProductList = _unitOfWork.Product.GetAll(includeProperties: "Category");
            CategoryList = _unitOfWork.Category.GetAll();
        }
    }
}
