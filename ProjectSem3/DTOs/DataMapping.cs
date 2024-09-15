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
            s => s.MapFrom(src => DateOnly.FromDateTime(DateTime.ParseExact(src.BirthDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)))
            );
        //s => s.MapFrom(s => DateOnly.ParseExact(s.BirthDate, "dd/MM/yyyy", CultureInfo.InvariantCulture))

        CreateMap<Account, AccountDTO>();



        //Phần anh Ý
    }
}
