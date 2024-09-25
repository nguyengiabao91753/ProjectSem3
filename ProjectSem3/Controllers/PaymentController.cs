using Microsoft.AspNetCore.Mvc;
using ProjectSem3.Services.Payment;

namespace ProjectSem3.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly PaymentService paymentService;

    public PaymentController(PaymentService _paymentService)
    {
        paymentService = _paymentService;
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
}
