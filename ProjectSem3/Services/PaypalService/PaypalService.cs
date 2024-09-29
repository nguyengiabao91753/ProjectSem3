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

    public PayPal.Api.Payment CreatePayment(IEnumerable<BookingDetailDTO> items, string baseUrl)
    {
        var subtotal = 0M;

        var itemList = new ItemList
        {
            items = items.Select(x =>
            {
                subtotal += (decimal)x.PriceAfterDiscount;
                return new Item
                {
                    name = x.TicketCode,
                    currency = "USD",
                    price = x.PriceAfterDiscount.ToString(),
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
                        shipping = shipping.ToString(),
                        tax = tax.ToString(),
                        subtotal = subtotal.ToString()
                    },
                    total = (shipping + tax + subtotal).ToString()
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

        var createdPayment = payment.Create(GetContext());

        return createdPayment;

    }

    public PayPal.Api.Payment ExecutePayment(ExecutePaymentDto dto, PaymentDTO paymentDTO)
    {
        var paymentExecution = new PaymentExecution { payer_id = dto.PayerId, };
        var payment = new PayPal.Api.Payment { id = dto.PaymentId };

        try
        {
            var executedPayment = payment.Execute(GetContext(), paymentExecution);

            var pa = mapper.Map<Models.Payment>(paymentDTO);
            db.Payments.Add(pa);

            if (db.SaveChanges() > 0)
            {
                return executedPayment;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
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
