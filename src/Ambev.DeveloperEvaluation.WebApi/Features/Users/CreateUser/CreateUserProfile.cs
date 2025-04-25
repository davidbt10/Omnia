using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;

/// <summary>
/// Profile for mapping between Application and API CreateUser responses
/// </summary>
public class CreateUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateUser feature
    /// </summary>
    public CreateUserProfile()
    {
        CreateMap<CreateUserRequest, CreateUserCommand>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Name.Firstname))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Name.Lastname))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
            .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Address.Number))
            .ForMember(dest => dest.Zipcode, opt => opt.MapFrom(src => src.Address.ZipCode))
            .ForMember(dest => dest.Lat, opt => opt.MapFrom(src => src.Address.GeoLocation.Lat))
            .ForMember(dest => dest.Long, opt => opt.MapFrom(src => src.Address.GeoLocation.Long));

        CreateMap<CreateUserResult, CreateUserResponse>()
            .ForMember(dest => dest.Name.Firstname, opt => opt.MapFrom(src => src.Name.Firstname))
            .ForMember(dest => dest.Name.Lastname, opt => opt.MapFrom(src => src.Name.Lastname))
            .ForMember(dest => dest.Address.City, opt => opt.MapFrom(src => src.Address.City))
            .ForMember(dest => dest.Address.Street, opt => opt.MapFrom(src => src.Address.Street))
            .ForMember(dest => dest.Address.Number, opt => opt.MapFrom(src => src.Address.Number))
            .ForMember(dest => dest.Address.GeoLocation.Lat, opt => opt.MapFrom(src => src.Address.GeoLocation.Lat))
            .ForMember(dest => dest.Address.GeoLocation.Long, opt => opt.MapFrom(src => src.Address.GeoLocation.Long));
    }
}
