using PayPal.Api;
using ProjectSem3.DTOs;

namespace ProjectSem3.Services.PaypalService;

public class PaypalService
{
    private readonly IConfiguration configuration;

    public PaypalService(IConfiguration _configuration)
    {
        configuration = _configuration;
    }

    public Payment CreatePayment(IEnumerable<PaymentDTO> items, string baseUrl)
    {
        var subtotal = 0M;

        var itemList = new ItemList
        {
            items = items.Select(x =>
            {
                subtotal += x.Amount;
                return new Item
                {
                    name = x.BusTickCode,
                    currency = "USD",
                    price = x.Amount.ToString()
                };
            }).ToList()
        };

        var shipping = 0M;
        var tax = 0M;

        var transactions = new List<Transaction>
        {
            new()
            {
                description = "Bus Ticket purchase",
                item_list = itemList,
                amount = new()
                {
                    currency = "USD",
                    details = new()
                    {
                        shipping = shipping.ToString(),
                        tax = tax.ToString(),
                        subtotal = subtotal.ToString()
                    },
                    total = (shipping + tax + subtotal).ToString()
                }
            }
        };

        var payment = new Payment
        {
            intent = "sale",
            payer = new Payer
            {
                payment_method = "paypal"
            },
            transactions = transactions,
            redirect_urls = new()
            {
                cancel_url = "/",
                return_url = $"/{baseUrl}/ExecutePayment"
            }
        };

        return payment.Create(GetContext());
    }

    public Payment ExecutePayment(ExecutePaymentDto dto)
    {
        var paymentExecution = new PaymentExecution { payer_id = dto.PayerId, };
        var payment = new Payment { id = dto.PaymentId };

        return payment.Execute(GetContext(), paymentExecution);
    }

    private APIContext GetContext() =>
        new(GetAccessToken())
        {
            Config = GetConfig()
        };

    private string GetAccessToken() => new OAuthTokenCredential(GetConfig()).GetAccessToken();

    private Dictionary<string, string> GetConfig() =>
        new()
        {
            {"mode", configuration["PayPal:Mode"] },
            {"clientId", configuration["PayPal:ClientId"] },
            {"clientSecret", configuration["PayPal:ClientSecret"] }
        };
}
