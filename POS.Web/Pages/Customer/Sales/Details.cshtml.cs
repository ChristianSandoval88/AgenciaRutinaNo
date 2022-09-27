using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using POS.Data.Repository.IRepository;
using POS.Models;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;

namespace POS.Web.Pages.Customer.Sales;

[BindProperties]
public class DetailsModel : PageModel
{
    private readonly IUnitOfWork _unitOfWork;
    public Product Product { get; set; } = new();
    public PaymentModel Payment { get; set; } = new();
    public DetailsModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public void OnGet(int? id)
    {
        if (id != null)
        {
            Product = _unitOfWork.Product.GetFirstOrDefault(filter: x => x.Id == id, includeProperties: "Category");
            Payment.ProductName = Product.Name;
        }
    }
    public IActionResult OnPost()
    {
        return RedirectToPage("KueskyPay", Payment);
    }
}
