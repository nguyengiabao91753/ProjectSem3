using AutoMapper;
using ProjectSem3.Models;

namespace ProjectSem3.DTOs;

public class DataMapping : Profile
{
    public DataMapping()
    {
        //Phần em
        CreateMap<AgeGroup, AgeGroupDTO>();
        CreateMap<AgeGroupDTO, AgeGroup>();


        //Phần anh Duy
        CreateMap<BusType, BusTypeDTO>().ReverseMap();
        CreateMap<Bus, BusDTO>()
        .ForMember(
            d => d.BusName,
            o => o.MapFrom(s => s.BusType.Name)
        );

        CreateMap<BusDTO, Bus>()
        .ForMember(
            d => d.BusType,
            o => o.Ignore()
        );

        //Phần anh Hải



        //Phần anh Ý
    }
}
