using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using POS.Data.Repository.IRepository;
using POS.Models;
using System.Net.Http.Headers;

namespace POS.Web.Pages.Customer.Sales
{
    [BindProperties]
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private static readonly HttpClient client = new HttpClient();
        public decimal monto { get; set; }
        public Product Product { get; set; }
        public DetailsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            client.BaseAddress = new Uri("https://testing.kueskipay.com/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("8a682242-3707-43c6-bc34-a784cf9af735");
        }
        public void OnGet(int? id)
        {
            if (id != null)
            {
                Product = _unitOfWork.Product.GetFirstOrDefault(filter: x => x.Id == id, includeProperties: "Category");
            }
        }
        public IActionResult OnPost()
        {
            var fecha = DateTime.Now.ToLongDateString()
                + " " + DateTime.Now.ToLongTimeString();

            data data = new data { order_id = $"Orden {fecha}", description = fecha, amount = new amount { total = monto, currency = "MXN" } };
            HttpResponseMessage response = client.PostAsJsonAsync("v1/payments", data).Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                result = result.Substring(result.IndexOf("https"), 60);
                return Redirect(result);
            }
            return Page();
        }
    }
    public class data
    {
        public string order_id { get; set; }
        public string description { get; set; }
        public amount amount { get; set; }
    }
    public class amount
    {
        public decimal total { get; set; }
        public string currency { get; set; }
    }
}
