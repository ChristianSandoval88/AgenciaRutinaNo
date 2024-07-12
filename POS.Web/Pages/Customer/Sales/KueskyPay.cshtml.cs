using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.Protocol;
using POS.Data.Repository.IRepository;
using POS.Models;
using System.Net.Http.Headers;

namespace POS.Web.Pages.Customer.Sales;

[BindProperties]
public class KueskyPayModel : PageModel
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<KueskyPayModel> logger;

    private HttpClient client { get; set; } = new();
    public PaymentModel Payment { get; set; } = new();
    public FormModel Form { get; set; } = new();

    public KueskyPayModel(IUnitOfWork unitOfWork, IConfiguration configuration, ILogger<KueskyPayModel> logger)
    {
        _unitOfWork = unitOfWork;
        this.logger = logger;
        client.BaseAddress = new Uri(configuration["BaseAddress"]);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(configuration["API_KEY"]);
    }
    public IActionResult OnGet(PaymentModel? _Payment)
    {
        if (_Payment == null)
            return RedirectToPage("Index");

        /*var total = _Payment.Qty * _Payment.Price;
        if (total < 300 || total > 5000)
            TempData["FueraDeLimite"] = $"El monto {total.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"))} está fuera de los límites de crédito.";
        */
        Payment = _Payment;
        return Page();
    }
    public async Task<IActionResult> OnPost()
    {
        var fecha = DateTime.Now;
        var order = fecha.Year.ToString() + fecha.Month.ToString() + fecha.Day.ToString() + fecha.Hour.ToString() + fecha.Minute.ToString();
        Order data = new Order
        {
            order_id = $"Orden {order}",
            description = "Viajes RutinaNo",
            amount = new() { total = Payment.Price * Payment.Qty, currency = "MXN", details = new() { subtotal= Payment.Price * Payment.Qty , shipping=0.0m, tax=0.0m } },
            items = new Items[] {
                new() { name = Payment.ProductName, description = Payment.ProductName, quantity = Payment.Qty, price = Payment.Price, currency = "MXN", sku = "001" }
            },
            shipping = new()
            {
                name = new() { name = Form.Name, last = Form.LastName },
                address = new() { address = Form.Address.Trim(), neighborhood = Form.Col, city = Form.City.Trim(), state = "Jalisco", zipcode = Form.ZipCode, country = "MX" },
                phone_number = Form.Phone.Trim(),
                email = Form.Email.Trim()
            },
            billing = new()
            {
                business = new() { name = "RutinaNo", rfc = "MOAV8804282U1" },
                address = new() { address = "Nicolas Romero 906", interior = "A", neighborhood = "Mezquitan Country", city = "Guadalajara", state = "Jalisco", zipcode = "44260", country = "MX" },
                phone_number = "3321783834",
                email = "rutinano@hotmail.com"
            },
            callbacks = new()
            {
                on_success = "https://rutinano.herokuapp.com/Customer/Sales/Success",
                on_reject = "https://rutinano.herokuapp.com/Customer/Sales/Error",
                on_canceled = "https://rutinano.herokuapp.com/Customer/Sales/Error",
                on_failed = "https://rutinano.herokuapp.com/Customer/Sales/Error"
            },
        };
        logger.LogInformation(data.ToJson());
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
    public Shipping shipping { get; set; }
    public Billing billing { get; set; }
}
public class Details
{
    public decimal subtotal { get; set; }
    public decimal shipping { get; set; }
    public decimal tax { get; set; }
}
public class Amount
{
    public decimal total { get; set; }
    public string currency { get; set; }
    public Details details { get; set; }

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
public class Billing
{
    public Business business { get; set; }
    public Address address { get; set; }
    public string phone_number { get; set; }
    public string email { get; set; }
}
public class Shipping
{
    public Name name { get; set; }
    public Address address { get; set; }
    public string phone_number { get; set; }
    public string email { get; set; }
}
public class Name
{
    public string name { get; set; }
    public string last { get; set; }
}
public class Business
{
    public string name { get; set; }
    public string rfc { get; set; }
}
public class Address
{
    public string address { get; set; }
    public string interior { get; set; }
    public string neighborhood { get; set; }
    public string city { get; set; }
    public string state { get; set; }
    public string zipcode { get; set; }
    public string country { get; set; } = "MX";
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