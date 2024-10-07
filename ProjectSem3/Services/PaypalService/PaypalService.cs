using AutoMapper;
using PayPal.Api;
using ProjectSem3.DTOs;
using ProjectSem3.Models;
using ProjectSem3.Services.BookingService;

namespace ProjectSem3.Services.PaypalService;

public class PaypalService
{
    private readonly IConfiguration configuration;
    private readonly DatabaseContext db;
    private readonly IMapper mapper;
    private readonly BookingServiceImpl bookingServiceImpl;

    public PaypalService(IConfiguration _configuration, DatabaseContext _db, IMapper _mapper, BookingServiceImpl _bookingServiceImpl)
    {
        configuration = _configuration;
        db = _db;
        mapper = _mapper;
        bookingServiceImpl = _bookingServiceImpl;
    }

    public PayPal.Api.Payment CreatePayment(BookingRequest bookingRequest, string baseUrl)
    {
        var bookingDetails = bookingRequest.BookingDetailDTOs;
        var booking = bookingRequest.BookingDTO;
        var quantityticket = bookingDetails.Count();

        var subtotal = bookingDetails.Sum(x => x.PriceAfterDiscount);

        var itemList = new ItemList
        {
            items = bookingDetails.Select(x =>
            {
                return new Item
                {
                    name = "Seat " + x.SeatName,
                    currency = "USD",
                    price = (x.PriceAfterDiscount).ToString("F2"),
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
                return_url = $"/{baseUrl}/api/Payment/execute-paypal"
            }
        };

        return payment.Create(GetContext());

    }

    public PayPal.Api.Payment ExecutePayment(BookingRequest bookingRequest, ExecutePaymentDto dto)
    {
        var paymentExecution = new PaymentExecution { payer_id = dto.PayerId };
        var payment = new PayPal.Api.Payment { id = dto.PaymentId };

        try
        {
            var executedPayment = payment.Execute(GetContext(), paymentExecution);
            var bookingDetails = bookingRequest.BookingDetailDTOs;
            var booking = bookingRequest.BookingDTO;

            if (executedPayment.state.ToLower() == "approved")
            {
                //var paymentModel = new Models.Payment
                //{
                //    BookingId = booking.BookingId,
                //    PaymentDate = DateTime.Now,
                //    Amount = bookingDetails.Sum(x => (decimal)x.PriceAfterDiscount),
                //    PaymentMethod = "Paypal"
                //};

                var result = bookingServiceImpl.Create(booking, bookingDetails, "Paypal");

                //db.Payments.Add(paymentModel);
                //var result = db.SaveChanges();

                if (result)
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
