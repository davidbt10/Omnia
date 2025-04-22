using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Mappings
{
    public class GetUserResultProfile : Profile
    {
        public GetUserResultProfile()
        {
            CreateMap<User, GetUserResult>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Username));
        }
    }
}
