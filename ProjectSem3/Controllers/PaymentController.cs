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

    [HttpPost("create-paypal")]
    public IActionResult CreatePaypal([FromBody] IEnumerable<BookingDetailDTO> dto)
    {
        try
        {
            var request = HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";

            // Gọi PaypalService đã inject
            var payment = paypalService.CreatePayment(dto, baseUrl);

            return Ok(payment);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("execute-paypal")]
    public IActionResult ExecutePaypal([FromBody] ExecutePaymentDto dto, [FromBody] PaymentDTO paymentDTO)
    {
        try
        {
            var payment = paypalService.ExecutePayment(dto, paymentDTO);

            if (payment != null)
            {
                return Ok(payment);
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
