using AutoMapper;
using SmartlyCodingExercise.Api.Models;
using SmartlyCodingExercise.Api.Models.Dtos;

namespace SmartlyCodingExercise.Api.Configurations
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Employee, EmployeeDto>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
