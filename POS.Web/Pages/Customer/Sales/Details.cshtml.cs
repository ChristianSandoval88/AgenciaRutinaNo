using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using POS.Data.Repository.IRepository;
using POS.Models;

namespace POS.Web.Pages.Customer.Sales
{
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public Product Product { get; set; }
        public DetailsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet(int? id)
        {
            if (id != null)
            {
                Product = _unitOfWork.Product.GetFirstOrDefault(filter: x => x.Id == id, includeProperties: "Category");
            }
        }
    }
}
