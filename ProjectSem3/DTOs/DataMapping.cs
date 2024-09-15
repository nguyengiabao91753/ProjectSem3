using AutoMapper;
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
            d=>d.BookingDate,
            s=>s.MapFrom(b => b.BookingDate.ToString("HH:mm:ss dd/MM/yyyy"))
            );

        CreateMap<BookingDTO, Booking>().ForMember(
           d => d.BirthDate,
           s => s.MapFrom(b => DateTime.ParseExact(b.BirthDate, "dd/MM/yyyy", CultureInfo.InvariantCulture))
           ).ForMember(
           d => d.BookingDate,
           s => s.MapFrom(b => DateTime.ParseExact(b.BookingDate, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture))
           );
        //Phần anh Duy



        //Phần anh Hải



        //Phần anh Ý
    }
}
