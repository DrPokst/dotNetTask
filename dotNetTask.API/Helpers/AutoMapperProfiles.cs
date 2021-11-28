using AutoMapper;
using dotNetTask.API.Dtos;
using dotNetTask.API.Entities;

namespace dotNetTask.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<UpdateEmployeeDto, Employee>();
            
        }
    }
}