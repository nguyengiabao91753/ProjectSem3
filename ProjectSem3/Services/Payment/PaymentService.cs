using ProjectSem3.DTOs;

namespace ProjectSem3.Services.Payment;

public interface PaymentService
{
    public List<PaymentDTO> GetAll();
    public bool Create(PaymentDTO dto);
}
