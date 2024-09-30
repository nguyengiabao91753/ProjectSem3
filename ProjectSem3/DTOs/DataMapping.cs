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

        CreateMap<BusesTrip, BusesTripDTO>().ForMember(
            d => d.BusTypeName,
            s => s.MapFrom(s => s.Bus.BusType.Name)
            ).ForMember(
            d => d.AirConditioned,
            s => s.MapFrom(s => s.Bus.AirConditioned)
            ).ForMember(
            d => d.SeatCount,
            s => s.MapFrom(s => s.Bus.SeatCount)
            ).ForMember(
            d => d.DepartureLocationName,
            s => s.MapFrom(s => s.Trip.DepartureLocation.Name)
            ).ForMember(
            d => d.ArrivalLocationName,
            s => s.MapFrom(s => s.Trip.ArrivalLocation.Name)
            ).ForMember(
            d => d.DateStart,
            s => s.MapFrom(s => s.Trip.DateStart)
            ).ForMember(
            d => d.DateEnd,
            s => s.MapFrom(s => s.Trip.DateEnd)
            );
        CreateMap<BusesTripDTO, BusesTrip>();

        CreateMap<Booking, BookingDTO>().ForMember(
            d => d.BookingDate,
            s => s.MapFrom(b => b.BookingDate.ToString("HH:mm:ss dd/MM/yyyy"))
            );

        CreateMap<BookingDTO, Booking>().ForMember(
           d => d.BookingDate,
           s => s.MapFrom(b => DateTime.ParseExact(b.BookingDate, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture))
           );

        CreateMap<BookingDetail, BookingDetailDTO>().ForMember(
            d=>d.AgeGroupName,
            s=>s.MapFrom(s=>s.AgeGroup.Name));
        CreateMap<BookingDetailDTO, BookingDetail>();
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
        CreateMap<Payment, PaymentDTO>()
        .ForMember(d => d.PaymentDate,
                    s => s.MapFrom(s => s.PaymentDate.ToString("HH:mm:ss dd/MM/yyyy"))
        );

        //Phần anh Hải



        //Phần anh Ý

        //Bang location
        CreateMap<Location, LocationDTO>();
        CreateMap<LocationDTO, Location>();
        // Bang trip
        CreateMap<Trip, TripDTO>()
            .ForMember(
                d => d.DateStart,
                s => s.MapFrom(src => src.DateStart.ToString("HH:mm:ss dd/MM/yyyy"))
            )
            .ForMember(
                d => d.DateEnd,
                s => s.MapFrom(src => src.DateEnd.ToString("HH:mm:ss dd/MM/yyyy"))
            )
            .ForMember(
                d => d.DepartureLocationName,
                o => o.MapFrom(s => s.DepartureLocation.Name) // Lấy tên từ bảng Location cho điểm xuất phát
            )
            .ForMember(
                d => d.ArrivalLocationName,
                o => o.MapFrom(s => s.ArrivalLocation.Name) // Lấy tên từ bảng Location cho điểm đến
            );

        CreateMap<TripDTO, Trip>()
            .ForMember(
                d => d.DateStart,
                s => s.MapFrom(dto => DateTime.ParseExact(dto.DateStart, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture))
            )
            .ForMember(
                d => d.DateEnd,
                s => s.MapFrom(dto => DateTime.ParseExact(dto.DateEnd, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture))
            );

    }
}
