using AutoMapper;
using EmployeeTestApi.Data.Dtos;
using EmployeeTestApi.Data.Models;

namespace EmployeeTestApi.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<EmployeeRm, EmployeeSm>();
            CreateMap<EmployeeRm, EmployeeListItemDto>()
                .ForMember(dest => dest.FullName,
                    opt =>
                        opt.MapFrom(src =>
                            $"{src.LastName} {src.FirstName} {src.FathersName}"))
                .ForMember(dest =>
                    dest.DepartmentName, opt =>
                    opt.MapFrom(src => src.Department.DepartmentName));
        }
    }
}