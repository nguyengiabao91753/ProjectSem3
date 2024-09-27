﻿using AutoMapper;
using ProjectSem3.DTOs;
using ProjectSem3.Models;

namespace ProjectSem3.Services.Payment;

public class PaymentServiceImpl : PaymentService
{
    private readonly DatabaseContext db;
    private readonly IMapper mapper;

    public PaymentServiceImpl(DatabaseContext _db, IMapper _mapper)
    {
        db = _db;
        mapper = _mapper;
    }

    public bool Create(PaymentDTO dto)
    {
        throw new NotImplementedException();
    }

    public List<PaymentDTO> GetAll()
    {
        return mapper.Map<List<PaymentDTO>>(db.Payments.OrderByDescending(p => p.PaymentId).ToList());
    }
}
