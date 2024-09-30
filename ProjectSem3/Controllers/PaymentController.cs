using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectSem3.DTOs;
using ProjectSem3.Services.PaymentService;
using ProjectSem3.Services.PaypalService;

namespace ProjectSem3.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly PaymentService paymentService;
    private readonly PaypalService paypalService;
    private readonly IMapper mapper;
    private IConfiguration configuration;

    public PaymentController(PaymentService _paymentService, PaypalService _paypalService, IMapper _mapper, IConfiguration _configuration)
    {
        paymentService = _paymentService;
        paypalService = _paypalService;
        mapper = _mapper;
        configuration = _configuration;
    }

    [HttpGet("get-all-payment")]
    public IActionResult GetAll()
    {
        try
        {
            var result = paymentService.GetAll();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("get-payment-by-id/{id}")]
    public IActionResult GetById(int id)
    {
        try
        {
            var result = paymentService.GetPaymentById(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("create-paypal/{bookingId}")]
    public IActionResult CreatePaypal(int bookingId)
    {
        try
        {
            var baseUrl = HttpContext.Request.Host.Value;

            var payment = paypalService.CreatePayment(bookingId, baseUrl);

            return Ok(payment);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("execute-paypal/{bookingId}")]
    public IActionResult ExecutePaypal(int bookingId, [FromBody] ExecutePaymentDto dto)
    {
        try
        {
            var executedPayment = paypalService.ExecutePayment(bookingId, dto);

            if (executedPayment != null)
            {
                var payerName = $"{executedPayment.payer.payer_info.first_name} {executedPayment.payer.payer_info.last_name}";
                return Ok(new
                {
                    payment = executedPayment,
                    payer_given_name = payerName
                });
            }
            else
            {
                return BadRequest("Payment execution failed !!!");
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
