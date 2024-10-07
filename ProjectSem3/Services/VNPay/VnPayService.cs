using ProjectSem3.DTOs;

namespace ProjectSem3.Services.VNPay;

public interface VnPayService
{
    public string CreatePaymentUrl(HttpContext context, VnPaymentRequestModel model);
    public VnPaymentResponseModel PaymentExecute(IQueryCollection collections);
}
