using AutoMapper;
using ProjectSem3.DTOs;
using ProjectSem3.Models;

namespace ProjectSem3.Services.PaymentService;

public class PaymentServiceImpl : PaymentService
{
    private readonly DatabaseContext db;
    private readonly IMapper mapper;

    public PaymentServiceImpl(DatabaseContext _db, IMapper _mapper)
    {
        db = _db;
        mapper = _mapper;
    }

    public bool Create(Payment payment)
    {
        try
        {
            db.Payments.Add(payment);
            return db.SaveChanges() > 0;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    //public bool Create(PaymentDTO dto)
    //{
    //    throw new NotImplementedException();
    //}

    public List<PaymentDTO> GetAll()
    {
        return mapper.Map<List<PaymentDTO>>(db.Payments.OrderByDescending(p => p.PaymentId).ToList());
    }

    public PaymentDTO GetPaymentById(int id)
    {
        return mapper.Map<PaymentDTO>(db.Payments.Find(id));
    }
}


