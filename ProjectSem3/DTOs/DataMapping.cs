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


        //Phần anh Duy



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
