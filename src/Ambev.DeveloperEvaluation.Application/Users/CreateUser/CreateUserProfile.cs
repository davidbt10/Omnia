using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Application.Users.CreateUser;

/// <summary>
/// Profile for mapping between User entity and CreateUserResponse
/// </summary>
public class CreateUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateUser operation
    /// </summary>
    public CreateUserProfile()
    {
        CreateMap<CreateUserCommand, User>()
            .ForMember(u => u.Username, opt => opt.MapFrom(c => c.Username))
            .ForMember(u => u.Email, opt => opt.MapFrom(c => c.Email))
            .ForMember(u => u.Password, opt => opt.MapFrom(c => c.Password))
            .ForMember(u => u.Phone, opt => opt.MapFrom(c => c.Phone))
            .ForMember(u => u.Status, opt => opt.MapFrom(c => c.Status))
            .ForMember(u => u.Role, opt => opt.MapFrom(c => c.Role))
            .AfterMap((src, dest) =>
            {
                dest.SetFullName(src.FirstName, src.LastName);
                dest.SetAddress(
                    src.City,
                    src.Street,
                    src.Number,
                    src.Zipcode,
                    src.Lat, src.Long);
            });

        CreateMap<User, CreateUserResult>();
    }
}
