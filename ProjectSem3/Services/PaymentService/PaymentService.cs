using ProjectSem3.DTOs;

namespace ProjectSem3.Services.PaymentService;

public interface PaymentService
{
    public List<PaymentDTO> GetAll();
    public PaymentDTO GetPaymentById(int id);
    //public bool Create(PaymentDTO dto);
}
