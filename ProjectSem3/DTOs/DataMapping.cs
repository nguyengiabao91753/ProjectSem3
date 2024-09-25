﻿using AutoMapper;
using ProjectSem3.Models;
using System.Globalization;

namespace ProjectSem3.DTOs;

public class DataMapping : Profile
{
    public DataMapping()
    {
        //Phần em
        CreateMap<AgeGroup, AgeGroupDTO>();
        CreateMap<AgeGroupDTO, AgeGroup>();

        CreateMap<Policy, PolicyDTO>();
        CreateMap<PolicyDTO, Policy>();

        CreateMap<BusesTrip, BusesTripDTO>();
        CreateMap<BusesTripDTO, BusesTrip>();

        CreateMap<Booking, BookingDTO>().ForMember(
            d => d.BirthDate,
            s => s.MapFrom(b => b.BirthDate.ToString("dd/MM/yyyy"))
            ).ForMember(
            d => d.BookingDate,
            s => s.MapFrom(b => b.BookingDate.ToString("HH:mm:ss dd/MM/yyyy"))
            );

        CreateMap<BookingDTO, Booking>().ForMember(
           d => d.BirthDate,
           s => s.MapFrom(b => DateTime.ParseExact(b.BirthDate, "dd/MM/yyyy", CultureInfo.InvariantCulture))
           ).ForMember(
           d => d.BookingDate,
           s => s.MapFrom(b => DateTime.ParseExact(b.BookingDate, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture))
           );
        //Phần anh Duy
        CreateMap<BusType, BusTypeDTO>().ReverseMap();

        CreateMap<Bus, BusDTO>()
        .ForMember(
            d => d.BusName,
            o => o.MapFrom(s => s.BusType.Name)
        );
        CreateMap<BusDTO, Bus>();

        CreateMap<BusesSeat, BusesSeatDTO>()
        .ForMember(
            d => d.BusLicensePlate,
            o => o.MapFrom(s => s.Bus.LicensePlate)
        );
        CreateMap<BusesSeatDTO, BusesSeat>();

        CreateMap<PaymentDTO, Payment>()
        .ForMember(d => d.PaymentDate,
                    s => s.MapFrom(s => DateTime.ParseExact(s.PaymentDate, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture))
        );
        // .ForMember(
        //    s => s.Booking.BirthDate,
        //    s => s.MapFrom(b => DateTime.ParseExact(b.BirthDate, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture))
        // );
        CreateMap<Payment, PaymentDTO>()
        .ForMember(d => d.PaymentDate,
                    s => s.MapFrom(s => s.PaymentDate.ToString("HH:mm:ss dd/MM/yyyy"))
        );
        //.ForMember(
        //    d => d.BirthDate,
        //    s => s.MapFrom(b => b.Booking.BirthDate.ToString("HH:mm:ss dd/MM/yyyy"))
        //)
        //.ForMember(
        //    d => d.FullName,
        //    s => s.MapFrom(b => b.Booking.FullName)
        //)
        //.ForMember(
        //    d => d.PhoneNumber,
        //    s => s.MapFrom(b => b.Booking.PhoneNumber)
        //);

        //Phần anh Hải



        //Phần anh Ý

        //Bang location
        CreateMap<Location, LocationDTO>();
        CreateMap<LocationDTO, Location>();
        // Bang trip
        CreateMap<Trip, TripDTO>().ForMember(
            d => d.DateStart,
            s => s.MapFrom(s => s.DateStart.ToString("HH:mm:ss dd/MM/yyyy "))
            ).ForMember(
            d => d.DateEnd,
            s => s.MapFrom(s => s.DateEnd.ToString("HH:mm:ss dd/MM/yyyy "))
            );
        CreateMap<TripDTO, Trip>().ForMember(
            d => d.DateStart,
            s => s.MapFrom(b => DateTime.ParseExact(b.DateStart, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture))
            ).ForMember(
            d => d.DateEnd,
            s => s.MapFrom(b => DateTime.ParseExact(b.DateEnd, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture))
            );

    }
}
