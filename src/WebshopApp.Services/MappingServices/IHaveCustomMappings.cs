using AutoMapper;

namespace WebshopApp.Services.MappingServices
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IMapperConfigurationExpression configuration);
    }
}
