using ProjectSem3.DTOs;
using ProjectSem3.Models;

namespace ProjectSem3.Services.PaymentService;

public interface PaymentService
{
    public List<PaymentDTO> GetAll();
    public PaymentDTO GetPaymentById(int id);
    public bool Create(Payment payment);
    //public bool Create(PaymentDTO dto);
}
