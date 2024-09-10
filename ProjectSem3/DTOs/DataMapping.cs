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



        //Phần anh Hải



        //Phần anh Ý
    }
}
