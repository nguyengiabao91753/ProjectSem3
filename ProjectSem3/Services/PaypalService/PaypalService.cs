using AutoMapper;
using PayPal.Api;
using ProjectSem3.DTOs;
using ProjectSem3.Models;

namespace ProjectSem3.Services.PaypalService;

public class PaypalService
{
    private readonly IConfiguration configuration;
    private readonly DatabaseContext db;
    private readonly IMapper mapper;

    public PaypalService(IConfiguration _configuration, DatabaseContext _db, IMapper _mapper)
    {
        configuration = _configuration;
        db = _db;
        mapper = _mapper;
    }

    public PayPal.Api.Payment CreatePayment(IEnumerable<BookingDTO> items, string baseUrl)
    {
        var subtotal = items.Sum(x => x.PriceAfterDiscount ?? 0);

        var itemList = new ItemList
        {
            items = items.Select(x =>
            {
                return new Item
                {
                    name = x.TicketCode,
                    currency = "USD",
                    price = (x.PriceAfterDiscount ?? 0).ToString("F2"),
                    quantity = "1"
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
                        shipping = shipping.ToString("F2"),
                        tax = tax.ToString("F2"),
                        subtotal = subtotal.ToString("F2")
                    },
                    total = (shipping + tax + subtotal).ToString("F2")
                }
            }
        };

        var payment = new PayPal.Api.Payment
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

    public PayPal.Api.Payment ExecutePayment(ExecutePaymentDto dto)
    {
        var paymentExecution = new PaymentExecution { payer_id = dto.PayerId };
        var payment = new PayPal.Api.Payment { id = dto.PaymentId };

        try
        {
            var executedPayment = payment.Execute(GetContext(), paymentExecution);

            if (executedPayment.state.ToLower() == "approved")
            {
                var paymentModel = new Models.Payment
                {
                    BookingId = dto.BookingItems.FirstOrDefault()?.BookingId,
                    PaymentDate = DateTime.Now,
                    Amount = dto.BookingItems.Sum(x => (decimal)x.PriceAfterDiscount),
                    PaymentMethod = "Paypal"
                };

                db.Payments.Add(paymentModel);
                var result = db.SaveChanges();

                if (result > 0)
                {
                    return executedPayment;
                }
                else
                {
                    throw new Exception("Failed to save payment to the database.");
                }
            }
            else
            {
                throw new Exception("Payment execution failed or was not approved.");
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return null;
        }

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
