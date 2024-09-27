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
            d=>d.BusTypeName,
            s=>s.MapFrom(s=>s.Bus.BusType.Name)
            ).ForMember(
            d=>d.AirConditioned,
            s=>s.MapFrom(s=>s.Bus.AirConditioned)
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
        CreateMap<Level, LevelDTO>();
        CreateMap<LevelDTO, Level>();

        CreateMap<User, UserDTO>().ForMember(
            //d == destination
            d => d.CreatedAt,
            //s == source
            s => s.MapFrom(s => s.CreatedAt.ToString("dd/MM/yyyy")))
            .ForMember(
            //d == destination
            d => d.BirthDate,
            //s == source
            s => s.MapFrom(s => s.BirthDate.ToString("dd/MM/yyyy"))); 

        CreateMap<UserDTO, User>().ForMember(
            d => d.CreatedAt,
            s => s.MapFrom(s => DateTime.ParseExact(s.CreatedAt, "dd/MM/yyyy", CultureInfo.InvariantCulture))
            ).ForMember(
            d => d.BirthDate,
            s => s.MapFrom(s => DateTime.ParseExact(s.BirthDate, "dd/MM/yyyy", CultureInfo.InvariantCulture))
            );
        //s => s.MapFrom(s => DateOnly.ParseExact(s.BirthDate, "dd/MM/yyyy", CultureInfo.InvariantCulture))

        CreateMap<Account, AccountDTO>();

        CreateMap<AccountUser, AccountUserDTO>().ForMember(
            //d == destination
            d => d.CreatedAt,
            //s == source
            s => s.MapFrom(s => s.CreatedAt.ToString("dd/MM/yyyy")))
            .ForMember(
            //d == destination
            d => d.BirthDate,
            //s == source
            s => s.MapFrom(s => s.BirthDate.ToString("dd/MM/yyyy")));
        CreateMap<AccountUserDTO, AccountUser>();
        CreateMap<Account, AccountUserDTO>()
    .ForMember(
        d => d.UserId,
        s => s.MapFrom(s => s.AccountId)
    )
    .ForMember(
        d => d.Username,
        s => s.MapFrom(s => s.Username)
    )
    .ForMember(
        d => d.Password,
        s => s.MapFrom(s => s.Password)
    )
    .ForMember(
        d => d.Status,
        s => s.MapFrom(s => s.Status)
    )
    .ForMember(
        d => d.LevelId,
        s => s.MapFrom(s => s.LevelId)
    );

        CreateMap<AccountUserDTO, Account>()
            .ForMember(
            d => d.AccountId,
            s => s.MapFrom(s => s.UserId))
            .ForMember(
            d => d.Username,
            s=> s.MapFrom(s=> s.Username))
            .ForMember(
            d => d.Password,
            s => s.MapFrom(s => s.Password)
            ).ForMember(
            d=> d.Status,
            s=> s.MapFrom(s=> s.Status)
            ).ForMember(
            d=> d.LevelId,
            s=> s.MapFrom(s=> s.LevelId)
            );
        CreateMap<AccountUserDTO, User>()
            .ForMember(
            d => d.FullName,
            s => s.MapFrom(s => s.FullName))
            .ForMember(
            d => d.BirthDate,
            s => s.MapFrom(s => DateTime.ParseExact(s.BirthDate, "dd-MM-yyyy", CultureInfo.InvariantCulture))
            ).ForMember(
            d => d.Email,
            s => s.MapFrom(s => s.Email)
            ).ForMember(
            d => d.PhoneNumber,
            s => s.MapFrom(s => s.PhoneNumber)
            ).ForMember(
            d => d.Address,
            s => s.MapFrom(s => s.Address)
            ).ForMember(
            d => d.CreatedAt,
            s => s.MapFrom(s => DateTime.ParseExact(s.CreatedAt, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture))
            );


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
