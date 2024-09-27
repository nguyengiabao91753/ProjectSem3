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
    private readonly IHttpContextAccessor context;
    private IConfiguration configuration;

    public PaymentController(PaymentService _paymentService, PaypalService _paypalService, IMapper _mapper, IHttpContextAccessor _context, IConfiguration _configuration)
    {
        paymentService = _paymentService;
        paypalService = _paypalService;
        mapper = _mapper;
        context = _context;
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

    //[HttpPost("transactions-detail")]
    //public async Task<Object> Post(string start_date, string end_date)
    //{
    //    const SecurityProtocolType tls13 = (SecurityProtocolType)12288;
    //    ServicePointManager.SecurityProtocol = tls13 | SecurityProtocolType.Tls12;

    //    TokenJson Token = new TokenJson();
    //    using (HttpClient client = new HttpClient())
    //    {
    //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    //        client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("en_US"));

    //        var clientId = "Aa6muE0xAPvHoWCDO5dDj-0w2zo4baRpI_oo92tRDOYVhJppD5EBwgUwieECjMAS5Sh9oBeCV8qLhLuF";
    //        var clientSecret = "ENfKOBGwUE61IEM7emFBsNVdX5MqWzqGF62HSssMWiw3GeJa1O9Bp4wZ1jN3--Wbox1cDOkp_skIsAxn";
    //        var bytes = Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}");

    //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(bytes));

    //        var keyValues = new List<KeyValuePair<string, string>>();
    //        keyValues.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
    //        var responseMessage = await client.PostAsync("https://api-m.sandbox.paypal.com/v1/oauth2/token", new FormUrlEncodedContent(keyValues));
    //        var response = await responseMessage.Content.ReadAsStringAsync();
    //        Token = JsonConvert.DeserializeObject<TokenJson>(response);

    //        if (Token != null)
    //        {
    //            var transactionHistoryUrl = "https://api-m.sandbox.paypal.com/v1/reporting/transactions?start_date=" + start_date + "&end_date=" + end_date + "&fields=all";
    //            using (HttpClient httpClient = new HttpClient())
    //            {
    //                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    //                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("en_Us"));
    //                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
    //                responseMessage = await client.GetAsync(transactionHistoryUrl);
    //                response = await responseMessage.Content.ReadAsStringAsync();
    //                var Transaction = JsonConvert.DeserializeObject(response);
    //                return Transaction;

    //            }
    //        }
    //        return "Please try again something getting problem";

    //    }

    //}

    [HttpPost("create-paypal")]
    public IActionResult CreatePaypal([FromBody] IEnumerable<PaymentDTO> payments)
    {
        try
        {
            var baseUrl = context.HttpContext.Request.Host.Value;

            return Ok(new PaypalService(configuration).CreatePayment(payments, baseUrl));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("execute-paypal")]
    public IActionResult ExecutePaypal([FromBody] ExecutePaymentDto dto)
    {
        try
        {
            return Ok(new PaypalService(configuration).ExecutePayment(dto));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
