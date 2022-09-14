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
    private HttpClient client { get; set; } = new HttpClient();
    public Product Product { get; set; }
    public PaymentModel Payment { get; set; }

    public DetailsModel(IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        client.BaseAddress = new Uri(configuration["BaseAddress"]);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(configuration["API_KEY"]);
    }
    public void OnGet(int? id)
    {
        if (id != null)
        {
            Product = _unitOfWork.Product.GetFirstOrDefault(filter: x => x.Id == id, includeProperties: "Category");
        }
    }
    public async Task<IActionResult> OnPost()
    {
        var fecha = DateTime.Now;
        var order = fecha.Year.ToString() + fecha.Month.ToString() + fecha.Day.ToString() + fecha.Hour.ToString() + fecha.Minute.ToString();
        Order data = new Order
        {
            order_id = $"Orden {order}",
            description = "Viajes RutinaNo!",
            amount = new() { total = Payment.Price * Payment.Qty, currency = "MXN" },
            items = new Items[] {
                new() { name = Product.Name, description = Product.Name, quantity = Payment.Qty, price = Payment.Price, currency = "MXN", sku = "001" }
            },
            callbacks = new() {
                on_success = "https://rutinano.herokuapp.com/Customer/Sales/Success",
                on_reject = "https://rutinano.herokuapp.com/Customer/Sales/Error",
                on_canceled = "https://rutinano.herokuapp.com/Customer/Sales/Error",
                on_failed = "https://rutinano.herokuapp.com/Customer/Sales/Error"
            },
        };
        try
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("v1/payments", data);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<Response>();
            if (result is not null && result.status == "success")
                return Redirect(result.data.callback_url);

            return Page();
        }
        catch (Exception)
        {
            return Page();
        }
        
    }
}
public class Order
{
    public string order_id { get; set; }
    public string description { get; set; }
    public Amount amount { get; set; }
    public Items[] items { get; set; }
    public Callbacks callbacks { get; set; }
}
public class Amount
{
    public decimal total { get; set; }
    public string currency { get; set; }
}
public class Items
{
    public string name { get; set; }
    public string description { get; set; }
    public int quantity { get; set; }
    public decimal price { get; set; }
    public string currency { get; set; }
    public string sku { get; set; }
}
public class Callbacks
{
    public string on_success { get; set; }
    public string on_reject { get; set; }
    public string on_canceled { get; set; }
    public string on_failed { get; set; }
}
public class Response
{
    public string status { get; set; }
    public ResponseData data { get; set; }
}
public class ResponseData
{
    public string payment_id { get; set; }
    public string callback_url { get; set; }
}
public class PaymentModel
{
    [Required(ErrorMessage = "Campo requerido")]
    [Range(1, 9, ErrorMessage = "El orden debe ser un valor númerico entre 1 y 9")]
    public int Qty { get; set; } = 1;

    public decimal Price { get; set; }

}